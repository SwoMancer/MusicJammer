namespace MusicJammer.Data.Interface
{
    /// <summary>
    /// Defines the <see cref="IStatusBar" />.
    /// <para>It removes or displays the status bar on Android and iOS. <br />
    /// Resources: DependencyService, OSAppTheme and interface. 
    /// <list type="bullet">
    /// <item><see href="https://coderedirect.com/questions/505936/hide-statusbar-for-specific-contentpage">coderedirect.com: hide statusbar for specific contentpage</see></item>
    /// <item><see href="https://stackoverflow.com/questions/47353986/xamarin-forms-forms-context-is-obsolete">stackoverflow.com: xamarin forms forms context is obsolete</see></item>
    /// <item><see href="https://stackoverflow.com/questions/51258783/forms-context-is-obsolete-so-how-should-i-get-activity-of-my-single-activity-app">stackoverflow.com: forms context is obsolete so how should i get activity of my single activity app</see></item>
    /// </list>
    /// </para>
    /// </summary>
    public interface IStatusBar
    {
        /// <summary>
        /// Hide the Status bar.
        /// </summary>
        void HideStatusBar();

        /// <summary>
        /// Show the Status bar.
        /// </summary>
        void ShowStatusBar();

        /// <summary>
        /// Is the Status bar visible?
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        bool IsStatusBarVisible();
    }
}
