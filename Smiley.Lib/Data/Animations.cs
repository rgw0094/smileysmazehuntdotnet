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
            10.0,
            new Vector2(31, 36));

        public static Animation FenwarFace = new Animation(
            SmileyTexture.General,
            new Rectangle(491, 461, 50, 48),
            3,
            4.0,
            new Vector2(25, 24));

        public static Animation FountainRipple = new Animation(
            SmileyTexture.Fountain,
            new Rectangle(0, 0, 338, 95),
            10,
            10.0,
            new Vector2(169, 47.5f));

        public static Animation Water = new Animation(
            SmileyTexture.Animations,
            new Rectangle(0, 0, 64, 64),
            16,
            16.0);

        public static Animation GreenWater = new Animation(
            SmileyTexture.Animations,
            new Rectangle(0, 640, 64, 64),
            16,
            16.0);

        public static Animation Lava = new Animation(
            SmileyTexture.Animations,
            new Rectangle(0, 128, 64, 64),
            10,
            10.0);

        public static Animation Spring = new Animation(
            SmileyTexture.Animations,
            new Rectangle(0, 64, 64, 64),
            7,
            14.0);

        public static Animation SuperSpring = new Animation(
            SmileyTexture.Animations,
            new Rectangle(0, 704, 64, 64),
            7,
            14.0);

        public static Animation SilverSwitch = new Animation(
            SmileyTexture.Animations,
            new Rectangle(320, 192, 64, 64),
            5, 5.0);

        public static Animation BrownSwitch = new Animation(
            SmileyTexture.Animations,
            new Rectangle(320, 256, 64, 64),
            5, 5.0);

        public static Animation BlueSwitch = new Animation(
            SmileyTexture.Animations,
            new Rectangle(320, 320, 64, 64),
            5, 5.0);

        public static Animation GreenSwitch = new Animation(
            SmileyTexture.Animations,
            new Rectangle(320, 382, 64, 64),
            5, 5.0);

        public static Animation YellowSwitch = new Animation(
            SmileyTexture.Animations,
            new Rectangle(320, 448, 64, 64),
            5, 5.0);

        public static Animation WhiteSwitch = new Animation(
            SmileyTexture.Animations,
            new Rectangle(320, 512, 64, 64),
            5, 5.0);

        public static Animation Smilelet = new Animation(
            SmileyTexture.General,
            new Rectangle(128, 193, 28, 26),
            8,
            8.0,
            new Vector2(14, 13));

        public static Animation MirrorSwitch = new Animation(
            SmileyTexture.Animations,
            new Rectangle(640, 512, 64, 64),
            5,
            20.0,
            new Vector2(0, 0),
            false,
            LoopMode.PingPong);

        public static Animation ShrinkTunnelSwitch = new Animation(
            SmileyTexture.Animations,
            new Rectangle(448, 704, 64, 64),
            5,
            20.0,
            new Vector2(0, 0),
            false,
            LoopMode.PingPong);

        public static Animation BunnySwitch = new Animation(
            SmileyTexture.Animations,
            new Rectangle(768, 704, 64, 64),
            4,
            16.0,
            new Vector2(0, 0),
            false,
            LoopMode.PingPong);

        public static Animation SmileyTongue = new Animation(
            SmileyTexture.Animations,
            new Rectangle(640, 427, 12, 85),
            13,
            118.0,
            new Vector2(6, 84));
    }
}
