using Xamarin.Forms;

namespace MusicJammer.Data.Effects
{
    /// <summary>
    /// Defines the <see cref="ShadowEffect" />.
    /// </summary>
    public class ShadowEffect : RoutingEffect
    {
        #region Fields
        /// <summary>
        /// Gets or sets the Radius.
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// Gets or sets the Color.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets the DistanceX.
        /// </summary>
        public float DistanceX { get; set; }

        /// <summary>
        /// Gets or sets the DistanceY.
        /// </summary>
        public float DistanceY { get; set; }
        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="ShadowEffect"/> class.
        /// </summary>
        public ShadowEffect() : base("Wrede.LabelShadowEffect")
        {
        }
    }
}
