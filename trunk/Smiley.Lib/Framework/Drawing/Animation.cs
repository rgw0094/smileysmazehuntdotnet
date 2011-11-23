using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Framework.Drawing
{
    public enum LoopMode
    {
        None,
        Loop,
        PingPong
    }

    public class Animation : GameObject
    {
        #region Private Variables

        private TileSet _tileSet;

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
        public Animation(SmileyTexture texture, Rectangle rect, int numFrames, double fps, Vector2? hotSpot = null, bool reverse = false, LoopMode loop = LoopMode.Loop)
        {
            _tileSet = new TileSet(texture, numFrames, rect, hotSpot);
            NumFrames = numFrames;
            FPS = fps;
            Reverse = reverse;
            LoopMode = loop;
        }

        #endregion

        #region Properties

        /// <summary>
        /// The number of frames in the animation.
        /// </summary>
        public int NumFrames
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

        public override void Update(float dt)
        {
        }

        public override void Draw()
        {
        }

        #endregion
    }
}
