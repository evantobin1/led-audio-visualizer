using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.SoundIn;
using CSCore.Streams;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LedAudioVisualizer
{
    public class AudioProcessor
    {
        private WasapiLoopbackCapture _capture;
        private MMDevice _audioDevice;
        private IWaveSource _waveSource;

        public event EventHandler<float[]> AudioDataAvailable; // Event for visual data (PCM or FFT)
        public int SampleRate { get; private set; } = 1; // Default sample rate

        public AudioProcessor()
        {
            using (var mmDeviceEnumerator = new MMDeviceEnumerator())
            {
                _audioDevice = mmDeviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                Debug.WriteLine($"Selected Audio Device: {_audioDevice.FriendlyName}");
            }
        }

        public void StartAudioCapture()
        {
            if (_audioDevice != null)
            {
                _capture = new WasapiLoopbackCapture();
                _capture.Device = _audioDevice;
                _capture.Initialize();

                // Use WaveSource for capturing PCM data
                _waveSource = new SoundInSource(_capture) { FillWithZeros = true }
                    .ToSampleSource()
                    .ToWaveSource(16); // 16 bits per sample (standard)

                // Start the capture
                _capture.Start();

                // Start polling the audio data asynchronously
                Task.Run(() => CaptureAudio());
            }
            else
            {
                Debug.WriteLine("No audio device available to start capture.");
            }
        }

        private void CaptureAudio()
        {
            byte[] buffer = new byte[_waveSource.WaveFormat.BytesPerSecond / 2]; // Half a second buffer
            while (_capture != null && _waveSource != null)
            {
                int read = _waveSource.Read(buffer, 0, buffer.Length);
                if (read > 0)
                {
                    // Process the audio data here (e.g., send it for visualization)
                    ProcessAudioData(buffer);
                }
            }
        }

        private void ProcessAudioData(byte[] buffer)
        {
            // Convert byte[] to float[] for better use in visualization and analysis
            int sampleCount = buffer.Length / 2; // Since it's 16-bit PCM, 2 bytes per sample
            float[] floatBuffer = new float[sampleCount];

            for (int i = 0; i < buffer.Length; i += 2)
            {
                // Convert 2 bytes (16 bits) into a short (Int16) and normalize to -1.0 to 1.0 range
                short sample = BitConverter.ToInt16(buffer, i);
                floatBuffer[i / 2] = sample / 32768f; // Normalize 16-bit PCM data to [-1, 1]
            }

            // Raise the event with the processed audio data (normalized float samples)
            AudioDataAvailable?.Invoke(this, floatBuffer);
        }

        public void StopAudioCapture()
        {
            if (_capture != null)
            {
                _capture.Stop();
                _capture.Dispose();
                _capture = null;
                Debug.WriteLine("Audio capture stopped.");
            }

            if (_waveSource != null)
            {
                _waveSource.Dispose();
                _waveSource = null;
            }
        }
    }
}
