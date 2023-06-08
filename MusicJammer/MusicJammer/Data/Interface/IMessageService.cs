using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicJammer.Data.Interface
{
    public interface IMessageService
    {
        /// <summary>
        /// Used to display a warning message to the user in android.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        Task ShowAsync(string title, string message);
    }
}
