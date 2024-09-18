using CSCore;
using CSCore.CoreAudioAPI;
using CSCore.SoundIn;
using CSCore.Streams;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MathNet.Numerics.IntegralTransforms;
using System.Numerics;
using CSCore.XAudio2;

namespace LedAudioVisualizer
{
    public enum AudioAnalysisType
    {
        PulseCodeModulation,
        FrequencyAnalysis
    }
    public class AudioProcessor
    {
        private WasapiLoopbackCapture _capture;
        private MMDevice _audioDevice;
        private IWaveSource _waveSource;

        public event EventHandler<float[]> AudioDataAvailable;                      // Event for visual data (FFT)
        public event EventHandler<(float[], float[], float[])> ColorDataAvailable;  // Event for color data
        public int NumLeds = 100;

        // Default frequency bands
        public int redMinFreq = 20;
        public int redMaxFreq = 200;
        public int greenMinFreq = 201;
        public int greenMaxFreq = 2000;
        public int blueMinFreq = 2001;
        public int blueMaxFreq = 20000;

        public readonly float MAX_POWER = 10.0f;
        public byte redPower;
        public byte greenPower;
        public byte bluePower;


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
                    ProcessAudioData_NoFilter(AudioDataAvailable, buffer);
                    ProcessColorData(ColorDataAvailable, buffer);
                }
            }
        }

        private void ProcessColorData(EventHandler<(float[], float[], float[])> callback, byte[] buffer)
        {
            int sampleCount = buffer.Length / 2;
            float[] floatBuffer = new float[sampleCount];

            // Convert byte[] to float[] for FFT processing
            for (int i = 0; i < buffer.Length; i += 2)
            {
                short sample = BitConverter.ToInt16(buffer, i);
                floatBuffer[i / 2] = sample / 32768f; // Normalize 16-bit PCM data to [-1, 1]
            }

            // Perform FFT
            Complex[] fftBuffer = new Complex[sampleCount];
            for (int i = 0; i < sampleCount; i++)
            {
                fftBuffer[i] = new Complex(floatBuffer[i], 0); // real part = sample, imaginary part = 0
            }

            Fourier.Forward(fftBuffer, FourierOptions.Matlab);

            // Define frequency ranges for bass, mids, and highs
            float sampleRate = 44100f; // Assuming 44.1 kHz sample rate
            int bassStartFreq = redMinFreq;    // Start of bass frequency range
            int bassEndFreq = redMaxFreq;      // End of bass frequency range

            int midStartFreq = greenMinFreq;
            int midEndFreq = greenMaxFreq;

            int highStartFreq = blueMinFreq;

            // Set up arrays to hold the amplitude for each LED
            int halfNumLeds = NumLeds / 2;  // Half the number of LEDs for mirroring
            float[] redChannel = new float[halfNumLeds];    // Bass (Red)
            float[] greenChannel = new float[halfNumLeds];  // Mid (Green)
            float[] blueChannel = new float[halfNumLeds];   // High (Blue)

            // Set the frequency resolution per LED
            float bassFreqRange = bassEndFreq - bassStartFreq;  // Total range of bass frequencies
            float midFreqRange = midEndFreq - midStartFreq;     // Total range of mid frequencies
            float highFreqRange = (sampleRate / 2) - highStartFreq; // Total range of high frequencies

            // Loop through the FFT result and map it to the red, green, and blue arrays
            for (int i = 0; i < sampleCount / 2; i++)
            {
                // Calculate the corresponding frequency for the current bin
                float frequency = (i * sampleRate) / sampleCount;

                // Get the magnitude of the current frequency bin
                float magnitude = (float)fftBuffer[i].Magnitude;

                // Map frequencies to the bass range (Red Channel)
                if (frequency >= bassStartFreq && frequency <= bassEndFreq)
                {
                    // Normalize the frequency to the bass LED range
                    float normalizedFreq = (frequency - bassStartFreq) / bassFreqRange;

                    // Linear cutoff: Apply a scaling factor that decreases with frequency
                    float linearCutoffFactor = 1.0f - normalizedFreq;  // Gradually decrease the magnitude

                    int ledIndex = (int)(normalizedFreq * redChannel.Length); // Map frequency to LED index
                    if (ledIndex >= 0 && ledIndex < redChannel.Length)
                    {
                        redChannel[ledIndex] += magnitude * linearCutoffFactor; // Apply the cutoff factor
                    }
                }

                // Map frequencies to the mid range (Green Channel)
                if (frequency >= midStartFreq && frequency <= midEndFreq)
                {
                    // Normalize the frequency to the mid LED range
                    float normalizedFreq = (frequency - midStartFreq) / midFreqRange;

                    // Linear cutoff: Apply a scaling factor that decreases with frequency
                    float linearCutoffFactor = 1.0f - normalizedFreq;  // Gradually decrease the magnitude

                    int ledIndex = (int)(normalizedFreq * greenChannel.Length); // Map frequency to LED index
                    if (ledIndex >= 0 && ledIndex < greenChannel.Length)
                    {
                        greenChannel[ledIndex] += magnitude * linearCutoffFactor; // Apply the cutoff factor
                    }
                }

                // Map frequencies to the high range (Blue Channel)
                if (frequency >= highStartFreq)
                {
                    // Normalize the frequency to the high LED range
                    float normalizedFreq = (frequency - highStartFreq) / highFreqRange;

                    // Linear cutoff: Apply a scaling factor that decreases with frequency
                    float linearCutoffFactor = 1.0f - normalizedFreq;  // Gradually decrease the magnitude

                    int ledIndex = (int)(normalizedFreq * blueChannel.Length); // Map frequency to LED index
                    if (ledIndex >= 0 && ledIndex < blueChannel.Length)
                    {
                        blueChannel[ledIndex] += magnitude * linearCutoffFactor; // Apply the cutoff factor
                    }
                }
            }

            // Mirror the arrays from the center outward
            float[] fullRedChannel = Utility.MirrorArrayOutward(redChannel);
            float[] fullGreenChannel = Utility.MirrorArray(greenChannel);
            float[] fullBlueChannel = Utility.MirrorArray(blueChannel);

            Utility.ScaleArray(fullRedChannel, redPower, MAX_POWER);
            Utility.ScaleArray(fullGreenChannel, greenPower, MAX_POWER);
            Utility.ScaleArray(fullBlueChannel, bluePower, MAX_POWER);

            // Apply threshold: Set any values below the threshold to 0
            Utility.ApplyThreshold(fullRedChannel, 0.10f);  // Example threshold of 0.05
            Utility.ApplyThreshold(fullGreenChannel, 0.10f);
            Utility.ApplyThreshold(fullBlueChannel, 0.10f);

            // Apply smoothing: Adjust the middle pixel based on adjacent pixels
            Utility.SmoothChannel(fullRedChannel);
            Utility.SmoothChannel(fullGreenChannel);
            Utility.SmoothChannel(fullBlueChannel);

            // Send the mirrored color data back via callback
            callback?.Invoke(this, (fullRedChannel, fullGreenChannel, fullBlueChannel));
        }

        private void ProcessAudioData_NoFilter(EventHandler<float[]> callback, byte[] buffer)
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

            // Send data for visualization
            callback.Invoke(this, magnitudes);
        }


        private void ProcessAudioData_LowPass(EventHandler<float[]> callback, byte[] buffer)
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

            // Set the cutoff frequency for the low-pass filter
            float cutoffFrequency = 1000f; // Low-pass cutoff at 1000 Hz
            int sampleRate = 44100; // Assuming 44.1 kHz sample rate
            int cutoffBin = (int)(cutoffFrequency / sampleRate * sampleCount);

            // Apply low-pass filter by zeroing out frequencies above the cutoff
            for (int i = cutoffBin; i < sampleCount / 2; i++)
            {
                fftBuffer[i] = new Complex(0, 0);
            }

            float[] magnitudes = new float[sampleCount / 2];
            for (int i = 0; i < sampleCount / 2; i++)
            {
                magnitudes[i] = (float)fftBuffer[i].Magnitude;
            }

            // Send filtered data for visualization
            callback.Invoke(this, magnitudes);
        }


        private void ProcessAudioData_HighPass(EventHandler<float[]> callback, byte[] buffer)
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

            // Set the cutoff frequency for the high-pass filter
            float cutoffFrequency = 1000f; // High-pass cutoff at 1000 Hz
            int sampleRate = 44100; // Assuming 44.1 kHz sample rate
            int cutoffBin = (int)(cutoffFrequency / sampleRate * sampleCount);

            // Apply high-pass filter by zeroing out frequencies below the cutoff
            for (int i = 0; i < cutoffBin; i++)
            {
                fftBuffer[i] = new Complex(0, 0);
            }

            float[] magnitudes = new float[sampleCount / 2];
            for (int i = 0; i < sampleCount / 2; i++)
            {
                magnitudes[i] = (float)fftBuffer[i].Magnitude;
            }

            // Send filtered data for visualization
            callback.Invoke(this, magnitudes);
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

    public static class AudioInterpreter
    { 
        public static float[] FrequencySpectrum(float[] inputBuffer)
        {
            return inputBuffer.Select(x => (float)Math.Pow(x, 2)).ToArray();
        }
    }

}
