namespace LedAudioVisualizer.Visualizers
{
    internal class SpectrumAudioVisualizer : IAudioVisualizer
    {

        public int redMinFreq = 20;
        public int redMaxFreq = 200;
        public int greenMinFreq = 201;
        public int greenMaxFreq = 2000;
        public int blueMinFreq = 2001;
        public int blueMaxFreq = 20000;

        public (double[], double[], double[]) VisualizeAudio(float[] frequencySpectrum, uint numLeds, float sampleRate)
        {
            int spectrumLength = frequencySpectrum.Length;

            // Define frequency ranges for bass, mids, and highs
            int bassStartFreq = redMinFreq;
            int bassEndFreq = redMaxFreq;

            int midStartFreq = greenMinFreq;
            int midEndFreq = greenMaxFreq;

            int highStartFreq = blueMinFreq;
            int highEndFreq = blueMaxFreq;

            // Set up arrays to hold the amplitude for each LED
            uint halfNumLeds = numLeds / 2;  // Half the number of LEDs for mirroring
            double[] redChannel = new double[halfNumLeds];    // Bass (Red)
            double[] greenChannel = new double[halfNumLeds];  // Mid (Green)
            double[] blueChannel = new double[halfNumLeds];   // High (Blue)

            // Set the frequency resolution per LED
            float bassFreqRange = bassEndFreq - bassStartFreq;  // Total range of bass frequencies
            float midFreqRange = midEndFreq - midStartFreq;     // Total range of mid frequencies
            float highFreqRange = highEndFreq - highStartFreq;  // Total range of high frequencies

            // Loop through the frequency spectrum and map it to the red, green, and blue arrays
            for (int i = 0; i < spectrumLength; i++)
            {
                // Calculate the corresponding frequency for the current bin
                float frequency = (i * sampleRate) / (2 * spectrumLength); // Divide by 2 since spectrum represents half the FFT result

                // Get the magnitude of the current frequency bin
                float magnitude = frequencySpectrum[i];

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
                if (frequency >= highStartFreq && frequency <= highEndFreq)
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

            // Bind the output data to values 0 -> 1
            float inputMax = frequencySpectrum.Max();
            redChannel = Utility.NormalizeArray(redChannel, 0, inputMax);
            greenChannel = Utility.NormalizeArray(greenChannel, 0, inputMax);
            blueChannel = Utility.NormalizeArray(blueChannel, 0, inputMax);

            // Mirror the arrays from the center outward
            double[] fullRedChannel = Utility.MirrorArrayOutward(redChannel);
            double[] fullGreenChannel = Utility.MirrorArray(greenChannel);
            double[] fullBlueChannel = Utility.MirrorArray(blueChannel);

            // Apply smoothing: Adjust the middle pixel based on adjacent pixels
            Utility.SmoothChannel(fullRedChannel);
            Utility.SmoothChannel(fullGreenChannel);
            Utility.SmoothChannel(fullBlueChannel);

            // Send the mirrored color data back via callback
            return (fullRedChannel, fullGreenChannel, fullBlueChannel);
        }

    }
}
