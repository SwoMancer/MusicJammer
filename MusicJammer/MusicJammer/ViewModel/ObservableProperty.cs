using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MusicJammer.ViewModel
{
    /// <summary>
    /// Defines the <see cref="ObservableProperty" />.<br/>
    /// Handles property changed.
    /// </summary>
    public class ObservableProperty : INotifyPropertyChanged
    {
        #region Field 
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        #region Events
        /// <summary>
        /// On property changed call me.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
