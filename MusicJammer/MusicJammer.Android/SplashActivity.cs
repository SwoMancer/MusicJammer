using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;

namespace MusicJammer.Droid
{
    /// <summary>
    /// Defines the <see cref="SplashActivity" />.
    /// </summary>
    [Activity(Label = "Music Jam", MainLauncher = true, Theme = "@style/MyTheme.Splash", NoHistory = true)]
    public class SplashActivity : Activity
    {
        /// <summary>
        /// The On Create.
        /// </summary>
        /// <param name="savedInstanceState">The savedInstanceState<see cref="Bundle"/>.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetColors();
        }

        /// <summary>
        /// The On Resume.
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }

        /// <summary>
        /// Set Colors.
        /// </summary>
        private void SetColors()
        {
            Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 69, 70, 68));
            Window.SetNavigationBarColor(Android.Graphics.Color.Argb(255, 69, 70, 68));
        }
    }
}
