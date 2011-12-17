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
        //Loot
        public static Sprite ManaLoot = new Sprite(SmileyTexture.General, new Rectangle(0, 160, 30, 30), new Vector2(15, 15));
        public static Sprite HealthLoot = new Sprite(SmileyTexture.General, new Rectangle(32, 160, 30, 30), new Vector2(15, 15));
        public static Sprite NewAbilityLoot = new Sprite(SmileyTexture.General, new Rectangle(0, 192, 64, 64), new Vector2(32, 32));

        //World Map
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

        //Options Screen
        public static Sprite OptionsBackground = new Sprite(SmileyTexture.UserInterface, new Rectangle(0, 0, 660, 492));
        public static Sprite SliderBar = new Sprite(SmileyTexture.UserInterface, new Rectangle(660, 0, 93, 21));
        public static Sprite OptionsPatch = new Sprite(SmileyTexture.UserInterface, new Rectangle(30, 30, 286, 246));

        //TextBox
        public static Sprite TextBox = new Sprite(SmileyTexture.General, new Rectangle(0, 256, 400, 250), new Vector2(0, 0));
        public static Sprite ArrowIcon = new Sprite(SmileyTexture.General, new Rectangle(128, 142, 32, 20), new Vector2(0, 0));
        public static Sprite OKIcon = new Sprite(SmileyTexture.General, new Rectangle(160, 142, 32, 20), new Vector2(0, 0));
        public static Sprite ExitIcon = new Sprite(SmileyTexture.General, new Rectangle(206, 64, 40, 40), new Vector2(0, 0));

        //Miscellaneous
        public static Sprite FountainBottom = new Sprite(SmileyTexture.Fountain, new Rectangle(0, 380, 340, 172), new Vector2(170, 120));
        public static Sprite FountainTop = new Sprite(SmileyTexture.Fountain, new Rectangle(340, 380, 118, 173), new Vector2(59, 120));
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

        //Main Menu
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
        public static Sprite ControlsBox = new Sprite(SmileyTexture.TitleScreen, new Rectangle(248, 769, 130, 30));
        public static Sprite SelectedControlsBox = new Sprite(SmileyTexture.TitleScreen, new Rectangle(248, 799, 130, 30));
        public static Sprite DifficultyPromptBackground = new Sprite(SmileyTexture.UserInterface, new Rectangle(661, 809, 325, 150));
        public static Sprite LeftArrow = new Sprite(SmileyTexture.UserInterface, new Rectangle(989, 810, 32, 32), new Vector2(16, 16));
        public static Sprite RightArrow = new Sprite(SmileyTexture.UserInterface, new Rectangle(989, 844, 32, 32), new Vector2(16, 16));
        public static Sprite OKButton = new Sprite(SmileyTexture.UserInterface, new Rectangle(989, 878, 32, 32), new Vector2(16, 16));
        public static Sprite LeftArrowHighlighted = new Sprite(SmileyTexture.UserInterface, new Rectangle(989, 912, 32, 32), new Vector2(16, 16));
        public static Sprite RightArrowHighlighted = new Sprite(SmileyTexture.UserInterface, new Rectangle(989, 946, 32, 32), new Vector2(16, 16));
        public static Sprite OKButtonHighlighted = new Sprite(SmileyTexture.UserInterface, new Rectangle(989, 980, 32, 32), new Vector2(16, 16));
        public static Sprite SmileyWithoutHat = new Sprite(SmileyTexture.UserInterface, new Rectangle(3, 590, 53, 42));
        public static Sprite SmileysHat = new Sprite(SmileyTexture.UserInterface, new Rectangle(71, 594, 51, 34));

        //Opening cinematic
        public static Sprite OpenCinematicOne = new Sprite(SmileyTexture.OpeningCinematic, new Rectangle(0, 0, 440, 340), new Vector2(220, 170));
        public static Sprite OpenCinematicTwo = new Sprite(SmileyTexture.OpeningCinematic, new Rectangle(440, 0, 440, 340), new Vector2(220, 170));
        public static Sprite OpenCinematicThree = new Sprite(SmileyTexture.OpeningCinematic, new Rectangle(0, 340, 440, 340), new Vector2(220, 170));
        public static Sprite OpenCinematicFour = new Sprite(SmileyTexture.OpeningCinematic, new Rectangle(0, 680, 440, 340), new Vector2(220, 215));
        public static Sprite OpenCinematicFive = new Sprite(SmileyTexture.OpeningCinematic, new Rectangle(440, 340, 440, 400), new Vector2(220, 200));
        public static Sprite OpenCinematicSix = new Sprite(SmileyTexture.OpeningCinematic, new Rectangle(440, 740, 182, 182), new Vector2(91, 91));

        //Minimap
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

        //GUI
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

        //Player
        public static Sprite PlayerShadow = new Sprite(SmileyTexture.General, new Rectangle(128, 162, 64, 15), new Vector2(32, 7));
        public static Sprite SmileysFace = new Sprite(SmileyTexture.General, new Rectangle(0, 506, 61, 72), new Vector2(30, 36));
        public static Sprite JesusBeam = new Sprite(SmileyTexture.General, new Rectangle(960, 201, 62, 421), new Vector2(30, 387));

        //Inventory
        public static Sprite InventoryCursor = new Sprite(SmileyTexture.General, new Rectangle(129, 65, 76, 76));
        public static Sprite Inventory = new Sprite(SmileyTexture.UserInterface, new Rectangle(0, 492, 660, 492));
        public static Sprite SelectedAbilityCheck = new Sprite(SmileyTexture.General, new Rectangle(209, 106, 20, 20), new Vector2(10, 10));
        public static Sprite SelectedKeys = new Sprite(SmileyTexture.UserInterface, new Rectangle(661, 559, 73, 249));
        public static Sprite WindowArrowLeft = new Sprite(SmileyTexture.UserInterface, new Rectangle(917, 3, 17, 104), new Vector2(9, 52));
        public static Sprite WindowArrowRight = new Sprite(SmileyTexture.UserInterface, new Rectangle(938, 3, 17, 104), new Vector2(9, 52));

        //Cornwallis
        public static Sprite CornwallisBody = new Sprite(SmileyTexture.Cornwallis, new Rectangle(0, 0, 107, 158), new Vector2(53, 78));
        public static Sprite RedCornwallisBody = new Sprite(SmileyTexture.Cornwallis, new Rectangle(107, 0, 107, 158), new Vector2(53, 78));
        public static Sprite SpikeShadow = new Sprite(SmileyTexture.Cornwallis, new Rectangle(0, 158, 20, 20), new Vector2(10, 10));

        //Phyrebozz
        public static Sprite FlameLauncher = new Sprite(SmileyTexture.Fireboss, new Rectangle(129, 158, 64, 64), new Vector2(32, 32));

        //Portly Penguin
        public static Sprite PenguinBody = new Sprite(SmileyTexture.PortlyPenguin, new Rectangle(0, 0, 128, 128), new Vector2(64, 64));
        public static Sprite PenguinSliding = new Sprite(SmileyTexture.PortlyPenguin, new Rectangle(128, 0, 128, 128), new Vector2(64, 64));
        public static Sprite PenguinDrowning = new Sprite(SmileyTexture.PortlyPenguin, new Rectangle(256, 0, 128, 128), new Vector2(64, 64));
        public static Sprite PenguinIceBlock = new Sprite(SmileyTexture.PortlyPenguin, new Rectangle(48, 128, 64, 64));
        public static Sprite FishProjectile = new Sprite(SmileyTexture.PortlyPenguin, new Rectangle(0, 128, 48, 32), new Vector2(24, 16));

        //Brian Fungus
        public static Sprite Mushboom = new Sprite(SmileyTexture.BrianFungus, new Rectangle(0, 0, 115, 105), new Vector2(57, 52));
        public static Sprite MushboomRightArm = new Sprite(SmileyTexture.BrianFungus, new Rectangle(0, 105, 39, 25));
        public static Sprite MushboomLeftArm = new Sprite(SmileyTexture.BrianFungus, new Rectangle(0, 131, 39, 25), new Vector2(0, 24));
        public static Sprite MushboomBomb = new Sprite(SmileyTexture.BrianFungus, new Rectangle(0, 156, 35, 46), new Vector2(18, 29));
        public static Sprite MushboomBombShadow = new Sprite(SmileyTexture.BrianFungus, new Rectangle(36, 190, 35, 12), new Vector2(18, -9));
        public static Sprite MushroomletProjectile = new Sprite(SmileyTexture.BrianFungus, new Rectangle(55, 105, 60, 65), new Vector2(30, 33));

        //Garmborn
        public static Sprite GarmbornBody = new Sprite(SmileyTexture.Garmborn, new Rectangle(0, 0, 192, 192), new Vector2(96, 96));
        public static Sprite Treelet = new Sprite(SmileyTexture.Garmborn, new Rectangle(192, 0, 128, 128), new Vector2(64, 64));
        public static Sprite GrayTreelet = new Sprite(SmileyTexture.Garmborn, new Rectangle(320, 0, 128, 128), new Vector2(64, 64));

        //Calypso
        public static Sprite Calypso = new Sprite(SmileyTexture.Calypso, new Rectangle(0, 0, 160, 160), new Vector2(80, 80));
        public static Sprite EvilCalypso = new Sprite(SmileyTexture.Calypso, new Rectangle(0, 160, 160, 160), new Vector2(80, 80));
        public static Sprite CalypsoShield = new Sprite(SmileyTexture.Calypso, new Rectangle(160, 0, 180, 180), new Vector2(90, 90));

        //Bartli
        public static Sprite BartliArm = new Sprite(SmileyTexture.Bartli, new Rectangle(31, 147, 40, 15), new Vector2(39, 7));
        public static Sprite BartliLeg = new Sprite(SmileyTexture.Bartli, new Rectangle(5, 143, 17, 26), new Vector2(8, 0));
        public static Sprite BartliShadow = new Sprite(SmileyTexture.Bartli, new Rectangle(73, 144, 92, 18), new Vector2(46, 9));
        public static Sprite BartletBlue = new Sprite(SmileyTexture.Bartli, new Rectangle(2, 170, 64, 54), new Vector2(32, 27));
        public static Sprite BartletRed = new Sprite(SmileyTexture.Bartli, new Rectangle(67, 170, 64, 54), new Vector2(32, 27));

        //Tut
        public static Sprite KingTut = new Sprite(SmileyTexture.Tut, new Rectangle(0, 0, 68, 227), new Vector2(34, 113));
        public static Sprite KingTutShadow = new Sprite(SmileyTexture.Tut, new Rectangle(68, 0, 68, 227), new Vector2(34, 113));
        public static Sprite KingTutInsideSarcophagus = new Sprite(SmileyTexture.Tut, new Rectangle(136, 0, 68, 227));
        public static Sprite KingTutLightningWedge = new Sprite(SmileyTexture.Tut, new Rectangle(206, 0, 620, 484), new Vector2(0, 242));

        //Magnitogorsk
        public static Sprite TentacleShadow = new Sprite(SmileyTexture.Magnitogorsk, new Rectangle(1, 319, 45, 9), new Vector2(23, 4));
        public static Sprite LovecraftIceBlock = new Sprite(SmileyTexture.Magnitogorsk, new Rectangle(0, 329, 512, 60), new Vector2(0, 30));

        //Barvinoid
        public static Sprite Barvinoid = new Sprite(SmileyTexture.Barvinoid, new Rectangle(0, 0, 283, 355), new Vector2(141, 289));
        public static Sprite BarvinoidRightEye = new Sprite(SmileyTexture.Barvinoid, new Rectangle(283, 0, 54, 51), new Vector2(133, 273));
        public static Sprite BarvinoidLeftEye = new Sprite(SmileyTexture.Barvinoid, new Rectangle(283, 52, 54, 51), new Vector2(-80, 273));
        public static Sprite BarvinoidMinion = new Sprite(SmileyTexture.Barvinoid, new Rectangle(283, 103, 45, 39), new Vector2(23, 20));
        public static Sprite BarvinoidMinionShadow = new Sprite(SmileyTexture.Barvinoid, new Rectangle(283, 206, 45, 39), new Vector2(23, 20));
        public static Sprite BarvinoidShadow = new Sprite(SmileyTexture.Barvinoid, new Rectangle(0, 356, 244, 148), new Vector2(119, 81));
        public static Sprite CrossHair = new Sprite(SmileyTexture.Barvinoid, new Rectangle(283, 245, 125, 124), new Vector2(64, 64));
        public static Sprite FloatingEyeShot = new Sprite(SmileyTexture.Barvinoid, new Rectangle(283, 142, 64, 64), new Vector2(32, 32));

        //Fenwar
        public static Sprite Fenwar = new Sprite(SmileyTexture.General, new Rectangle(129, 581, 62, 73), new Vector2(32, 36));
        public static Sprite FenwarOrb = new Sprite(SmileyTexture.General, new Rectangle(386, 130, 60, 60), new Vector2(30, 30));
        public static Sprite FenwarBullet = new Sprite(SmileyTexture.General, new Rectangle(356, 196, 22, 22), new Vector2(11, 11));
        public static Sprite FenwarBulletRed = new Sprite(SmileyTexture.General, new Rectangle(356, 218, 22, 22), new Vector2(11, 11));
        public static Sprite FenwarBomb = new Sprite(SmileyTexture.General, new Rectangle(390, 200, 36, 48), new Vector2(18, 24));

        //Projectiles
        public static Sprite BasicProjectile = new Sprite(SmileyTexture.Projectiles, new Rectangle(0, 0, 20, 20), new Vector2(10, 10));
        public static Sprite FrisbeeProjectile = new Sprite(SmileyTexture.Projectiles, new Rectangle(1, 21, 62, 62), new Vector2(31, 31));
        public static Sprite SpikeProjectile = new Sprite(SmileyTexture.Projectiles, new Rectangle(0, 85, 64, 14), new Vector2(32, 7));
        public static Sprite LightningOrbProjectile = new Sprite(SmileyTexture.Projectiles, new Rectangle(0, 98, 31, 31), new Vector2(16, 16));
        public static Sprite CannonBallProjectile = new Sprite(SmileyTexture.Projectiles, new Rectangle(32, 98, 32, 32), new Vector2(16, 16));
        public static Sprite LaserProjectile = new Sprite(SmileyTexture.Projectiles, new Rectangle(64, 0, 5, 130), new Vector2(2, 65));
        public static Sprite TutProjectile = new Sprite(SmileyTexture.Projectiles, new Rectangle(0, 130, 64, 20), new Vector2(32, 10));
        public static Sprite TutProjectileMummy = new Sprite(SmileyTexture.Projectiles, new Rectangle(0, 148, 64, 64), new Vector2(32, 32));
        public static Sprite CandyProjectile = new Sprite(SmileyTexture.General, new Rectangle(196, 144, 58, 45), new Vector2(29, 22));
        public static Sprite Figure8Projectile = new Sprite(SmileyTexture.Projectiles, new Rectangle(1, 215, 30, 30), new Vector2(15, 15));
        public static Sprite SlimeProjectile = new Sprite(SmileyTexture.Projectiles, new Rectangle(129, 221, 24, 34), new Vector2(12, 17));
        public static Sprite OrangeProjectile = new Sprite(SmileyTexture.Projectiles, new Rectangle(21, 0, 16, 16), new Vector2(7, 7));
        public static Sprite BoomerangProjectile = new Sprite(SmileyTexture.Projectiles, new Rectangle(32, 214, 32, 32), new Vector2(16, 16));
        public static Sprite SkullProjectile = new Sprite(SmileyTexture.Projectiles, new Rectangle(25, 247, 23, 30), new Vector2(11, 15));
        public static Sprite AcornProjectile = new Sprite(SmileyTexture.Projectiles, new Rectangle(1, 247, 21, 23), new Vector2(10, 11));

        //Enemies
        public static Sprite BombRedCircle = new Sprite(SmileyTexture.General, new Rectangle(640, 128, 192, 192), new Vector2(96, 96));
        public static Sprite ClownChainDow = new Sprite(SmileyTexture.Animations, new Rectangle(960, 64, 16, 16), new Vector2(8, 8));
        public static Sprite ClownHead = new Sprite(SmileyTexture.Animations, new Rectangle(960, 192, 64, 64), new Vector2(32, 32));
        public static Sprite BuzzardWing = new Sprite(SmileyTexture.General, new Rectangle(154, 221, 98, 27), new Vector2(100, 14));
        public static Sprite FlailLink = new Sprite(SmileyTexture.General, new Rectangle(80, 0, 16, 16), new Vector2(8, 8));
        public static Sprite FlailHead = new Sprite(SmileyTexture.General, new Rectangle(0, 0, 64, 64), new Vector2(32, 32));
        public static Sprite StunStar = new Sprite(SmileyTexture.General, new Rectangle(64, 16, 10, 10), new Vector2(5, 5));
        public static Sprite EvilWall = new Sprite(SmileyTexture.General, new Rectangle(256, 64, 64, 64), new Vector2(32, 32));
        public static Sprite EvilWallSpike = new Sprite(SmileyTexture.General, new Rectangle(320, 64, 64, 64), new Vector2(32, 32));

        //Particles
        public static Sprite BloodSplat = new Sprite(SmileyTexture.Particles, new Rectangle(0, 96, 32, 32), new Vector2(16, 16));
        public static Sprite Shockwave = new Sprite(SmileyTexture.Particles, new Rectangle(0, 96, 32, 32), new Vector2(16, 16));
    }
}
