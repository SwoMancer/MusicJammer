using System;
using System.Globalization;
using Xamarin.Forms;

namespace MusicJammer.Data
{
    /// <summary>
    /// Defines the <see cref="RGB" />.
    /// <para>Used to handle the colores used for the themes.</para>
    /// </summary>
    public class RGB
    {
        #region Fields
        /// <summary>
        /// Gets or sets the red.
        /// </summary>
        public int Red { get; set; }
        /// <summary>
        /// Gets or sets the green.
        /// </summary>
        public int Green { get; set; }
        /// <summary>
        /// Gets or sets the blue.
        /// </summary>
        public int Blue { get; set; }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the hex.
        /// </summary>
        public string Hex
        {
            get
            {
                return "#" + Red.ToString("X2") + Green.ToString("X2") + Blue.ToString("X2");
            }
            set
            {
                ToHex(value);
            }
        }
        /// <summary>
        /// Gets the color.
        /// </summary>
        public Color Color
        {
            get
            {
                return Color.FromHex(this.Hex);
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RGB"/> class.
        /// </summary>
        public RGB() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="RGB"/> class.
        /// </summary>
        /// <param name="red">The red<see cref="int"/>.</param>
        /// <param name="green">The green<see cref="int"/>.</param>
        /// <param name="blue">The blue<see cref="int"/>.</param>
        public RGB(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RGB"/> class.
        /// </summary>
        /// <param name="hex">The hex<see cref="string"/>.</param>
        public RGB(string hex)
        {
            ToHex(hex);
        }
        #endregion
        #region Methods
        /// <summary>
        /// The advanced shade.
        /// </summary>
        /// <param name="percent">The percent<see cref="double"/>.</param>
        /// <returns>The <see cref="RGB"/>.</returns>
        public RGB AdvancedShade(double percent)
        {
            RGB rgb = RGB.AdvancedShade(this, percent);
            return rgb;
        }
        /// <summary>
        /// The advanced tint.
        /// </summary>
        /// <param name="percent">The percent<see cref="double"/>.</param>
        /// <returns>The <see cref="RGB"/>.</returns>
        public RGB AdvancedTint(double percent)
        {
            RGB rgb = RGB.AdvancedTint(this, percent);
            return rgb;
        }
        /// <summary>
        /// The advanced shade.
        /// </summary>
        /// <param name="rgb">The rgb<see cref="RGB"/>.</param>
        /// <param name="percent">The percent<see cref="double"/>.</param>
        /// <returns>The <see cref="RGB"/>.</returns>
        public static RGB AdvancedShade(RGB rgb, double percent)
        {
            RGB nrgb = new RGB();

            nrgb.Red = AdvancedShadeOneColor(rgb.Red, percent);
            nrgb.Green = AdvancedShadeOneColor(rgb.Green, percent);
            nrgb.Blue = AdvancedShadeOneColor(rgb.Blue, percent);

            return nrgb;
        }
        /// <summary>
        /// The advanced tint.
        /// </summary>
        /// <param name="rgb">The rgb<see cref="RGB"/>.</param>
        /// <param name="percent">The percent<see cref="double"/>.</param>
        /// <returns>The <see cref="RGB"/>.</returns>
        public static RGB AdvancedTint(RGB rgb, double percent)
        {
            RGB nrgb = new RGB();

            nrgb.Red = AdvancedTintOneColor(rgb.Red, percent);
            nrgb.Green = AdvancedTintOneColor(rgb.Green, percent);
            nrgb.Blue = AdvancedTintOneColor(rgb.Blue, percent);

            return nrgb;
        }
        /// <summary>
        /// The advanced shade one color.
        /// </summary>
        /// <param name="color">The color<see cref="int"/>.</param>
        /// <param name="percent">The percent<see cref="double"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int AdvancedShadeOneColor(int color, double percent)
        {
            double q = color;
            double w = percent * 100.0;

            return (int)Math.Round((double)(q - (q / 100 * w)));
        }
        /// <summary>
        /// The advanced tint one color.
        /// </summary>
        /// <param name="color">The color<see cref="int"/>.</param>
        /// <param name="percent">The percent<see cref="double"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int AdvancedTintOneColor(int color, double percent)
        {
            double q = color;
            double w = percent * 100.0;

            return (int)Math.Round((double)((255.0 - q) / 100.0 * w + q));
        }
        /// <summary>
        /// To hex.
        /// </summary>
        /// <param name="hex">The hex<see cref="string"/>.</param>
        private void ToHex(string hex)
        {
            if (hex.IndexOf('#') != -1)
                hex = hex.Replace("#", "");

            Red = int.Parse(hex.Substring(0, 2), NumberStyles.AllowHexSpecifier);
            Green = int.Parse(hex.Substring(2, 2), NumberStyles.AllowHexSpecifier);
            Blue = int.Parse(hex.Substring(4, 2), NumberStyles.AllowHexSpecifier);
        }
        /// <summary>
        /// The color range.
        /// </summary>
        /// <param name="range">The range<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private int ColorRange(int range)
        {
            if (range > 255)
            {
                return 255;
            }
            else if (range < 0)
            {
                return 0;
            }
            else
            {
                return range;
            }
        }
        #endregion
    }
}
