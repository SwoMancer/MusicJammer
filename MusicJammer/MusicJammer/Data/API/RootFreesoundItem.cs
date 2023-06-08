using System;
using System.Collections.Generic;
using System.Text;
using MusicJammer.Model;

namespace MusicJammer.Data.API
{
    /// <summary>
    /// Defines the <see cref="RootFreesoundItem" />.<br/>
    /// It is a JSON response collection of <see cref="FreesoundItem"/>s from the freesounds.org web API.
    /// </summary>
    public class RootFreesoundItem
    {
        #region Fields
        public int count { get; set; }
        public string next { get; set; }
        public List<FreesoundItem> results { get; set; }
        public object previous { get; set; }
        #endregion
    }
}
