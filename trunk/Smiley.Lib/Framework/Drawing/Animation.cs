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

        private SpriteSet _sprites;
        private float _lastFrameChange;
        private int _activeFrame;
        private bool _goingBackwards;

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
            _sprites = sprites;
            FPS = fps;
            Reverse = reverse;
            Loop = loop;
            PingPong = pingPong;

            if (reverse)
            {
                _goingBackwards = true;
            }
        }

        #endregion

        #region Properties

        public float FPS { get; set; }
        public bool Reverse { get; set; }
        public bool Loop { get; set; }
        public bool PingPong { get; set; }

        /// <summary>
        /// Returns whether or not the animation is currently playing.
        /// </summary>
        public bool IsPlaying
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of frames in the animation.
        /// </summary>
        public int NumFrames
        {
            get { return _sprites.Count; }
        }

        /// <summary>
        /// Gets or sets the currently active frame.
        /// </summary>
        public int ActiveFrame
        {
            get { return _activeFrame; }
            set
            {
                if (value > _sprites.Count - 1)
                    throw new ArgumentException("Invalid frame number bro");

                _activeFrame = value;
                _lastFrameChange = SMH.GameTime;
            }
        }

        /// <summary>
        /// Gets the sprite for the current frame.
        /// </summary>
        public Sprite ActiveSprite
        {
            get { return _sprites[_activeFrame]; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates a new Animation with the same properties as the current instance.
        /// </summary>
        /// <returns></returns>
        public Animation Clone()
        {
            return new Animation(_sprites, FPS, Reverse, Loop, PingPong);
        }

        /// <summary>
        /// Starts playing the animation from its current frame.
        /// </summary>
        public void Play()
        {
            IsPlaying = true;
            _lastFrameChange = SMH.GameTime;
            _goingBackwards = Reverse;
        }

        /// <summary>
        /// Starts playing the animation from the given frame.
        /// </summary>
        /// <param name="startFrame"></param>
        public void Play(int startFrame)
        {
            ActiveFrame = startFrame;
            Play();
        }

        /// <summary>
        /// Stops the animation.
        /// </summary>
        public void Stop()
        {
            IsPlaying = false;
        }

        /// <summary>
        /// Updates the animation.
        /// </summary>
        /// <param name="dt"></param>
        public void Update(float dt)
        {
            if (IsPlaying && SMH.TimePassed(_lastFrameChange, 1f / FPS))
            {
                if ((_goingBackwards && _activeFrame == 0) || (!_goingBackwards && _activeFrame == _sprites.Count - 1))
                {
                    if (PingPong)
                    {
                        _goingBackwards = !_goingBackwards;
                    }
                    if (!Loop)
                    {
                        IsPlaying = false;
                    }
                }

                if (IsPlaying)
                {
                    if (_goingBackwards)
                    {
                        _activeFrame = _activeFrame == 0 ? _sprites.Count - 1 : _activeFrame - 1;
                    }
                    else
                    {
                        _activeFrame = _activeFrame == _sprites.Count - 1 ? 0 : _activeFrame + 1;
                    }
                    _lastFrameChange = SMH.Now;
                }
            }
        }

        #endregion
    }
}
