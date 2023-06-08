using Plugin.SimpleAudioPlayer;
using System.Collections.Generic;
using System.IO;

namespace MusicJammer.Data.Sound
{
    /// <summary>
    /// Defines the <see cref="MasterPlayer" />.
    /// <para>Used to play single audio files and streams</para>
    /// </summary>
    public class MasterPlayer
    {
        #region Fields
        /// <summary>
        /// Defines the players.
        /// </summary>
        private List<ISimpleAudioPlayer> _players = new List<ISimpleAudioPlayer>();

        /// <summary>
        /// Defines the alone player.
        /// </summary>
        private ISimpleAudioPlayer _alonePlayer = CrossSimpleAudioPlayer.Current;
        #endregion
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MasterPlayer"/> class.
        /// </summary>
        public MasterPlayer() { }
        #endregion
        #region Methods
        /// <summary>
        /// Defines the <see cref="PlaySingel" />.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        public void PlaySingel(Stream stream)
        {
            if (_alonePlayer.IsPlaying)
                _alonePlayer.Pause();
            _alonePlayer.Load(stream);
            _alonePlayer.Play();
        }

        /// <summary>
        /// Load the streams.
        /// </summary>
        /// <param name="streams">The streams<see cref="List{Stream}"/>.</param>
        public void Load(List<Stream> streams)
        {
            foreach (Stream stream in streams)
            {
                Add(stream);
            }
        }

        /// <summary>
        /// Dispose of all players safely for memory.
        /// </summary>
        public void DisposeAll()
        {
            if (_players.Count <= 0)
                return;

            PauseAll();
            foreach (ISimpleAudioPlayer player in _players)
            {
                player.Dispose();
            }
            _players.Clear();
        }

        /// <summary>
        /// Add a stream.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        public void Add(Stream stream)
        {
            ISimpleAudioPlayer player = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
            player.Load(stream);
            _players.Add(player);
        }

        /// <summary>
        /// The play all the players.
        /// </summary>
        public void PlayAll()
        {
            if (_players.Count <= 0)
                return;

            foreach (ISimpleAudioPlayer player in _players)
            {
                if (!player.IsPlaying)
                    player.Play();
            }
        }

        /// <summary>
        /// Pause all players.
        /// </summary>
        public void PauseAll()
        {
            if (_players.Count <= 0)
                return;

            foreach (ISimpleAudioPlayer player in _players)
            {
                if (player.IsPlaying)
                    player.Pause();
            }
        }
        #endregion
    }
}
