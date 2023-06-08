using System;
using System.Collections.Generic;
using System.Text;

namespace MusicJammer.Model
{
    /// <summary>
    /// Defines the <see cref="FreesoundItem" />.
    /// <para>
    /// Used to hold cached data from freesounds.org web API and to display objects in collection views.
    /// </para>
    /// </summary>
    public class FreesoundItem
    {
        #region Properties
        public int id { get; set; }
        public string name { get; set; }
        public List<string> tags { get; set; }
        public string license { get; set; }
        public string username { get; set; }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="FreesoundItem"/> class.
        /// </summary>
        public FreesoundItem()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FreesoundItem"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="tags"></param>
        /// <param name="license"></param>
        /// <param name="username"></param>
        public FreesoundItem(int id, string name, List<string> tags, string license, string username)
        {
            this.id = id;
            this.name = name;
            this.tags = tags;
            this.license = license;
            this.username = username;
        }
        #endregion
    }
}
