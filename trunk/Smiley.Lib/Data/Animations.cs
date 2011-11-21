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
        public static Animation Abilities;

        static Animations()
        {
            Fenwar = new Animation(
                SmileyTexture.General,
                new Rectangle(401, 385, 62, 73),
                6,
                10.0,
                new Vector2(31, 36));
            FenwarFace = new Animation(
                SmileyTexture.General,
                new Rectangle(491, 461, 50, 48),
                3,
                4.0,
                new Vector2(25, 24));
            FountainRipple = new Animation(
                SmileyTexture.Fountain,
                new Rectangle(0, 0, 338, 95),
                10,
                10.0,
                new Vector2(169, 47.5f));
            Water = new Animation(
                SmileyTexture.Animations,
                new Rectangle(0, 0, 64, 64),
                16,
                16.0);
            GreenWater = new Animation(
                SmileyTexture.Animations,
                new Rectangle(0, 640, 64, 64),
                16,
                16.0);
            Lava = new Animation(
                SmileyTexture.Animations,
                new Rectangle(0, 128, 64, 64),
                10,
                10.0);
            Spring = new Animation(
                SmileyTexture.Animations,
                new Rectangle(0, 64, 64, 64),
                7,
                14.0);
            SuperSpring = new Animation(
                SmileyTexture.Animations,
                new Rectangle(0, 704, 64, 64),
                7,
                14.0);
            SilverSwitch = new Animation(
                SmileyTexture.Animations,
                new Rectangle(320, 192, 64, 64),
                5, 5.0);
            BrownSwitch = new Animation(
                SmileyTexture.Animations,
                new Rectangle(320, 256, 64, 64),
                5, 5.0);
            BlueSwitch = new Animation(
                SmileyTexture.Animations,
                new Rectangle(320, 320, 64, 64),
                5, 5.0);
            GreenSwitch = new Animation(
                SmileyTexture.Animations,
                new Rectangle(320, 382, 64, 64),
                5, 5.0);
            YellowSwitch = new Animation(
                SmileyTexture.Animations,
                new Rectangle(320, 448, 64, 64),
                5, 5.0);
            WhiteSwitch = new Animation(
                SmileyTexture.Animations,
                new Rectangle(320, 512, 64, 64),
                5, 5.0);
            Smilelet = new Animation(
                SmileyTexture.General,
                new Rectangle(128, 193, 28, 26),
                8, 
                8.0,
                new Vector2(14,13));
            MirrorSwitch = new Animation(
                SmileyTexture.Animations,
                new Rectangle(640, 512, 64, 64),
                5,
                20.0,
                new Vector2(0, 0),
                false,
                LoopMode.PingPong);
            ShrinkTunnelSwitch = new Animation(
                SmileyTexture.Animations,
                new Rectangle(448, 704, 64, 64),
                5,
                20.0,
                new Vector2(0, 0),
                false,
                LoopMode.PingPong);
            BunnySwitch = new Animation(
                SmileyTexture.Animations,
                new Rectangle(768, 704, 64, 64),
                4,
                16.0,
                new Vector2(0, 0),
                false,
                LoopMode.PingPong);
            Abilities = new Animation(
                SmileyTexture.General,
                new Rectangle(192, 0, 64, 64),
                16,
                16.0,
                new Vector2(32, 32));
        }
    }
}
