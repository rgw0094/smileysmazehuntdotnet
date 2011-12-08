using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;

namespace Smiley.Lib
{
    /// <summary>
    /// Game constants
    /// </summary>
    public class Constants
    {
        public const int MonocleManNPCId = 13;
        public const int MonocleManTextID = 902;
        public const int SpeirDykeTextId = 5;
        public const int BillClintonTextID = 8;
        public const int BillClintonTextID2 = 19;

        public const int SmallGemValue = 1;
        public const int MediumGemValue = 3;
        public const int LargeGemValue = 5;

        public const float PlayerWidth = 61f;
        public const float PlayerHeight = 72f;

        public const float SwitchDelay = 0.15f;
        public const float TongueSwitchDelay = 0.40f;
        public const int NumSwitchFrames = 5;
        public const float SwitchFPS = (float)NumSwitchFrames / SwitchDelay;

        public const float KnockbackDuration = 0.2f;
        public const float InitialMana = 50f;
        public const float ManaRegenerationRate = 6f;
        public const float ManaPerItem = 25f;

        public const int ID_DrawAfterSmiley = 990;

        public const float SlimeAcceleration = 500f;	    //Player acceleration on slime
        public const float PlayerAcceleration = 5000f;		//Normal player acceleration
        public const float DefaultSmileyRadius = 28f;

        public const float ShrinkTunnelSpeed = 500f;
        public const float HoverDuration = 5f;
        public const float HealFlashDuration = 1.0f;
        public const float SpringVelocity = 210.0f;
        public const float JesusSandleTime = 1.65f;
        public const float SpeedBootsModifier = 1.75f;
        public const float MoveSpeed = 300f;
        public const float CaneTime = 1.5f;
        public const float MaxFrisbeePower = 2.8f;

        public const float ManaRegenDelay = 4.0f;

        public static Dictionary<Direction, float> SmileyAngles;
        public static Dictionary<Direction, Vector2> MouthPositions;

        static Constants()
        {
            SmileyAngles = new Dictionary<Direction, float>();
            SmileyAngles[Direction.Up] = 0;
            SmileyAngles[Direction.UpRight] = (float)Math.PI * .25f;
            SmileyAngles[Direction.Right] = (float)Math.PI * 0.5f;
            SmileyAngles[Direction.DownRight] = (float)Math.PI * .75f;
            SmileyAngles[Direction.Down] = (float)Math.PI * 1.0f;
            SmileyAngles[Direction.DownLeft] = (float)Math.PI * 1.25f;
            SmileyAngles[Direction.Left] = (float)Math.PI * 1.5f;
            SmileyAngles[Direction.UpLeft] = (float)Math.PI * 1.75f;

            MouthPositions = new Dictionary<Direction, Vector2>();
            MouthPositions[Direction.Left] = new Vector2(-20, 10);
            MouthPositions[Direction.Right] = new Vector2(18, 10);
            MouthPositions[Direction.Up] = new Vector2(0, -10);
            MouthPositions[Direction.Down] = new Vector2(-2, 13);
            MouthPositions[Direction.UpLeft] = new Vector2(-10, -5);
            MouthPositions[Direction.UpRight] = new Vector2(5, -5);
            MouthPositions[Direction.DownLeft] = new Vector2(-10, 10);
            MouthPositions[Direction.DownRight] = new Vector2(10, 10);
        }
    }
}
