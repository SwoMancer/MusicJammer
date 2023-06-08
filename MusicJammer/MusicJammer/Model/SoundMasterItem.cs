//DESKTOP - 4098PCL\gwred
//2023 - 01 - 09 15:02:29

namespace MusicJammer.Model
{
    using MusicJammer.Data.Sound;

    /// <summary>
    /// Defines the <see cref="SoundMasterItem" />.<br />
    /// It is used to play and contain a sound that can be shown and played.
    /// </summary>
    public class SoundMasterItem
    {
        #region Fields
        /// <summary>
        /// Gets or sets the sound item.
        /// </summary>
        public SoundItem Item { get; set; }

        /// <summary>
        /// Gets or sets the sound controller.
        /// </summary>
        public SoundController Controller { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SoundMasterItem"/> class.
        /// </summary>
        /// <param name="item">The item<see cref="SoundItem"/>.</param>
        /// <param name="controller">The controller<see cref="SoundController"/>.</param>
        public SoundMasterItem(SoundItem item, SoundController controller)
        {
            Item = item;
            Controller = controller;
        }
        #endregion
    }
}
