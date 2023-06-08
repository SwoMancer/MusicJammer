using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicJammer.Data.Service
{
    /// <summary>
    /// Defines the <see cref="MessageService" />.
    /// </summary>
    internal class MessageService : Interface.IMessageService
    {
        /// <summary>
        /// Used to display a warning message to the user in android.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        public async Task ShowAsync(string title, string message)
        {
            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert(title, message, "Ok");
        }
    }
}
