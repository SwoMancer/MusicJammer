using MusicJammer;
using MusicJammer.Data;
using MusicJammer.Model;
using MusicJammer.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MusicJammer.PseudoModel
{
    /// <summary>
    /// Defines the <see cref="TabPseudoModel" />.
    /// </summary>
    public class TabPseudoModel : ObservableProperty
    {
        #region Fields
        /// <summary>
        /// The name of the save file.
        /// </summary>
        private static readonly string FILENAME = "TabViewModel.json";
        /// <summary>
        /// Defines the tab items.
        /// </summary>
        private ObservableCollection<TabItem> _tabItems = new ObservableCollection<TabItem>();
        /// <summary>
        /// Defines the id next in series.
        /// </summary>
        private int _idNextInSeries = 0;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the Tab items.
        /// </summary>
        public ObservableCollection<TabItem> TabItems
        {
            get
            {
                return _tabItems;
            }
        }

        /// <summary>
        /// Gets the possible tabs names.
        /// </summary>
        public string[] PossibleTabsName
        {
            get
            {
                List<string> tabNames = new List<string>();
                foreach (TabItem tabItem in _tabItems)
                {
                    tabNames.Add(tabItem.Name);
                }
                return tabNames.ToArray();
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// ReplaceTab a sound item in a tab.
        /// </summary>
        /// <param name="oldTabItem">The oldTabItem<see cref="TabItem"/>.</param>
        /// <param name="oldSoundItem">The oldSoundItem<see cref="SoundItem"/>.</param>
        /// <param name="newSoundItem">The newSoundItem<see cref="SoundItem"/>.</param>
        public void ReplaceSoundItem(TabItem oldTabItem, SoundItem oldSoundItem, SoundItem newSoundItem)
        {
            TabItem newTabItem = oldTabItem;
            newTabItem.SoundVM.Replace(oldSoundItem, newSoundItem);
            ReplaceTab(oldTabItem, newTabItem);
        }

        /// <summary>
        /// ReplaceTab a tab.
        /// </summary>
        /// <param name="oldTabItem">The oldTabItem<see cref="TabItem"/>.</param>
        /// <param name="newTabItem">The newTabItem<see cref="TabItem"/>.</param>
        public void ReplaceTab(TabItem oldTabItem, TabItem newTabItem)
        {
            _tabItems[_tabItems.IndexOf(FindTabItem(oldTabItem))] = newTabItem;
        }

        /// <summary>
        /// Drop all tabs.
        /// </summary>
        public void DropAllTabs()
        {
            _tabItems.Clear();
        }

        /// <summary>
        /// Add a sound into a tab.
        /// </summary>
        /// <param name="tabItem">The tabItem<see cref="TabItem"/>.</param>
        /// <param name="soundItem">The soundItem<see cref="SoundItem"/>.</param>
        public void AddSound(TabItem tabItem, SoundItem soundItem)
        {
            _tabItems[_tabItems.IndexOf(FindTabItem(tabItem))].SoundVM.Add(soundItem);
        }

        /// <summary>
        /// Find a tab item by name.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="TabItem"/>.</returns>
        public TabItem FindTabItemByName(string name)
        {
            return _tabItems.Where(i => i.Name == name).FirstOrDefault();
        }

        /// <summary>
        /// Create a new tab.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        public void CreateNewTab(string name)
        {
            //Shouldn't need more than 10
            if (19 <= _idNextInSeries)
                return;

            TabItem tabItem = new TabItem(_idNextInSeries++, name);
            _tabItems.Add(tabItem);
        }

        /// <summary>
        /// Save this object to a local file.
        /// </summary>
        public void Save()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FILENAME);

            try
            {
                string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(fileName, jsonString);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        /// <summary>
        /// Save this object to a local file in Async.
        /// </summary>
        public Task AsyncSave()
        {
            Task task = Task.Run(() =>
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FILENAME);

                try
                {
                    string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
                    File.WriteAllText(fileName, jsonString);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });
            return task;
        }

        /// <summary>
        /// Try loading the file if it exists; otherwise, create a new one.
        /// </summary>
        public void Load()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FILENAME);

            //Finns den redan?
            if (File.Exists(fileName))
            {
                string jsonText = File.ReadAllText(fileName);
                TabPseudoModel tabViewModel = JsonConvert.DeserializeObject<TabPseudoModel>(jsonText);

                App.TabVM = tabViewModel;
            }
            //Om den inte finns skapa en ny.
            else
            {
                App.TabVM = new TabPseudoModel();
            }
        }

        /// <summary>
        ///Try loading the file if it exists; otherwise, create a new one.
        /// </summary>
        public async Task LoadOrCreateAsync()
        {
            switch (FileHandler.AlreadyExists("TabViewModel.json"))
            {
                case true:
                    App.TabVM = await Task.Run(() => FileHandler.LoadFile<TabPseudoModel>(FILENAME));
                    break;
                case false:
                    App.TabVM = new TabPseudoModel();
                    break;
            }
        }
        /// <summary>
        /// Find tab item.
        /// </summary>
        /// <param name="tabItem">The tabItem<see cref="TabItem"/>.</param>
        /// <returns>The <see cref="TabItem"/>.</returns>
        private TabItem FindTabItem(TabItem tabItem)
        {
            return _tabItems[_tabItems.IndexOf(_tabItems.Where(i => i.Name == tabItem.Name).FirstOrDefault())];
        }
        #endregion
        #region Commands
        /// <summary>
        /// Move the item up in the collection.
        /// </summary>
        public ICommand MoveUpCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    TabItem tabItem = e as TabItem;
                    if (_tabItems.IndexOf(tabItem) == 0)
                        return;

                    _tabItems.Move(_tabItems.IndexOf(tabItem), _tabItems.IndexOf(tabItem) - 1);
                    await AsyncSave();
                });
            }
        }

        /// <summary>
        /// Move the item down in the collection.
        /// </summary>
        public ICommand MoveDownCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    TabItem tabItem = e as TabItem;
                    if (_tabItems.IndexOf(tabItem) == _tabItems.Count - 1)
                        return;

                    _tabItems.Move(_tabItems.IndexOf(tabItem), _tabItems.IndexOf(tabItem) + 1);
                    await AsyncSave();
                });
            }
        }

        /// <summary>
        /// Remove the item from the collection.
        /// </summary>
        public ICommand RemoveCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    TabItem tabItem = e as TabItem;


                    _tabItems.Remove(tabItem);
                    await AsyncSave();
                    tabItem.Dispose();
                });
            }
        }
        #endregion
    }
}
