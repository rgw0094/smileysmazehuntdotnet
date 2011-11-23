using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Smiley.Lib.Enums
{
    public enum Input
    {
        [Description("Left")]
        Left,
        [Description("Right")]
        Right,
        [Description("Up")]
        Up,
        [Description("Down")]
        Down,
        [Description("Attack/Select")]
        Attack,
        [Description("Use Ability")]
        Ability,
        [Description("Aim")]
        Aim,
        [Description("Last Ability")]
        PreviousAbility,
        [Description("Next Ability")]
        NextAbility,
        [Description("Inventory/Map")]
        Pause
    }

    public enum InputDevice
    {
        Keyboard,
        GamePad
    }
}
