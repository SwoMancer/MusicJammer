namespace MusicJammer.Model
{
    /// <summary>
    /// Defines the <see cref="UnauthorizedRespond" />.<br/>
    /// Used for HttpClient and RestSharp for unauthorized respondse messages.
    /// </summary>
    public class UnauthorizedRespond
    {
        /// <summary>
        /// The respond.
        /// </summary>
        public string detail { get; set; }
    }
}
