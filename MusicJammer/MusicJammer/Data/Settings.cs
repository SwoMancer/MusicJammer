//DESKTOP - 4098PCL\gwred
//2023 - 01 - 09 15:01:53

using System;
using Xamarin.Forms;
using MusicJammer.PseudoModel;

namespace MusicJammer.Data
{
    /// <summary>
    /// Defines the <see cref="Settings" />.
    /// It is used to determine how the application should look.
    /// </summary>
    public class Settings : ViewModel.ObservableProperty
    {
        #region Fields
        /// <summary>
        /// Gets or sets the main color.
        /// </summary>
        public RGB MainColor { get; set; } = new RGB();

        /// <summary>
        /// Gets or sets the main shade.
        /// </summary>
        public RGB MainShade { get; set; } = new RGB();

        /// <summary>
        /// Gets or sets the highlight color.
        /// </summary>
        public RGB HighlightColor { get; set; } = new RGB();

        /// <summary>
        /// Gets or sets the complimentary highlight color.
        /// </summary>
        public RGB ComplimentaryHighlightColor { get; set; } = new RGB();

        /// <summary>
        /// Gets or sets the title color.
        /// </summary>
        public RGB TitleColor { get; set; } = new RGB();

        /// <summary>
        /// Gets or sets the text color.
        /// </summary>
        public RGB TextColor { get; set; } = new RGB();

        /// <summary>
        /// Defines if is in light mode.
        /// </summary>
        private bool _isInLightMode = true;

        /// <summary>
        /// Defines if it have startede.
        /// </summary>
        private bool _haveStartede = true;

        /// <summary>
        /// Defines the app theme.
        /// </summary>
        public enum AppTheme
        { /// <summary>
          /// Defines the Light.
          /// </summary>
            Light,
            /// <summary>
            /// Defines the Dark.
            /// </summary>
            Dark
        }
        /// <summary>
        /// Defines if is bar visible.
        /// </summary>
        private Boolean _isBarVisible = true;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the if is bar visible.
        /// </summary>
        public Boolean IsBarVisible
        {
            get
            {
                return _isBarVisible;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings()
        {
        }
        #endregion
        #region Methods
        /// <summary>
        /// The deploy navigation bar change.
        /// </summary>
        public void DeployNavigationBarChange()
        {
            UppdateNavbar();
        }

        /// <summary>
        /// The deploy navigate menu change.
        /// </summary>
        public void DeployNavigateMenuChange()
        {
            NavItemComponentsCreate();
        }

        /// <summary>
        /// The update navigate menu change.
        /// </summary>
        public void UpdateNavigateMenuChange()
        {
            NavItemComponentsConfig();
        }

        /// <summary>
        /// The change theme.
        /// </summary>
        /// <param name="theme">The theme<see cref="AppTheme"/>.</param>
        public void ChangeTheme(AppTheme theme)
        {
            switch (theme)
            {
                case AppTheme.Light:
                    SetWhiteStyle();
                    break;
                case AppTheme.Dark:
                    SetDarkStyle();
                    break;
            }
        }

        /// <summary>
        /// The change theme by phone theme.
        /// </summary>
        public void ChangeThemeByPhoneTheme()
        {
            OSAppTheme currentTheme = Application.Current.RequestedTheme;

            if (_haveStartede)
            {
                ThemeChangeImplementation();
                _haveStartede = false;
            }
            else
            {
                switch (currentTheme)
                {
                    case OSAppTheme.Light:
                        if (!_isInLightMode)
                        {
                            _isInLightMode = true;
                            ThemeChangeImplementation();
                        }
                        break;
                    case OSAppTheme.Dark:
                        if (_isInLightMode)
                        {
                            _isInLightMode = false;
                            ThemeChangeImplementation();
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// The theme change implementation.
        /// </summary>
        private void ThemeChangeImplementation()
        {
            OSAppTheme currentTheme = Application.Current.RequestedTheme;

            switch (currentTheme)
            {
                case OSAppTheme.Unspecified:
                    Default();
                    break;
                case OSAppTheme.Light:
                    SetWhiteStyle();
                    break;
                case OSAppTheme.Dark:
                    SetDarkStyle();
                    break;
                default:
                    Default();
                    break;
            }
        }

        /// <summary>
        /// The default.
        /// </summary>
        private void Default()
        {
            SetDarkStyle();
        }

        /// <summary>
        /// The set dark style.
        /// </summary>
        private void SetDarkStyle()
        {
            MainColor = Style.DavysGrey;
            HighlightColor = Style.ArylideYellow;
            ComplimentaryHighlightColor = Style.BarbiePink;
            TitleColor = Style.White;
            TextColor = Style.DavysGrey.AdvancedTint(0.5);
            MainShade = Style.DavysGrey.AdvancedShade(0.125);

        }

        /// <summary>
        /// The set white style.
        /// </summary>
        private void SetWhiteStyle()
        {
            MainColor = RGB.AdvancedShade(Style.White, 0.05);
            HighlightColor = Style.BarbiePink;
            ComplimentaryHighlightColor = Style.ArylideYellow;
            TitleColor = Style.DavysGrey;
            TextColor = Style.DavysGrey.AdvancedTint(0.125);
            MainShade = Style.White;

        }

        /// <summary>
        /// The nav item components config.
        /// </summary>
        private void NavItemComponentsConfig()
        {
            OSAppTheme currentTheme = Application.Current.RequestedTheme;
            switch (currentTheme)
            {
                case OSAppTheme.Unspecified:
                    App.NavigateMenuPM.UppdateItems(TextColor, HighlightColor, NavigateMenuPseudoModel.Icon.Dark);
                    break;
                case OSAppTheme.Light:
                    App.NavigateMenuPM.UppdateItems(TextColor, HighlightColor, NavigateMenuPseudoModel.Icon.Light);
                    break;
                case OSAppTheme.Dark:
                    App.NavigateMenuPM.UppdateItems(TextColor, HighlightColor, NavigateMenuPseudoModel.Icon.Dark);
                    break;
                default:
                    App.NavigateMenuPM.UppdateItems(TextColor, HighlightColor, NavigateMenuPseudoModel.Icon.Dark);
                    break;
            }
        }

        /// <summary>
        /// The nav item components create.
        /// </summary>
        private void NavItemComponentsCreate()
        {
            OSAppTheme currentTheme = Application.Current.RequestedTheme;
            switch (currentTheme)
            {
                case OSAppTheme.Unspecified:
                    App.NavigateMenuPM.CreateItems(TextColor, HighlightColor, NavigateMenuPseudoModel.Icon.Dark);
                    break;
                case OSAppTheme.Light:
                    App.NavigateMenuPM.CreateItems(TextColor, HighlightColor, NavigateMenuPseudoModel.Icon.Light);
                    break;
                case OSAppTheme.Dark:
                    App.NavigateMenuPM.CreateItems(TextColor, HighlightColor, NavigateMenuPseudoModel.Icon.Dark);
                    break;
                default:
                    App.NavigateMenuPM.CreateItems(TextColor, HighlightColor, NavigateMenuPseudoModel.Icon.Dark);
                    break;
            }
        }

        /// <summary>
        /// The toggel bar.
        /// Removes or brings up the Android bar.
        /// </summary>
        public void ToggelBar()
        {
            if (_isBarVisible)
            {
                DependencyService.Get<Data.Interface.IStatusBar>().HideStatusBar();
            }
            else
            {
                DependencyService.Get<Data.Interface.IStatusBar>().ShowStatusBar();
            }
            _isBarVisible = !_isBarVisible;
        }

        /// <summary>
        /// The uppdate navbar.
        /// </summary>
        private void UppdateNavbar()
        {
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = MainShade.Color;
            ((NavigationPage)Application.Current.MainPage).BarTextColor = TitleColor.Color;

            DependencyService.Get<Data.Interface.IBarColor>().StatusBarColor(App.Setting.MainShade.Hex);
            DependencyService.Get<Data.Interface.IBarColor>().NavigationBarColor(App.Setting.MainShade.Hex);
        }
        #endregion
    }
}
