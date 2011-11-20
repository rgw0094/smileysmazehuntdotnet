using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Smiley.Lib.Enums
{
    public enum Level
    {
        [Description("Smiley Town")]
        FOUNTAIN_AREA = 0,
        [Description("Dunes of Salabia")]
        OLDE_TOWNE = 1,
        [Description("Tut's Tomb")]
        TUTS_TOMB = 2,
        [Description("Forest of Fungoria")]
        FOREST_OF_FUNGORIA = 3,
        [Description("Sessaria Snowplains")]
        SESSARIA_SNOWPLAINS = 4,
        [Description("World of Despair")]
        WORLD_OF_DESPAIR = 5,
        [Description("The Serpentine Path")]
        SERPENTINE_PATH = 6,
        [Description("Castle of Evil")]
        CASTLE_OF_EVIL = 7,
        [Description("Smolder Hollow")]
        SMOLDER_HOLLOW = 8,
        [Description ("Conservatory")] //TODO: better name
        CONSERVATORY = 9,
        [Description("Debug Area")]
        DEBUG_AREA = 10
    }
}
