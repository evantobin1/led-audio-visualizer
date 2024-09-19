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
        public static void ScaleArray(double[] array, byte power, float maxPower)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] *= ((power / maxPower)); // Normalize all values to [0, 1]
            }
        }

        public static double[] NormalizeArray(double[] data, float min, float max)
        {
            if(data.Length == 0)
            {
                return data;
            }
            // Get the minimum and maximum values from the array
            double dataMin = data.Min();
            double dataMax = data.Max();

            // Initialize a new array for normalized data
            double[] normalizedData = new double[data.Length];

            // Handle the case where the min and max are the same (avoiding division by zero)
            if (dataMax == dataMin)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    normalizedData[i] = min; // If all values are the same, map them to the minimum of the new range
                }
                return normalizedData;
            }

            // Normalize the array elements
            for (int i = 0; i < data.Length; i++)
            {
                // Scale the data to [0, 1] range, then scale to [min, max] range
                normalizedData[i] = (data[i] - dataMin) / (dataMax - dataMin) * (max - min) + min;
            }

            return normalizedData;
        }

        // Apply a threshold to the array, setting values below the threshold to 0
        public static void ApplyThreshold(double[] array, float threshold)
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
        public static void SmoothChannel(double[] channel)
        {
            for (int i = 1; i < channel.Length - 1; i++) // Avoid the first and last pixel
            {
                double prev = channel[i - 1];
                double next = channel[i + 1];
                double current = channel[i];

                // If both adjacent pixels are higher than the current one, set the current one to their average
                if (prev > current && next > current)
                {
                    channel[i] = (prev + next) / 2.0f;
                }
            }
        }



        // Mirror the array outward from the center
        public static double[] MirrorArrayOutward(double[] halfArray)
        {
            int fullSize = halfArray.Length * 2;
            double[] fullArray = new double[fullSize];

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
        public static double[] MirrorArray(double[] halfArray)
        {
            int fullSize = halfArray.Length * 2;
            double[] fullArray = new double[fullSize];

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
