namespace MusicJammer.Data.Interface
{
    /// <summary>
    /// Defines the <see cref="ITheme" />.
    /// <para>Used to set dark or light theme on android and ios.<br />
    /// Resources:
    /// <list type="bullet">
    /// <item>DependencyService</item>
    /// <item>OSAppTheme</item>
    /// <item>interface</item>
    /// </list>
    /// <see href="https://coderedirect.com/questions/505936/hide-statusbar-for-specific-contentpage">coderedirect.com: hide statusbar for specific contentpage</see>
    /// </para>
    /// </summary>
    public interface ITheme
    {
        /// <summary>
        /// Set to the light mode theme.
        /// </summary>
        void LightModeTheme();

        /// <summary>
        /// Set the dark mode theme.
        /// </summary>
        void DarkModeTheme();
    }
}
