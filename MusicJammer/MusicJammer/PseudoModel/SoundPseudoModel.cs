using MusicJammer.Data.Sound;
using MusicJammer.Model;
using MusicJammer.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicJammer.PseudoModel
{
    /// <summary>
    /// Defines the <see cref="SoundPseudoModel" />.<br />
    /// Handels all the sounds.
    /// </summary>
    public class SoundPseudoModel : ObservableProperty
    {
        #region Fields
        /// <summary>
        /// Defines the collection for sound masters.
        /// </summary>
        private ObservableCollection<SoundMasterItem> _soundMaster = new ObservableCollection<SoundMasterItem>();

        /// <summary>
        /// Defines the collection for sound infos.
        /// </summary>
        private ObservableCollection<SoundItem> _soundInfo = new ObservableCollection<SoundItem>();
        #endregion
        #region Properties
        /// <summary>
        /// Gets the collection for sound master items.
        /// </summary>
        public ObservableCollection<SoundMasterItem> SoundMasterItems
        {
            get
            {
                return _soundMaster;
            }
        }

        /// <summary>
        /// Gets the collection for sound items in the collection.
        /// </summary>
        public ObservableCollection<SoundItem> SoundItems
        {
            get
            {
                return _soundInfo;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Add a sound item.
        /// </summary>
        /// <param name="soundItem">The soundItem<see cref="SoundItem"/>.</param>
        public void Add(SoundItem soundItem)
        {
            _soundInfo.Add(soundItem);

            if (soundItem.IsVisible)
            {
                //_soundMasterItems.Add(new SoundMasterItem(_soundItems[_soundItems.IndexOf(soundItem)], new SoundController()));
                _soundMaster.Add(new SoundMasterItem(soundItem, new SoundController()));
                OnPropertyChanged("Add");
            }
        }

        /// <summary>
        /// ReplaceTab an old sound item with a new sound item in the collection.
        /// </summary>
        /// <param name="oldInfo">The oldInfo<see cref="SoundItem"/>.</param>
        /// <param name="newInfo">The newInfo<see cref="SoundItem"/>.</param>
        public void Replace(SoundItem oldInfo, SoundItem newInfo)
        {
            _soundInfo[_soundInfo.IndexOf(oldInfo)] = newInfo;

            //Remove
            if (oldInfo.IsVisible == true && newInfo.IsVisible == false)
            {
                _soundMaster.Remove(_soundMaster.Where(i => i.Item.IsSoundBaseEqual(oldInfo)).FirstOrDefault());
            }//Add
            else if (oldInfo.IsVisible == false && newInfo.IsVisible == true)
            {
                _soundMaster.Add(new SoundMasterItem(newInfo, new SoundController()));
            }//ReplaceTab
            else if (oldInfo.IsVisible == true && newInfo.IsVisible == true)
            {
                foreach (SoundMasterItem item in _soundMaster)
                {
                    if (item.Item.Equals(oldInfo))
                    {
                        //_soundMasterItems[_soundMasterItems.IndexOf(item)].Item = _soundItems[_soundItems.IndexOf(newSoundItem)];
                        _soundMaster[_soundMaster.IndexOf(item)].Item = newInfo;
                        OnPropertyChanged("ReplaceTab");
                    }
                }
            }
        }

        /// <summary>
        /// Remove a sound item from the collection.
        /// </summary>
        /// <param name="info">The info<see cref="SoundItem"/>.</param>
        public void Remove(SoundItem info)
        {
            _soundInfo.Remove(info);

            _soundMaster.Remove(_soundMaster.Where(i => i.Item.IsSoundBaseEqual(info)).FirstOrDefault());
            OnPropertyChanged("Remove");
        }

        /// <summary>
        /// Remove all master sounds in the collection.
        /// </summary>
        public void MasterRemoveAll()
        {
            _soundMaster.Clear();
        }

        /// <summary>
        /// Goes through all the sounds to see which ones should appear.
        /// </summary>
        public void RemasterSounds()
        {
            //Loop through only items with changes...
            foreach (SoundItem soundItem in _soundInfo.Where(i => i.IsVisibleChange).ToList())
            {
                //To be shown...
                if (soundItem.IsVisible)
                {
                    //If it doesn't already exist...
                    if (_soundMaster.Where(i => i.Item.Id == soundItem.Id).Count() == 0)
                    {
                        _soundMaster.Add(new SoundMasterItem(soundItem, new SoundController()));
                    }
                }
                //Should not be displayed...
                else
                {
                    //If it already exists...
                    if (_soundMaster.Where(i => i.Item.Id == soundItem.Id).Count() != 0)
                    {
                        _soundMaster.Remove(_soundMaster.Where(i => i.Item.Id == soundItem.Id).First());
                    }
                }

            }
        }
        /// <summary>
        /// Find a sound item by its <see cref="SoundBase"/>
        /// </summary>
        /// <param name="soundBase">The soundBase<see cref="SoundItem"/>.</param>
        /// <returns>The <see cref="SoundItem"/>.</returns>
        private SoundItem FindSoundItem(SoundItem soundBase)
        {
            return _soundInfo.Where(i => i.GetSoundBase.IsSoundBaseEqual(soundBase)).FirstOrDefault();
        }

        /// <summary>
        /// Create a sound master items.
        /// </summary>
        private void CreateSoundMasterItems()
        {
            //If soundItems is empty, stop and return.
            if (_soundInfo.Count <= 0)
                return;

            //If soundItems do not contain a visible trait, stop and return.
            if (_soundInfo.Where(i => i.IsVisible == true).FirstOrDefault() == null)
                return;

            //Loop throw all visible sound item...
            foreach (SoundItem soundItem in _soundInfo)
            {
                if (soundItem.IsVisible)
                {
                    //Use unique sound item to add Sound master item to the collection.
                    _soundMaster.Add(new SoundMasterItem(soundItem, new SoundController()));
                }
            }
        }
        /// <summary>
        /// Unload sounds from memory, so they can not be played.
        /// </summary>
        public void Unload()
        {
            if (_soundMaster.Where(i => i.Controller == null).Count() == 0)
                return;

            foreach (SoundMasterItem soundMasterItem in _soundMaster)
            {
                soundMasterItem.Controller.Dispose();
            }
        }

        /// <summary>
        /// Load sounds into memory so that they can be played.
        /// </summary>
        public void Load()
        {
            foreach (SoundMasterItem soundMasterItem in _soundMaster)
            {
                soundMasterItem.Controller.Load(soundMasterItem.Item);
            }
        }
        #endregion
        #region Commands
        /// <summary>
        /// Play a sound.
        /// </summary>
        public ICommand PlayCommand
        {
            get
            {
                return new Command((e) =>
                {
                    SoundMasterItem soundItem = e as SoundMasterItem;
                    //PlayASoundControlles(soundItems);
                    soundItem.Controller.Play();

                });
            }
        }

        /// <summary>
        /// Play all sounds.
        /// </summary>
        public ICommand PlayAllCommand
        {
            get
            {
                return new Command((e) =>
                {
                    TabItem tab = e as TabItem;

                    try
                    {
                        foreach (SoundMasterItem sound in _soundMaster.Where(f => f.Item.ActiveStateOn))
                        {
                            sound.Controller.EngageLoop();
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                });
            }
        }

        /// <summary>
        /// Pause all sounds.
        /// </summary>
        public ICommand PauseAllCommand
        {
            get
            {
                return new Command((e) =>
                {
                    TabItem tab = e as TabItem;
                    foreach (SoundMasterItem sound in tab.SoundVM.SoundMasterItems)
                    {
                        sound.Controller.Stop();
                    }
                });
            }
        }

        /// <summary>
        /// Remove a sound.
        /// </summary>
        public ICommand RemoveCommand
        {
            get
            {
                return new Command((e) =>
                {
                    SoundItem item = e as SoundItem;
                    Remove(item);
                });
            }
        }
        #endregion
    }
}
