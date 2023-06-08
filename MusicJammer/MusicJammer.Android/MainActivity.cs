using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Plugin.CurrentActivity;

namespace MusicJammer.Droid
{
    /// <summary>
    /// Defines the <see cref="MainActivity" />.
    /// </summary>
    [Activity(Label = "MusicJammer", Icon = "@mipmap/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        /// <summary>
        /// Gets the Instance.
        /// </summary>
        public static Xamarin.Forms.Platform.Android.FormsAppCompatActivity Instance { get; private set; }

        /// <summary>
        /// The On Create.
        /// </summary>
        /// <param name="savedInstanceState">The savedInstanceState<see cref="Bundle"/>.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetColors();

            Instance = this;
            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        /// <summary>
        /// The On Request Permissions Result.
        /// </summary>
        /// <param name="requestCode">The requestCode<see cref="int"/>.</param>
        /// <param name="permissions">The permissions<see cref="string[]"/>.</param>
        /// <param name="grantResults">The grantResults<see cref="Android.Content.PM.Permission[]"/>.</param>
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        /// <summary>
        /// The Set Colors.
        /// </summary>
        private void SetColors()
        {
            Window.SetStatusBarColor(Android.Graphics.Color.Argb(255, 69, 70, 68));
        }
    }
}
