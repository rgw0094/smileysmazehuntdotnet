using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smiley.Lib
{
    /// <summary>
    /// Game constants
    /// </summary>
    public class Constants
    {
        public const double SmallGemValue = 1.0;
        public const double MediumGemValue = 3.0;
        public const double LargeGemValue = 5.0;

        public const float SwitchDelay = 0.15f;
        public const float TongueSwitchDelay = 0.40f;
        public const int NumSwitchFrames = 5;
        public const float SwitchFPS = (float)NumSwitchFrames / SwitchDelay;

        public const int ID_DrawAfterSmiley = 990;
    }
}
