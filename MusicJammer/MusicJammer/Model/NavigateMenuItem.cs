using Xamarin.Forms;

namespace MusicJammer.Model
{

    /// <summary>
    /// Defines the <see cref="NavigateMenuItem" />.
    /// <para>
    /// Used in creating items for navigation bar collection view to display.
    /// </para>
    /// </summary>
    public class NavigateMenuItem
    {
        #region Fields
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the normal image.
        /// </summary>
        public ImageSource NormalImage { get; set; }

        /// <summary>
        /// Gets or sets the selected image.
        /// </summary>
        public ImageSource SelectedImage { get; set; }

        /// <summary>
        /// Gets or sets the normal text color.
        /// </summary>
        public Data.RGB NormalTextColor { get; set; }

        /// <summary>
        /// Gets or sets the selected text color.
        /// </summary>
        public Data.RGB SelectedTextColor { get; set; }

        /// <summary>
        /// Gets or sets the order id.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether if is disable.
        /// </summary>
        public bool IsDisable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether if is selected.
        /// </summary>
        public bool isSelected { get; set; }
        #endregion
        #region Properties
        /// <summary>
        /// Gets the active image.
        /// </summary>
        public ImageSource ActiveImage
        {
            get
            {
                if (isSelected)
                    return this.SelectedImage;
                return NormalImage;
            }
        }

        /// <summary>
        /// Gets the active color.
        /// </summary>
        public Data.RGB ActiveColor
        {
            get
            {
                if (isSelected)
                    return this.SelectedTextColor;
                return NormalTextColor;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigateMenuItem"/> class.
        /// </summary>
        public NavigateMenuItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigateMenuItem"/> class.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="orderId">The orderId<see cref="int"/>.</param>
        /// <param name="normalImage">The normalImage<see cref="ImageSource"/>.</param>
        /// <param name="selectedImage">The selectedImage<see cref="ImageSource"/>.</param>
        /// <param name="normalTextColor">The normalTextColor<see cref="Data.RGB"/>.</param>
        /// <param name="selectedTextColor">The selectedTextColor<see cref="Data.RGB"/>.</param>
        public NavigateMenuItem(string name, int orderId, ImageSource normalImage, ImageSource selectedImage, Data.RGB normalTextColor, Data.RGB selectedTextColor)
        {
            this.Name = name;
            this.OrderId = orderId;
            this.NormalImage = normalImage;
            this.SelectedImage = selectedImage;
            this.NormalTextColor = normalTextColor;
            this.SelectedTextColor = selectedTextColor;
        }
        #endregion
    }
}
