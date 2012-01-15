using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smiley.Lib.UI.Windows
{
    public abstract class BaseWindow
    {
        /// <summary>
        /// Updates the window, and returns whether or not to keep
        /// the window alive for next frame.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public abstract bool Update(float dt);

        /// <summary>
        /// Draws the window.
        /// </summary>
        public abstract void Draw();
    }
}
