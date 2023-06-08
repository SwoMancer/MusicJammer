using MusicJammer.Data;
using MusicJammer.Model;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicJammer.View
{
    /// <summary>
    /// Defines the <see cref="SoundsPage" />.<br/>
    /// Handles which sounds can be modified and removed.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SoundsPage : ContentPage
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SoundsPage"/> class.
        /// </summary>
        public SoundsPage()
        {
            InitializeComponent();

            App.SoundsVM.Activation();
            BindingContext = App.SoundsVM;

            CustomCollectionViewMenu headMenu = (CustomCollectionViewMenu)FindByName("HeadTabCollecionView");
        }
        #endregion
        #region Events
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
        }

        /// <summary>
        /// The On Disappearing.
        /// </summary>
        protected async override void OnDisappearing()
        {
            base.OnDisappearing();

            await Task.Run(() =>
            {
                foreach (TabItem tabItem in App.TabVM.TabItems)
                {
                    tabItem.SoundVM.RemasterSounds();
                }
            });

            await App.TabVM.AsyncSave();

        }

        /// <summary>
        /// The Head Tab Collecion View Selection Changed.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="SelectionChangedEventArgs"/>.</param>
        private void HeadTabCollecionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem previousTabItem = e.PreviousSelection.FirstOrDefault() as TabItem;
            TabItem currentTabItem = e.CurrentSelection.FirstOrDefault() as TabItem;

            if (currentTabItem == null || currentTabItem.Equals(BodyCarouselView.CurrentItem))
                return;

            if (previousTabItem != null)
            {
                if (previousTabItem.Equals(currentTabItem))
                    return;
            }



            BodyCarouselView.ScrollTo(currentTabItem, true, ScrollToPosition.Center, false);
        }


        /// <summary>
        /// The Body Carousel View Current Item Changed.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="CurrentItemChangedEventArgs"/>.</param>
        private void BodyCarouselView_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            TabItem previousTabItem = e.PreviousItem as TabItem;
            TabItem currentTabItem = e.CurrentItem as TabItem;

            if (currentTabItem == null || currentTabItem.Equals(currentTabItem.Equals(HeadTabCollecionView.SelectedItem)))
                return;

            if (previousTabItem != null)
            {
                if (previousTabItem.Equals(currentTabItem))
                    return;
            }

            HeadTabCollecionView.SelectedItem = currentTabItem;
            HeadTabCollecionView.ScrollTo(currentTabItem, true, ScrollToPosition.Center, true);
        }
        #endregion
    }
}
