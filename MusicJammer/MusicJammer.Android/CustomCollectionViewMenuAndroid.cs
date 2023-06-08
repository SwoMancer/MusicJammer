using MusicJammer.Data;
using MusicJammer.Droid;
using Xamarin.Forms;
using Android.Content;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomCollectionViewMenu), typeof(CustomCollectionViewMenuAndroid))]
namespace MusicJammer.Droid
{
    using Android.Content;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;

    /// <summary>
    /// Defines the <see cref="CustomCollectionViewMenuAndroid" />.
    /// </summary>
    public class CustomCollectionViewMenuAndroid : CollectionViewRenderer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomCollectionViewMenuAndroid"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="Context"/>.</param>
        public CustomCollectionViewMenuAndroid(Context context) : base(context)
        {
        }

        /// <summary>
        /// The On Element Changed.
        /// </summary>
        /// <param name="elementChangedEvent">The elementChangedEvent<see cref="ElementChangedEventArgs{ItemsView}"/>.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<ItemsView> elementChangedEvent)
        {
            base.OnElementChanged(elementChangedEvent);

            if (elementChangedEvent.NewElement != null)
            {

            }
        }
    }
}
