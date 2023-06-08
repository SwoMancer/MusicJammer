using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MusicJammer.View;

namespace MusicJammer.PseudoModel
{
    /// <summary>
    /// Defines the <see cref="NavigateMenuPseudoModel" />. <br/>
    /// It is used as a view model for the navigation bar that's displayed in almost every view.
    /// </summary>
    public class NavigateMenuPseudoModel
    {
        /// <summary>
        /// Defines the Icon.
        /// </summary>
        public enum Icon
        { /// <summary>
          /// Defines the Dark.
          /// </summary>
            Dark,
            /// <summary>
            /// Defines the Light.
            /// </summary>
            Light
        }
        #region Fields
        /// <summary>
        /// Defines the menu items.
        /// </summary>
        private ObservableCollection<Model.NavigateMenuItem> _menuItems = new ObservableCollection<Model.NavigateMenuItem>();
        /// <summary>
        /// Defines the navigation.
        /// </summary>
        private INavigation _navigation;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the MenuItems.
        /// </summary>
        public ObservableCollection<Model.NavigateMenuItem> MenuItems
        {
            get
            {
                return _menuItems;
            }
        }

        /// <summary>
        /// Sets the Navigation.
        /// </summary>
        public INavigation SetNavigation
        {
            set
            {
                _navigation = value;
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Drops all items in collection.
        /// </summary>
        public void DropItemsCollection()
        {
            if (_menuItems.Any())
                _menuItems.Clear();
        }

        /// <summary>
        /// Select a item by the order id.
        /// </summary>
        /// <param name="orderId">The orderId<see cref="int"/>.</param>
        public void SelectItem(int orderId)
        {
            //UpdateListGUI(_menuItems.First(item => item.isSelected == true));
            DeselectItems();
            UpdateListGUI(_menuItems.First(item => item.OrderId == orderId));
        }

        /// <summary>
        /// Select a item by the name.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        public void SelectItem(string name)
        {

            //UpdateListGUI(_menuItems.First(item => item.isSelected == true));
            DeselectItems();
            UpdateListGUI(_menuItems.First(item => item.Name == name));
        }

        /// <summary>
        /// Toggle to disable the item by the order id.
        /// </summary>
        /// <param name="orderId">The orderId<see cref="int"/>.</param>
        public void ToggleDisableItem(int orderId)
        {
            if (!(orderId >= _menuItems.Count && orderId < 0))
                _menuItems[_menuItems.IndexOf(_menuItems.First(item => item.OrderId == orderId))].IsDisable = !_menuItems.First(item => item.OrderId == orderId).IsDisable;
        }

        /// <summary>
        /// Insert a new item into the collection.
        /// </summary>
        /// <param name="items">The items<see cref="ObservableCollection{Model.NavigateMenuItem}"/>.</param>
        public void InsertNewItems(ObservableCollection<Model.NavigateMenuItem> items)
        {
            _menuItems = items;
        }

        /// <summary>
        /// Order items after order ids.
        /// </summary>
        public void OrderItems()
        {
            ListConvert(_menuItems.OrderBy(i => i.OrderId).ToList());
        }

        /// <summary>
        /// Create all items.
        /// </summary>
        /// <param name="textColor">The textColor<see cref="Data.RGB"/>.</param>
        /// <param name="selectedTextColor">The selectedTextColor<see cref="Data.RGB"/>.</param>
        /// <param name="icon">The icon<see cref="Icon"/>.</param>
        public void CreateItems(Data.RGB textColor, Data.RGB selectedTextColor, Icon icon)
        {
            string[] itemNames = { "Crafter", "Sounds", "Library", "Manager", "Settings" };
            string[] imageName = { "megaphone", "packageOut", "packageIn", "folder", "settings" };
            int i = 0;

            DropItemsCollection();

            foreach (string name in itemNames)
            {
                string pathHighlight = HighlightImagePath(icon, imageName[i]);
                string pathNormal = imageName[i] + "Normal.png";

                _menuItems.Add(new Model.NavigateMenuItem(name, i++,
                ImageSource.FromFile(pathNormal),
                ImageSource.FromFile(pathHighlight),
                textColor, selectedTextColor)
                { });
            }
        }

        /// <summary>
        /// Get the highlight image path for an item.
        /// </summary>
        /// <param name="icon">The icon<see cref="Icon"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string HighlightImagePath(Icon icon, string name)
        {
            string path = name;

            switch (icon)
            {
                case Icon.Dark:
                    path = path + "Dark";
                    break;
                case Icon.Light:
                    path = path + "Light";
                    break;
            }

            string pathHighlight = path + "Highlight.png";
            return pathHighlight;
        }

        /// <summary>
        /// Update items after the new theme.
        /// </summary>
        /// <param name="textColor">The textColor<see cref="Data.RGB"/>.</param>
        /// <param name="selectedTextColor">The selectedTextColor<see cref="Data.RGB"/>.</param>
        /// <param name="icon">The icon<see cref="Icon"/>.</param>
        public void UppdateItems(Data.RGB textColor, Data.RGB selectedTextColor, Icon icon)
        {
            if (_menuItems.Any())
            {
                string[] imageName = { "megaphone", "packageOut", "packageIn", "folder", "settings" };
                int z = 0;

                foreach (Model.NavigateMenuItem item in _menuItems)
                {
                    _menuItems.First(i => i.OrderId == item.OrderId).NormalTextColor = textColor;
                    _menuItems.First(i => i.OrderId == item.OrderId).SelectedTextColor = selectedTextColor;
                    _menuItems.First(i => i.OrderId == item.OrderId).NormalImage = imageName[z] + "Normal.png";
                    _menuItems.First(i => i.OrderId == item.OrderId).SelectedImage = HighlightImagePath(icon, imageName[z++]);
                }
            }
        }

        /// <summary>
        /// Remove and adds a new list of items to the collection.
        /// </summary>
        /// <param name="listItems">The listItems<see cref="List{Model.NavigateMenuItem}"/>.</param>
        private void ListConvert(List<Model.NavigateMenuItem> listItems)
        {
            _menuItems.Clear();
            foreach (Model.NavigateMenuItem listItem in listItems)
            {
                _menuItems.Add(listItem);
            }
        }

        /// <summary>
        /// Deselect all items.
        /// </summary>
        private void DeselectItems()
        {
            try
            {
                List<Model.NavigateMenuItem> selectedItems = _menuItems.Where(i => i.isSelected == true).ToList();
                if (selectedItems.Any())
                {
                    foreach (Model.NavigateMenuItem selectedItem in selectedItems)
                    {
                        if (_menuItems.FirstOrDefault(i => i.OrderId == selectedItem.OrderId) != null)
                        {
                            _menuItems.FirstOrDefault(i => i.OrderId == selectedItem.OrderId).isSelected = false;
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Update list GUI.
        /// </summary>
        /// <param name="navigateMenuItem">The navigateMenuItem<see cref="Model.NavigateMenuItem"/>.</param>
        private void UpdateListGUI(Model.NavigateMenuItem navigateMenuItem)
        {
            //_menuItems.Remove(navigateMenuItem);
            navigateMenuItem.isSelected = !navigateMenuItem.isSelected;
        }

        /// <summary>
        /// Navigate to a newly selected view.
        /// </summary>
        /// <param name="orderId">The orderId<see cref="int"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        private async Task ToNewPage(int orderId)
        {
            try
            {
                switch (orderId)
                {
                    case 0:
                        await _navigation.PushAsync(new MusicMasterPage(), false);
                        break;
                    case 1:
                        await _navigation.PushAsync(new SoundsPage(), false);
                        break;
                    case 2:
                        await _navigation.PushAsync(new LibraryPage(), false);
                        break;
                    case 3:
                        await _navigation.PushAsync(new ManagerPage(), false);
                        break;
                    case 4:
                        await _navigation.PushAsync(new SettingsPage(), false);
                        break;
                    default:
                        await _navigation.PushAsync(new MusicMasterPage(), false);
                        break;
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion
        #region Commands
        /// <summary>
        /// Users click on a navigate button in the collection.
        /// </summary>
        public ICommand SendToPageCommand
        {
            get
            {
                return new Command(async (e) =>
                {
                    Model.NavigateMenuItem menuItem = e as Model.NavigateMenuItem;

                    //If it is already in the selected view, do not move.
                    if (!menuItem.Equals(_menuItems.First(item => item.isSelected == true)))
                    {
                        await ToNewPage(menuItem.OrderId);
                        SelectItem(menuItem.OrderId);
                    }
                });
            }
        }
        #endregion
    }
}
