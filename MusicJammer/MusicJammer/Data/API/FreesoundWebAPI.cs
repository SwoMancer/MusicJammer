using MusicJammer.Data.API.FreesoundConverter;
using MusicJammer.Model;
using MusicJammer.ViewModel;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MusicJammer.Data.API
{
    /// <summary>
    /// Defines the <see cref="FreesoundWebAPI" />.
    /// <para>The FreesoundWebAPI sends and receives data from the freesounds.org web API. It handles the token authorization and verification.</para>
    /// </summary>
    public class FreesoundWebAPI : ObservableProperty
    {
        #region Fields
        /// <summary>
        /// CLIENT_SECRET is the API key / client secret 
        /// </summary>
        private static string CLIENT_SECRET = "leoQiU2FAnBza5ETzuKdOvV0UUZMyiKI3ViMe1NT";
        private static string CLIENT_ID = "nswyRfG1NTNK2DUfNmc8";
        private static string FILENAME = "freesound.json";
        public string Authorization_code { get; set; }
        public FreesoundConverter.Token CurrentToken { get; set; }
        public DateTime TokenTime { get; set; }
        #endregion
        #region Properties
        /// <summary>
        /// It is used to see if the Token is valid to be consumed.
        /// </summary>
        public bool IsTokenValid
        {
            get
            {
                if (CurrentToken == null)
                    return false;

                if (TokenTime.Equals(new DateTime()))
                    return false;


                int res = DateTime.Compare(DateTime.Now, TokenTime);

                if (res > 0)
                    return false;

                return true;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="FreesoundWebAPI"/> class.
        /// </summary>
        public FreesoundWebAPI()
        {
            Authorization_code = null;
            CurrentToken = null;
            TokenTime = new DateTime();
        }
        #endregion
        #region Methods
        #region Save, Load and Create files
        /// <summary>
        /// Saves this <see cref="FreesoundWebAPI"/> object to a local file.
        /// </summary>
        public async Task SaveAsync()
        {
            if (IsTokenValid)
            {
                await Task.Run(() => FileHandler.SaveFile(FILENAME, this));
            }
            Console.WriteLine();
        }
        /// <summary>
        /// Load the local <see cref="FreesoundWebAPI"/> file to an object, or if there is no file to load, create a new object instead.
        /// </summary>
        public static async Task<FreesoundWebAPI> LoadOrCreateAsync()
        {
            if (FileHandler.AlreadyExists(FILENAME))
            {
                return await Task.Run(() => FileHandler.LoadFile<FreesoundWebAPI>(FILENAME));
            }
            return new FreesoundWebAPI();
        }
        #endregion
        #region HttpClient, RestSharp and WebAuthenticator
        #region Get
        /// <summary>
        /// Send a get request to the Freesounds.org web API to download a sound by giving a sound id.
        /// </summary>
        /// <param name="soundId"><see cref="int"/></param>
        /// <returns><see cref="SoundItem"/></returns>
        public async Task<SoundItem> DownloadASound(int soundId)
        {
            //URL:
            //https://freesound.org/apiv2/sounds/639486/download/
            string url = "https://freesound.org/apiv2/sounds/" + soundId + "/download/";

            //Create client and authentication bearer...
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", CurrentToken.access_token);

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //It can take a long time so task it...
                    Task<byte[]> readTask = response.Content.ReadAsByteArrayAsync();

                    //Find the name of sound...
                    string[] headDisposition = (string[])response.Content.Headers.GetValues("Content-Disposition");
                    string filename = headDisposition[0];
                    filename = filename.Remove(0, filename.IndexOf("\"") + 1);
                    filename = filename.Remove(filename.IndexOf("\""));

                    //Create sound item...
                    SoundItem soundItem = new SoundItem(filename, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), filename), filename);

                    //await ReadAsByteArrayAsync and save it...
                    byte[] fileBytes = await readTask;
                    await FileHandler.SaveFileAsync(fileBytes, filename);

                    return soundItem;
                }
                return new SoundItem("xxx", "xxx", "xxx");
            }
            catch (Exception)
            {
                return new SoundItem("xxx", "xxx", "xxx");
            }
        }
        /// <summary>
        /// Send a get request to the Freesounds.org web API to get a list of sounds by giving a search query.
        /// </summary>
        /// <param name="query"><see cref="string"/></param>
        /// <returns><see cref="RootFreesoundItem"/></returns>
        public async Task<RootFreesoundItem> GetListOfSoundsAsync(string query)
        {
            //URL:
            //https://freesound.org/apiv2/search/text/?query=piano&token=leoQiU2FAnBza5ETzuKdOvV0UUZMyiKI3ViMe1NT
            //string url = "https://freesound.org/apiv2/search/text/?query=" + query + "&token=" + CLIENT_SECRET + "&filter=type:(mp3 OR wav)";

            //Create url...
            API.Url url = new API.Url();
            url.Start = "https://freesound.org/apiv2/search/text/";
            url.GetParameters.Add("query", query);
            url.GetParameters.Add("token", CLIENT_SECRET);
            url.GetParameters.Add("filter", "type:(mp3 OR wav)");

            //Send get request...
            return await GetRequest<RootFreesoundItem>(await url.ToUrlAsync(), CurrentToken.access_token);
        }
        /// <summary>
        /// Send a general get request to the Freesounds.org web API to get back a <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="access_token"></param>
        /// <returns><typeparamref name="T"/></returns>
        private static async Task<T> GetRequest<T>(string url, string access_token)
        {
            HttpClient client = new HttpClient();
            try
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", access_token);
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<T>(result);

                    return json;
                }
                return default(T);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        /// <summary>
        /// Send a general get request to the Freesounds.org web API to get back a <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns><typeparamref name="T"/></returns>
        private static async Task<T> GetRequest<T>(string url)
        {
            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<T>(result);

                    return json;
                }
                return default(T);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        #endregion
        #region Post
        /// <summary>
        /// Send a post request to the Freesounds.org web API to get back a valid <see cref="Token" />.
        /// </summary>
        private async Task PostRequestRestAsync()
        {
            await Task.Run(() =>
            {
                var client = new RestClient("https://freesound.org/apiv2/oauth2/access_token/");
                //client.Timeout = -1;
                var request = new RestRequest();
                request.Method = Method.Post;
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Cookie", "csrftoken=LKhenvHYv4TsoImQRNAnSJiTnNlbfVRel5Y8a3EVixDRR1Ue9u6vTTGDiBjUUwMc; sessionid=g9yoa9c20k4rhi1aqwc4kgya4q2jqpwc");
                request.AddParameter("client_id", CLIENT_ID);
                request.AddParameter("client_secret", CLIENT_SECRET);
                request.AddParameter("grant_type", "authorization_code");
                request.AddParameter("code", Authorization_code);
                RestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    FreesoundConverter.Token json = JsonConvert.DeserializeObject<FreesoundConverter.Token>(response.Content);

                    CurrentToken = json;
                }
            });
        }
        /// <summary>
        /// Send a post request to the Freesounds.org web API to get back a valid <see cref="Token" />.
        /// <para>You need to use the <see cref="OAuth2Authentication"/> method before to get an Authorization_code that can be exchanged for a token with an access_token.</para>
        /// </summary>
        public async Task ExchangeCodeToAccessToken(string Authorization_code)
        {
            //https://freesound.org/docs/api/authentication.html#token-authentication

            //Create Post data
            this.Authorization_code = Authorization_code;
            FreesoundConverter.ExchangePostCode post = new FreesoundConverter.ExchangePostCode(CLIENT_ID, CLIENT_SECRET, "authorization_code", Authorization_code);


            //Create Url
            //https://freesound.org/apiv2/oauth2/access_token/
            Url url = new Url();
            url.Start = "https://freesound.org/apiv2/oauth2/access_token/";

            //Post
            await PostRequestRestAsync();
            //Token = await PostRequest<FreesoundConverter.Token, FreesoundConverter.ExchangePostCode>(await url.ToUrlAsync(), Authorization_code, post);

            //Token can not be used in 23.999999 hours...
            TokenTime = DateTime.Now;
            TokenTime = TokenTime.AddSeconds(CurrentToken.expires_in - 60);
            OnPropertyChanged(nameof(IsTokenValid));
        }
        #endregion
        #region Web Authenticator
        /// <summary>
        /// Use <see cref="WebAuthenticator"/> to promote a window for the user to collect their temporary Authorization_code.
        /// </summary>
        public static async Task OAuth2Authentication()
        {
            //URL:
            string returnUrl = "musicjam://";
            //string urls = "https://freesound.org/apiv2/oauth2/authorize/?client_id=" + CLIENT_ID + "&response_type=code&state=" + "xyz";
            //state=xyz

            //Create url...
            Url url = new Url();
            url.Start = "https://freesound.org/apiv2/oauth2/authorize/";
            url.GetParameters.Add("client_id", CLIENT_ID);
            url.GetParameters.Add("response_type", "code");
            url.GetParameters.Add("state", "xyz");

            try
            {
                string t = await url.ToUrlAsync();
                Console.WriteLine(t);

                var authResult = await WebAuthenticator.AuthenticateAsync(
                new Uri(await url.ToUrlAsync()),
                new Uri(returnUrl));

                //    musicjam://
                // Callback wont work...
                //https://github.com/MTG/freesound/issues/1186
                //User will have to past it.
            }
            catch (TaskCanceledException)
            {
                //The user canceled the auth window...
                //this will always happen because the freesounds.org web API callback does not work.
                //So the user will have to copy and paste their temporary Authorization_code.
            }
            catch (Exception)
            {

            }
        }
        #endregion
        #endregion










        #endregion
    }
}
