using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;
using Smiley.Lib.Data;

namespace Smiley.Lib.Framework.Drawing
{
    public class Animation
    {
        #region Private Variables

        private AnimationInfo _info;
        private float _lastFrameChange;
        private int _activeFrame;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new Animation.
        /// </summary>
        /// <param name="info">The info defining the animation. See <see cref="Animations"/> for predefined AnimationInfos</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Animation(AnimationInfo info, float x, float y)
        {
            _info = info;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current x position.
        /// </summary>
        public float X
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the current y position.
        /// </summary>
        public float Y
        {
            get;
            set;
        }

        /// <summary>
        /// Returns whether or not the animation is currently playing.
        /// </summary>
        public bool IsPlaying
        {
            get;
            private set;
        }

        #endregion

        #region Public Methods

        public void Play()
        {
            IsPlaying = true;
            _lastFrameChange = SMH.Now;
        }

        public void Stop()
        {
            IsPlaying = false;
        }

        public void Update(float dt)
        {
            if (IsPlaying && SMH.TimePassed(_lastFrameChange, 1f / _info.FPS))
            {
                _activeFrame = _activeFrame == _info.TileSet.Count - 1 ? 0 : _activeFrame + 1;
                _lastFrameChange = SMH.Now;
            }
        }

        public void Draw()
        {
            SMH.Graphics.DrawSprite(_info.TileSet[_activeFrame], X, Y);
        }

        #endregion
    }
}
