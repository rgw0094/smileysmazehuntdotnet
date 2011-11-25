using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework.Drawing;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Data
{
    public class AnimationInfo
    {
        public AnimationInfo(SmileyTexture texture, Rectangle rect, int numFrames, float fps, Vector2? hotSpot = null, bool reverse = false, bool loop = false, bool pingPong = false)
        {
            TileSet = new TileSet(texture, numFrames, rect, hotSpot);
            FPS = fps;
            Reverse = reverse;
            Loop = loop;
            PingPong = pingPong;
        }

        public TileSet TileSet { get; private set; }
        public float FPS { get; private set; }
        public bool Reverse { get; private set; }
        public bool Loop { get; private set; }
        public bool PingPong { get; private set; }
    }

    public static class Animations
    {
        public static AnimationInfo Fenwar = new AnimationInfo(
            SmileyTexture.General,
            new Rectangle(401, 385, 62, 73),
            6,
            10,
            new Vector2(31, 36));

        public static AnimationInfo FenwarFace = new AnimationInfo(
            SmileyTexture.General,
            new Rectangle(491, 461, 50, 48),
            3,
            4,
            new Vector2(25, 24));

        public static AnimationInfo FountainRipple = new AnimationInfo(
            SmileyTexture.Fountain,
            new Rectangle(0, 0, 338, 95),
            10,
            10,
            new Vector2(169, 47.5f));

        public static AnimationInfo Water = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(0, 0, 64, 64),
            16,
            16);

        public static AnimationInfo GreenWater = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(0, 640, 64, 64),
            16,
            16);

        public static AnimationInfo Lava = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(0, 128, 64, 64),
            10,
            10);

        public static AnimationInfo Spring = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(0, 64, 64, 64),
            7,
            14);

        public static AnimationInfo SuperSpring = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(0, 704, 64, 64),
            7,
            14);

        public static AnimationInfo SilverSwitch = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(320, 192, 64, 64),
            5, 5);

        public static AnimationInfo BrownSwitch = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(320, 256, 64, 64),
            5, 5);

        public static AnimationInfo BlueSwitch = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(320, 320, 64, 64),
            5, 5);

        public static AnimationInfo GreenSwitch = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(320, 382, 64, 64),
            5, 5);

        public static AnimationInfo YellowSwitch = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(320, 448, 64, 64),
            5, 5);

        public static AnimationInfo WhiteSwitch = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(320, 512, 64, 64),
            5, 5);

        public static AnimationInfo Smilelet = new AnimationInfo(
            SmileyTexture.General,
            new Rectangle(128, 193, 28, 26),
            8,
            8,
            new Vector2(14, 13));

        public static AnimationInfo MirrorSwitch = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(640, 512, 64, 64),
            5,
            20,
            new Vector2(0, 0),
            false, false, true);

        public static AnimationInfo ShrinkTunnelSwitch = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(448, 704, 64, 64),
            5,
            20,
            new Vector2(0, 0),
            false, false, true);

        public static AnimationInfo BunnySwitch = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(768, 704, 64, 64),
            4,
            16,
            new Vector2(0, 0),
            false, false, true);

        public static AnimationInfo SmileyTongue = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(640, 427, 12, 85),
            13,
            118,
            new Vector2(6, 84));

        public static AnimationInfo GroundSpike = new AnimationInfo(
            SmileyTexture.Cornwallis,
            new Rectangle(20, 158, 20, 60),
            5,
            20,
            new Vector2(10, 50));

        public static AnimationInfo PhyreBawz = new AnimationInfo(
            SmileyTexture.Fireboss,
            new Rectangle(0, 0, 97, 158),
            4,
            20,
            new Vector2(48, 79));

        public static AnimationInfo PhyreBawzDownMouth = new AnimationInfo(
            SmileyTexture.Fireboss,
            new Rectangle(0, 158, 32, 17),
            4,
            12,
            new Vector2(0, 0),
            false, false, true);

        public static AnimationInfo PhyreBawzLeftMouth = new AnimationInfo(
            SmileyTexture.Fireboss,
            new Rectangle(0, 175, 27, 13),
            4,
            12,
            new Vector2(0, 0),
            false, false, true);

        public static AnimationInfo PhyreBawzRightMouth = new AnimationInfo(
            SmileyTexture.Fireboss,
            new Rectangle(0, 188, 27, 13),
            4,
            12,
            new Vector2(0, 0),
            false, false, true);

        public static AnimationInfo Owlet = new AnimationInfo(
            SmileyTexture.Garmborn,
            new Rectangle(0, 192, 84, 36),
            4,
            20,
            new Vector2(42, 18),
            false, true, true);

        public static AnimationInfo Bartli = new AnimationInfo(
            SmileyTexture.Bartli,
            new Rectangle(0, 0, 110, 132),
            2,
            20,
            new Vector2(55, 132),
            false, true, true);

        public static AnimationInfo LightningEye = new AnimationInfo(
            SmileyTexture.Magnitogorsk,
            new Rectangle(0, 193, 88, 41),
            5,
            10,
            new Vector2(44, 20),
            false, true, false);

        public static AnimationInfo IceEye = new AnimationInfo(
            SmileyTexture.Magnitogorsk,
            new Rectangle(0, 235, 88, 41),
            5,
            10,
            new Vector2(44, 20),
            false, true, false);

        public static AnimationInfo FireEye = new AnimationInfo(
            SmileyTexture.Magnitogorsk,
            new Rectangle(0, 277, 88, 41),
            5,
            10,
            new Vector2(44, 20),
            false, true, false);

        public static AnimationInfo BarvinoidMouth = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(0, 768, 67, 70),
            9,
            10,
            new Vector2(31, 30));

        public static AnimationInfo EvilEye = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(640, 192, 64, 64),
            5,
            10,
            new Vector2(32, 32));

        public static AnimationInfo Burrow0 = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(640, 320, 64, 64),
            5,
            10,
            new Vector2(32, 32));

        public static AnimationInfo Burrow1 = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(640, 320, 64, 64),
            5,
            10,
            new Vector2(32, 32));

        public static AnimationInfo BombSpawn = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(448, 64, 64, 64),
            8,
            10,
            new Vector2(32, 32));

        public static AnimationInfo BombEyesGlow = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(640, 128, 64, 64),
            6,
            20,
            new Vector2(32, 32),
            false, false, true);

        public static AnimationInfo Batlet = new AnimationInfo(
            SmileyTexture.Animations,
            new Rectangle(640, 384, 82, 35),
            4,
            16,
            new Vector2(32, 32),
            false, false, true);
    }
}
