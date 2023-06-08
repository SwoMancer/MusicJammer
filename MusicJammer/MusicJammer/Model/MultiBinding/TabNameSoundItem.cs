//DESKTOP - 4098PCL\gwred
//2023 - 01 - 09 15:02:34

namespace MusicJammer.Model.MultiBinding
{
    /// <summary>
    /// Defines the <see cref="TabNameSoundItem" />.
    /// <para>
    /// It is a multi binding class for <see cref="string"/> and <see cref="SoundItem"/>.
    /// See <see cref="Data.Converter.TabNameSoundItemConverter"/> for its converter.
    /// </para>
    /// </summary>
    public class TabNameSoundItem
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the soundItem.
        /// </summary>
        public SoundItem SoundItem { get; set; }
    }
}
