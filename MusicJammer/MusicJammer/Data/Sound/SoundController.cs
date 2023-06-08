//DESKTOP - 4098PCL\gwred
//2023 - 01 - 09 15:01:05

using MusicJammer.ViewModel;
using Plugin.SimpleAudioPlayer;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace MusicJammer.Data.Sound
{
    /// <summary>
    /// Defines the <see cref="SoundController" />.
    /// <para>Containing the information that's needed to play a sound with the <see cref="ISimpleAudioPlayer"/></para>
    /// </summary>
    public class SoundController : ObservableProperty
    {
        #region Fields
        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        private double _volume { get; set; }
        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        private double _balance { get; set; }
        /// <summary>
        /// Defines the player.
        /// </summary>
        private ISimpleAudioPlayer _player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
        /// <summary>
        /// Gets or sets a value indicating whether is in use.
        /// </summary>
        private bool _isInUse { get; set; } = false;
        /// <summary>
        /// Gets or sets a value indicating whether is event on.
        /// </summary>
        private bool _isEventOn { get; set; } = false;
        /// <summary>
        /// Gets or sets the sound.
        /// </summary>
        public SoundBase Sound { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether player is looping.
        /// </summary>
        public bool IsLooping { get; set; } = false;
        #endregion
        #region Properties
        /// <summary>
        /// Gets the sound base.
        /// </summary>
        public SoundBase SoundBase
        {
            get
            {
                return Sound;
            }
        }
        /// <summary>
        /// Gets a value indicating whether player is in use.
        /// </summary>
        public bool IsInUse
        {
            get
            {
                return _isInUse;
            }
        }
        /// <summary>
        /// Gets a value indicating whether loop event is on.
        /// </summary>
        public bool IsEventOn
        {
            get
            {
                return _isEventOn;
            }
        }
        /// <summary>
        /// Gets or sets the Volume. Volume can be between 0 and 1.
        /// </summary>
        public double Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                _volume = value;
                OnPropertyChanged("Volume");
                ConfigVolumeAndBalance();
            }
        }
        /// <summary>
        /// Gets or sets the Volume. Volume can be between 0 and 100.
        /// </summary>
        public int VolumeProcent
        {
            get
            {
                return (int)(_volume * 100);
            }
            set
            {
                if (value > 100)
                {
                    _volume = 1;
                }
                else if (value < 0)
                {
                    _volume = 0;
                }
                else
                {
                    _volume = (double)value / 100;
                }
                OnPropertyChanged("VolumeProcent");
                ConfigVolumeAndBalance();
            }
        }
        /// <summary>
        /// Gets or sets the Balance and is stereo controls.
        /// <para>Balance can be between -1 and 1:
        /// <list type="bullet">
        /// <item>where -1 is left</item>
        /// <item>1 is right</item>
        /// <item>0 is in the middle</item>
        /// </list></para>
        /// </summary>
        public double Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                _balance = value;
                OnPropertyChanged("Balance");
                ConfigVolumeAndBalance();
            }
        }
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SoundController"/> class.
        /// </summary>
        public SoundController()
        {
            _volume = _player.Volume;
            _balance = _player.Balance;
        }
        #endregion
        #region Methods
        /// <summary>
        /// The Configure volume And balance.
        /// </summary>
        public void ConfigVolumeAndBalance()
        {
            _player.Volume = _volume;
            _player.Balance = _balance;
        }

        /// <summary>
        /// The engage the loop and start playing.
        /// </summary>
        public void EngageLoop()
        {
            _player.PlaybackEnded += _player_PlaybackEnded;
            Play();
            _isEventOn = true;
        }

        /// <summary>
        /// Use to play a sound.
        /// </summary>
        public void Play()
        {
            if (!IsEventOn)
                _player.Play();
        }

        /// <summary>
        /// Use to play a sound after delay.
        /// </summary>
        /// <param name="seconds">The seconds<see cref="int"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public Task DelayPlay(int seconds)
        {
            return Task.Run(() =>
            {
                Task.Delay(seconds * 1000).Wait();
                _player.Play();
            });
        }

        /// <summary>
        /// Stop current playing sound and disengage loop event.
        /// </summary>
        public void Stop()
        {
            _player.PlaybackEnded -= _player_PlaybackEnded;
            _isEventOn = false;
            _player.Stop();
        }

        /// <summary>
        /// Insert a sound item that the player can play. It will not work if the player is already in use.
        /// </summary>
        /// <param name="sound">The sound<see cref="SoundBase"/>.</param>
        public void Load(SoundBase sound)
        {
            if (_isInUse)
                return;

            Sound = sound;
            _player.Load(sound.Stream);
        }

        /// <summary>
        /// Pause the current playing sound in the player.
        /// </summary>
        public void Pause()
        {
            _player.Pause();
        }

        /// <summary>
        /// Reclaim memory and unsubscribe from the loop event. Make the player safe for removal and to be on standby.
        /// </summary>
        public void Dispose()
        {
            _player.PlaybackEnded -= _player_PlaybackEnded;
            _isEventOn = false;
            _player.Pause();
            _player.Dispose();
            _isInUse = false;
            _player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            ConfigVolumeAndBalance();
        }

        /// <summary>
        /// The Reset.
        /// </summary>
        public void Reset()
        {
            Pause();
            Stop();
            Reload();
            ConfigVolumeAndBalance();
        }

        /// <summary>
        /// Load already existing data again.
        /// </summary>
        public void Reload()
        {
            if (_isInUse)
                return;

            _player.Load(Sound.Stream);
        }

        /// <summary>
        /// It is the loop event.
        /// </summary>
        /// <param name="sender">The sender <see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void _player_PlaybackEnded(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                if (!Sound.PlayStateOn)
                {
                    if (Sound.WaitPeriod > 0)
                        Thread.Sleep(Sound.WaitPeriod * 1000);

                    if (IsEventOn)
                        _player.Play();
                }
            });
        }
        #endregion
    }
}
