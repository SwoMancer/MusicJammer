using MusicJammer.Data.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicJammer.View
{
    /// <summary>
    /// Defines the <see cref="OAuth2Page" />.<br/>
    /// Lead user step by step throw OAuth2 login.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OAuth2Page : ContentPage
    {
        #region Fields
        /// <summary>
        /// Defines the is text changed event on?
        /// </summary>
        private bool _IsTextChangedEventOn = false;
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="OAuth2Page"/> class.
        /// </summary>
        public OAuth2Page()
        {
            InitializeComponent();
        }
        #endregion
        #region Methods
        /// <summary>
        /// Unsubscribe from event on entry 
        /// </summary>
        private void UnsubscribeChangedText()
        {
            Entry tokenEntry = (Entry)FindByName("TokenEntry");
            if (_IsTextChangedEventOn)
                tokenEntry.TextChanged -= TokenEntry_TextChanged;
        }
        #endregion
        #region Events
        /// <summary>
        /// Handles the TextChanged event of the TokenEntry control.
        /// Se if Entry has any text.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void TokenEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            Button insertedTokenAuthenticationButton = (Button)FindByName("InsertedTokenAuthenticationButton");

            if (!string.IsNullOrEmpty(entry.Text))
            {
                insertedTokenAuthenticationButton.IsEnabled = true;
            }
            else
            {
                insertedTokenAuthenticationButton.IsEnabled = false;
            }
        }
        /// <summary>
        /// Handles the Clicked event of the ProceedAuthenticationButton control.
        /// User try to Authentication.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void ProceedAuthenticationButton_Clicked(object sender, EventArgs e)
        {
            var authorizeTask = FreesoundWebAPI.OAuth2Authentication();

            Button insertedTokenAuthenticationButton = (Button)FindByName("InsertedTokenAuthenticationButton");
            Button proceedAuthenticationButton = (Button)FindByName("ProceedAuthenticationButton");
            Entry tokenEntry = (Entry)FindByName("TokenEntry");

            insertedTokenAuthenticationButton.IsVisible = true;
            tokenEntry.IsVisible = true;
            tokenEntry.IsEnabled = true;
            proceedAuthenticationButton.Text = "try again to get my token";

            tokenEntry.TextChanged += TokenEntry_TextChanged;
            _IsTextChangedEventOn = true;

            await authorizeTask;

        }
        /// <summary>
        /// Handles the Clicked event of the CancelAuthenticationButton control.
        /// User how dont what to authentication any more.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void CancelAuthenticationButton_Clicked(object sender, EventArgs e)
        {
            Task popTask = Navigation.PopAsync();
            UnsubscribeChangedText();

            await popTask;
        }
        /// <summary>
        /// Handles the Clicked event of the InsertedTokenAuthenticationButton control.
        /// User have a token to insert.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void InsertedTokenAuthenticationButton_Clicked(object sender, EventArgs e)
        {
            Task popTask = Navigation.PopAsync();
            Entry tokenEntry = (Entry)FindByName("TokenEntry");
            //App.FreesoundOAuth2.Token = new Token() { authorization_code = tokenEntry.Text.Trim() };

            //Task saveTask = App.FreesoundOAuth2.SaveAsync();

            await App.FreesoundAPI.ExchangeCodeToAccessToken(tokenEntry.Text.Trim());

            UnsubscribeChangedText();

            await App.FreesoundAPI.SaveAsync();

            await popTask;
        }
        #endregion
    }
}