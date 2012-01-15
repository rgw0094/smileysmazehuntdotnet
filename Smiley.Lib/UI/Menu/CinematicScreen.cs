using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Data;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;
using Smiley.Lib.Framework.Drawing;
using Microsoft.Xna.Framework.Input;

namespace Smiley.Lib.UI.Menu
{
    public enum SceneState
    {
        ShowPicture,
        ShowText,
        Wait,
        FadeText,
        FadePicture,
        TransitionToCredits
    }

    public class CinematicScreen : BaseMenuScreen
    {
        private const int FinalScene = 6;
        private const float SceneOneMusicLength = 26.57f;
        private const float MaxPictureOffset = -600f;

        private const string SceneOneText = 
@"Our story takes us to a strange and far away land. 
 Here lives Smiley in the peaceful and ethnically 
                       diverse Smiley Town.";
        private const string SceneTwoText = 
@"Smiley enjoys life in this seaside town with the companionship
                              of his beautiful lover.";
        private const string SceneThreeText = 
@"But one fateful day while his lover was out picking
                        flowers, disaster struck...";
        private const string SceneFourText = 
@"The terrible and mighty Lord Fenwar's tyranny befell
                      another unfortunate victim.";
        private const string SceneFiveText = 
@"The dastardly villain and his minions whisked away
        Smiley's lover to his most evil of castles.";
        private const string SceneSixText = 
@"It is now up to Smiley to uncover Fenwar's diabolical plan
   and rescue his lover! Undoubtedly, hours of puzzle- and
                   adventure-based excitement await!";

        #region Private Variables

        private float _backgroundAlpha;
        private int _scene;
        private SceneState _sceneState;
        private float _sceneDuration;
        private float _timeInSceneState;
        private float _pictureOffset;
        private float _textAlpha;
        private float _timeCinematicStarted;
        private bool _musicTransitionedYet;
        private bool _musicFadeoutYet;
        private string _text;
        private bool _inTransition;
        private float _transitionScale;
        private float _timeInTransition;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new CinematicsScreen.
        /// </summary>
        /// <param name="mainMenu"></param>
        public CinematicScreen(MainMenu mainMenu)
            : base(mainMenu)
        {
            _pictureOffset = MaxPictureOffset;
            SMH.Sound.StopMusic();
            _transitionScale = 0.45f;
        }

        #endregion

        #region Public Methods

        public override bool ShouldDrawMouse
        {
            get { return false; }
        }

        public override bool ShouldDrawBackground
        {
            get { return false; }
        }

        public override void Draw()
        {
            if (_backgroundAlpha < 255)
                SMH.Graphics.DrawSprite(Sprites.MenuBackground, 0, 0);
            SMH.Graphics.DrawRect(new Rect(0, 0, 1024, 768), Color.FromNonPremultiplied(0, 0, 0, (int)_backgroundAlpha), true);

            if (_backgroundAlpha < 255f) return;

            if (_inTransition)
            {
                DrawTransition();
                return;
            }

            int x = _scene == FinalScene ? 510 : 512;
            int y = (_scene == FinalScene ? 374 : 284) + (int)_pictureOffset;

            if (_scene == FinalScene)
            {
            }

            //Image
            if (_scene == 1)
            {
                SMH.Graphics.DrawSprite(Sprites.OpenCinematicOne, x, y);
            }
            else if (_scene == 2)
            {
                SMH.Graphics.DrawSprite(Sprites.OpenCinematicTwo, x, y);
            }
            else if (_scene == 3)
            {
                SMH.Graphics.DrawSprite(Sprites.OpenCinematicThree, x, y);
            }
            else if (_scene == 4)
            {
                SMH.Graphics.DrawSprite(Sprites.OpenCinematicFour, x, y);
            }
            else if (_scene == 5)
            {
                SMH.Graphics.DrawSprite(Sprites.OpenCinematicFive, x, y);
            }
            else if (_scene == FinalScene)
            {
                SMH.Graphics.DrawSprite(Sprites.OpenCinematicSix, x, y);
            }

            //Text
            SMH.Graphics.DrawString(SmileyFont.Cinematic, _text, 512, 480, TextAlignment.Center, Color.FromNonPremultiplied(255, 255, 255, (int)_textAlpha));
        }

        public override void Update(float dt)
        {
            if (_inTransition)
            {
                UpdateTransition(dt);
            }

            if (SMH.Input.IsDown(Keys.F2))
            {
                EnterScene(FinalScene);
                return;
            }

            _timeInSceneState += dt;

            //Fade in before doing anything
            if (_backgroundAlpha < 255f)
            {
                _backgroundAlpha = Math.Min(255f, _backgroundAlpha + 255f * dt);
                if (_backgroundAlpha == 255f)
                {
                    EnterScene(1);
                    _timeCinematicStarted = SMH.Now;
                }
                else
                {
                    return;
                }
            }

            if (SMH.Now - _timeCinematicStarted > SceneOneMusicLength && !_musicTransitionedYet)
            {
                //Music transition
                SMH.Sound.StopMusic();
                _musicTransitionedYet = true;
            }

            if (!_musicFadeoutYet && _scene == FinalScene && _sceneState == SceneState.Wait && _timeInSceneState > 3.5)
            {
                //Start fading out the music near the end of the final _scene
                SMH.Sound.FadeOutMusic();
                _musicFadeoutYet = true;
            }

            if (_sceneState == SceneState.ShowPicture)
            {
                _pictureOffset += 300f * dt;
                if (_pictureOffset >= 0)
                {
                    _pictureOffset = 0f;
                    EnterSceneState(SceneState.ShowText);
                }
            }
            else if (_sceneState == SceneState.ShowText)
            {
                _textAlpha += 320 * dt;
                if (_textAlpha >= 255f)
                {
                    _textAlpha = 255f;
                    EnterSceneState(SceneState.Wait);
                }
            }
            else if (_sceneState == SceneState.Wait)
            {
                if (_timeInSceneState > _sceneDuration)
                {
                    EnterSceneState(SceneState.FadeText);
                }
            }
            else if (_sceneState == SceneState.FadeText)
            {
                _textAlpha -= 320 * dt;
                if (_textAlpha <= 0f)
                {
                    _textAlpha = 0f;
                    EnterSceneState(SceneState.FadePicture);
                }
            }
            else if (_sceneState == SceneState.FadePicture)
            {
                _pictureOffset -= 300f * dt;
                if (_pictureOffset <= MaxPictureOffset)
                {
                    _pictureOffset = MaxPictureOffset;
                    EnterScene(_scene + 1);
                }
            }

            if (SMH.Input.IsPressed(Keys.Enter))
            {
                //TODO:smh->resources->Purge(ResourceGroups::Cinematic);	
                SMH.StartGame(true);
            }
        }

        #endregion

        #region Private Methods

        private void EnterScene(int newScene)
        {
            _scene = newScene;

            if (_scene == 1)
            {
                _text = SceneOneText;
                _sceneDuration = 3.4f;
                SMH.Sound.PlayMusic(Music.OpeningCinematic);
            }
            else if (_scene == 2)
            {
                _text = SceneTwoText;
                _sceneDuration = 3.4f;
            }
            else if (_scene == 3)
            {
                _text = SceneThreeText;
                _sceneDuration = 3.4f;
            }
            else if (_scene == 4)
            {
                _text = SceneFourText;
                _sceneDuration = 3.4f;
                SMH.Sound.PlayMusic(Music.FenwarLietMotif);
            }
            else if (_scene == 5)
            {
                _text = SceneFiveText;
                _sceneDuration = 3.4f;
            }
            else if (_scene == 6)
            {
                _sceneDuration = 5f;
                _text = SceneSixText;
            }

            EnterSceneState(SceneState.ShowPicture);
        }

        /// <summary>
        /// Enters a new _scene state.
        /// </summary>
        /// <param name="newState"></param>
        private void EnterSceneState(SceneState newState)
        {
            _sceneState = newState;
            _timeInSceneState = 0f;

            if (_scene == FinalScene && _sceneState == SceneState.FadePicture)
            {
                StartTransition();
            }
        }

        private void StartTransition()
        {
            _inTransition = true;
            _timeInTransition = 0f;
        }

        private void DrawTransition()
        {
            SMH.Environment.Draw();
            SMH.Player.Draw();
            SMH.GUI.Draw();

            SMH.Graphics.DrawRect(new Rect(0, 0, 1024, 384f - 198f * _transitionScale), Color.Black, true);
            SMH.Graphics.DrawRect(new Rect(0, 0, 512f - 198f * _transitionScale, 768), Color.Black, true);
            SMH.Graphics.DrawRect(new Rect(512f + 198f * _transitionScale, 0, 1024, 768), Color.Black, true);
            SMH.Graphics.DrawRect(new Rect(0, 384f + 198f * _transitionScale, 1024, 768), Color.Black, true);
            SMH.Graphics.DrawSprite(Sprites.Loading, 512, 384f, Color.White, 0f, _transitionScale);
        }

        private void UpdateTransition(float dt)
        {
            _timeInTransition += dt;
            _transitionScale += 2f * dt;

            if (_timeInTransition > 1.5f)
            {
                SMH.Sound.PlayMusic(Music.Town);
                SMH.StartGame(true);
                //TODO:smh->resources->Purge(ResourceGroups::Cinematic);	
            }
        }

        #endregion
    }
}
