using MusicJammer.Data.API;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace MusicJammer.View
{
    /// <summary>
    /// Defines the <see cref="TunnelPage" />.<br/>
    /// The first page is to be used when the user enters the application for the first time or after the application is terminated.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TunnelPage : ContentPage
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="TunnelPage"/> class.
        /// </summary>
        public TunnelPage()
        {
            InitializeComponent();
        }
        #endregion
        #region Events
        /// <summary>
        /// The On Appearing.
        /// </summary>
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //Task navigationTask = Navigation.PushAsync(new ManagerPage(), false);
            Task tabsTask = App.TabVM.LoadOrCreateAsync();

            //App.FreesoundOAuth2 = new FreesoundOAuth2();
            //Task oAuth2Task = App.FreesoundOAuth2.LoadAsync();
            //App.FreesoundOAuth2 = await
            App.FreesoundAPI = await FreesoundWebAPI.LoadOrCreateAsync();

            Task miscTask = Task.Run(() =>
            {
                App.NavigateMenuPM.SetNavigation = Navigation;
                NavigationPage.SetHasNavigationBar(this, true);
                App.Setting.ChangeThemeByPhoneTheme();
                App.NavigateMenuPM.SelectItem("Manager");
            });

            //await Task.WhenAll(tabsTask, oAuth2Task);

            ActivityIndicator loaderActivityIndicator = (ActivityIndicator)FindByName("TunnelLoaderActivityIndicator");
            Label loaderLabel = (Label)FindByName("WhatIsLoadingLabel");

            loaderLabel.Text = "Finding your key...";
            //await oAuth2Task;
            loaderLabel.Text = "Finding your saved tabs and sounds...";
            await tabsTask;
            loaderLabel.Text = "Preforming setup Setting...";
            await miscTask;
            loaderLabel.Text = "Done.";
            loaderActivityIndicator.IsRunning = false;
            await Navigation.PushAsync(new ManagerPage(), true);
        }
        #endregion
    }
}
