﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Framework.Drawing
{
    public class Rect
    {
        public Rect(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public float X
        {
            get;
            set;
        }

        public float Y
        {
            get;
            set;
        }

        public float Width
        {
            get;
            set;
        }

        public float Height
        {
            get;
            set;
        }

        public bool Contains(Vector2 v)
        {
            return v.X >= X && v.X <= X + Width &&
                   v.Y >= Y && v.Y <= Y + Height;
        }
    }
}