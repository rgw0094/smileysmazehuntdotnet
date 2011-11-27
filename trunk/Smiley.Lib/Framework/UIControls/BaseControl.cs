using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Framework.UIControls
{
    public abstract class BaseControl
    {
        public float X { get; set; }
        public float Y { get; set; }

        public abstract void Draw();
        public abstract void Update(float dt);
    }
}
