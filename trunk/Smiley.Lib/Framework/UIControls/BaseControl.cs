using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Framework.UIControls
{
    public abstract class BaseControl : GameObject
    {
        public float X { get; set; }
        public float Y { get; set; }
    }
}
