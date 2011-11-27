using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smiley.Lib.Menu
{
    public class LoadingScreen : BaseMenuScreen
    {
        public LoadingScreen(MainMenu mainMenu)
            : base(mainMenu)
        {
        }

        public override bool ShouldDrawMouse
        {
            get { return true; }
        }

        public override bool ShouldDrawBackground
        {
            get { return true; }
        }

        public override void Draw()
        {
        }

        public override void Update(float dt)
        {
        }
    }
}
