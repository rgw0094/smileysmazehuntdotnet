using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.UI.Controls;
using Smiley.Lib.Framework.Drawing;
using Microsoft.Xna.Framework;
using Smiley.Lib.Data;
using Smiley.Lib.Enums;

namespace Smiley.Lib.UI.Windows
{
    public enum MiniMenuMode
    {
        Exit,
        SaveGame,
        ExitPrompt,
    }

    public enum MiniMenuButton
    {
        Save,
        Cancel,
        Quit,
        Options,
        Yes,
        No
    }

    public class ButtonInfo
    {
        public Button Button { get; set; }
        public MiniMenuButton Type { get; set; }
    }

    public class MiniMenu : BaseWindow
    {
        #region Private Variables

        private float _x;
        private float _y;
        private float _xOffset;
        private float _yOffset;
        private int _selected;
        private MiniMenuMode _mode;
        private List<ButtonInfo> _buttons = new List<ButtonInfo>();

        #endregion

        #region Constructors

        public MiniMenu(MiniMenuMode mode)
        {
            _x = (1024 - 250) / 2;
            _y = (768 - 75 - 75 - 30) / 2;
            _mode = mode;

            switch (mode)
            {
                case MiniMenuMode.Exit:
                    AddButton(250, "Resume", MiniMenuButton.Cancel);
                    AddButton(350, "Options", MiniMenuButton.Options);
                    AddButton(450, "Exit", MiniMenuButton.Quit);
                    break;

                case MiniMenuMode.SaveGame:
                    AddButton(300, "Cancel", MiniMenuButton.Cancel);
                    AddButton(450, "Save", MiniMenuButton.Save);
                    break;

                case MiniMenuMode.ExitPrompt:
                    AddButton(512 - 250, 350, "Exit", MiniMenuButton.Yes);
                    AddButton(512 + 50, 350, "Cancel", MiniMenuButton.No);
                    break;
            }
        }

        #endregion

        #region Public Methods

        public override bool Update(float dt)
        {
            //Update buttons
            foreach (ButtonInfo bi in _buttons)
            {
                bi.Button.Update(dt);

                if (bi.Button.IsClicked())
                {
                    switch (bi.Type)
                    {
                        case MiniMenuButton.Cancel:
                            return false;
                        case MiniMenuButton.Quit:
                            SMH.WindowManager.OpenMiniMenu(MiniMenuMode.ExitPrompt);
                            return true;
                        case MiniMenuButton.Save:
                            SMH.SaveManager.Save();
                            SMH.PopupMessageManager.ShowSaveConfirmation();
                            return false;
                        case MiniMenuButton.Options:
                            SMH.WindowManager.OpenOptionsWindow();
                            return true;
                        case MiniMenuButton.Yes:
                            SMH.ShowMenu();
                            return false;
                        case MiniMenuButton.No:
                            SMH.WindowManager.OpenMiniMenu(MiniMenuMode.Exit);
                            return true;
                    }
                }

            }

            return true;
        }

        public override void Draw()
        {
            //Shade the screen behind the menu
            SMH.Graphics.DrawRect(new Rect(0, 0, 1024, 768), Color.FromNonPremultiplied(0, 0, 0, 100), true);

            if (_mode == MiniMenuMode.ExitPrompt)
            {
                SMH.Graphics.DrawString(SmileyFont.AbilityTitle, "Are you sure you wish to exit?", 512, 200, TextAlignment.Center, Color.White);
                SMH.Graphics.DrawString(SmileyFont.AbilityTitle, "Any unsaved progress will be lost.", 512, 240, TextAlignment.Center, Color.White);
            }

            //Draw buttons
            foreach (ButtonInfo button in _buttons)
            {
                button.Button.Draw();
            }

            //Draw the mouse
            SMH.Graphics.DrawSprite(Sprites.MouseCursor, SMH.Input.Cursor.X, SMH.Input.Cursor.Y);
        }

        #endregion

        #region Private Methods

        private void AddButton(float y, string text, MiniMenuButton buttonType)
        {
            AddButton(512 - 125, y, text, buttonType);
        }

        private void AddButton(float x, float y, string text, MiniMenuButton buttonType)
        {
            _buttons.Add(new ButtonInfo
                    {
                        Button = new Button(x, y, text),
                        Type = buttonType
                    });
        }

        #endregion
    }
}
