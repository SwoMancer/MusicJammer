    /*
     * BarColorImplementation:
     * Används för att bytta färge på Android.
     * 
     * Resurser:
     * DependencyService, OSAppTheme och interface. 
     * https://coderedirect.com/questions/505936/hide-statusbar-for-specific-contentpage
     * https://stackoverflow.com/questions/47353986/xamarin-forms-forms-context-is-obsolete
     * https://stackoverflow.com/questions/51258783/forms-context-is-obsolete-so-how-should-i-get-activity-of-my-single-activity-app
     * https://stackoverflow.com/questions/37993741/xamarin-forms-change-statusbar-color
     */

[assembly: Xamarin.Forms.Dependency(typeof(MusicJammer.Droid.BarColorImplementation))]
namespace MusicJammer.Droid
{
    using Plugin.CurrentActivity;

    /// <summary>
    /// Defines the <see cref="BarColorImplementation" />.
    /// </summary>
    public class BarColorImplementation : Data.Interface.IBarColor
    {
        /// <summary>
        /// Set All Bar Color.
        /// </summary>
        /// <param name="hexBackground">The hexBackground<see cref="string"/>.</param>
        /// <param name="hexTitle">The hexTitle<see cref="string"/>.</param>
        public void AllBarColor(string hexBackground, string hexTitle)
        {
            CrossCurrentActivity.Current.Activity.Window.SetNavigationBarColor(Android.Graphics.Color.ParseColor(hexBackground));
            //CrossCurrentActivity.Current.Activity.Window.SetTitleColor(Android.Graphics.Color.ParseColor(hexTitle));
            CrossCurrentActivity.Current.Activity.Window.SetStatusBarColor(Android.Graphics.Color.ParseColor(hexBackground));
        }

        /// <summary>
        /// set Navigation Bar Color.
        /// </summary>
        /// <param name="hex">The hex<see cref="string"/>.</param>
        public void NavigationBarColor(string hex)
        {
            //var activity = (Activity)MainActivity.Instance;
            CrossCurrentActivity.Current.Activity.Window.SetNavigationBarColor(Android.Graphics.Color.ParseColor(hex));
        }

        /// <summary>
        /// set Navigation Bar Title Color.
        /// </summary>
        /// <param name="hex">The hex<see cref="string"/>.</param>
        public void NavigationBarTitleColor(string hex)
        {
            //var activity = (Activity)MainActivity.Instance;
            //CrossCurrentActivity.Current.Activity.Window.SetTitleColor(Android.Graphics.Color.ParseColor(hex));
        }

        /// <summary>
        /// set Status Bar Color.
        /// </summary>
        /// <param name="hex">The hex<see cref="string"/>.</param>
        public void StatusBarColor(string hex)
        {
            //var activity = (Activity)MainActivity.Instance;
            CrossCurrentActivity.Current.Activity.Window.SetStatusBarColor(Android.Graphics.Color.ParseColor(hex));
        }
    }
}
