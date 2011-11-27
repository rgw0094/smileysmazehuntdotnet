using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework.Drawing;
using Smiley.Lib.Util;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Framework
{
    public class CollisionCircle
    {
        /// <summary>
        /// X-coordinate of the center of the circle.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// Y-coordinate of the center of the circle.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// Radius of the circle.
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// Returns whether or not the circle contains a point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Contains(float x, float y)
        {
            return Math.Abs(SmileyUtil.Distance(X, Y, x, y)) < Radius;
        }

        /// <summary>
        /// Returns whether or not the circle intersects another circle.
        /// </summary>
        /// <param name="circle"></param>
        /// <returns></returns>
        public bool Intersects(CollisionCircle circle)
        {
            return SmileyUtil.Distance(X, Y, circle.X, circle.Y) < Radius + circle.Radius;
        }

        /// <summary>
        /// Returns whether or not the circle intersects a rectangle.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool Intersects(Rect rect)
        {
            //Test for when the circle is completely inside the box
            if (rect.Contains(new Vector2(X, Y)))
                return true;

            //Test the 4 corners of the box
            if (SmileyUtil.Distance(rect.X, rect.Y, X, Y) < Radius) return true;
            if (SmileyUtil.Distance(rect.Right, rect.Y, X, Y) < Radius) return true;
            if (SmileyUtil.Distance(rect.Right, rect.Bottom, X, Y) < Radius) return true;
            if (SmileyUtil.Distance(rect.X, rect.Bottom, X, Y) < Radius) return true;

            //Test middle of box
            if (SmileyUtil.Distance(rect.X + (rect.Right - rect.X) / 2f, rect.Y + (rect.Bottom - rect.Y) / 2f, X, Y) < Radius)
                return true;

            //Test top and bottom of box
            if (X > rect.X && X < rect.Right)
            {
                if (Math.Abs(rect.Bottom - Y) < Radius) return true;
                if (Math.Abs(rect.Y - Y) < Radius) return true;
            }

            //Test left and right side of box
            if (Y > rect.Y && Y < rect.Bottom)
            {
                if (Math.Abs(rect.Right - X) < Radius) return true;
                if (Math.Abs(rect.X - X) < Radius) return true;
            }

            //If all tests pass there is no intersection
            return false;
        }
    }
}
