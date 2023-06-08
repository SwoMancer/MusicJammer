//https://stackoverflow.com/questions/37993741/xamarin-forms-change-statusbar-color

using Android.App;

[assembly: Xamarin.Forms.Dependency(typeof(MusicJammer.Droid.ThemeImplementation))]
namespace MusicJammer.Droid
{
    /// <summary>
    /// Defines the <see cref="ThemeImplementation" />.
    /// </summary>
    public class ThemeImplementation : Data.Interface.ITheme
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThemeImplementation"/> class.
        /// </summary>
        public ThemeImplementation()
        {
        }
        /// <summary>
        /// Set to Light Mode Theme.
        /// </summary>
        public void LightModeTheme()
        {
            var activity = (Activity)MainActivity.Instance;

            activity.Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 255, 255, 255));
        }

        /// <summary>
        /// Set to Dark Mode Theme.
        /// </summary>
        public void DarkModeTheme()
        {
            var activity = (Activity)MainActivity.Instance;

            activity.Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 28, 28, 28));
        }
    }
}
