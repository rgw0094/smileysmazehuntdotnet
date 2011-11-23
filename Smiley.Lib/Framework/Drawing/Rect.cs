using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Framework.Drawing
{
    public class Rect
    {
        public Rect(Vector2 topLeft, float width, float height)
        {
            TopLeft = topLeft;
            Width = width;
            Height = height;
        }

        public Vector2 TopLeft
        {
            get;
            private set;
        }

        public float Width
        {
            get;
            private set;
        }

        public float Height
        {
            get;
            private set;
        }

        public bool Contains(Vector2 v)
        {
            return v.X >= TopLeft.X && v.X <= TopLeft.X + Width &&
                   v.Y >= TopLeft.Y && v.Y <= TopLeft.Y + Height;
        }
    }
}
