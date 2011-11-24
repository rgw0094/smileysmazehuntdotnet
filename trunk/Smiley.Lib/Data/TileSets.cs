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
        public static TileSet Particles = new TileSet(
            SmileyTexture.Particles,
            16,
            new Rectangle(0, 0, 32, 32),
            new Vector2(16, 16));

        public static TileSet MainLayer = new TileSet(
            SmileyTexture.MainLayer,
            264,
            new Rectangle(0, 0, 64, 64));

        public static TileSet WalkLayer = new TileSet(
            SmileyTexture.WalkLayer,
            264,
            new Rectangle(0, 0, 64, 64));

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

        public static TileSet LevelIcons = new TileSet(
            SmileyTexture.UserInterface,
            5,
            new Rectangle(0, 984, 40, 40));

        public static TileSet KeyIcons = new TileSet(
            SmileyTexture.UserInterface,
            5,
            new Rectangle(200, 984, 40, 40));

        public static TileSet UpgradeIcons = new TileSet(
            SmileyTexture.UserInterface,
            3,
            new Rectangle(360, 984, 40, 40));

        public static TileSet AdviceIcons = new TileSet(
            SmileyTexture.UserInterface,
            8,
            new Rectangle(480, 984, 40, 40));
    }
}
