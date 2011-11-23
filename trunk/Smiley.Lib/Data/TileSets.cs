using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework.Drawing;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Data
{
    public static class TileSets
    {
        public static TileSet Abilities = new TileSet(
            SmileyTexture.General,
            16,
            new Rectangle(192, 0, 64, 64),
            new Vector2(32, 32));

        public static TileSet Player = new TileSet(
            SmileyTexture.General,
            8,
            new Rectangle(1, 507, 61, 72),
            new Vector2(31, 48));
    }
}
