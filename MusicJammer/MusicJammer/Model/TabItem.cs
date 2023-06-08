using MusicJammer.ViewModel;
using System;
using Xamarin.Essentials;
using MusicJammer.PseudoModel;

namespace MusicJammer.Model
{
    /// <summary>
    /// Defines the <see cref="TabItem" />.
    /// 
    /// </summary>
    public class TabItem : ObservableProperty
    {
        #region Fields
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the sound view model.
        /// </summary>
        public SoundPseudoModel SoundVM { get; set; }

        /// <summary>
        /// Gets or sets the nav size.
        /// </summary>
        public int NavSize { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="TabItem"/> class.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        public TabItem(int id, string name)
        {
            this.Id = id;
            this.Name = name;
            this.NavSize = ScreenBlockSize(5);
            this.SoundVM = new SoundPseudoModel();
        }
        #endregion
        #region Methods
        /// <summary>
        /// The screen block size.
        /// </summary>
        /// <param name="blocks">The blocks<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private static int ScreenBlockSize(int blocks)
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var width = mainDisplayInfo.Width;
            var xamarinWidth = width / mainDisplayInfo.Density;

            return (int)(xamarinWidth / blocks);
        }
        /// <summary>
        /// Safely dispose of all sound controllers for memory reclamation.
        /// </summary>
        public void Dispose()
        {
            foreach (SoundMasterItem item in SoundVM.SoundMasterItems)
            {
                item.Controller.Dispose();
            }
        }
        #endregion
    }
}
