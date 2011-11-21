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
    public static class Sprites
    {
        public static Sprite Fenwar;
        public static Sprite FenwarOrb;
        public static Sprite FenwarBullet;
        public static Sprite FenwarBulletRed;
        public static Sprite FenwarBomb;

        public static Sprite ManaLoot;
        public static Sprite HealthLoot;
        public static Sprite NewAbilityLoot;

        public static Sprite SessariaMap;
        public static Sprite SalabiaMap;
        public static Sprite FundoriaMap;
        public static Sprite DespairMap;
        public static Sprite PathMap;
        public static Sprite CastleMap;
        public static Sprite FountainMap;
        public static Sprite TutMap;
        public static Sprite ConservatoryMap;
        public static Sprite SmolderMap;
        public static Sprite SessariaMapBorder;
        public static Sprite SalabiaMapBorder;
        public static Sprite FundoriaMapBorder;
        public static Sprite DespairMapBorder;
        public static Sprite PathMapBorder;
        public static Sprite CastleMapBorder;
        public static Sprite FountainMapBorder;
        public static Sprite TutMapBorder;
        public static Sprite ConservatoryMapBorder;
        public static Sprite SmolderMapBorder;
        public static Sprite WorldMapBackgroundTile;

        public static Sprite FountainBottom;
        public static Sprite FountainTop;

        public static Sprite TextBox;
        public static Sprite ArrowIcon;
        public static Sprite OKIcon;
        public static Sprite ExitIcon;

        public static Sprite MouseCursor;
        public static Sprite SillyPad;
        public static Sprite BlueSquare;
        public static Sprite BlackSquare;
        public static Sprite RedSquare;
        public static Sprite YellowSquare;
        public static Sprite WhiteSquare;
        public static Sprite HugeTongue;
        public static Sprite StretchableBlackSquare;
        public static Sprite ReflectionShield;
        public static Sprite IceBlock;
        public static Sprite Loading;
        public static Sprite BillClinton;
        public static Sprite AdviceManUp;
        public static Sprite AdviceManLeft;
        public static Sprite AdviceManDown;
        public static Sprite ParallaxPit;
        public static Sprite AdviceBackground;

        static Sprites()
        {
            Fenwar = new Sprite(SmileyTexture.General, new Rectangle(129, 581, 62, 73), new Vector2(32, 36));
            FenwarOrb = new Sprite(SmileyTexture.General, new Rectangle(386, 130, 60, 60), new Vector2(30, 30));
            FenwarBullet = new Sprite(SmileyTexture.General, new Rectangle(356, 196, 22, 22), new Vector2(11, 11));
            FenwarBulletRed = new Sprite(SmileyTexture.General, new Rectangle(356, 218, 22, 22), new Vector2(11, 11));
            FenwarBomb = new Sprite(SmileyTexture.General, new Rectangle(390, 200, 36, 48), new Vector2(18, 24));

            ManaLoot = new Sprite(SmileyTexture.General, new Rectangle(0, 160, 30, 30), new Vector2(15, 15));
            HealthLoot = new Sprite(SmileyTexture.General, new Rectangle(32, 160, 30, 30), new Vector2(15, 15));
            NewAbilityLoot = new Sprite(SmileyTexture.General, new Rectangle(0, 192, 64, 64), new Vector2(32, 32));

            SessariaMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(0, 0, 243, 121), new Vector2(121, 60));
            SalabiaMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(245, 0, 153, 123), new Vector2(76, 61));
            FundoriaMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(0, 123, 168, 198), new Vector2(84, 99));
            DespairMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(169, 124, 148, 147), new Vector2(74, 73));
            PathMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(320, 125, 108, 96), new Vector2(54, 48));
            CastleMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(320, 223, 107, 81), new Vector2(53, 40));
            FountainMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(170, 273, 128, 70), new Vector2(64, 35));
            TutMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(1, 322, 99, 74), new Vector2(50, 37));
            ConservatoryMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(112, 333, 49, 52), new Vector2(25, 26));
            SmolderMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(1, 397, 120, 112), new Vector2(60, 56));
            SessariaMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(0, 0, 252, 130), new Vector2(126, 65));
            SalabiaMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(253, 0, 161, 132), new Vector2(80, 66));
            FundoriaMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(1, 131, 177, 206), new Vector2(88, 103));
            DespairMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(179, 133, 156, 155), new Vector2(78, 72));
            PathMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(336, 133, 119, 104), new Vector2(59, 52));
            ConservatoryMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(415, 1, 57, 60), new Vector2(28, 30));
            FountainMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(179, 289, 136, 78), new Vector2(68, 37));
            CastleMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(336, 238, 116, 89), new Vector2(58, 44));
            SmolderMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(1, 338, 128, 121), new Vector2(64, 60));
            TutMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(130, 368, 107, 82), new Vector2(53, 41));
            WorldMapBackgroundTile = new Sprite(SmileyTexture.WorldMap, new Rectangle(175, 352, 128, 128), new Vector2(53, 41));

            FountainBottom = new Sprite(SmileyTexture.Fountain, new Rectangle(0, 380, 340, 172), new Vector2(170, 120));
            FountainTop = new Sprite(SmileyTexture.Fountain, new Rectangle(340, 380, 118, 173), new Vector2(59, 120));

            TextBox = new Sprite(SmileyTexture.General, new Rectangle(0, 256, 400, 250), new Vector2(0, 0));
            ArrowIcon = new Sprite(SmileyTexture.General, new Rectangle(128, 142, 32, 20), new Vector2(0, 0));
            OKIcon = new Sprite(SmileyTexture.General, new Rectangle(160, 142, 32, 20), new Vector2(0, 0));
            ExitIcon = new Sprite(SmileyTexture.General, new Rectangle(206, 64, 40, 40), new Vector2(0, 0));

            MouseCursor = new Sprite(SmileyTexture.General, new Rectangle(96, 96, 32, 32), new Vector2(1, 1));
            SillyPad = new Sprite(SmileyTexture.General, new Rectangle(576, 0, 64, 64), new Vector2(0, 0));
            BlueSquare = new Sprite(SmileyTexture.General, new Rectangle(832, 64, 64, 64), new Vector2(0, 0));
            BlackSquare = new Sprite(SmileyTexture.General, new Rectangle(896, 64, 64, 64), new Vector2(0, 0));
            RedSquare = new Sprite(SmileyTexture.General, new Rectangle(960, 64, 64, 64), new Vector2(0, 0));
            YellowSquare = new Sprite(SmileyTexture.General, new Rectangle(832, 128, 64, 64), new Vector2(0, 0));
            WhiteSquare = new Sprite(SmileyTexture.General, new Rectangle(320, 128, 64, 64), new Vector2(0, 0));
            HugeTongue = new Sprite(SmileyTexture.General, new Rectangle(21, 660, 524, 227), new Vector2(0, 113));
            StretchableBlackSquare = new Sprite(SmileyTexture.General, new Rectangle(897, 65, 62, 62), new Vector2(0, 0));
            ReflectionShield = new Sprite(SmileyTexture.General, new Rectangle(0, 64, 96, 96), new Vector2(48, 48));
            IceBlock = new Sprite(SmileyTexture.General, new Rectangle(128, 0, 64, 64), new Vector2(32, 32));
            Loading = new Sprite(SmileyTexture.General, new Rectangle(623, 623, 400, 400), new Vector2(200, 200));
            BillClinton = new Sprite(SmileyTexture.General, new Rectangle(0, 512, 64, 64), new Vector2(32, 32));
            AdviceManUp = new Sprite(SmileyTexture.General, new Rectangle(192, 834, 64, 64), new Vector2(32, 32));
            AdviceManLeft = new Sprite(SmileyTexture.General, new Rectangle(64, 834, 64, 64), new Vector2(32, 22));
            AdviceManDown = new Sprite(SmileyTexture.General, new Rectangle(0, 834, 64, 64), new Vector2(32, 32));
            ParallaxPit = new Sprite(SmileyTexture.General, new Rectangle(960, 0, 64, 64), new Vector2(0, 0));
            AdviceBackground = new Sprite(SmileyTexture.UserInterface, new Rectangle(692, 396, 130, 30), new Vector2(0, 0));
        }
    }
}
