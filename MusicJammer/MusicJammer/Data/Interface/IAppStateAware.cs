using System.Threading.Tasks;

namespace MusicJammer.Data.Interface
{
    /// <summary>
    /// Defines the <see cref="IAppStateAware" used to get hold of Android state awares/>.
    /// </summary>
    public interface IAppStateAware
    {
        /// <summary>
        /// The OnResumeApplicationAsync. 
        /// <para>Its called when the application resume</para>
        /// </summary>
        Task OnResumeApplicationAsync();

        /// <summary>
        /// The OnSleepApplicationAsync.
        /// <para>Its called when the application start to sleep</para>
        /// </summary>
        Task OnSleepApplicationAsync();

        /// <summary>
        /// The OnStartApplicationAsync.
        /// <para>Its called when the application start</para>
        /// </summary>
        Task OnStartApplicationAsync();
    }
}
