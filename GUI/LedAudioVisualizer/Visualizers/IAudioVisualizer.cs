namespace LedAudioVisualizer.Visualizers
{
    public interface IAudioVisualizer
    {
        /// <summary>
        /// Returns (double[] R, double[] G, double[] B) of lengths[numLeds].
        /// </summary>
        /// <param name="frequencySpectrum"></param>
        /// <param name="numLeds"></param>
        /// <param name="sampleRate"></param>
        /// <returns></returns>
        public (double[], double[], double[]) VisualizeAudio(float[] frequencySpectrum, uint numLeds, float sampleRate);

    }
}
