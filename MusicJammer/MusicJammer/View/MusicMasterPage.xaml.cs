using MusicJammer.Data.Interface;
using MusicJammer.Model;
using MusicJammer.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicJammer.View
{
    /// <summary>
    /// Defines the <see cref="MusicMasterPage" />.<br/>
    /// Handles sounds functions relate to up play feature.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MusicMasterPage : ContentPage, IAppStateAware
    {
        #region Fields
        /// <summary>
        /// Defines the current tab name.
        /// </summary>
        private string _currentTabName;
        /// <summary>
        /// Defines the previous tab item.
        /// </summary>
        private TabItem previousTabItem;
        /// <summary>
        /// Defines the current tab item.
        /// </summary>
        private TabItem currentTabItem;
        /// <summary>
        /// Defines the random.
        /// </summary>
        private Random random = new Random();
        /// <summary>
        /// Defines the speed.
        /// </summary>
        public SensorSpeed speed = SensorSpeed.Game;
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MusicMasterPage"/> class.
        /// </summary>
        public MusicMasterPage()
        {
            InitializeComponent();

            App.MusicMasterVM.Activation();
            BindingContext = App.MusicMasterVM;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Toggle accelerometer. <br/>
        /// https://docs.microsoft.com/en-us/xamarin/essentials/detect-shake
        /// </summary>
        public async void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                else
                    Accelerometer.Start(speed);
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Shake Detected", "We are sorry, but your hardware does not support shake detected.", "OK");
            }
            catch (Exception)
            {
                await DisplayAlert("Shake Detected error", "Shake detected seems not to work today.", "OK");
            }
        }
        #endregion
        #region Events
        /// <summary>
        /// The accelerometer shake is detected.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            Console.WriteLine("ShakeDetected");
            if (currentTabItem != null)
            {
                foreach (SoundMasterItem sound in currentTabItem.SoundVM.SoundMasterItems)
                {
                    sound.Item.WaitPeriod = random.Next(1, 119);
                }
            }
        }

        /// <summary>
        /// The body carousel view that current item changed.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="CurrentItemChangedEventArgs"/>.</param>
        private void BodyCarouselView_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {

            previousTabItem = e.PreviousItem as TabItem;
            currentTabItem = e.CurrentItem as TabItem;

            if (currentTabItem == null || currentTabItem.Equals(currentTabItem.Equals(HeadTabCollecionView.SelectedItem)))
                return;

            if (previousTabItem != null)
            {
                if (previousTabItem.Equals(currentTabItem))
                    return;
            }

            HeadTabCollecionView.SelectedItem = currentTabItem;
            HeadTabCollecionView.ScrollTo(currentTabItem, true, ScrollToPosition.Center, true);


            _currentTabName = currentTabItem.Name;
        }

        /// <summary>
        /// The Head tab collecion view selection changed.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="SelectionChangedEventArgs"/>.</param>
        private void HeadTabCollecionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            previousTabItem = e.PreviousSelection.FirstOrDefault() as TabItem;
            currentTabItem = e.CurrentSelection.FirstOrDefault() as TabItem;

            if (currentTabItem == null || currentTabItem.Equals(BodyCarouselView.CurrentItem))
            {
                if (previousTabItem != null)
                    previousTabItem.SoundVM.Unload();

                currentTabItem.SoundVM.Load();

                return;
            }

            if (previousTabItem != null)
            {
                if (previousTabItem.Equals(currentTabItem))
                    return;
            }

            BodyCarouselView.ScrollTo(currentTabItem, true, ScrollToPosition.Center, false);

        }
        #region OnStats
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

            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
            Accelerometer.Start(speed);

        }

        /// <summary>
        /// The On Disappearing.
        /// </summary>
        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            Accelerometer.ShakeDetected -= Accelerometer_ShakeDetected;
            Accelerometer.Stop();

            await App.TabVM.AsyncSave();
        }

        /// <summary>
        /// The On Resume Application Async.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public Task OnResumeApplicationAsync()
        {
            return Task.Run(() =>
            {

            });
        }

        /// <summary>
        /// The On Sleep Application Async.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public Task OnSleepApplicationAsync()
        {
            return Task.Run(() =>
            {

            });
        }

        /// <summary>
        /// The On Start Application Async.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public Task OnStartApplicationAsync()
        {
            return Task.Run(() =>
            {

            });
        }
        #endregion
        #endregion
    }
}
