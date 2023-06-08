/*
 * StatusBarImplementation:
 * Används för att ta bort eller ta fram status baren på Android och ios.
 * 
 * Resurser:
 * DependencyService, OSAppTheme och interface. 
 * https://coderedirect.com/questions/505936/hide-statusbar-for-specific-contentpage
 * https://stackoverflow.com/questions/47353986/xamarin-forms-forms-context-is-obsolete
 * https://stackoverflow.com/questions/51258783/forms-context-is-obsolete-so-how-should-i-get-activity-of-my-single-activity-app
 */

using Android.App;
using Android.Views;

[assembly: Xamarin.Forms.Dependency(typeof(MusicJammer.Droid.StatusBarImplementation))]
namespace MusicJammer.Droid
{
    /// <summary>
    /// Defines the <see cref="StatusBarImplementation" />.
    /// </summary>
    public class StatusBarImplementation : Data.Interface.IStatusBar
    {
        /// <summary>
        /// Defines the original flags.
        /// </summary>
        public WindowManagerFlags _originalFlags;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusBarImplementation"/> class.
        /// </summary>
        public StatusBarImplementation()
        {
        }

        /// <summary>
        /// Hide the Status Bar.
        /// </summary>
        public void HideStatusBar()
        {
            var activity = (Activity)MainActivity.Instance;

            var attrs = activity.Window.Attributes;
            _originalFlags = attrs.Flags;
            attrs.Flags |= WindowManagerFlags.Fullscreen;
            activity.Window.Attributes = attrs;
        }

        /// <summary>
        /// Is Status Bar Visible?
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsStatusBarVisible()
        {
            var activity = (Activity)MainActivity.Instance;

            var attrs = activity.Window.Attributes;

            if (attrs.Flags.HasFlag(WindowManagerFlags.Fullscreen))
                return false;
            return true;
        }

        /// <summary>
        /// Show Status Bar.
        /// </summary>
        public void ShowStatusBar()
        {
            var activity = (Activity)MainActivity.Instance;

            var attrs = activity.Window.Attributes;
            attrs.Flags = _originalFlags;
            activity.Window.Attributes = attrs;
        }
    }
}
