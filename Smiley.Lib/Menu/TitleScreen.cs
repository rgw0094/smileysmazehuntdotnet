using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Smiley.Lib.Framework.UIControls;
using Smiley.Lib.Data;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Menu
{
    public class TitleScreen : BaseMenuScreen
    {
        private const float ButtonYOffset = 125f;
        private const float ButtonEffectDuration = 0.3f;

        #region Private Variables

        private float _smileyTitleX;
        private float _smileyTitleY; //Where and how large the title is drawn
        private float _smileyTitleSize;
        private SmileyTitleState _smileyTitleState; //whether it's coming at you, going back, or stationary
        private bool _smileyTitleExited; //whether it's done exiting or not
        private ControlActionGroup _controlActionGroup;
        private TitleScreenButton _clickedButton;
        private Dictionary<TitleScreenButton, Button> _buttons = new Dictionary<TitleScreenButton, Button>();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructs a new title screen.
        /// </summary>
        public TitleScreen(MainMenu mainMenu)
             : base(mainMenu)
        {
            EnterState(MenuState.InScreen);

            //The buttons start off the screen, the control action group will move them up
            _buttons[TitleScreenButton.Exit] = new Button(67f, 785f, "Exit");
            _buttons[TitleScreenButton.Options] = new Button(387f, 785f, "Options");
            _buttons[TitleScreenButton.Play] = new Button(707f, 785f, "Play");

            _smileyTitleX = 1024f / 2f;
            _smileyTitleY = 118;
            _smileyTitleSize = 0.0001f;
            _smileyTitleState = SmileyTitleState.ComingAtYou;
            _smileyTitleExited = false;

            _controlActionGroup = new ControlActionGroup(_buttons.Values);
            _controlActionGroup.BeginAction(ControlAction.CascadingMove, 0f, -ButtonYOffset, ButtonEffectDuration);
        }

        #endregion

        #region BaseMenuScreen Overrides

        public override bool ShouldDrawBackground
        {
            get { return true; }
        }

        public override bool ShouldDrawMouse
        {
            get { return true; }
        }

        public override void Draw()
        {
            //Draw buttons
            foreach (Button button in _buttons.Values)
            {
                button.Draw();
            }

            //Draw title
            SMH.Graphics.DrawSprite(Sprites.SmileyTitle, _smileyTitleX, _smileyTitleY, Color.White, 0f, _smileyTitleSize);
        }

        public override void Update(float dt)
        {
            //Update buttons
            foreach (KeyValuePair<TitleScreenButton, Button> kvp in _buttons)
            {
                Button button = kvp.Value;
                button.Update(dt);

                if (button.IsClicked())
                {
                    _clickedButton = kvp.Key;
                    EnterState(MenuState.ExitingScreen);
                    _controlActionGroup.BeginAction(ControlAction.CascadingMove, 0f, ButtonYOffset, ButtonEffectDuration);
                    _smileyTitleState = SmileyTitleState.Exiting;
                }
            }

            //Update title
            switch (_smileyTitleState)
            {
                case SmileyTitleState.ComingAtYou:
                    _smileyTitleSize += 1.8f * dt;
                    if (_smileyTitleSize > 1.23f) _smileyTitleState = SmileyTitleState.GoingBack;
                    break;

                case SmileyTitleState.GoingBack:
                    _smileyTitleSize -= 1.8f * dt;
                    if (_smileyTitleSize <= 1f)
                    {
                        _smileyTitleState = SmileyTitleState.Stopped;
                        _smileyTitleSize = 1f;
                    }
                    break;
                case SmileyTitleState.Stopped:
                    _smileyTitleSize = 1f;
                    break;
                case SmileyTitleState.Exiting:
                    _smileyTitleSize -= 3.0f * dt;
                    if (_smileyTitleSize < 0.0001f)
                    {
                        _smileyTitleSize = 0.0001f;
                        _smileyTitleExited = true;
                    }
                    break;
            }

            if (_controlActionGroup.Update(dt) && State == MenuState.ExitingScreen)
            {
                switch (_clickedButton)
                {
                    case TitleScreenButton.Play:
                        MainMenu.ShowScreen<SelectFileScreen>();
                        break;
                    case TitleScreenButton.Options:
                        MainMenu.ShowScreen<OptionsScreen>();
                        break;
                    case TitleScreenButton.Exit:
                        SMH.Game.Exit();
                        break;
                }
            }
        }

        #endregion

        #region Private Classes/Enums

        private enum SmileyTitleState
        {
            ComingAtYou,
            GoingBack,
            Stopped,
            Exiting
        }

        private enum TitleScreenButton
        {
            Exit,
            Options,
            Play
        }

        #endregion
    }
}
