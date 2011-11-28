using System;
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

        public float Right
        {
            get { return X + Width; }
        }

        public float Bottom
        {
            get { return Y + Height; }
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
            return v.X >= X && v.X <= Right &&
                   v.Y >= Y && v.Y <= Bottom;
        }

        public bool Intersects(Rect rect)
        {
            return Math.Abs(X + Right - rect.X - rect.Right) < (Right - X + rect.Right - rect.X) &&
                   Math.Abs(Y + Bottom - rect.Y - rect.Bottom) < (Bottom - Y + rect.Bottom - rect.Y);
        }
    }
}
