using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Framework
{
    public enum LoopMode
    {
        None,
        Loop,
        PingPong
    }

    public class Animation
    {
        #region Constructors

        /// <summary>
        /// Constructs a new Animation.
        /// </summary>
        public Animation(SmileyTexture texture, Rectangle? rect, int frames, double fps, Vector2? hotSpot = null, bool reverse = false, LoopMode loop = LoopMode.Loop)
        {
            Rect = rect;
            Frames = frames;
            FPS = fps;
            Reverse = reverse;
            HotSpot = hotSpot;
            LoopMode = loop;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Texture for the animation.
        /// </summary>
        public SmileyTexture Texture
        {
            get;
            private set;
        }

        /// <summary>
        /// Rectangle within the texture to use, or null to use the whole texture.
        /// </summary>
        public Rectangle? Rect
        {
            get;
            private set;
        }

        /// <summary>
        /// The number of frames in the animation.
        /// </summary>
        public int Frames
        {
            get;
            private set;
        }

        /// <summary>
        /// FPS to play the animation.
        /// </summary>
        public double FPS
        {
            get;
            private set;
        }

        /// <summary>
        /// Whether or not to play the animation in reverse.
        /// </summary>
        public bool Reverse
        {
            get;
            private set;
        }

        /// <summary>
        /// Whether or not to loop the animation.
        /// </summary>
        public LoopMode LoopMode
        {
            get;
            private set;
        }

        /// <summary>
        /// Center of a frame in the animation.
        /// </summary>
        public Vector2? HotSpot
        {
            get;
            private set;
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
        }

        public void Stop()
        {

        }

        public void Update(double dt)
        {
        }

        #endregion
    }
}
