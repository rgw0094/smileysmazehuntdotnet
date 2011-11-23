using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework.Drawing;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Data
{
    public partial class SmileyData
    {
        public Sprite Sprite_Fenwar;
        public Sprite Sprite_FenwarOrb;
        public Sprite Sprite_FenwarBullet;
        public Sprite Sprite_FenwarBulletRed;
        public Sprite Sprite_FenwarBomb;

        public Sprite Sprite_ManaLoot;
        public Sprite Sprite_HealthLoot;
        public Sprite Sprite_NewAbilityLoot;

        public Sprite Sprite_SessariaMap;
        public Sprite Sprite_SalabiaMap;
        public Sprite Sprite_FundoriaMap;
        public Sprite Sprite_DespairMap;
        public Sprite Sprite_PathMap;
        public Sprite Sprite_CastleMap;
        public Sprite Sprite_FountainMap;
        public Sprite Sprite_TutMap;
        public Sprite Sprite_ConservatoryMap;
        public Sprite Sprite_SmolderMap;
        public Sprite Sprite_SessariaMapBorder;
        public Sprite Sprite_SalabiaMapBorder;
        public Sprite Sprite_FundoriaMapBorder;
        public Sprite Sprite_DespairMapBorder;
        public Sprite Sprite_PathMapBorder;
        public Sprite Sprite_CastleMapBorder;
        public Sprite Sprite_FountainMapBorder;
        public Sprite Sprite_TutMapBorder;
        public Sprite Sprite_ConservatoryMapBorder;
        public Sprite Sprite_SmolderMapBorder;
        public Sprite Sprite_WorldMapBackgroundTile;

        public Sprite Sprite_FountainBottom;
        public Sprite Sprite_FountainTop;

        public Sprite Sprite_TextBox;
        public Sprite Sprite_ArrowIcon;
        public Sprite Sprite_OKIcon;
        public Sprite Sprite_ExitIcon;

        public Sprite Sprite_MouseCursor;
        public Sprite Sprite_SillyPad;
        public Sprite Sprite_BlueSquare;
        public Sprite Sprite_BlackSquare;
        public Sprite Sprite_RedSquare;
        public Sprite Sprite_YellowSquare;
        public Sprite Sprite_WhiteSquare;
        public Sprite Sprite_HugeTongue;
        public Sprite Sprite_StretchableBlackSquare;
        public Sprite Sprite_ReflectionShield;
        public Sprite Sprite_IceBlock;
        public Sprite Sprite_Loading;
        public Sprite Sprite_BillClinton;
        public Sprite Sprite_AdviceManUp;
        public Sprite Sprite_AdviceManLeft;
        public Sprite Sprite_AdviceManDown;
        public Sprite Sprite_ParallaxPit;
        public Sprite Sprite_AdviceBackground;

        public Sprite Sprite_MenuBackground;
        public Sprite Sprite_MenuSpeechBubble;
        public Sprite Sprite_ButtonBackground;
        public Sprite Sprite_ButtonBackgroundHighlighted;
        public Sprite Sprite_SmileyTitle;
        public Sprite Sprite_LoadingText;
        public Sprite Sprite_GameOverG;
        public Sprite Sprite_GameOverA;
        public Sprite Sprite_GameOverM;
        public Sprite Sprite_GameOverE1;
        public Sprite Sprite_GameOverO;
        public Sprite Sprite_GameOverV;
        public Sprite Sprite_GameOverE2;
        public Sprite Sprite_GameOverR;
        public Sprite Sprite_GameOverText;
        public Sprite Sprite_ControlsBox;
        public Sprite Sprite_SelectedControlsBox;
        public Sprite Sprite_DifficultyPromptBackground;
        public Sprite Sprite_LeftArrow;
        public Sprite Sprite_RightArrow;
        public Sprite Sprite_OKButton;
        public Sprite Sprite_LeftArrowHighlighted;
        public Sprite Sprite_RightArrowHighlighted;
        public Sprite Sprite_OKButtonHighlighted;
        public Sprite Sprite_SmileyWithoutHat;
        public Sprite Sprite_SmileysHat;

        private void LoadSprites()
        {
            Sprite_Fenwar = new Sprite(SmileyTexture.General, new Rectangle(129, 581, 62, 73), new Vector2(32, 36));
            Sprite_FenwarOrb = new Sprite(SmileyTexture.General, new Rectangle(386, 130, 60, 60), new Vector2(30, 30));
            Sprite_FenwarBullet = new Sprite(SmileyTexture.General, new Rectangle(356, 196, 22, 22), new Vector2(11, 11));
            Sprite_FenwarBulletRed = new Sprite(SmileyTexture.General, new Rectangle(356, 218, 22, 22), new Vector2(11, 11));
            Sprite_FenwarBomb = new Sprite(SmileyTexture.General, new Rectangle(390, 200, 36, 48), new Vector2(18, 24));

            Sprite_ManaLoot = new Sprite(SmileyTexture.General, new Rectangle(0, 160, 30, 30), new Vector2(15, 15));
            Sprite_HealthLoot = new Sprite(SmileyTexture.General, new Rectangle(32, 160, 30, 30), new Vector2(15, 15));
            Sprite_NewAbilityLoot = new Sprite(SmileyTexture.General, new Rectangle(0, 192, 64, 64), new Vector2(32, 32));

            Sprite_SessariaMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(0, 0, 243, 121), new Vector2(121, 60));
            Sprite_SalabiaMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(245, 0, 153, 123), new Vector2(76, 61));
            Sprite_FundoriaMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(0, 123, 168, 198), new Vector2(84, 99));
            Sprite_DespairMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(169, 124, 148, 147), new Vector2(74, 73));
            Sprite_PathMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(320, 125, 108, 96), new Vector2(54, 48));
            Sprite_CastleMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(320, 223, 107, 81), new Vector2(53, 40));
            Sprite_FountainMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(170, 273, 128, 70), new Vector2(64, 35));
            Sprite_TutMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(1, 322, 99, 74), new Vector2(50, 37));
            Sprite_ConservatoryMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(112, 333, 49, 52), new Vector2(25, 26));
            Sprite_SmolderMap = new Sprite(SmileyTexture.WorldMap, new Rectangle(1, 397, 120, 112), new Vector2(60, 56));
            Sprite_SessariaMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(0, 0, 252, 130), new Vector2(126, 65));
            Sprite_SalabiaMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(253, 0, 161, 132), new Vector2(80, 66));
            Sprite_FundoriaMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(1, 131, 177, 206), new Vector2(88, 103));
            Sprite_DespairMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(179, 133, 156, 155), new Vector2(78, 72));
            Sprite_PathMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(336, 133, 119, 104), new Vector2(59, 52));
            Sprite_ConservatoryMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(415, 1, 57, 60), new Vector2(28, 30));
            Sprite_FountainMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(179, 289, 136, 78), new Vector2(68, 37));
            Sprite_CastleMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(336, 238, 116, 89), new Vector2(58, 44));
            Sprite_SmolderMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(1, 338, 128, 121), new Vector2(64, 60));
            Sprite_TutMapBorder = new Sprite(SmileyTexture.WorldMapBorders, new Rectangle(130, 368, 107, 82), new Vector2(53, 41));
            Sprite_WorldMapBackgroundTile = new Sprite(SmileyTexture.WorldMap, new Rectangle(175, 352, 128, 128), new Vector2(53, 41));

            Sprite_FountainBottom = new Sprite(SmileyTexture.Fountain, new Rectangle(0, 380, 340, 172), new Vector2(170, 120));
            Sprite_FountainTop = new Sprite(SmileyTexture.Fountain, new Rectangle(340, 380, 118, 173), new Vector2(59, 120));

            Sprite_TextBox = new Sprite(SmileyTexture.General, new Rectangle(0, 256, 400, 250), new Vector2(0, 0));
            Sprite_ArrowIcon = new Sprite(SmileyTexture.General, new Rectangle(128, 142, 32, 20), new Vector2(0, 0));
            Sprite_OKIcon = new Sprite(SmileyTexture.General, new Rectangle(160, 142, 32, 20), new Vector2(0, 0));
            Sprite_ExitIcon = new Sprite(SmileyTexture.General, new Rectangle(206, 64, 40, 40), new Vector2(0, 0));

            Sprite_MouseCursor = new Sprite(SmileyTexture.General, new Rectangle(96, 96, 32, 32), new Vector2(1, 1));
            Sprite_SillyPad = new Sprite(SmileyTexture.General, new Rectangle(576, 0, 64, 64), new Vector2(0, 0));
            Sprite_BlueSquare = new Sprite(SmileyTexture.General, new Rectangle(832, 64, 64, 64), new Vector2(0, 0));
            Sprite_BlackSquare = new Sprite(SmileyTexture.General, new Rectangle(896, 64, 64, 64), new Vector2(0, 0));
            Sprite_RedSquare = new Sprite(SmileyTexture.General, new Rectangle(960, 64, 64, 64), new Vector2(0, 0));
            Sprite_YellowSquare = new Sprite(SmileyTexture.General, new Rectangle(832, 128, 64, 64), new Vector2(0, 0));
            Sprite_WhiteSquare = new Sprite(SmileyTexture.General, new Rectangle(320, 128, 64, 64), new Vector2(0, 0));
            Sprite_HugeTongue = new Sprite(SmileyTexture.General, new Rectangle(21, 660, 524, 227), new Vector2(0, 113));
            Sprite_StretchableBlackSquare = new Sprite(SmileyTexture.General, new Rectangle(897, 65, 62, 62), new Vector2(0, 0));
            Sprite_ReflectionShield = new Sprite(SmileyTexture.General, new Rectangle(0, 64, 96, 96), new Vector2(48, 48));
            Sprite_IceBlock = new Sprite(SmileyTexture.General, new Rectangle(128, 0, 64, 64), new Vector2(32, 32));
            Sprite_Loading = new Sprite(SmileyTexture.General, new Rectangle(623, 623, 400, 400), new Vector2(200, 200));
            Sprite_BillClinton = new Sprite(SmileyTexture.General, new Rectangle(0, 512, 64, 64), new Vector2(32, 32));
            Sprite_AdviceManUp = new Sprite(SmileyTexture.General, new Rectangle(192, 834, 64, 64), new Vector2(32, 32));
            Sprite_AdviceManLeft = new Sprite(SmileyTexture.General, new Rectangle(64, 834, 64, 64), new Vector2(32, 22));
            Sprite_AdviceManDown = new Sprite(SmileyTexture.General, new Rectangle(0, 834, 64, 64), new Vector2(32, 32));
            Sprite_ParallaxPit = new Sprite(SmileyTexture.General, new Rectangle(960, 0, 64, 64), new Vector2(0, 0));
            Sprite_AdviceBackground = new Sprite(SmileyTexture.UserInterface, new Rectangle(692, 396, 130, 30), new Vector2(0, 0));

            Sprite_MenuBackground = new Sprite(SmileyTexture.TitleScreen, new Rectangle(0, 0, 1024, 768), new Vector2(0, 0));
            Sprite_MenuSpeechBubble = new Sprite(SmileyTexture.UserInterface, new Rectangle(0, 769, 245, 73));
            Sprite_ButtonBackground = new Sprite(SmileyTexture.UserInterface, new Rectangle(661, 22, 246, 71));
            Sprite_ButtonBackgroundHighlighted = new Sprite(SmileyTexture.UserInterface, new Rectangle(661, 93, 246, 71));
            Sprite_SmileyTitle = new Sprite(SmileyTexture.TitleScreen, new Rectangle(380, 769, 430, 214), new Vector2(215,104));
            Sprite_LoadingText = new Sprite(SmileyTexture.UserInterface, new Rectangle(1, 844, 334, 88), new Vector2(168,45));
            Sprite_GameOverG = new Sprite(SmileyTexture.UserInterface, new Rectangle(811, 769, 48, 58), new Vector2(170,24));
            Sprite_GameOverA = new Sprite(SmileyTexture.UserInterface, new Rectangle(863, 769, 48, 63), new Vector2(127, 30));
            Sprite_GameOverM = new Sprite(SmileyTexture.UserInterface, new Rectangle(913, 769, 52, 68), new Vector2(88, 36));
            Sprite_GameOverE1 = new Sprite(SmileyTexture.UserInterface, new Rectangle(967, 769, 35, 69), new Vector2(38, 37));
            Sprite_GameOverO = new Sprite(SmileyTexture.UserInterface, new Rectangle(811, 833, 41, 72), new Vector2(17, 37));
            Sprite_GameOverV = new Sprite(SmileyTexture.UserInterface, new Rectangle(852, 833, 45, 69), new Vector2(54, 35));
            Sprite_GameOverE2 = new Sprite(SmileyTexture.UserInterface, new Rectangle(897, 838, 46, 64), new Vector2(87, 29));
            Sprite_GameOverR = new Sprite(SmileyTexture.UserInterface, new Rectangle(943, 839, 53, 59), new Vector2(53, 59));
            Sprite_GameOverText = new Sprite(SmileyTexture.UserInterface, new Rectangle(0, 934, 340, 74), new Vector2(170, 37));
            Sprite_ControlsBox = new Sprite(SmileyTexture.UserInterface, new Rectangle(248, 769, 130, 30));
            Sprite_SelectedControlsBox = new Sprite(SmileyTexture.UserInterface, new Rectangle(248, 799, 130, 30));
            Sprite_DifficultyPromptBackground = new Sprite(SmileyTexture.UserInterface, new Rectangle(661, 809, 325, 150));
            Sprite_LeftArrow = new Sprite(SmileyTexture.UserInterface, new Rectangle(989, 810, 32, 32));
            Sprite_RightArrow = new Sprite(SmileyTexture.UserInterface, new Rectangle(989, 844, 32, 32));
            Sprite_OKButton = new Sprite(SmileyTexture.UserInterface, new Rectangle(989, 878, 32, 32));
            Sprite_LeftArrowHighlighted = new Sprite(SmileyTexture.UserInterface, new Rectangle(989, 912, 32, 32));
            Sprite_RightArrowHighlighted = new Sprite(SmileyTexture.UserInterface, new Rectangle(989, 946, 32, 32));
            Sprite_OKButtonHighlighted = new Sprite(SmileyTexture.UserInterface, new Rectangle(989, 980, 32, 32));
            Sprite_SmileyWithoutHat = new Sprite(SmileyTexture.UserInterface, new Rectangle(3, 590, 53, 42));
            Sprite_SmileysHat = new Sprite(SmileyTexture.UserInterface, new Rectangle(71, 594, 51, 34));
        }
    }
}
