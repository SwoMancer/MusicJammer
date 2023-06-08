using MusicJammer.Data;
using MusicJammer.Data.API;
using MusicJammer.Model;
using MusicJammer.Model.MultiBinding;
using MusicJammer.PseudoModel;
using MusicJammer.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace MusicJammer.ViewModel
{
    /// <summary>
    /// Defines the <see cref="LibraryViewModel" />.<br/>
    /// View model for <see cref="LibraryPage"/>
    /// </summary>
    public class LibraryViewModel : ViewModelBase
    {
        #region Fields
        private readonly Data.Interface.IMessageService _MessageService;
        private INavigation _navigation;
        private ObservableCollection<FreesoundItem> _Freesounds = new ObservableCollection<FreesoundItem>();
        private bool _IsLocalSoundsVisible = true;
        private StackLayout _LoaderDisplayStackLayout;
        public TemporarilysSoundPseudoModel temporarilysVM { get; set; }
        public TabPseudoModel tabVM { get; set; }
        #endregion
        #region Properties
        public INavigation SetNavigation
        {
            set
            {
                _navigation = value;
            }
        }
        public ObservableCollection<FreesoundItem> FreesoundItems
        {
            get
            {
                return _Freesounds;
            }
            set
            {
                _Freesounds = value;
                OnPropertyChanged(nameof(FreesoundItems));
            }
        }
        public StackLayout LoaderDisplayStackLayout 
        {
            set
            {
                _LoaderDisplayStackLayout = value;
            }
        }

        public bool IsTokenNotValid
        {
            get { return !App.FreesoundAPI.IsTokenValid; }
        }

        public bool IsFreesoundsVisible
        {
            get
            {
                return !_IsLocalSoundsVisible;
            }
            set
            {
                _IsLocalSoundsVisible = !value;
                HeaderPropertyChanged();

            }
        }
        public bool IsLocalSoundsVisible
        {
            get
            {
                return _IsLocalSoundsVisible;
            }
            set
            {
                _IsLocalSoundsVisible = value;
                HeaderPropertyChanged();
            }
        }
        public RGB ColorButtonLocalSounds
        {
            get
            {
                switch (IsLocalSoundsVisible)
                {
                    case true:
                        return Data.Style.BarbiePink;
                    case false:
                        return App.Setting.MainShade;
                }
                return App.Setting.MainShade;
            }
        }
        public RGB ColorButtonFreesounds
        {
            get
            {
                switch (!IsLocalSoundsVisible)
                {
                    case true:
                        return Data.Style.BarbiePink;
                    case false:
                        return App.Setting.MainShade;
                }
                return App.Setting.MainShade;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryViewModel"/> class.
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="navigateMenuViewModel"></param>
        /// <param name="temporarilysVM"></param>
        /// <param name="tabVM"></param>
        public LibraryViewModel(Settings settings, NavigateMenuPseudoModel navigateMenuViewModel, TemporarilysSoundPseudoModel temporarilysVM, TabPseudoModel tabVM) : base(settings, navigateMenuViewModel)
        {
            _MessageService = DependencyService.Get<Data.Interface.IMessageService>();
            InitializePseudoModels();
            InitializeCommands();
        }
        #endregion
        #region Commands
        /// <summary>
        /// Initialize commands into <see cref="ViewModelBase.Commands"/>
        /// </summary>
        private void InitializeCommands()
        {
            Commands.Add("ButtonLocalSounds", new Command(ButtonLocalSoundsCommand));
            Commands.Add("ButtonFreesounds", new Command(ButtonFreesoundsCommand));
            Commands.Add("ButtonAPIFreesounds", new Command(ButtonListFreesoundsCommand));
            Commands.Add("ButtonDownloadFreesounds", new Command(ButtonDownloadFreesoundsCommand));
            Commands.Add("ButtonLogin", new Command(ButtonLoginCommand));
        }
        /// <summary>
        /// If the login button is click.
        /// </summary>
        /// <param name="obj"></param>
        private async void ButtonLoginCommand(object obj)
        {
            await PopupAuthentication();
        }
        /// <summary>
        /// If the Freesounds tab button is click.
        /// </summary>
        /// <param name="obj"></param>
        private void ButtonFreesoundsCommand(object obj)
        {
            IsFreesoundsVisible = true;
        }
        /// <summary>
        /// If the loacl sounds tab button is click.
        /// </summary>
        /// <param name="obj"></param>
        private void ButtonLocalSoundsCommand(object obj)
        {
            IsLocalSoundsVisible = true;
        }
        /// <summary>
        /// If the download sound from freesounds button is click.
        /// </summary>
        /// <param name="obj"></param>
        private async void ButtonDownloadFreesoundsCommand(object obj)
        {
            TabNameFreesoundItem tabNameFreesoundItem = obj as TabNameFreesoundItem;
            

            switch (App.FreesoundAPI.IsTokenValid)
            {
                case true:
                    _LoaderDisplayStackLayout.IsVisible = true;
                    SoundItem soundItem = await App.FreesoundAPI.DownloadASound(tabNameFreesoundItem.FreesoundItem.id);

                    if (tabNameFreesoundItem.Name == "xxx" || soundItem.Name == "xxx")
                        return;

                    soundItem.WaitPeriod = 0;
                    soundItem.ActiveStateOn = true;
                    soundItem.PlayStateOn = true;


                     App.TabVM.AddSound(App.TabVM.FindTabItemByName(tabNameFreesoundItem.Name), soundItem);
                    _LoaderDisplayStackLayout.IsVisible = false;
                    break;
                default:
                    await PopupAuthentication();
                    break;
            }

        }
        /// <summary>
        /// If the list freesounds button is click.
        /// Get a list from web API.
        /// </summary>
        /// <param name="obj"></param>
        private async void ButtonListFreesoundsCommand(object obj)
        {
            string query = (string)obj;

            if (string.IsNullOrEmpty(query))
            {
                await _MessageService.ShowAsync("Input error", "You need to input a search query and can not leave the entry empty.");
            }
            else
            {
                if (App.FreesoundAPI.IsTokenValid)
                {
                    _LoaderDisplayStackLayout.IsVisible = true;
                    ObservableCollection<FreesoundItem> items = new ObservableCollection<FreesoundItem>();
                    RootFreesoundItem root = await App.FreesoundAPI.GetListOfSoundsAsync(query);
                    foreach (FreesoundItem item in root.results)
                    {
                        items.Add(item);
                    }

                    FreesoundItems = items;
                    _LoaderDisplayStackLayout.IsVisible = false;
                }
                else { await PopupAuthentication(); }
            }
        }
        #endregion
        #region Methods
        /// <summary>
        /// Update the property so the view changes according to tabs.
        /// </summary>
        private void HeaderPropertyChanged()
        {
            //Dirty but usefull...
            OnPropertyChanged(nameof(IsFreesoundsVisible));
            OnPropertyChanged(nameof(IsLocalSoundsVisible));
            OnPropertyChanged(nameof(ColorButtonLocalSounds));
            OnPropertyChanged(nameof(ColorButtonFreesounds));
        }
        /// <summary>
        /// Initialize Pseudo Models
        /// </summary>
        private void InitializePseudoModels()
        {
            this.temporarilysVM = temporarilysVM;
            this.tabVM = tabVM;
        }
        /// <summary>
        /// Show authentication process.
        /// </summary>
        /// <returns></returns>
        private async Task PopupAuthentication()
        {
            await _navigation.PushAsync(new OAuth2Page(), false);
        }

        public override void Activation()
        {
            base.Activation();
            temporarilysVM = App.TemporarilySoundVM;
            tabVM = App.TabVM;
        }
        #endregion
    }
}
