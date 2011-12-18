using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Smiley.Lib.Enums
{
    public enum SaveSlot
    {
        [Description("save1.sav")]
        Slot1 = 0,
        [Description("save2.sav")]
        Slot2 = 1,
        [Description("save3.sav")]
        Slot3 = 2,
        [Description("save4.sav")]
        Slot4 = 3
    }
}
