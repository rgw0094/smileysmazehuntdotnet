using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smiley.Lib.Enums
{
    public enum Ability
    {
        NUM_ABILITIES = 12,
        NO_ABILITY = 12,
        CANE = 0,
        FIRE_BREATH = 1,
        FRISBEE = 2,
        SPRINT_BOOTS = 3,
        LIGHTNING_ORB = 4,
        REFLECTION_SHIELD = 5,
        SILLY_PAD = 6,
        WATER_BOOTS = 7,
        ICE_BREATH = 8,
        SHRINK = 9,
        TUTS_MASK = 10,
        HOVER = 11
    }

    public enum AbilityType
    { 
        Passive,
        Activated,
        Hold
    }
}
