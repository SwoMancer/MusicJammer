using MusicJammer.Data;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace MusicJammer.View
{
    /// <summary>
    /// Defines the <see cref="LibraryPage" />.<br />
    /// Handles user needs to find sounds that can be utilized.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LibraryPage : ContentPage
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryPage"/> class.
        /// </summary>
        public LibraryPage()
        {
            InitializeComponent();
            App.LibraryVM.SetNavigation = Navigation;

            App.LibraryVM.LoaderDisplayStackLayout = (StackLayout)FindByName("LoaderStackLayout");
            App.LibraryVM.Activation();
            BindingContext = App.LibraryVM;

        }
        #endregion
        #region Events
        /// <summary>
        /// If sound button is clicked.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        public async void GetSoundButton_Clicked(object sender, EventArgs e)
        {
            await FileHandler.PickAndShow();
        }


        /// <summary>
        /// The on apply template event.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            GradientStop gradientStop;

            gradientStop = (GradientStop)GetTemplateChild("gradientStopTop");
            gradientStop.Color = Color.FromHex(App.Setting.MainColor.Hex);
            gradientStop = (GradientStop)GetTemplateChild("gradientStopDown");
            gradientStop.Color = Color.FromHex(App.Setting.MainColor.Hex);
        }
        /// <summary>
        /// The SoundListView_Scrolled. Can not be removed.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="ScrolledEventArgs"/>.</param>
        private void SoundListView_Scrolled(object sender, ScrolledEventArgs e) { }
        #endregion
    }
}
