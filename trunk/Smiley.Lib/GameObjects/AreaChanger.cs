using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;
using Smiley.Lib.Data;

namespace Smiley.Lib.GameObjects.Environment
{
    public enum AreaChangeState
    {
        In,
        Out,
        Inactive
    }

    public class AreaChanger
    {
        #region Private Variables

        private AreaChangeState _state;
        private int _destinationX;
        private int _destinationY;
        private Level _destinationLevel;
        private bool _doneZoomingIn;
        private float _timeLevelLoaded;
        private float _zoneTextAlpha;
        private float _loadingEffectScale;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new AreaChanger.
        /// </summary>
        public AreaChanger()
        {
            _state = AreaChangeState.Inactive;
            _timeLevelLoaded = SMH.Now + 2.5f;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets whether or not the area is currently changing.
        /// </summary>
        public bool IsChangingAreas
        {
            get { return _state != AreaChangeState.Inactive; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The environment will call this method when it has just finished loading
        /// a new area. This tells the AreaChanger to display the new area
        /// name for 2.5 seconds.
        /// </summary>
        public void DisplayNewAreaName()
        {
            _timeLevelLoaded = SMH.Now;
            _zoneTextAlpha = 255f;
        }

        /// <summary>
        /// Moves Smiley to a new area and starts the loading effect.
        /// </summary>
        /// <param name="destinationX"></param>
        /// <param name="destinationY"></param>
        /// <param name="destinationArea"></param>
        public void ChangeArea(int destinationX, int destinationY, Level destinationLevel)
        {
            _doneZoomingIn = false;
            _destinationX = destinationX;
            _destinationY = destinationY;
            _destinationLevel = destinationLevel;

            _state = AreaChangeState.In;
            _loadingEffectScale = 3f;
            SMH.Sound.PlaySound(Sound.AreaChangeUp);
            SMH.Sound.StopAllLoopedSounds();

            //TODO:smh->fadeScreenToNormal();
            //TODO:smh->setScreenColor(Colors::BLACK,0.0);
        }

        /// <summary>
        /// Draws the Area Changer
        /// </summary>
        public void Draw(float dt)
        {
            if (IsChangingAreas)
            {
                //Top
                SMH.Graphics.DrawRect(0, 0, 1024, 384f - 198f * _loadingEffectScale, Color.Black, true);
                //Left
                SMH.Graphics.DrawRect(0, 0, 512f - 198f * _loadingEffectScale, 768f, Color.Black, true);
                //Right
                float x = 512f + 198f * _loadingEffectScale;
                SMH.Graphics.DrawRect(x, 0, 1024f - x, 768, Color.Black, true);
                //Bottom
                float y = 384f + 198f * _loadingEffectScale;
                SMH.Graphics.DrawRect(0, y, 1024, 768f - y, Color.Black, true);
                //Circle
                SMH.Graphics.DrawSprite(Sprites.Loading, 512f, 384f, Color.White, 0f, _loadingEffectScale);
            }

            //After entering a new zone, display the ZONE NAME for 2.5 seconds after entering
            if (SMH.Now < _timeLevelLoaded + 2.5f && !SMH.WindowManager.IsAnyWindowOpen)
            {
                //After 1.5 seconds start fading out the zone name
                if (SMH.Now> _timeLevelLoaded + 1.5f)
                {
                    _zoneTextAlpha -= 255f * dt;
                    if (_zoneTextAlpha < 0f) _zoneTextAlpha = 0f;
                }
                SMH.Graphics.DrawString(SmileyFont.NewArea, SMH.SaveManager.CurrentSave.Level.GetDescription(), 512f, 200f, TextAlignment.Center, Color.FromNonPremultiplied(255, 255, 255, (int)_zoneTextAlpha));
            }
        }

        /// <summary>
        /// Updates the area changer.
        /// </summary>
        /// <param name="dt"></param>
        public void Update(float dt)
        {
            //Circle zooming in
            if (_state == AreaChangeState.In)
            {
                if (_doneZoomingIn)
                {
                    _loadingEffectScale = 0f;

                    //Relocate Smiley
                    if (_destinationLevel == SMH.SaveManager.CurrentSave.Level)
                    {
                        //Move smiley to a new location in the same area
                        SMH.Player.MoveTo(_destinationX, _destinationY);
                        SMH.Environment.Update(0f);
                        //TODO:
                        SMH.EnemyManager.Update(0f);
                        SMH.LootManager.Update(0f);
                        SMH.ProjectileManager.Update(0f);
                    }
                    else
                    {
                        SMH.Environment.LoadLevel(_destinationLevel, SMH.SaveManager.CurrentSave.Level, true);
                        _zoneTextAlpha = 255f;
                    }
                    _state = AreaChangeState.Out;
                }
                else
                {
                    _loadingEffectScale -= 3f * dt;
                }

                //When done zooming in don't actually move Smiley until the next frame so
                //that it draws the circle completely zoomed in while the level is loading
                if (_loadingEffectScale < 0.00001 && !_doneZoomingIn)
                {
                    _doneZoomingIn = true;
                    SMH.Sound.PlaySound(Sound.AreaChangeDown);
                }
            }
            else if (_state == AreaChangeState.Out)
            {
                //Circle zooming out
                _loadingEffectScale += 3f * dt;
                if (_loadingEffectScale > 3.0)
                {
                    _loadingEffectScale = 3f;
                    _state = AreaChangeState.Inactive;
                }
            }
        }

        #endregion
    }
}
