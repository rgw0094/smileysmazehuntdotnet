using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework.Drawing;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Data
{
    public static class SpriteSets
    {
        public static SpriteSet Particles = new SpriteSet(
            SmileyTexture.Particles,
            16,
            new Rectangle(0, 0, 32, 32),
            new Vector2(16, 16));

        public static SpriteSet MainLayer = new SpriteSet(
            SmileyTexture.MainLayer,
            264,
            new Rectangle(0, 0, 64, 64));

        public static SpriteSet WalkLayer = new SpriteSet(
            SmileyTexture.WalkLayer,
            264,
            new Rectangle(0, 0, 64, 64));

        public static SpriteSet Abilities = new SpriteSet(
            SmileyTexture.General,
            16,
            new Rectangle(192, 0, 64, 64),
            new Vector2(32, 32));

        public static SpriteSet Player = new SpriteSet(
            SmileyTexture.General,
            8,
            new Rectangle(1, 507, 61, 72),
            new Vector2(31, 48));

        public static SpriteSet LevelIcons = new SpriteSet(
            SmileyTexture.UserInterface,
            5,
            new Rectangle(0, 984, 40, 40));

        public static SpriteSet KeyIcons = new SpriteSet(
            SmileyTexture.UserInterface,
            5,
            new Rectangle(200, 984, 40, 40));

        public static SpriteSet UpgradeIcons = new SpriteSet(
            SmileyTexture.UserInterface,
            3,
            new Rectangle(360, 984, 40, 40));

        public static SpriteSet AdviceIcons = new SpriteSet(
            SmileyTexture.UserInterface,
            8,
            new Rectangle(480, 984, 40, 40));
    }
}
