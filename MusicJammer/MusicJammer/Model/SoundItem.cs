using System;

namespace MusicJammer.Model
{
    /// <summary>
    /// Defines the <see cref="SoundItem" />.<br />
    /// It is an item to be used in displaying sounds that can be played.
    /// </summary>
    public class SoundItem : Data.Sound.SoundBase
    {
        #region Fields
        /// <summary>
        /// Defines the random.
        /// </summary>
        private Random random = new Random();
        /// <summary>
        /// Defines the if is visible.
        /// </summary>
        private bool _isVisible = false;
        /// <summary>
        /// Gets or sets the create date time.
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether it is unique.
        /// </summary>
        public bool IsUnique { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether it is visible change.
        /// </summary>
        public bool IsVisibleChange { get; set; }
        /// <summary>
        /// Gets or sets the normal color.
        /// </summary>
        public string NormalColor { get; set; }
        /// <summary>
        /// Gets or sets the selected color.
        /// </summary>
        public string SelectedColor { get; set; }
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether it is visible.
        /// </summary>
        public bool IsVisible
        {
            get
            {
                IsVisibleChange = true;
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }
        /// <summary>
        /// Gets the background color.
        /// </summary>
        public string BackgroundColor
        {
            get
            {
                if (IsUnique)
                    return NormalColor;
                return SelectedColor;
            }
        }
        /// <summary>
        /// Gets the easy to read name.
        /// </summary>
        public string EasyReadName
        {
            get
            {
                return Name.Replace('-', ' ').Substring(0, Name.IndexOf('.')).Trim();
            }
        }

        /// <summary>
        /// Gets the its type of file.
        /// </summary>
        public string Type
        {
            get
            {
                return Name.Substring(Name.IndexOf('.') + 1).Trim();
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SoundItem"/> class.
        /// </summary>
        /// <param name="name">The display name<see cref="string"/>.</param>
        /// <param name="path">The file path<see cref="string"/>.</param>
        /// <param name="fileName">The file Name<see cref="string"/>.</param>
        public SoundItem(string name, string path, string fileName)
        {
            this.Name = name;
            this.Path = path;
            this.CreateDateTime = DateTime.Now;
            this.IsUnique = true;
            this.IsVisible = false;
            this.FileName = fileName;
            this.Unique = name + path + CreateDateTime.ToString();
            this.ActiveStateOn = true;
            this.PlayStateOn = true;
            this.WaitPeriod = 0;
            this.Id = random.Next(1, 10000).ToString() + DateTime.Now.ToString();
        }
        #endregion
    }
}
