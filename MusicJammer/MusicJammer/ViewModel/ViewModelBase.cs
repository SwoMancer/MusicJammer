using MusicJammer.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MusicJammer.PseudoModel;

namespace MusicJammer.ViewModel
{
    /// <summary>
    /// Defines the <see cref="ViewModelBase" />.<br/>
    /// Base model for all view models.
    /// </summary>
    public abstract class ViewModelBase : ObservableProperty
    {
        #region Fields
        /// <summary>
        /// Commands property store commands methods and their XAML name.
        /// </summary>
        public Dictionary<string, ICommand> Commands { get; protected set; }
        public NavigateMenuPseudoModel NavigateMenuViewModel { get; set; }
        public Data.Settings Settings { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="navigateMenuViewModel"></param>
        public ViewModelBase(Data.Settings settings, NavigateMenuPseudoModel navigateMenuViewModel)
        {
            Commands = new Dictionary<string, ICommand>();
            this.Settings = settings;
            this.NavigateMenuViewModel = navigateMenuViewModel;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Bind the app properties to the local view model. Remember to override this method if more properties are added to constructors.
        /// </summary>
        public virtual void Activation()
        {
            this.Settings = App.Setting;
            this.NavigateMenuViewModel = App.NavigateMenuPM;
        }
        #endregion
    }
}
