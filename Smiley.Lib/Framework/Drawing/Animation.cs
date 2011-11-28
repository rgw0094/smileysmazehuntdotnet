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

        private float _lastFrameChange;
        private int _activeFrame;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new Animation.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="rect"></param>
        /// <param name="numFrames"></param>
        /// <param name="fps"></param>
        /// <param name="hotSpot"></param>
        /// <param name="reverse"></param>
        /// <param name="loop"></param>
        /// <param name="pingPong"></param>
        public Animation(SmileyTexture texture, Rectangle rect, int numFrames, float fps, Vector2? hotSpot = null, bool reverse = false, bool loop = false, bool pingPong = false)
            : this(new SpriteSet(texture, numFrames, rect, hotSpot), fps, reverse, loop, pingPong)
        {
        }

        /// <summary>
        /// Constructs a new Animation.
        /// </summary>
        /// <param name="sprites"></param>
        /// <param name="fps"></param>
        /// <param name="reverse"></param>
        /// <param name="loop"></param>
        /// <param name="pingPong"></param>
        public Animation(SpriteSet sprites, float fps, bool reverse = false, bool loop = false, bool pingPong = false)
        {
            Sprites = sprites;
            FPS = fps;
            Reverse = reverse;
            Loop = loop;
            PingPong = pingPong;
        }

        #endregion

        #region Properties

        public SpriteSet Sprites { get; private set; }
        public float FPS { get; private set; }
        public bool Reverse { get; private set; }
        public bool Loop { get; private set; }
        public bool PingPong { get; private set; }

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

        /// <summary>
        /// Creates a new Animation with the same properties as the current instance.
        /// </summary>
        /// <returns></returns>
        public Animation Clone()
        {
            return new Animation(Sprites, FPS, Reverse, Loop, PingPong);
        }

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
            if (IsPlaying && SMH.TimePassed(_lastFrameChange, 1f / FPS))
            {
                _activeFrame = _activeFrame == Sprites.Count - 1 ? 0 : _activeFrame + 1;
                _lastFrameChange = SMH.Now;
            }
        }

        public void Draw(float x, float y)
        {
            Draw(x, y, 1f);
        }

        public void Draw(float x, float y, float alpha)
        {
            //TODO:
            SMH.Graphics.DrawSprite(Sprites[_activeFrame], x, y);
        }

        #endregion
    }
}
