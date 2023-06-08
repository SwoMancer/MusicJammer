namespace MusicJammer.Data.Sound
{
    using MusicJammer.ViewModel;
    using Newtonsoft.Json;
    using System.ComponentModel;
    using System.IO;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Defines the <see cref="SoundBase" />.
    /// <para>Containing the information that's needed to display a sound</para>
    /// </summary>
    public class SoundBase : ObservableProperty
    {
        #region Fields
        /// <summary>
        /// Defines the wait period.
        /// </summary>
        private int _waitPeriod = 0;
        /// <summary>
        /// Defines the play state and active state.
        /// </summary>
        private bool _playState = true, _activeState = true;
        /// <summary>
        /// Gets or sets the Sound Stream.
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Gets or sets the Unique.
        /// </summary>
        public string Unique { get; set; }
        #endregion
        #region Properties

        /// <summary>
        /// Gets or sets the Wait Period between plays.
        /// </summary>
        public int WaitPeriod
        {
            get
            {
                return _waitPeriod;
            }
            set
            {
                _waitPeriod = value;
                OnPropertyChanged(nameof(WaitPeriod));
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether play state is on.
        /// </summary>
        public bool PlayStateOn
        {
            get
            {
                return _playState;
            }
            set
            {
                _playState = value;
                OnPropertyChanged(nameof(_playState));
                OnPropertyChanged("PlayStateOn");
                OnPropertyChanged("PlayStateOff");
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether play state is off.
        /// </summary>
        public bool PlayStateOff
        {
            get
            {
                return !_playState;
            }
            set
            {
                _playState = !value;
                OnPropertyChanged(nameof(_playState));
                OnPropertyChanged("PlayStateOff");
                OnPropertyChanged("PlayStateOn");
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether active state is on.
        /// </summary>
        public bool ActiveStateOn
        {
            get
            {
                return _activeState;
            }
            set
            {
                _activeState = value;
                OnPropertyChanged(nameof(_activeState));
                OnPropertyChanged("ActiveStateOn");
                OnPropertyChanged("ActiveStateOff");

            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether active state is off.
        /// </summary>
        public bool ActiveStateOff
        {
            get
            {
                return !_activeState;
            }
            set
            {
                _activeState = !value;
                OnPropertyChanged(nameof(_activeState));
                OnPropertyChanged("ActiveStateOff");
                OnPropertyChanged("ActiveStateOn");

            }
        }
        /// <summary>
        /// Gets the Stream.
        /// </summary>
        [JsonIgnore]
        public Stream Stream
        {
            get
            {
                byte[] data = FileHandler.LoadFile(FileName);
                MemoryStream stream = new MemoryStream(data);
                return stream;
            }
        }
        /// <summary>
        /// Gets the SoundBase.
        /// </summary>
        [JsonIgnore]
        public SoundBase GetSoundBase
        {
            get
            {
                return this;
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SoundBase"/> class.
        /// </summary>
        public SoundBase()
        {
            _waitPeriod = 0;
            _playState = true;
            _activeState = true;
        }
        #endregion
        #region Methods
        /// <summary>
        /// Ask if a Sound Base is Equal?
        /// </summary>
        /// <param name="sound">The sound<see cref="SoundBase"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsSoundBaseEqual(SoundBase sound)
        {
            return this.Unique == sound.Unique;
        }
        #endregion
    }
}
