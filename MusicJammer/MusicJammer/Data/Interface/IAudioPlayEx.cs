namespace MusicJammer.Data.Interface
{
    /// <summary>
    /// Defines the <see cref="IAudioPlayEx" />.
    /// <para>Resources: <br/>
    /// <see href="https://stackoverflow.com/questions/34256176/how-to-play-sounds-on-xamarin-forms">Stackoverflow.com: How to play sounds on Xamarin forms</see></para>
    /// </summary>
    public interface IAudioPlayEx
    {
        /// <summary>
        /// Play the audio file.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        void PlayAudioFile(string fileName);
    }
}
