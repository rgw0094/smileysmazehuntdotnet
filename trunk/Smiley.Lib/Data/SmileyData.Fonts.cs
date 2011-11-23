using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Smiley.Lib.Data
{
    public partial class SmileyData
    {
        public SpriteFont Font_Button { get; private set; }
        public SpriteFont Font_Controls { get; private set; }

        private void LoadFonts(ContentManager cm)
        {
            Font_Button = cm.Load<SpriteFont>("Fonts\\Button");
            Font_Controls = cm.Load<SpriteFont>("Fonts\\Controls");
        }
    }
}
