using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smiley.Lib.Menu
{
    public class OptionsScreen : BaseMenuScreen
    {
        public override bool ShouldDrawMouse
        {
            get { return true; }
        }

        public override bool ShouldDrawBackground
        {
            get { return true; }
        }
    }
}
