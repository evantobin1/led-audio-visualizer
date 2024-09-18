using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedAudioVisualizer
{
    public static class Utility
    {
        // Normalize array values to the range [0, 1]
        public static void ScaleArray(float[] array, byte power, float maxPower)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] *= ((power / maxPower)); // Normalize all values to [0, 1]
            }
        }

        // Apply a threshold to the array, setting values below the threshold to 0
        public static void ApplyThreshold(float[] array, float threshold)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] < threshold)
                {
                    array[i] = 0;  // Set to zero if below the threshold
                }
            }
        }

        // Smooth the array by setting a pixel to the average of its neighbors if they are both higher
        public static void SmoothChannel(float[] channel)
        {
            for (int i = 1; i < channel.Length - 1; i++) // Avoid the first and last pixel
            {
                float prev = channel[i - 1];
                float next = channel[i + 1];
                float current = channel[i];

                // If both adjacent pixels are higher than the current one, set the current one to their average
                if (prev > current && next > current)
                {
                    channel[i] = (prev + next) / 2.0f;
                }
            }
        }

        // Mirror the array outward from the center
        public static float[] MirrorArrayOutward(float[] halfArray)
        {
            int fullSize = halfArray.Length * 2;
            float[] fullArray = new float[fullSize];

            // Start from the center and expand outward
            int center = fullSize / 2;

            // Copy the first half into both sides of the center
            for (int i = 0; i < halfArray.Length; i++)
            {
                fullArray[center - i - 1] = halfArray[i];  // Fill to the left of the center
                fullArray[center + i] = halfArray[i];      // Fill to the right of the center
            }

            return fullArray;
        }

        // Mirror the array from one side to another
        public static float[] MirrorArray(float[] halfArray)
        {
            int fullSize = halfArray.Length * 2;
            float[] fullArray = new float[fullSize];

            // Copy the first half
            for (int i = 0; i < halfArray.Length; i++)
            {
                fullArray[i] = halfArray[i];
            }

            // Copy the mirrored second half
            for (int i = 0; i < halfArray.Length; i++)
            {
                fullArray[fullSize - 1 - i] = halfArray[i];  // Mirror the array
            }

            return fullArray;
        }
    }
}
