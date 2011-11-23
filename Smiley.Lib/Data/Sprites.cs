using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework.Drawing;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Data
{
    public static class Sprites
    {
        public static Sprite Fenwar = new Sprite(SmileyTexture.General, new Rectangle(129, 581, 62, 73), new Vector2(32, 36));
        public static Sprite FenwarOrb = new Sprite(SmileyTexture.General, new Rectangle(386, 130, 60, 60), new Vector2(30, 30));
        public static Sprite FenwarBullet = new Sprite(SmileyTexture.General, new Rectangle(356, 196, 22, 22), new Vector2(11, 11));
        public static Sprite FenwarBulletRed = new Sprite(SmileyTexture.General, new Rectangle(356, 218, 22, 22), new Vector2(11, 11));
        public static Sprite FenwarBomb = new Sprite(SmileyTexture.General, new Rectangle(390, 200, 36, 48), new Vector2(18, 24));

        public static Sprite ManaLoot = new Sprite(SmileyTexture.General, new Rectangle(0, 160, 30, 30), new Vector2(15, 15));
        public static Sprite HealthLoot = new Sprite(SmileyTexture.General, new Rectangle(32, 160, 30, 30), new Vector2(15, 15));
        public static Sprite NewAbilityLoot = new Sprite(SmileyTexture.General, new Rectangle(0, 192, 64, 64), new Vector2(32, 32));

        public static Sprite SessariaMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(0, 0, 243, 121), new Vector2(121, 60));
        public static Sprite SalabiaMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(245, 0, 153, 123), new Vector2(76, 61));
        public static Sprite FundoriaMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(0, 123, 168, 198), new Vector2(84, 99));
        public static Sprite DespairMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(169, 124, 148, 147), new Vector2(74, 73));
        public static Sprite PathMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(320, 125, 108, 96), new Vector2(54, 48));
        public static Sprite CastleMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(320, 223, 107, 81), new Vector2(53, 40));
        public static Sprite FountainMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(170, 273, 128, 70), new Vector2(64, 35));
        public static Sprite TutMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(1, 322, 99, 74), new Vector2(50, 37));
        public static Sprite ConservatoryMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(112, 333, 49, 52), new Vector2(25, 26));
        public static Sprite SmolderMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(1, 397, 120, 112), new Vector2(60, 56));
        public static Sprite SessariaMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(0, 0, 252, 130), new Vector2(126, 65));
        public static Sprite SalabiaMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(253, 0, 161, 132), new Vector2(80, 66));
        public static Sprite FundoriaMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(1, 131, 177, 206), new Vector2(88, 103));
        public static Sprite DespairMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(179, 133, 156, 155), new Vector2(78, 72));
        public static Sprite PathMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(336, 133, 119, 104), new Vector2(59, 52));
        public static Sprite ConservatoryMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(415, 1, 57, 60), new Vector2(28, 30));
        public static Sprite FountainMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(179, 289, 136, 78), new Vector2(68, 37));
        public static Sprite CastleMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(336, 238, 116, 89), new Vector2(58, 44));
        public static Sprite SmolderMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(1, 338, 128, 121), new Vector2(64, 60));
        public static Sprite TutMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(130, 368, 107, 82), new Vector2(53, 41));
        public static Sprite WorldMapBackgroundTile = new Sprite(SmileyTexture.WorldMap, new Rectangle(175, 352, 128, 128), new Vector2(53, 41));

        public static Sprite FountainBottom = new Sprite(SmileyTexture.Fountain, new Rectangle(0, 380, 340, 172), new Vector2(170, 120));
        public static Sprite FountainTop = new Sprite(SmileyTexture.Fountain, new Rectangle(340, 380, 118, 173), new Vector2(59, 120));

        public static Sprite TextBox = new Sprite(SmileyTexture.General, new Rectangle(0, 256, 400, 250), new Vector2(0, 0));
        public static Sprite ArrowIcon = new Sprite(SmileyTexture.General, new Rectangle(128, 142, 32, 20), new Vector2(0, 0));
        public static Sprite OKIcon = new Sprite(SmileyTexture.General, new Rectangle(160, 142, 32, 20), new Vector2(0, 0));
        public static Sprite ExitIcon = new Sprite(SmileyTexture.General, new Rectangle(206, 64, 40, 40), new Vector2(0, 0));

        public static Sprite MouseCursor = new Sprite(SmileyTexture.General, new Rectangle(96, 96, 32, 32), new Vector2(1, 1));
        public static Sprite SillyPad = new Sprite(SmileyTexture.General, new Rectangle(576, 0, 64, 64), new Vector2(0, 0));
        public static Sprite BlueSquare = new Sprite(SmileyTexture.General, new Rectangle(832, 64, 64, 64), new Vector2(0, 0));
        public static Sprite BlackSquare = new Sprite(SmileyTexture.General, new Rectangle(896, 64, 64, 64), new Vector2(0, 0));
        public static Sprite RedSquare = new Sprite(SmileyTexture.General, new Rectangle(960, 64, 64, 64), new Vector2(0, 0));
        public static Sprite YellowSquare = new Sprite(SmileyTexture.General, new Rectangle(832, 128, 64, 64), new Vector2(0, 0));
        public static Sprite WhiteSquare = new Sprite(SmileyTexture.General, new Rectangle(320, 128, 64, 64), new Vector2(0, 0));
        public static Sprite HugeTongue = new Sprite(SmileyTexture.General, new Rectangle(21, 660, 524, 227), new Vector2(0, 113));
        public static Sprite StretchableBlackSquare = new Sprite(SmileyTexture.General, new Rectangle(897, 65, 62, 62), new Vector2(0, 0));
        public static Sprite ReflectionShield = new Sprite(SmileyTexture.General, new Rectangle(0, 64, 96, 96), new Vector2(48, 48));
        public static Sprite IceBlock = new Sprite(SmileyTexture.General, new Rectangle(128, 0, 64, 64), new Vector2(32, 32));
        public static Sprite Loading = new Sprite(SmileyTexture.General, new Rectangle(623, 623, 400, 400), new Vector2(200, 200));
        public static Sprite BillClinton = new Sprite(SmileyTexture.General, new Rectangle(0, 512, 64, 64), new Vector2(32, 32));
        public static Sprite AdviceManUp = new Sprite(SmileyTexture.General, new Rectangle(192, 834, 64, 64), new Vector2(32, 32));
        public static Sprite AdviceManLeft = new Sprite(SmileyTexture.General, new Rectangle(64, 834, 64, 64), new Vector2(32, 22));
        public static Sprite AdviceManDown = new Sprite(SmileyTexture.General, new Rectangle(0, 834, 64, 64), new Vector2(32, 32));
        public static Sprite ParallaxPit = new Sprite(SmileyTexture.General, new Rectangle(960, 0, 64, 64), new Vector2(0, 0));
        public static Sprite AdviceBackground = new Sprite(SmileyTexture.UserInterface, new Rectangle(692, 396, 130, 30), new Vector2(0, 0));

        public static Sprite MenuBackground = new Sprite(SmileyTexture.TitleScreen, new Rectangle(0, 0, 1024, 768), new Vector2(0, 0));
        public static Sprite MenuSpeechBubble = new Sprite(SmileyTexture.UserInterface, new Rectangle(0, 769, 245, 73));
        public static Sprite ButtonBackground = new Sprite(SmileyTexture.UserInterface, new Rectangle(661, 22, 246, 71));
        public static Sprite ButtonBackgroundHighlighted = new Sprite(SmileyTexture.UserInterface, new Rectangle(661, 93, 246, 71));
        public static Sprite SmileyTitle = new Sprite(SmileyTexture.TitleScreen, new Rectangle(380, 769, 430, 214), new Vector2(215, 104));
        public static Sprite LoadingText = new Sprite(SmileyTexture.UserInterface, new Rectangle(1, 844, 334, 88), new Vector2(168, 45));
        public static Sprite GameOverG = new Sprite(SmileyTexture.UserInterface, new Rectangle(811, 769, 48, 58), new Vector2(170, 24));
        public static Sprite GameOverA = new Sprite(SmileyTexture.UserInterface, new Rectangle(863, 769, 48, 63), new Vector2(127, 30));
        public static Sprite GameOverM = new Sprite(SmileyTexture.UserInterface, new Rectangle(913, 769, 52, 68), new Vector2(88, 36));
        public static Sprite GameOverE1 = new Sprite(SmileyTexture.UserInterface, new Rectangle(967, 769, 35, 69), new Vector2(38, 37));
        public static Sprite GameOverO = new Sprite(SmileyTexture.UserInterface, new Rectangle(811, 833, 41, 72), new Vector2(17, 37));
        public static Sprite GameOverV = new Sprite(SmileyTexture.UserInterface, new Rectangle(852, 833, 45, 69), new Vector2(54, 35));
        public static Sprite GameOverE2 = new Sprite(SmileyTexture.UserInterface, new Rectangle(897, 838, 46, 64), new Vector2(87, 29));
        public static Sprite GameOverR = new Sprite(SmileyTexture.UserInterface, new Rectangle(943, 839, 53, 59), new Vector2(53, 59));
        public static Sprite GameOverText = new Sprite(SmileyTexture.UserInterface, new Rectangle(0, 934, 340, 74), new Vector2(170, 37));
        public static Sprite ControlsBox = new Sprite(SmileyTexture.UserInterface, new Rectangle(248, 769, 130, 30));
        public static Sprite SelectedControlsBox = new Sprite(SmileyTexture.UserInterface, new Rectangle(248, 799, 130, 30));
        public static Sprite DifficultyPromptBackground = new Sprite(SmileyTexture.UserInterface, new Rectangle(661, 809, 325, 150));
        public static Sprite LeftArrow = new Sprite(SmileyTexture.UserInterface, new Rectangle(989, 810, 32, 32));
        public static Sprite RightArrow = new Sprite(SmileyTexture.UserInterface, new Rectangle(989, 844, 32, 32));
        public static Sprite OKButton = new Sprite(SmileyTexture.UserInterface, new Rectangle(989, 878, 32, 32));
        public static Sprite LeftArrowHighlighted = new Sprite(SmileyTexture.UserInterface, new Rectangle(989, 912, 32, 32));
        public static Sprite RightArrowHighlighted = new Sprite(SmileyTexture.UserInterface, new Rectangle(989, 946, 32, 32));
        public static Sprite OKButtonHighlighted = new Sprite(SmileyTexture.UserInterface, new Rectangle(989, 980, 32, 32));
        public static Sprite SmileyWithoutHat = new Sprite(SmileyTexture.UserInterface, new Rectangle(3, 590, 53, 42));
        public static Sprite SmileysHat = new Sprite(SmileyTexture.UserInterface, new Rectangle(71, 594, 51, 34));

        public static Sprite OpenCinematicOne = new Sprite(SmileyTexture.OpeningCinematic, new Rectangle(0, 0, 440, 340), new Vector2(220, 170));
        public static Sprite OpenCinematicTwo = new Sprite(SmileyTexture.OpeningCinematic, new Rectangle(440, 0, 440, 340), new Vector2(220, 170));
        public static Sprite OpenCinematicThree = new Sprite(SmileyTexture.OpeningCinematic, new Rectangle(0, 340, 440, 340), new Vector2(220, 170));
        public static Sprite OpenCinematicFour = new Sprite(SmileyTexture.OpeningCinematic, new Rectangle(0, 680, 440, 340), new Vector2(220, 215));
        public static Sprite OpenCinematicFive = new Sprite(SmileyTexture.OpeningCinematic, new Rectangle(440, 340, 440, 400), new Vector2(220, 200));
        public static Sprite OpenCinematicSix = new Sprite(SmileyTexture.OpeningCinematic, new Rectangle(440, 740, 182, 182), new Vector2(91, 91));

        public static Sprite FogLeft = new Sprite(SmileyTexture.General, new Rectangle(384, 64, 64, 64));
        public static Sprite FogRight = new Sprite(SmileyTexture.General, new Rectangle(448, 64, 64, 64));
        public static Sprite FogUp = new Sprite(SmileyTexture.General, new Rectangle(512, 64, 64, 64));
        public static Sprite FogDown = new Sprite(SmileyTexture.General, new Rectangle(576, 64, 64, 64));
        public static Sprite FogUpLeft = new Sprite(SmileyTexture.General, new Rectangle(640, 64, 32, 32));
        public static Sprite FogUpRight = new Sprite(SmileyTexture.General, new Rectangle(672, 64, 32, 32));
        public static Sprite FogDownRight = new Sprite(SmileyTexture.General, new Rectangle(672, 96, 32, 32));
        public static Sprite FogDownLeft = new Sprite(SmileyTexture.General, new Rectangle(640, 96, 32, 32));
        public static Sprite MapTopBorder = new Sprite(SmileyTexture.UserInterface, new Rectangle(0, 0, 660, 30));
        public static Sprite MapLeftBorder = new Sprite(SmileyTexture.UserInterface, new Rectangle(0, 30, 30, 432));
        public static Sprite RightBorder = new Sprite(SmileyTexture.UserInterface, new Rectangle(630, 30, 30, 432));
        public static Sprite BottomBorder = new Sprite(SmileyTexture.UserInterface, new Rectangle(0, 462, 660, 30));
        public static Sprite MiniMapNoCollision = new Sprite(SmileyTexture.General, new Rectangle(768, 64, 16, 16));
        public static Sprite MiniMapCollision = new Sprite(SmileyTexture.General, new Rectangle(768, 64, 16, 16));
        public static Sprite MiniMapBlueSquare = new Sprite(SmileyTexture.General, new Rectangle(832, 64, 16, 16));
        public static Sprite MiniMapBlackSquare = new Sprite(SmileyTexture.General, new Rectangle(896, 64, 16, 16));
        public static Sprite MiniMapRedSquare = new Sprite(SmileyTexture.General, new Rectangle(960, 64, 16, 16));

        public static Sprite AbilityBackground = new Sprite(SmileyTexture.UserInterface, new Rectangle(661, 427, 146, 131));
        public static Sprite ManaBarBackgroundLeftTip = new Sprite(SmileyTexture.UserInterface, new Rectangle(661, 282, 6, 22));
        public static Sprite ManaBarBackgroundRightTip = new Sprite(SmileyTexture.UserInterface, new Rectangle(668, 282, 6, 22));
        public static Sprite ManaBarBackgroundCenter = new Sprite(SmileyTexture.UserInterface, new Rectangle(675, 282, 349, 22));
        public static Sprite ManaBar = new Sprite(SmileyTexture.UserInterface, new Rectangle(661, 304, 360, 16));
        public static Sprite MoneyIcon = new Sprite(SmileyTexture.UserInterface, new Rectangle(746, 196, 48, 48));
        public static Sprite EmptyHealth = new Sprite(SmileyTexture.UserInterface, new Rectangle(661, 166, 30, 30));
        public static Sprite QuarterHealth = new Sprite(SmileyTexture.UserInterface, new Rectangle(691, 166, 30, 30));
        public static Sprite HalfHealth = new Sprite(SmileyTexture.UserInterface, new Rectangle(721, 166, 30, 30));
        public static Sprite ThreeQuartersHealth = new Sprite(SmileyTexture.UserInterface, new Rectangle(751, 166, 30, 30));
        public static Sprite FullHealth = new Sprite(SmileyTexture.UserInterface, new Rectangle(781, 166, 30, 30));
        public static Sprite BossHealthBackground = new Sprite(SmileyTexture.UserInterface, new Rectangle(661, 320, 256, 43));
        public static Sprite BossHealthBar = new Sprite(SmileyTexture.UserInterface, new Rectangle(661, 363, 230, 32));
        public static Sprite KeyBackground = new Sprite(SmileyTexture.UserInterface, new Rectangle(742, 751, 276, 54));

        public static Sprite PlayerShadow = new Sprite(SmileyTexture.General, new Rectangle(128, 162, 64, 15), new Vector2(32, 7));
        public static Sprite SmileysFace = new Sprite(SmileyTexture.General, new Rectangle(0, 506, 61, 72), new Vector2(30, 36));
        public static Sprite JesusBeam = new Sprite(SmileyTexture.General, new Rectangle(960, 201, 62, 421), new Vector2(30, 387));
    }
}
