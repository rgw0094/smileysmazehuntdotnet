using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Framework.Drawing
{
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
        /// <param name="pingPong"></param>
        public Animation(SmileyTexture texture, Rectangle rect, int numFrames, double fps, Vector2? hotSpot = null, bool reverse = false, bool loop = false, bool pingPong = false)
        {
            _tileSet = new TileSet(texture, numFrames, rect, hotSpot);
            NumFrames = numFrames;
            FPS = fps;
            Reverse = reverse;
            Loop = loop;
            PingPong = pingPong;
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
        public bool Loop
        {
            get;
            private set;
        }

        /// <summary>
        /// Whether or not the animation ping pongs.
        /// </summary>
        public bool PingPong
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
