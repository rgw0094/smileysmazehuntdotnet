using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Smiley.Lib.Enums
{
    public enum Difficulty
    {
        [Description("Very Easy")]
        VeryEasy = 0,
        [Description("Easy")]
        Easy = 1,
        [Description("Medium")]
        Medium = 2,
        [Description("Hard")]
        Hard = 3,
        [Description("Very Hard")]
        VeryHard = 4
    }
}
