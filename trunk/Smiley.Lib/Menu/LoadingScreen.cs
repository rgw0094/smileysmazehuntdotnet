using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smiley.Lib.Menu
{
    public class LoadingScreen : BaseMenuScreen
    {
        private float _timeEnteredScreen;
        private int _fileNumber;
        private bool _startedLoadYet;
        private bool _fromLoadScreen;
        private bool _isNewGame;

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
