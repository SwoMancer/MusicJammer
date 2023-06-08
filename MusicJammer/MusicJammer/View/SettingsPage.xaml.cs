using MusicJammer.Model;
using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicJammer.View
{
    /// <summary>
    /// Defines the <see cref="SettingsPage" />.<br/>
    /// Handles shard utilities.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsPage"/> class.
        /// </summary>
        public SettingsPage()
        {
            InitializeComponent();

            App.SettingsVM.Activation();

            BindingContext = App.SettingsVM;
            LogoStackLayout.BackgroundColor = Color.FromHex(App.Setting.MainShade.Hex);

            TableSection tableSection = new TableSection();
            tableSection.Title = "Resource";

           

            tableSection.Add(CreateImageCell("Image by gregor cresnar premium", "folderNormal.png", "https://www.flaticon.com/authors/gregor-cresnar-premium"));
            tableSection.Add(CreateImageCell("Image by gregor cresnar premium", "megaphoneNormal.png", "https://www.flaticon.com/authors/gregor-cresnar-premium"));
            tableSection.Add(CreateImageCell("Image by gregor cresnar premium", "packageInNormal.png", "https://www.flaticon.com/authors/gregor-cresnar-premium"));
            tableSection.Add(CreateImageCell("Image by gregor cresnar premium", "packageOutNormal.png", "https://www.flaticon.com/authors/gregor-cresnar-premium"));
            tableSection.Add(CreateImageCell("Image by gregor cresnar premium", "settingsNormal.png", "https://www.flaticon.com/authors/gregor-cresnar-premium"));

            TableView table = (TableView)FindByName("SettingsTableView");
            table.Root.Add(tableSection);
            //table.Root
        }
        #endregion
        #region Events
        /// <summary>
        /// The Top Bar Switch is Toggled.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="ToggledEventArgs"/>.</param>
        private void TopBarSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            App.Setting.ToggelBar();
        }
        /// <summary>
        /// The OnApplyTemplate.
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
        /// The On Appearing.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            //TopBarSwitch.OnChanged += TopBarSwitch_Toggled;
        }
        /// <summary>
        /// The On Disappearing.
        /// </summary>
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            //TopBarSwitch.OnChanged -= TopBarSwitch_Toggled;
        }
        private void PrintTokenButton_Clicked(object sender, EventArgs e)
        {
            //Console.WriteLine(App.FreesoundAPI.CurrentToken.ToString());
        }
        #endregion
        #region Methods
        /// <summary>
        /// Create a new Image Cell with a link.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="path"></param>
        /// <param name="url"></param>
        /// <returns><see cref="ImageCell"/></returns>
        private ImageCell CreateImageCell(string text, string path, string url)
        {
            Uri uri = new Uri(url);
            ImageCell cell = new ImageCell();
            cell.Text = text;
            cell.ImageSource = ImageSource.FromFile(path);
            cell.Tapped += new EventHandler(delegate (Object o, EventArgs a)
            {
                try
                {
                    Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
                }
                catch { }
            });
            return cell;
        }
        #endregion
    }
}
//Icon
//https://www.flaticon.com/premium-icon/settings_563608?term=Setting&page=1&position=27&page=1&position=27&related_id=563608&origin=search
//<a href="https://www.flaticon.com/free-icons/shipping-and-delivery" title="shipping and delivery icons">Shipping and delivery icons created by Gregor Cresnar Premium - Flaticon</a>