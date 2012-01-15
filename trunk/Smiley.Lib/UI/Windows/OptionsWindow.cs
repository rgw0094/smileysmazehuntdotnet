using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework.Drawing;
using Smiley.Lib.UI.Controls;
using Smiley.Lib.Enums;
using Smiley.Lib.Data;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.UI.Windows
{
    public class OptionsWindow : BaseWindow
    {
        #region Private Variables

        private Button _doneButton;
        private Slider _musicSlider;
        private Slider _soundSlider;
        private float _windowX;
        private float _windowY;
        private Input? _editedInput;

        #endregion

        #region Constructors

        public OptionsWindow()
        {
            _soundSlider = new Slider(0, 0, 0, 100);
            _soundSlider.Value = SMH.Sound.SoundVolume;
            _musicSlider = new Slider(0, 0, 0, 100);
            _musicSlider.Value = SMH.Sound.MusicVolume;
            _doneButton = new Button(0, 0, "Save");

            SetWindowPosition(182f, 138f);
        }

        #endregion

        #region Public Properties/Methods

        public float X
        {
            get { return _windowX; }
            set
            {
                SetWindowPosition(value, _windowY);
            }
        }

        public float Y
        {
            get { return _windowY; }
            set
            {
                SetWindowPosition(_windowX, value);
            }
        }

        public override bool Update(float dt)
        {
            //Update volume sliders
            _soundSlider.Update(dt);
            _musicSlider.Update(dt);

            SMH.Sound.SoundVolume = _soundSlider.Value;
            SMH.Sound.MusicVolume = _musicSlider.Value;

            //Update done button
            _doneButton.Update(dt);
            if (_doneButton.IsClicked())
            {
                //Make it so none of the buttons is in "Edit mode"
                if (SMH.State == GameState.Game)
                {
                    //If the OptionsWindow is open while in game mode then it was launched from
                    //the mini menu, so return to there. This is a terrible way for the control
                    //to flow but oh well
                    SMH.WindowManager.OpenMiniMenu(MiniMenuMode.Exit);
                    return true;
                }
                return false;
            }

            //Update input boxes
            for (int col = 0; col < 2; col++)
            {
                for (int row = 0; row < 5; row++)
                {

                    Input input = (Input)(col * 5 + row);
                    float x = _windowX + 40 + col * 140f;
                    float y = _windowY + 80 + row * 80f;

                    //Listen for click to enable edit mode
                    Rect inputBox = new Rect(x, y, 130, 30);
                    if (SMH.Input.IsDown(Input.Attack) && inputBox.Contains(SMH.Input.Cursor))
                    {
                        _editedInput = input;
                    }

                    //If the input is in edit mode, listen for the new input
                    if (_editedInput != null && SMH.Input.ListenForNewInput(_editedInput.Value))
                    {
                        _editedInput = null;
                    }
                }
            }

            return true;
        }

        public override void Draw()
        {
            if (SMH.State == GameState.Game)
                SMH.Graphics.DrawRect(new Rect(0, 0, 1024, 768), Color.FromNonPremultiplied(0, 0, 0, 100), true);

            //Draw background
            SMH.Graphics.DrawSprite(Sprites.OptionsBackground, _windowX, _windowY);

            //Draw volume sliders
            _musicSlider.Draw();
            _soundSlider.Draw();

            SMH.Graphics.DrawString(SmileyFont.AbilityTitle, "Volume", _windowX + 488, _windowY + 42, TextAlignment.Center, Color.White);
            SMH.Graphics.DrawString(SmileyFont.AbilityTitle, "Sound", _soundSlider.X + _soundSlider.Width / 2f, _soundSlider.Y + _soundSlider.Height + 3f, TextAlignment.Center, Color.White, 0.8f);
            SMH.Graphics.DrawString(SmileyFont.AbilityTitle, "Music", _musicSlider.X + _musicSlider.Width / 2f, _musicSlider.Y + _musicSlider.Height + 3f, TextAlignment.Center, Color.White, 0.8f);

            _doneButton.Draw();

            //Draw input boxes.
            for (int col = 0; col < 2; col++)
            {
                for (int row = 0; row < 5; row++)
                {
                    Input input = (Input)(col * 5 + row);
                    float x = _windowX + 40 + col * 140f;
                    float y = _windowY + 45 + row * 80f;

                    //Input name
                    bool isInputBeingEdited = _editedInput != null && _editedInput.Value == input;
                    SMH.Graphics.DrawString(SmileyFont.Controls, input.GetDescription(), x + 65f, y, TextAlignment.Center, Color.White);

                    //Input box
                    if (isInputBeingEdited)
                    {
                        SMH.Graphics.DrawSprite(Sprites.SelectedControlsBox, x, y + 35);
                    }
                    else
                    {
                        SMH.Graphics.DrawSprite(Sprites.ControlsBox, x, y + 35);
                    }

                    //Current setting
                    string inputName = isInputBeingEdited ? "Press Button" : SMH.Input.GetInputDescription(input);
                    SMH.Graphics.DrawString(SmileyFont.Controls, inputName, x + 65f, y + 40f, TextAlignment.Center, Color.White, 0.8f);
                }
            }

            //Draw the mouse
            if (SMH.Input.IsCursorInWindow)
            {
                SMH.Graphics.DrawSprite(Sprites.MouseCursor, SMH.Input.Cursor.X, SMH.Input.Cursor.Y);
            }
        }

        #endregion

        #region Private Methods

        private void SetWindowPosition(float x, float y)
        {
            _windowX = x;
            _windowY = y;

            _soundSlider.X = _windowX + 367;
            _soundSlider.Y = _windowY + 92;
            _musicSlider.X = _windowX + 507;
            _musicSlider.Y = _windowY + 92;
            _doneButton.X = _windowX + 360;
            _doneButton.Y = _windowY + 340;
        }

        #endregion
    }
}
