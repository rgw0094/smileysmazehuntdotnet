﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Util;
using Smiley.Lib.Data;
using Smiley.Lib.GameObjects.Environment;

namespace Smiley.Lib.GameObjects
{
    public abstract class GameObject
    {
        /// <summary>
        /// Gets or sets the object's X coordinate.
        /// </summary>
        public float X
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the object's Y coordinate.
        /// </summary>
        public float Y
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the object's X-coordinate relative to the top left of the screen.
        /// </summary>
        public float ScreenX
        {
            get { return SMH.GetScreenX(X); }
        }

        /// <summary>
        /// Gets the object's Y-coordinate relative to the top left of the screen.
        /// </summary>
        public float ScreenY
        {
            get { return SMH.GetScreenY(Y); }
        }

        /// <summary>
        /// The speed that object can move.
        /// </summary>
        public float Speed
        {
            get;
            set;
        }

        /// <summary>
        /// The object's current X velocity.
        /// </summary>
        public float DX
        {
            get;
            set;
        }

        /// <summary>
        /// The object's current Y velocity.
        /// </summary>
        public float DY
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the tile the object is currently on.
        /// </summary>
        public Tile Tile
        {
            get { return SMH.Environment.Tiles[SmileyUtil.GetGridX(X), SmileyUtil.GetGridY(Y)]; }
        }

        /// <summary>
        /// Draws the object.
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="dt"></param>
        public abstract void Update(float dt);
    }
}
