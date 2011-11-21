using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Smiley.Lib.Enums
{
    public enum SmileyTexture
    {
        [Description("animations")]
        Animations,
        [Description("sprites")]
        General,
        [Description("enemies")]
        Enemies,
        [Description("npcs")]
        NPCs,
        [Description("itemlayer1")]
        ItemLayer1,
        [Description("itemlayer2")]
        ItemLayer2,
        [Description("mainlayer")]
        MainLayer,
        [Description("walkLayer")]
        WalkLayer,
        [Description("fountain")]
        Fountain,
        [Description("Cinematic")]
        Cinematic,
        [Description("psychedelic")]
        Psychedelic,
        [Description("EndingCinematic")]
        EndingCinematic,
        [Description("WorldMap")]
        WorldMap,
        [Description("WorldMapBorders")]
        WorldMapBorders
    }
}
