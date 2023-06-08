using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicJammer.Data.API
{
    /// <summary>
    /// Defines the <see cref="Url" />.
    /// <para>The URL creates a URL used to send a request.</para>
    /// </summary>
    public class Url
    {
        #region Fields
        public string Start { get; set; }
        public IDictionary<string, string> GetParameters { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Url"/> class.
        /// </summary>
        public Url()
        {
            GetParameters = new Dictionary<string, string>();
        }
        #endregion
        #region Methods
        /// <summary>
        /// Used to get a valid web URL for a web request.
        /// </summary>
        /// <returns>an url <see cref="string"/></returns>
        public async Task<string> ToUrlAsync()
        {
            string url = Start;
            url += await Task.Run(() => GetParametersToString());
            //url += '/';

            return url;
        }
        /// <summary>
        /// Used to get an URL get parameters for a url.
        /// </summary>
        /// <returns>an url <see cref="string"/></returns>
        private string GetParametersToString()
        {
            bool isFirstGetParameter = true;
            string getUrl = string.Empty;

            if (GetParameters.Count >= 1)
            {
                foreach (KeyValuePair<string, string> item in GetParameters)
                {
                    if (isFirstGetParameter)
                    {
                        isFirstGetParameter = false;
                        getUrl += '?';
                    }
                    else
                    {
                        getUrl += '&';
                    }

                    getUrl += item.Key + '=' + item.Value;
                }
            }

            return getUrl;
        }
        #endregion
    }
}
