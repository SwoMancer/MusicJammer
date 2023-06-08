using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MusicJammer.View
{
    /// <summary>
    /// Defines the <see cref="NavLayout" />.<br/>
    /// It is the navigation bar.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavLayout : StackLayout
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavLayout"/> class.
        /// </summary>
        public NavLayout()
        {
            InitializeComponent();
        }
    }
}
