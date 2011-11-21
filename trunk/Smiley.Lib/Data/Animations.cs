using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Smiley.Lib.Framework;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Data
{
    public static class Animations
    {
        public static Animation Fenwar;
        public static Animation FenwarFace;
        public static Animation FountainRipple;
        public static Animation Water;
        public static Animation GreenWater;
        public static Animation Lava;
        public static Animation Spring;
        public static Animation SuperSpring;
        public static Animation SilverSwitch;
        public static Animation BrownSwitch;
        public static Animation BlueSwitch;
        public static Animation GreenSwitch;
        public static Animation YellowSwitch;
        public static Animation WhiteSwitch;
        public static Animation SavePoint;
        public static Animation Smilelet;
        public static Animation MirrorSwitch;
        public static Animation ShrinkTunnelSwitch;
        public static Animation BunnySwitch;

        static Animations()
        {
            Fenwar = new Animation(
                SmileyTexture.General,
                new Rectangle(401,385,62,73),
                6,
                10.0,
                false,
                LoopMode.Loop,
                new Vector2(31, 36));
            FenwarFace = new Animation(
                SmileyTexture.General,
                new Rectangle(491, 461, 50, 48),
                3,
                4.0,
                false,
                LoopMode.None,
                new Vector2(25, 24));
            FountainRipple = new Animation(
                SmileyTexture.Fountain,
                new Rectangle(0, 0, 338, 95),
                10,
                10.0,
                false,
                LoopMode.Loop,
                new Vector2(169, 47.5f));


        }
    }
}
