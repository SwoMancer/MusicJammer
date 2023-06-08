using Newtonsoft.Json;
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
    /// Defines the <see cref="Token" />.
    /// <para>The Token class is for RestSharp and HttpClient to send and receive data in JSON format and object. A token is used to save data in the FreesoundWebAPI class.</para>
    /// </summary>
    public class Token
    {
        #region Fields
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        #endregion
        #region Methods
        /// <summary>
        /// Its a ToString
        /// </summary>
        /// <returns>a <see cref="string"/> in Json format</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        #endregion
        

    }
}
