using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Smiley.Lib.Enums;

namespace Smiley.Lib.Services
{
    public class SoundManager
    {
        #region Private Variables

        private Music _currentMusic;
        private Music _previousMusic;
        private TimeSpan _previousMusicPosition;
        private int _soundVolumne;
        private int _musicVolume;

        #endregion

        #region SoundManager

        /// <summary>
        /// 
        /// </summary>
        public SoundManager()
        {
            //Music volume starts at what it was when app was closed last
            //setMusicVolume(smh->hge->Ini_GetInt("Options", "musicVolume", 100));
            //setSoundVolume(smh->hge->Ini_GetInt("Options", "soundVolume", 100));

            //abilityChannelActive = false;
            //environmentChannelActive = false;
            //iceChannelActive = false;
        }

        #endregion

        #region Properties

        public string CurrentSongName
        {
            get { return _currentMusic.GetDescription(); }
        }

        /// <summary>
        /// Gets or sets the current sound volume from 0 to 100.
        /// </summary>
        public int SoundVolume
        {
            get { return _soundVolumne; }
            set
            {
                _soundVolumne = Math.Min(100, Math.Max(0, value));
                //smh->hge->System_SetState(HGE_FXVOLUME, soundVolume);
                //smh->hge->Ini_SetInt("Options", "soundVolume", soundVolume);
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
                _musicVolume = Math.Min(100, Math.Max(0, value));
                //        smh->hge->Channel_SetVolume(musicChannel, musicVolume);
                //smh->hge->Ini_SetInt("Options", "musicVolume", musicVolume);
            }
        }

        #endregion

        #region Public Methods

        public void ResetLoopingChannels()
        {
            //stopAbilityChannel();
            //stopIceChannel();
            //stopEnvironmentChannel();
        }

        /**
         * Changes the music channel to play the specified song.
         */
        public void PlayMusic(Music music)
        {

            _previousMusic = _currentMusic;
            _previousMusicPosition = MediaPlayer.PlayPosition;
            _currentMusic = music;

            //smh->hge->Channel_Stop(musicChannel);
            //smh->hge->Music_SetPos(smh->resources->GetMusic(music),0,0);
            //musicChannel = smh->hge->Music_Play(smh->resources->GetMusic(music),true,musicVolume);

        }

        public void PlayLevelMusic(Level level)
        {
            if (level == Level.FOUNTAIN_AREA)
            {
                PlayMusic(Music.Town);
            }
            else if (level == Level.OLDE_TOWNE)
            {
                PlayMusic(Music.OldeTown);
            }
            else if (level == Level.SMOLDER_HOLLOW)
            {
                PlayMusic(Music.SmolderHollow);
            }
            else if (level == Level.FOREST_OF_FUNGORIA)
            {
                PlayMusic(Music.Forest);
            }
            else if (level == Level.SESSARIA_SNOWPLAINS)
            {
                PlayMusic(Music.Ice);
            }
            else if (level == Level.TUTS_TOMB)
            {
                PlayMusic(Music.KingTut);
            }
            else if (level == Level.WORLD_OF_DESPAIR)
            {
                PlayMusic(Music.Despair);
            }
            else if (level == Level.SERPENTINE_PATH)
            {
                PlayMusic(Music.SerpentinePath);
            }
            else if (level == Level.CASTLE_OF_EVIL)
            {
                PlayMusic(Music.CastleOfEvil);
            }
            else if (level == Level.CONSERVATORY)
            {
                PlayMusic(Music.Conservatory);
            }
            else if (level == Level.DEBUG_AREA)
            {
                StopMusic();
            }
        }

        /// <summary>
        /// Stops the music immediately.
        /// </summary>
        public void StopMusic()
        {
            MediaPlayer.Stop();
        }

        /// <summary>
        /// Stops the music by fading out.
        /// </summary>
        public void FadeOutMusic()
        {
            //smh->hge->Channel_SlideTo(musicChannel,3.0f,0,-101,-1.0f);
        }

        /// <summary>
        /// Returns to the last song played at the position where it stopped.
        /// </summary>
        public void playPreviousMusic()
        {
            //smh->hge->Channel_Stop(musicChannel);
            //musicChannel = smh->hge->Music_Play(smh->resources->GetMusic(previousMusic.c_str()),true,musicVolume);
            //smh->hge->Channel_SetPos(musicChannel, previousMusicPosition);
            //currentMusic = previousMusic;
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
            //if (smh->timePassedSince(lastSwitchSoundTime) > SWITCH_SOUND_DELAY) {
            //        bool inRange = abs(gridX - smh->player->gridX) <= 8 && abs(gridY - smh->player->gridY) <= 6;
            //        if (alwaysPlaySound || inRange) {
            //                lastSwitchSoundTime = smh->getGameTime();
            //                playSound("snd_switch");
            //        }
            //}
        }

        public void PlayEnvironmentEffect(Sound sound, bool loop)
        {
            //if (environmentChannelActive) return;
            //if (smh->player->getHealth() <= 0.0) return;

            //environmentChannel = smh->hge->Effect_PlayEx(smh->resources->GetEffect(effect),100,0,1.0f,loop);
            //environmentChannelActive = true;
        }

        public void StopEnvironmentChannel()
        {
            //smh->hge->Channel_Stop(environmentChannel);
            //environmentChannelActive = false;
        }

        public void PlayAbilityEffect(Sound sound, bool loop)
        {
            //if (abilityChannelActive) return;
            //if (smh->player->getHealth() <= 0.0) return;

            //abilityChannel = smh->hge->Effect_PlayEx(smh->resources->GetEffect(effect),100,0,1.0f,loop);
            //abilityChannelActive = true;
        }

        public void StopAbilityChannel()
        {
            //smh->hge->Channel_Stop(abilityChannel);
            //abilityChannelActive = false;
        }

        public void PlayIceEffect(Sound sound, bool loop)
        {
            //if (iceChannelActive) return;
            //if (smh->player->getHealth() <= 0.0) return;

            //iceChannel = smh->hge->Effect_PlayEx(smh->resources->GetEffect(effect),100,0,1.0f,loop);
            //iceChannelActive = true;
        }

        public void StopIceChannel()
        {
            //smh->hge->Channel_Stop(iceChannel);
            //iceChannelActive = false;
        }

        public void PlaySound(Sound sound)
        {
            PlaySound(sound, 0);
        }

        public void PlaySound(Sound sound, float delay)
        {

            //bool play = false;

            //if (delay == 0.0) {
            //        play = true;
            //} else {
            //        bool soundFound = false;
            //        //Search the last played list and find the last time that the sound was played to check if the delay has passed yet
            //        for (std::list<Sound>::iterator i = lastPlayTimes.begin(); i != lastPlayTimes.end(); i++) {
            //                if (strcmp(i->name.c_str(), sound) == 0) {
            //                        soundFound = true;
            //                        if (smh->timePassedSince(i->lastTimePlayed) >= delay) {
            //                                play = true;
            //                                i->lastTimePlayed = smh->getGameTime();
            //                        }
            //                }
            //        }
            //        //Handle the case where the sound hasn't been played yet
            //        if (!soundFound) {
            //                play = true;
            //                Sound newSound;
            //                newSound.name = sound;
            //                newSound.lastTimePlayed = smh->getGameTime();
            //                lastPlayTimes.push_back(newSound);
            //        }
            //}

            //if (play) {
            //        smh->hge->Effect_Play(smh->resources->GetEffect(sound));
            //}
        }

        #endregion
    }
}
