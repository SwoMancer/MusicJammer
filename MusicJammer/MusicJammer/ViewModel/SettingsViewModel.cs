using MusicJammer.Data;
using MusicJammer.Model;
using MusicJammer.PseudoModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MusicJammer.ViewModel
{
    /// <summary>
    /// Defines the <see cref="SettingsViewModel" />.<br/>
    /// View model for <see cref="View.SettingsPage"/>
    /// </summary>
    public class SettingsViewModel : ViewModelBase
    {
        #region Fields
        private bool _IsDebugOn = false;
        #endregion
        #region Properties
        public bool IsDebugOn
        {
            get { return _IsDebugOn; }
            set
            {
                _IsDebugOn = value;
                OnPropertyChanged(nameof(IsDebugOn));
            }
        }
        public string HasOAuth2Token
        { 
            get
            {
                return "OAuth2 Token exist: " + (App.FreesoundAPI.CurrentToken != null);
            } 
        }
        public string HasTabs
        {
            get
            {
                return "Tabs: " + App.TabVM.TabItems.Count + " ST";
            }
        }
        public string HasSoundsInTabs
        {
            get
            {
                int sounds = 0;
                foreach (TabItem tabItem in App.TabVM.TabItems)
                {
                    sounds += tabItem.SoundVM.SoundItems.Count;
                }
                return "All sounds in tabs: " + sounds + " ST";
            }
        }
        public string HasTokenFile
        {
            get
            {
                return "Token file exist: " + FileHandler.AlreadyExists("oAuth2.json");
            }
        }
        public string HasTabsFile
        {
            get
            {
                return "Tabs file exist: " + FileHandler.AlreadyExists("TabViewModel.json");
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="navigateMenuViewModel"></param>
        public SettingsViewModel(Settings settings, NavigateMenuPseudoModel navigateMenuViewModel) : base(settings, navigateMenuViewModel)
        {

        }
        #endregion
        #region Methods
        public override void Activation()
        {
            base.Activation();
        }
        #endregion
    }
}
