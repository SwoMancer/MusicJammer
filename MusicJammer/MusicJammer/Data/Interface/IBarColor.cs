namespace MusicJammer.Data.Interface
{
    /// <summary>
    /// Defines the <see cref="IBarColor" />. It edits the status bar on Android and iOS.
    /// <para>
    /// Resources: DependencyService, OSAppTheme and interface. 
    /// <list type="bullet">
    /// <item><see href="https://coderedirect.com/questions/505936/hide-statusbar-for-specific-contentpage">coderedirect.com: hide statusbar for specific contentpage</see></item>
    /// <item><see href="https://stackoverflow.com/questions/47353986/xamarin-forms-forms-context-is-obsolete">stackoverflow.com: xamarin forms forms context is obsolete</see></item>
    /// <item><see href="https://stackoverflow.com/questions/51258783/forms-context-is-obsolete-so-how-should-i-get-activity-of-my-single-activity-app">stackoverflow.com: forms context is obsolete so how should i get activity of my single activity app</see></item>
    /// </list>
    /// </para>
    /// </summary>
    public interface IBarColor
    {
        /// <summary>
        /// Set the Status bars color.
        /// </summary>
        /// <param name="hex">The hex<see cref="string"/>.</param>
        void StatusBarColor(string hex);

        /// <summary>
        /// Set the Navigation bars color.
        /// </summary>
        /// <param name="hex">The hex<see cref="string"/>.</param>
        void NavigationBarColor(string hex);

        /// <summary>
        /// Set the Navigation bar titles color.
        /// </summary>
        /// <param name="hex">The hex<see cref="string"/>.</param>
        void NavigationBarTitleColor(string hex);

        /// <summary>
        /// Set all the Bars Colors.
        /// </summary>
        /// <param name="hexBackground">The hexBackground<see cref="string"/>.</param>
        /// <param name="hexTitle">The hexTitle<see cref="string"/>.</param>
        void AllBarColor(string hexBackground, string hexTitle);
    }
}
