using MusicJammer;
using MusicJammer.Model;
using MusicJammer.Model.MultiBinding;
using MusicJammer.ViewModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicJammer.PseudoModel
{
    /// <summary>
    /// Defines the <see cref="TemporarilysSoundPseudoModel" />.<br />
    /// It handles sounds that are not deemed to be used for playing currently.
    /// </summary>
    public class TemporarilysSoundPseudoModel : ObservableProperty
    {
        #region Fields
        /// <summary>
        /// Defines the sound items.
        /// </summary>
        private ObservableCollection<SoundItem> _soundItems = new ObservableCollection<SoundItem>();
        /// <summary>
        /// Defines the freesound item.
        /// </summary>
        private ObservableCollection<FreesoundItem> _freesoundItems = new ObservableCollection<FreesoundItem>();
        #endregion
        #region Properties
        /// <summary>
        /// Gets the Sound items
        /// Returns an <c>ObservableCollection</c> of  <c>Model.SoundItem</c>..
        /// </summary>
        public ObservableCollection<SoundItem> SoundItems
        {
            get
            {
                return _soundItems;
            }
        }
        /// <summary>
        /// Gets the freesound items
        /// Returns an <c>ObservableCollection</c> of  <c>Model.FreesoundItem</c>..
        /// </summary>
        public ObservableCollection<FreesoundItem> FreesoundItems
        {
            get
            {
                return _freesoundItems;
            }
        }

        /// <summary>
        /// Gets the tab names.
        /// </summary>
        public string[] TabNames
        {
            get
            {
                return App.TabVM.PossibleTabsName;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="TemporarilysSoundPseudoModel"/> class.
        /// </summary>
        public TemporarilysSoundPseudoModel()
        {
        }
        #endregion
        #region Methods
        /// <summary>
        /// Add a sound item.
        /// </summary>
        /// <param name="soundItem">The soundItem<see cref="SoundItem"/>.</param>
        public async void AddSoundItemAsync(SoundItem soundItem)
        {
            _soundItems.Add(soundItem);
            await RemoveReplicasAsync();
            OnPropertyChanged(nameof(_soundItems));
        }

        /// <summary>
        /// Remove all replicas items.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task RemoveReplicasAsync()
        {
            await Task.Run(() =>
            {
                List<SoundItem> soundItems = _soundItems.Distinct().ToList();
                _soundItems.Clear();

                foreach (SoundItem soundItem in soundItems)
                {
                    _soundItems.Add(soundItem);
                }
            });
        }
        #endregion
        #region Commands

        /// <summary>
        /// Saves a sound item to a tab.
        /// </summary>
        public ICommand SaveSoundToTabCommand
        {
            get
            {
                return new Command((e) =>
                {
                    TabNameSoundItem tabNameSoundItem = e as TabNameSoundItem;

                    tabNameSoundItem.SoundItem.WaitPeriod = 0;
                    tabNameSoundItem.SoundItem.ActiveStateOn = true;
                    tabNameSoundItem.SoundItem.PlayStateOn = true;

                    if (tabNameSoundItem.Name == "xxx" || tabNameSoundItem.SoundItem.Name == "xxx")
                        return;

                    //To stop audio from being linked to each other in the first creation.
                    //To stop a bug where the same sounds in different tabs link and interfere with each other.
                    SoundItem item = new SoundItem(tabNameSoundItem.SoundItem.Name, tabNameSoundItem.SoundItem.Path, tabNameSoundItem.SoundItem.FileName);

                    App.TabVM.AddSound(App.TabVM.FindTabItemByName(tabNameSoundItem.Name), item);

                });
            }
        }
        #endregion
    }
}
