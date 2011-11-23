using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework.Drawing;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Data
{
    public static partial class SmileyData
    {
        public static Animation Animation_Fenwar;
        public static Animation Animation_FenwarFace;
        public static Animation Animation_FountainRipple;
        public static Animation Animation_Water;
        public static Animation Animation_GreenWater;
        public static Animation Animation_Lava;
        public static Animation Animation_Spring;
        public static Animation Animation_SuperSpring;
        public static Animation Animation_SilverSwitch;
        public static Animation Animation_BrownSwitch;
        public static Animation Animation_BlueSwitch;
        public static Animation Animation_GreenSwitch;
        public static Animation Animation_YellowSwitch;
        public static Animation Animation_WhiteSwitch;
        public static Animation Animation_SavePoint;
        public static Animation Animation_Smilelet;
        public static Animation Animation_MirrorSwitch;
        public static Animation Animation_ShrinkTunnelSwitch;
        public static Animation Animation_BunnySwitch;
        public static Animation Animation_Abilities;

        private static void LoadAnimations()
        {
            Animation_Fenwar = new Animation(
                SmileyTexture.General,
                new Rectangle(401, 385, 62, 73),
                6,
                10.0,
                new Vector2(31, 36));
            Animation_FenwarFace = new Animation(
                SmileyTexture.General,
                new Rectangle(491, 461, 50, 48),
                3,
                4.0,
                new Vector2(25, 24));
            Animation_FountainRipple = new Animation(
                SmileyTexture.Fountain,
                new Rectangle(0, 0, 338, 95),
                10,
                10.0,
                new Vector2(169, 47.5f));
            Animation_Water = new Animation(
                SmileyTexture.Animations,
                new Rectangle(0, 0, 64, 64),
                16,
                16.0);
            Animation_GreenWater = new Animation(
                SmileyTexture.Animations,
                new Rectangle(0, 640, 64, 64),
                16,
                16.0);
            Animation_Lava = new Animation(
                SmileyTexture.Animations,
                new Rectangle(0, 128, 64, 64),
                10,
                10.0);
            Animation_Spring = new Animation(
                SmileyTexture.Animations,
                new Rectangle(0, 64, 64, 64),
                7,
                14.0);
            Animation_SuperSpring = new Animation(
                SmileyTexture.Animations,
                new Rectangle(0, 704, 64, 64),
                7,
                14.0);
            Animation_SilverSwitch = new Animation(
                SmileyTexture.Animations,
                new Rectangle(320, 192, 64, 64),
                5, 5.0);
            Animation_BrownSwitch = new Animation(
                SmileyTexture.Animations,
                new Rectangle(320, 256, 64, 64),
                5, 5.0);
            Animation_BlueSwitch = new Animation(
                SmileyTexture.Animations,
                new Rectangle(320, 320, 64, 64),
                5, 5.0);
            Animation_GreenSwitch = new Animation(
                SmileyTexture.Animations,
                new Rectangle(320, 382, 64, 64),
                5, 5.0);
            Animation_YellowSwitch = new Animation(
                SmileyTexture.Animations,
                new Rectangle(320, 448, 64, 64),
                5, 5.0);
            Animation_WhiteSwitch = new Animation(
                SmileyTexture.Animations,
                new Rectangle(320, 512, 64, 64),
                5, 5.0);
            Animation_Smilelet = new Animation(
                SmileyTexture.General,
                new Rectangle(128, 193, 28, 26),
                8,
                8.0,
                new Vector2(14, 13));
            Animation_MirrorSwitch = new Animation(
                SmileyTexture.Animations,
                new Rectangle(640, 512, 64, 64),
                5,
                20.0,
                new Vector2(0, 0),
                false,
                LoopMode.PingPong);
            Animation_ShrinkTunnelSwitch = new Animation(
                SmileyTexture.Animations,
                new Rectangle(448, 704, 64, 64),
                5,
                20.0,
                new Vector2(0, 0),
                false,
                LoopMode.PingPong);
            Animation_BunnySwitch = new Animation(
                SmileyTexture.Animations,
                new Rectangle(768, 704, 64, 64),
                4,
                16.0,
                new Vector2(0, 0),
                false,
                LoopMode.PingPong);
            Animation_Abilities = new Animation(
                SmileyTexture.General,
                new Rectangle(192, 0, 64, 64),
                16,
                16.0,
                new Vector2(32, 32));
        }
    }
}
