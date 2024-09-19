using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.SoundIn;
using CSCore.Streams;
using MathNet.Numerics.IntegralTransforms;
using System.Numerics;

namespace LedAudioVisualizer
{
    public class AudioListener
    {
        private readonly MMDevice _audioDevice;
        private WasapiLoopbackCapture? _capture;
        private IWaveSource? _waveSource;
        private event EventHandler<float[]> AudioDataAvailable;

        public int SampleRate { get => _waveSource?.WaveFormat.SampleRate ?? 0; }

        public AudioListener(MMDevice audioDevice, EventHandler<float[]> audioDataAvailable)
        {
            _audioDevice = audioDevice;
            AudioDataAvailable += audioDataAvailable;
        }


        public void StartAudioCapture()
        {
            if (_audioDevice == null)
            {
                Console.WriteLine("No audio device available to start capture.");
                return;
            }

            _capture = new();
            _capture.Device = _audioDevice;
            _capture.Initialize();           

            // Start the capture
            _capture.Start();

            // Start polling the audio data on a separate thread
            Task.Run(() =>
            {
                _waveSource = new SoundInSource(_capture) { FillWithZeros = true }
                    .ToSampleSource()
                    .ToWaveSource(16); // 16 bits per sample (standard)
                byte[] buffer = new byte[_waveSource.WaveFormat.BytesPerSecond / 2]; // Half a second buffer
                while (_capture != null && _waveSource != null)
                {
                    int read = _waveSource.Read(buffer, 0, buffer.Length);
                    if (read > 0)
                    {
                        float[] magnitudes = GetFrequencySpectrum(buffer);
                        AudioDataAvailable.Invoke(this, magnitudes);
                    }
                }
            });
        }

        public void StopAudioCapture()
        {
            if (_capture != null)
            {
                _capture.Stop();
                _capture.Dispose();
                _capture = null;
                Console.WriteLine("Audio capture stopped.");
            }

            if (_waveSource != null)
            {
                _waveSource.Dispose();
                _waveSource = null;
            }
        }

        private static float[] GetFrequencySpectrum(byte[] buffer)
        {
            int sampleCount = buffer.Length / 2;
            float[] floatBuffer = new float[sampleCount];

            for (int i = 0; i < buffer.Length; i += 2)
            {
                short sample = BitConverter.ToInt16(buffer, i);
                floatBuffer[i / 2] = sample / 32768f;
            }

            Complex[] fftBuffer = new Complex[sampleCount];
            for (int i = 0; i < sampleCount; i++)
            {
                fftBuffer[i] = new Complex(floatBuffer[i], 0);
            }

            Fourier.Forward(fftBuffer, FourierOptions.Matlab);

            float[] magnitudes = new float[sampleCount / 2];
            for (int i = 0; i < sampleCount / 2; i++)
            {
                magnitudes[i] = (float)fftBuffer[i].Magnitude;
            }

            return magnitudes;
        }

    }
}
