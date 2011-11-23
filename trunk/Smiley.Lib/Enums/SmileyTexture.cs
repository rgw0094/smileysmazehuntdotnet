using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Smiley.Lib.Enums
{
    public enum SmileyTexture
    {
        [Description("Graphics\\animations")]
        Animations,
        [Description("Graphics\\sprites")]
        General,
        [Description("Graphics\\enemies")]
        Enemies,
        [Description("Graphics\\npcs")]
        NPCs,
        [Description("Graphics\\itemlayer1")]
        ItemLayer1,
        [Description("Graphics\\itemlayer2")]
        ItemLayer2,
        [Description("Graphics\\mainlayer")]
        MainLayer,
        [Description("Graphics\\walkLayer")]
        WalkLayer,
        [Description("Graphics\\fountain")]
        Fountain,
        [Description("Graphics\\Cinematic")]
        Cinematic,
        [Description("Graphics\\psychedelic")]
        Psychedelic,
        [Description("Graphics\\EndingCinematic")]
        EndingCinematic,
        [Description("Graphics\\WorldMap")]
        WorldMap,
        [Description("Graphics\\WorldMapBorders")]
        WorldMapBorders,
        [Description("Graphics\\UserInterface")]
        UserInterface,
        [Description("Graphics\\titlescreen")]
        TitleScreen
    }
}
