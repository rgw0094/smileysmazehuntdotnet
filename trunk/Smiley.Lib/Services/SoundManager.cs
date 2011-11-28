using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework.Content;

namespace Smiley.Lib.Services
{
    /// <summary>
    /// Manages the playing of all sounds and music.
    /// </summary>
    public class SoundManager
    {
        private const float SwitchSoundDelay = 0.5f;

        #region Private Variables

        private SoundEffectInstance _currentMusic;
        private SoundEffectInstance _previousMusic;
        private Dictionary<Sound, SoundEffectInstance> _loopedSounds = new Dictionary<Sound, SoundEffectInstance>();
        private Dictionary<Sound, float> _lastPlayedTimes = new Dictionary<Sound, float>();
        private ContentManager _contentManager;
        private float _lastSwitchTime;
        private int _soundVolumne;
        private int _musicVolume;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a new SoundManager.
        /// </summary>
        public SoundManager(ContentManager contentManager)
        {
            _contentManager = contentManager;

            //Music volume starts at what it was when app was closed last
            //TODO: load from config
            MusicVolume = 100;
            SoundVolume = 100;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current sound volume from 0 to 100.
        /// </summary>
        public int SoundVolume
        {
            get { return _soundVolumne; }
            set
            {
                if (_soundVolumne != value)
                {
                    _soundVolumne = Math.Min(100, Math.Max(0, value));
                    //TODO: update in config;
                }
            }
        }

        /// <summary>
        /// Gets or sets the current music volume from 0 to 100.
        /// </summary>
        public int MusicVolume
        {
            get { return _musicVolume; }
            set
            {
                if (_musicVolume != value)
                {
                    _musicVolume = Math.Min(100, Math.Max(0, value));
                    if (_currentMusic != null)
                    {
                        _currentMusic.Volume = value;
                    }
                    //TODO: update in config;
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Changes the music channel to play the specified song.
        /// </summary>
        /// <param name="music"></param>
        public void PlayMusic(Music music)
        {
            if (_previousMusic != null)
                _previousMusic.Dispose();

            if (_currentMusic != null)
                _currentMusic.Pause();

            _previousMusic = _currentMusic;
            _currentMusic = _contentManager.Load<SoundEffect>(music.GetDescription()).CreateInstance();
            _currentMusic.IsLooped = true;
            _currentMusic.Volume = (float)MusicVolume / 100f;
            _currentMusic.Play();
        }

        /// <summary>
        /// Plays the music for the given level.
        /// </summary>
        /// <param name="level"></param>
        public void PlayLevelMusic(Level level)
        {
            if (level == Level.FOUNTAIN_AREA)
                PlayMusic(Music.Town);
            else if (level == Level.OLDE_TOWNE)
                PlayMusic(Music.OldeTown);
            else if (level == Level.SMOLDER_HOLLOW)
                PlayMusic(Music.SmolderHollow);
            else if (level == Level.FOREST_OF_FUNGORIA)
                PlayMusic(Music.Forest);
            else if (level == Level.SESSARIA_SNOWPLAINS)
                PlayMusic(Music.Ice);
            else if (level == Level.TUTS_TOMB)
                PlayMusic(Music.KingTut);
            else if (level == Level.WORLD_OF_DESPAIR)
                PlayMusic(Music.Despair);
            else if (level == Level.SERPENTINE_PATH)
                PlayMusic(Music.SerpentinePath);
            else if (level == Level.CASTLE_OF_EVIL)
                PlayMusic(Music.CastleOfEvil);
            else if (level == Level.CONSERVATORY)
                PlayMusic(Music.Conservatory);
            else if (level == Level.DEBUG_AREA)
                StopMusic();
        }

        /// <summary>
        /// Stops the music immediately.
        /// </summary>
        public void StopMusic()
        {
            _currentMusic.Stop();
            _currentMusic.Dispose();
            _currentMusic = null;
        }

        /// <summary>
        /// Stops the music by fading out.
        /// </summary>
        public void FadeOutMusic()
        {
            //TODO:
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns to the last song played at the position where it stopped.
        /// </summary>
        public void ResumePreviousMusic()
        {
            if (_previousMusic == null) throw new Exception("ResumePreviousMusic called when there is no previous song");

            StopMusic();
            _currentMusic = _previousMusic;
            _currentMusic.Resume();
        }

        /// <summary>
        /// Plays the switch sound effect originating from point (gridX, gridY). The sound will only be played if
        /// alwaysPlaySound is true or the sound is originating from a point close to Smiley. 
        /// This method can be called as much as you want but it will only play a sound at maximum every SWITCH_SOUND_DELAY
        /// seconds to avoid it sounding like the dickens if a ton of switches are toggled at once.
        /// </summary>
        /// <param name="gridX"></param>
        /// <param name="gridY"></param>
        /// <param name="alwaysPlaySound"></param>
        public void PlaySwitchSound(int gridX, int gridY, bool alwaysPlaySound)
        {
            if (SMH.TimePassed(_lastSwitchTime, SwitchSoundDelay))
            {
                //TODO:
                bool inRange = true;// abs(gridX - smh->player->gridX) <= 8 && abs(gridY - smh->player->gridY) <= 6;
                if (alwaysPlaySound || inRange)
                {
                    _lastSwitchTime= SMH.Now;
                    PlaySound(Sound.Switch);
                }
            }
        }

        /// <summary>
        /// Stops all currently playing sounds;
        /// </summary>
        public void StopAllLoopedSounds()
        {
            foreach (KeyValuePair<Sound, SoundEffectInstance> kvp in _loopedSounds)
            {
                kvp.Value.Stop();
                kvp.Value.Dispose();
            }
            _loopedSounds.Clear();
        }

        /// <summary>
        /// Starts looping the given sound. The sound will continue to loop until StopLoopingSound
        /// is called. The same sound cannot be looped more than once.
        /// </summary>
        /// <param name="sound"></param>
        public void StartLoopingSound(Sound sound)
        {
            if (_loopedSounds.ContainsKey(sound))
                throw new Exception(sound.ToString() + " is already looping");

            SoundEffectInstance instance = _contentManager.Load<SoundEffect>(sound.GetDescription()).CreateInstance();
            instance.IsLooped = true;
            instance.Volume = SoundVolume;
            instance.Play();

            _loopedSounds[sound] = instance;
        }

        /// <summary>
        /// Stops looping a sound.
        /// </summary>
        /// <param name="sound"></param>
        public void StopLoopingSound(Sound sound)
        {
            SoundEffectInstance instance = null;
            if (!_loopedSounds.TryGetValue(sound, out instance))
            {
                throw new Exception(sound.ToString() + " is not looping");
            }

            instance.Stop();
            instance.Dispose();
            _loopedSounds.Remove(sound);
        }

        /// <summary>
        /// Plays a sound effect.
        /// </summary>
        /// <param name="sound"></param>
        public void PlaySound(Sound sound)
        {
            PlaySound(sound, 0);
        }

        /// <summary>
        /// Plays a sound unless the same sound was already played in the last specified amount of time.
        /// </summary>
        /// <param name="sound"></param>
        /// <param name="delay"></param>
        public void PlaySound(Sound sound, float delay)
        {
            float lastPlayed;
            bool playSound = true;
            if (_lastPlayedTimes.TryGetValue(sound, out lastPlayed))
            {
                playSound = SMH.TimePassed(lastPlayed, delay);
            }
            else
            {
                _lastPlayedTimes[sound] = SMH.Now;
            }

            if (playSound)
            {
                SoundEffect sfx = _contentManager.Load<SoundEffect>(sound.GetDescription());
                sfx.Play((float)SoundVolume / 100f, 0, 0);
            }
        }

        #endregion
    }
}
