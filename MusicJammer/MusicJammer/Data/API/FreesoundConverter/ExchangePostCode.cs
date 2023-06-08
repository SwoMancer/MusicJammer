using System;
using System.Collections.Generic;
using System.Text;

namespace MusicJammer.Data.API.FreesoundConverter
{
    /*
     * Freesound API documentation:
     * https://freesound.org/docs/api/
     */
    /// <summary>
    /// Defines the <see cref="ExchangePostCode" />.
    /// <para>The exchangePostCode class is for RestSharp and HttpClient to send and receive data in JSON format and object form.</para>
    /// </summary>
    public class ExchangePostCode
    {
        #region Fields
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string grant_type { get; set; }
        public string code { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ExchangePostCode"/> class.
        /// </summary>
        public ExchangePostCode() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ExchangePostCode"/> class.
        /// </summary>
        /// <param name="client_id"></param>
        /// <param name="client_secret"></param>
        /// <param name="grant_type"></param>
        /// <param name="code"></param>
        public ExchangePostCode(string client_id, string client_secret, string grant_type, string code)
        {
            this.client_id = client_id;
            this.client_secret = client_secret;
            this.grant_type = grant_type;
            this.code = code;
        }
        #endregion
    }
}
