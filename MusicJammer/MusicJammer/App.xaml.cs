using MusicJammer.Data;
using MusicJammer.Data.API;
using MusicJammer.Data.Interface;
using MusicJammer.Data.Service;
using MusicJammer.PseudoModel;
using MusicJammer.View;
using MusicJammer.ViewModel;
using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;

namespace MusicJammer
{
    /// <summary>
    /// Defines the <see cref="App" />.
    /// </summary>
    public partial class App : Application
    {
        #region Fields
        #region View Models
        public static SettingsViewModel SettingsVM { get; set; }
        public static MusicMasterViewModel MusicMasterVM { get; set; }
        public static SoundsViewModel SoundsVM { get; set; }
        public static LibraryViewModel LibraryVM { get; set; }
        public static ManagerViewModel ManagerVM { get; set; }
        /// <summary>
        /// Gets or sets the TemporarilySoundVM.
        /// </summary>
        public static TemporarilysSoundPseudoModel TemporarilySoundVM { get; set; }

        //Tar hand om olika tabs med sina ljud filer
        /// <summary>
        /// Gets or sets the TabVM.
        /// </summary>
        public static TabPseudoModel TabVM { get; set; }
        #endregion
        #region Pseudo Models
        public static NavigateMenuPseudoModel NavigateMenuPM { get; set; }
        #endregion
        #region API / Misc data
        public static Settings Setting { get; set; }

        //Ta bort mig senare
        public static FreesoundWebAPI FreesoundAPI { get; set; }
        //Är en container för andra vi Model så att det kan användas samtidigts
        /// <summary>
        /// Gets or sets the ContainerVM.
        /// </summary>
        //public static ViewModel.ContainerViewModel ContainerVM { get; set; }

        //public static Data.Settings Settings { get; set; }
        #endregion
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            InitializeDependencyService();
            InitializeComponent();
            InitializePseudoModels();
            InitializeViewModels();

            BindingContext = NavigateMenuPM;

            Setting.ChangeThemeByPhoneTheme();
            Setting.DeployNavigateMenuChange();

            MainPage = new NavigationPage(new TunnelPage())
            {
                BarBackgroundColor = Color.FromHex(Setting.MainShade.Hex),
                BarTextColor = Color.FromHex(Setting.TitleColor.Hex),

            };
        }
        #endregion
        #region Methods
        /// <summary>
        /// Initialize Dependency Service.
        /// </summary>
        private void InitializeDependencyService()
        {
            DependencyService.Register<Data.Interface.IMessageService, MessageService>();
        }
        /// <summary>
        /// Initializ Pseudo Models.
        /// </summary>
        private void InitializePseudoModels()
        {
            //Old
            TemporarilySoundVM = new TemporarilysSoundPseudoModel();
            //ContainerVM = new ViewModel.ContainerViewModel();

            //New
            TabVM = new TabPseudoModel();
            NavigateMenuPM = new NavigateMenuPseudoModel();
            Setting = new Settings();
        }

        /// <summary>
        /// Initialize View Models.
        /// </summary>
        private void InitializeViewModels()
        {
            ManagerVM = new ManagerViewModel(Setting, NavigateMenuPM, TabVM);
            LibraryVM = new LibraryViewModel(Setting, NavigateMenuPM, TemporarilySoundVM, TabVM);
            SoundsVM = new SoundsViewModel(Setting, NavigateMenuPM, TabVM);
            MusicMasterVM = new MusicMasterViewModel(Setting, NavigateMenuPM, TabVM);
            SettingsVM = new SettingsViewModel(Setting, NavigateMenuPM);
        }

        /// <summary>
        /// Send user To Same Page.
        /// </summary>
        private static void SendToSamePage()
        {
            //https://stackoverflow.com/questions/27305217/obtain-current-page-name-in-xamarin-forms-app
            var actionPage = App.Current.MainPage;
            if (actionPage.Navigation != null)
                actionPage = actionPage.Navigation.NavigationStack.Last();


            string pageName = actionPage.GetType().Name;
            switch (pageName)
            {
                case "LibraryPage":
                    //NavigateMenuVM.SelectItem("Library");
                    Current.MainPage.Navigation.PushAsync(new LibraryPage(), false);
                    break;
                case "ManagerPage":
                    //NavigateMenuVM.SelectItem("Manager");
                    Current.MainPage.Navigation.PushAsync(new ManagerPage(), false);
                    break;
                case "MusicMasterPage":
                    //NavigateMenuVM.SelectItem("Crafter");
                    Current.MainPage.Navigation.PushAsync(new MusicMasterPage(), false);
                    break;
                case "SettingsPage":
                    //NavigateMenuVM.SelectItem("Settings");
                    Current.MainPage.Navigation.PushAsync(new SettingsPage(), false);
                    break;
                case "SoundsPage":
                    //NavigateMenuVM.SelectItem("Sounds");
                    Current.MainPage.Navigation.PushAsync(new SoundsPage(), false);
                    break;
                default:
                    Current.MainPage.Navigation.PushAsync(new TunnelPage(), false);
                    break;
            }
        }
        #endregion
        #region Events
        /// <summary>
        /// The On Start.
        /// https://stackoverflow.com/questions/46201143/call-onresumo-method-within-a-contentpage-xamarin-forms
        /// </summary>
        protected override async void OnStart()
        {
            //Ändra på tema så att det passar enhetens inställningar.
            Current.RequestedThemeChanged += (s, a) =>
            {
                Setting.ChangeThemeByPhoneTheme();
                Setting.DeployNavigationBarChange();
                Setting.UpdateNavigateMenuChange();
                SendToSamePage();
            };

            Setting.ChangeThemeByPhoneTheme();
            Setting.DeployNavigationBarChange();

            if (Application.Current.MainPage is IAppStateAware appStateAwarePage)
                await appStateAwarePage.OnStartApplicationAsync();
            if (Application.Current.MainPage.BindingContext is IAppStateAware appStateAwareVm)
                await appStateAwareVm.OnStartApplicationAsync();
        }

        /// <summary>
        /// The On Sleep.
        /// </summary>
        protected override async void OnSleep()
        {
            if (Application.Current.MainPage is IAppStateAware appStateAwarePage)
                await appStateAwarePage.OnSleepApplicationAsync();
            if (Application.Current.MainPage.BindingContext is IAppStateAware appStateAwareVm)
                await appStateAwareVm.OnSleepApplicationAsync();
        }

        /// <summary>
        /// The On Resume.
        /// </summary>
        protected override async void OnResume()
        {
            if (Application.Current.MainPage is IAppStateAware appStateAwarePage)
                await appStateAwarePage.OnResumeApplicationAsync();
            if (Application.Current.MainPage.BindingContext is IAppStateAware appStateAwareVm)
                await appStateAwareVm.OnResumeApplicationAsync();
        }
        #endregion
    }
}
