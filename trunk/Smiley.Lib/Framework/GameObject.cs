using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Smiley.Lib.Framework
{
    /// <summary>
    /// Base class for all game objects.
    /// </summary>
    public abstract class GameObject
    {
        /// <summary>
        /// Draws the object.
        /// </summary>
        public virtual void Draw()
        {
        }

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="dt">Time since Update was last called.</param>
        public virtual void Update(float dt)
        {
        }
    }
}
