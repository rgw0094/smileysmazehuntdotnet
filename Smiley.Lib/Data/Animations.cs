using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework.Drawing;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Data
{
    public static class Animations
    {
        public static Animation Fenwar = new Animation(
            SmileyTexture.General,
            new Rectangle(401, 385, 62, 73),
            6,
            10,
            new Vector2(31, 36));

        public static Animation FenwarFace = new Animation(
            SmileyTexture.General,
            new Rectangle(491, 461, 50, 48),
            3,
            4,
            new Vector2(25, 24));

        public static Animation FountainRipple = new Animation(
            SmileyTexture.Fountain,
            new Rectangle(0, 0, 338, 95),
            10,
            10,
            new Vector2(169, 47.5f));

        public static Animation Water = new Animation(
            SmileyTexture.Animations,
            new Rectangle(0, 0, 64, 64),
            16,
            16);

        public static Animation GreenWater = new Animation(
            SmileyTexture.Animations,
            new Rectangle(0, 640, 64, 64),
            16,
            16);

        public static Animation Lava = new Animation(
            SmileyTexture.Animations,
            new Rectangle(0, 128, 64, 64),
            10,
            10);

        public static Animation Spring = new Animation(
            SmileyTexture.Animations,
            new Rectangle(0, 64, 64, 64),
            7,
            14);

        public static Animation SuperSpring = new Animation(
            SmileyTexture.Animations,
            new Rectangle(0, 704, 64, 64),
            7,
            14);

        public static Animation SilverSwitch = new Animation(SmileyTexture.Animations, new Rectangle(320, 192, 64, 64), 5, Constants.SwitchFPS);
        public static Animation BrownSwitch = new Animation(SmileyTexture.Animations, new Rectangle(320, 256, 64, 64), 5, Constants.SwitchFPS);
        public static Animation BlueSwitch = new Animation(SmileyTexture.Animations, new Rectangle(320, 320, 64, 64), 5, Constants.SwitchFPS);
        public static Animation GreenSwitch = new Animation(SmileyTexture.Animations, new Rectangle(320, 382, 64, 64), 5, Constants.SwitchFPS);
        public static Animation YellowSwitch = new Animation(SmileyTexture.Animations, new Rectangle(320, 448, 64, 64), 5, Constants.SwitchFPS);
        public static Animation WhiteSwitch = new Animation(SmileyTexture.Animations, new Rectangle(320, 512, 64, 64), 5, Constants.SwitchFPS);

        public static Animation SilverCylinder = new Animation(SmileyTexture.Animations, new Rectangle(0, 3 * 64, 64, 64), 5, Constants.SwitchFPS);
        public static Animation BrownCylinder = new Animation(SmileyTexture.Animations, new Rectangle(0, 4 * 64, 64, 64), 5, Constants.SwitchFPS);
        public static Animation BlueCylinder = new Animation(SmileyTexture.Animations, new Rectangle(0, 5 * 64, 64, 64), 5, Constants.SwitchFPS);
        public static Animation GreenCylinder = new Animation(SmileyTexture.Animations, new Rectangle(0, 6 * 64, 64, 64), 5, Constants.SwitchFPS);
        public static Animation YellowCylinder = new Animation(SmileyTexture.Animations, new Rectangle(0, 7 * 64, 64, 64), 5, Constants.SwitchFPS);
        public static Animation WhiteCylinder = new Animation(SmileyTexture.Animations, new Rectangle(0, 8 * 64, 64, 64), 5, Constants.SwitchFPS);

        public static Animation SaveShrine = new Animation(SmileyTexture.Animations, new Rectangle(0, 576, 64, 64), 16, 16, null, false, true, false);

        public static Animation Smilelet = new Animation(
            SmileyTexture.General,
            new Rectangle(128, 193, 28, 26),
            8,
            8,
            new Vector2(14, 13));

        public static Animation MirrorSwitch = new Animation(
            SmileyTexture.Animations,
            new Rectangle(640, 512, 64, 64),
            5,
            20,
            new Vector2(0, 0),
            false, false, true);

        public static Animation ShrinkTunnelSwitch = new Animation(
            SmileyTexture.Animations,
            new Rectangle(448, 704, 64, 64),
            5,
            20,
            new Vector2(0, 0),
            false, false, true);

        public static Animation BunnySwitch = new Animation(
            SmileyTexture.Animations,
            new Rectangle(768, 704, 64, 64),
            4,
            16,
            new Vector2(0, 0),
            false, false, true);

        public static Animation SmileyTongue = new Animation(
            SmileyTexture.Animations,
            new Rectangle(640, 427, 12, 85),
            13,
            118,
            new Vector2(6, 84));

        public static Animation GroundSpike = new Animation(
            SmileyTexture.Cornwallis,
            new Rectangle(20, 158, 20, 60),
            5,
            20,
            new Vector2(10, 50));

        public static Animation PhyreBawz = new Animation(
            SmileyTexture.Fireboss,
            new Rectangle(0, 0, 97, 158),
            4,
            20,
            new Vector2(48, 79));

        public static Animation PhyreBawzDownMouth = new Animation(
            SmileyTexture.Fireboss,
            new Rectangle(0, 158, 32, 17),
            4,
            12,
            new Vector2(0, 0),
            false, false, true);

        public static Animation PhyreBawzLeftMouth = new Animation(
            SmileyTexture.Fireboss,
            new Rectangle(0, 175, 27, 13),
            4,
            12,
            new Vector2(0, 0),
            false, false, true);

        public static Animation PhyreBawzRightMouth = new Animation(
            SmileyTexture.Fireboss,
            new Rectangle(0, 188, 27, 13),
            4,
            12,
            new Vector2(0, 0),
            false, false, true);

        public static Animation Owlet = new Animation(
            SmileyTexture.Garmborn,
            new Rectangle(0, 192, 84, 36),
            4,
            20,
            new Vector2(42, 18),
            false, true, true);

        public static Animation Bartli = new Animation(
            SmileyTexture.Bartli,
            new Rectangle(0, 0, 110, 132),
            2,
            20,
            new Vector2(55, 132),
            false, true, true);

        public static Animation LightningEye = new Animation(
            SmileyTexture.Magnitogorsk,
            new Rectangle(0, 193, 88, 41),
            5,
            10,
            new Vector2(44, 20),
            false, true, false);

        public static Animation IceEye = new Animation(
            SmileyTexture.Magnitogorsk,
            new Rectangle(0, 235, 88, 41),
            5,
            10,
            new Vector2(44, 20),
            false, true, false);

        public static Animation FireEye = new Animation(
            SmileyTexture.Magnitogorsk,
            new Rectangle(0, 277, 88, 41),
            5,
            10,
            new Vector2(44, 20),
            false, true, false);

        public static Animation BarvinoidMouth = new Animation(
            SmileyTexture.Animations,
            new Rectangle(0, 768, 67, 70),
            9,
            10,
            new Vector2(31, 30));

        public static Animation EvilEye = new Animation(
            SmileyTexture.Animations,
            new Rectangle(640, 192, 64, 64),
            5,
            10,
            new Vector2(32, 32));

        public static Animation Burrow0 = new Animation(
            SmileyTexture.Animations,
            new Rectangle(640, 320, 64, 64),
            5,
            10,
            new Vector2(32, 32));

        public static Animation Burrow1 = new Animation(
            SmileyTexture.Animations,
            new Rectangle(640, 320, 64, 64),
            5,
            10,
            new Vector2(32, 32));

        public static Animation BombSpawn = new Animation(
            SmileyTexture.Animations,
            new Rectangle(448, 64, 64, 64),
            8,
            10,
            new Vector2(32, 32));

        public static Animation BombEyesGlow = new Animation(
            SmileyTexture.Animations,
            new Rectangle(640, 128, 64, 64),
            6,
            20,
            new Vector2(32, 32),
            false, false, true);

        public static Animation Batlet = new Animation(
            SmileyTexture.Animations,
            new Rectangle(640, 384, 82, 35),
            4,
            16,
            new Vector2(32, 32),
            false, false, true);
    }
}
