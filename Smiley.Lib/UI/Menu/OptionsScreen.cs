using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.UI.Windows;

namespace Smiley.Lib.UI.Menu
{
    public class OptionsScreen : BaseMenuScreen
    {
        private OptionsWindow _optionsWindow = new OptionsWindow();

        public OptionsScreen(MainMenu mainMenu)
            : base(mainMenu)
        {
            _optionsWindow.Y = -512;
            EnterState(MenuState.EnteringScreen);
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
            _optionsWindow.Draw();
        }

        public override void Update(float dt)
        {
            if (State == MenuState.EnteringScreen)
            {
                _optionsWindow.Y += 1800f * dt;
                if (_optionsWindow.Y >= 138)
                {
                    _optionsWindow.Y = 138;
                    EnterState(MenuState.InScreen);
                }
            }
            else if (State == MenuState.ExitingScreen)
            {
                _optionsWindow.Y -= 1800f * dt;
                if (_optionsWindow.Y <= 512)
                {
                    MainMenu.ShowScreen<TitleScreen>();
                }
            }

            if (!_optionsWindow.Update(dt))
            {
                EnterState(MenuState.ExitingScreen);
            }
        }
    }
}
