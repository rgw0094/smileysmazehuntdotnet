using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.UI.Controls;
using Smiley.Lib.Framework.Drawing;
using Smiley.Lib.Enums;
using Smiley.Lib.Data;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.UI.Menu
{
    public enum SelectFileButton
    {
        Back,
        Delete,
        Start
    }

    public class SaveBox
    {
        public Rect CollisionBox { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public int SaveFile { get; set; }
    }

    public class SelectFileScreen : BaseMenuScreen
    {
        #region Private Variables

        private float _windowX;
        private float _windowY;
        private bool _deletePromptActive;
        private bool _mouseOverYes;
        private bool _mouseOverNo;
        private SaveSlot _selectedSlot;
        private float _smileyX;
        private float _smileyY;
        private Rect _yesDeleteBox;
        private Rect _noDeleteBox;
        private SelectFileButton _clickedButton;
        private Dictionary<SelectFileButton, Button> _buttons = new Dictionary<SelectFileButton, Button>();
        private Dictionary<SaveSlot, SaveBox> _saveBoxes = new Dictionary<SaveSlot, SaveBox>();
        private DifficultyPrompt _difficultyPrompt = new DifficultyPrompt(DifficultyPrompt.DefaultX, DifficultyPrompt.DefaultY);

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new SelectFileScreen.
        /// </summary>
        public SelectFileScreen(MainMenu mainMenu)
            : base(mainMenu)
        {
            _buttons[SelectFileButton.Back] = new Button(0, 0, "Back");
            _buttons[SelectFileButton.Delete] = new Button(0, 0, "Delete");
            _buttons[SelectFileButton.Start] = new Button(0, 0, "Start");

            foreach (SaveSlot saveSlot in Enum.GetValues(typeof(SaveSlot)))
            {
                _saveBoxes[saveSlot] = new SaveBox();
            }

            SetWindowPosition(182f, -512f);
        }

        #endregion

        #region Properties

        public override bool ShouldDrawMouse
        {
            get { return true; }
        }

        public override bool ShouldDrawBackground
        {
            get { return true; }
        }

        #endregion

        #region Public Methods

        public override void Draw()
        {
            SMH.Graphics.DrawSprite(Sprites.OptionsBackground, _windowX, _windowY);
            SMH.Graphics.DrawSprite(Sprites.OptionsPatch, _windowX + 344f, _windowY + 30f);

            //Draw smiley next to the selected game
            SMH.Graphics.DrawSprite(Sprites.SmileysFace, _smileyX, _smileyY);

            //Draw save boxes
            int i = 0;
            foreach (KeyValuePair<SaveSlot, SaveBox> kvp in _saveBoxes)
            {
                SaveBox box = kvp.Value;
                SaveSlot slot = kvp.Key;

                if (_selectedSlot == kvp.Key && _deletePromptActive)
                {
                    SMH.Graphics.DrawSprite(Sprites.MenuSpeechBubble, box.X + 30f, box.Y - 2f);
                    SMH.Graphics.DrawString(SmileyFont.AbilityTitle, "Delete this file?", box.X + 75f, box.Y + 5f, TextAlignment.Left, Color.Black, 0.9f);

                    SMH.Graphics.DrawString(SmileyFont.Segoe14, "Yes", box.X + 125f, box.Y + 65f, TextAlignment.Center, _mouseOverYes ? Color.Red : Color.White);
                    SMH.Graphics.DrawString(SmileyFont.Segoe14, "No", box.X + 205f, box.Y + 65f, TextAlignment.Center, _mouseOverNo ? Color.Red : Color.White);
                }
                else
                {
                    Color color = kvp.Key == _selectedSlot ? Color.LightBlue : Color.White;

                    //File Name
                    SMH.Graphics.DrawString(SmileyFont.AbilityTitle, SMH.SaveManager.Saves[slot].IsEmpty ? "- Empty -" : "Save " + (i + 1).ToString(), box.X + 70f, box.Y + 5f, TextAlignment.Left, color);

                    //Time Played
                    SMH.Graphics.DrawString(SmileyFont.Segoe14, "Time Played: ", box.X + 70f, box.Y + 50f, TextAlignment.Left, color);
                    SMH.Graphics.DrawString(SmileyFont.Segoe14, SMH.SaveManager.Saves[slot].IsEmpty ? "0:00:00" :
                        SMH.SaveManager.Saves[slot].TimePlayed.ToString(), box.X + 250f, box.Y + 50f, TextAlignment.Right, color);

                    //Completion percentage
                    SMH.Graphics.DrawString(SmileyFont.Segoe14, "Complete: ", box.X + 70f, box.Y + 75f, TextAlignment.Left, color);
                    SMH.Graphics.DrawString(SmileyFont.Segoe14, SMH.SaveManager.Saves[slot].IsEmpty ? "0%" :
                        SMH.SaveManager.Saves[slot].CalculateCompletionPercentage().ToString(), box.X + 250f, box.Y + 75f, TextAlignment.Right, color);
                }

                i++;
            }

            //Draw buttons
            foreach (Button button in _buttons.Values)
            {
                button.Draw();
            }

            if (_difficultyPrompt.IsVisible)
            {
                _difficultyPrompt.Draw();
            }
        }

        public override void Update(float dt)
        {
            if (_difficultyPrompt.IsVisible)
            {
                Difficulty? result = _difficultyPrompt.Update(dt);
                if (result == null)
                    return;
                SMH.SaveManager.CurrentSave.Difficulty = result.Value;
            }

            if (State == MenuState.EnteringScreen)
            {
                SetWindowPosition(_windowX, _windowY + 1800f * dt);
                if (_windowY > 138f)
                {
                    EnterState(MenuState.InScreen);
                    SetWindowPosition(_windowX, 138f);
                }
            }
            else if (State == MenuState.ExitingScreen)
            {
                SetWindowPosition(_windowX, _windowY - 1800f * dt);
                if (_windowY <= -512f)
                {
                    //Done exiting screen - perform action based on what button was clicked.
                    switch (_clickedButton)
                    {
                        case SelectFileButton.Back:
                            MainMenu.ShowScreen<TitleScreen>();
                            break;
                        case SelectFileButton.Start:
                            if (SMH.SaveManager.CurrentSave.IsEmpty)
                            {
                                SMH.Environment.LoadLevelAsynch(Level.FOUNTAIN_AREA);
                                MainMenu.ShowScreen<CinematicScreen>();
                            }
                            else
                            {
                                SMH.StartGame(false);
                            }
                            break;
                    }
                }
            }

            //Set "start" button text based on whether or not an empty file is selected.
            _buttons[SelectFileButton.Start].Text = SMH.SaveManager.Saves[_selectedSlot].IsEmpty ? "Start" : "Continue";

            //Update buttons
            foreach (KeyValuePair<SelectFileButton, Button> kvp in _buttons)
            {
                kvp.Value.Update(dt);
                if (kvp.Value.IsClicked() && kvp.Key != SelectFileButton.Delete)
                {
                    _clickedButton = kvp.Key;
                    SMH.SaveManager.CurrentSave = SMH.SaveManager.Saves[_selectedSlot];
                    EnterState(MenuState.ExitingScreen);
                    if (kvp.Key == SelectFileButton.Start && SMH.SaveManager.Saves[_selectedSlot].IsEmpty)
                    {
                        _difficultyPrompt.IsVisible = true;
                    }
                }
            }

            //Click delete button
            if (_buttons[SelectFileButton.Delete].IsClicked())
            {
                if (!SMH.SaveManager.Saves[_selectedSlot].IsEmpty)
                {
                    _deletePromptActive = true;
                }
            }

            //Update save box selections
            if (!_deletePromptActive)
            {
                foreach (KeyValuePair<SaveSlot, SaveBox> kvp in _saveBoxes)
                {
                    if (SMH.Input.IsDown(Input.Attack) && kvp.Value.CollisionBox.Contains(SMH.Input.Cursor))
                    {
                        _selectedSlot = kvp.Key;
                    }
                }
            }

            //Listen for response to delete prompt
            if (_deletePromptActive)
            {
                SaveBox box = _saveBoxes[_selectedSlot];
                _yesDeleteBox = new Rect(box.X + 100f, box.Y + 60f, 50f, 35f);
                _noDeleteBox = new Rect(box.X + 180f, box.Y + 60f, 50f, 35f);

                _mouseOverYes = _yesDeleteBox.Contains(SMH.Input.Cursor);
                _mouseOverNo = _noDeleteBox.Contains(SMH.Input.Cursor);

                if (SMH.Input.IsDown(Input.Attack))
                {
                    if (_mouseOverYes)
                    {
                        SMH.SaveManager.Delete(_selectedSlot);
                        _deletePromptActive = false;
                    }
                    else if (_mouseOverNo)
                    {
                        _deletePromptActive = false;
                    }
                }
            }

            //Move the Smiley selector towards the selected file.
            if (_smileyY > _saveBoxes[_selectedSlot].Y + 45f)
            {
                _smileyY -= 750f * dt;
                if (_smileyY < _saveBoxes[_selectedSlot].Y + 45f)
                {
                    _smileyY = _saveBoxes[_selectedSlot].Y + 45f;
                }
            }
            else if (_smileyY < _saveBoxes[_selectedSlot].Y + 45f)
            {
                _smileyY += 750f * dt;
                if (_smileyY > _saveBoxes[_selectedSlot].Y + 45f)
                {
                    _smileyY = _saveBoxes[_selectedSlot].Y + 45f;
                }
            }
        }

        #endregion

        #region Private Methods

        private void SetWindowPosition(float x, float y)
        {
            _windowX = x;
            _windowY = y;

            _buttons[SelectFileButton.Back].X = _windowX + 360f;
            _buttons[SelectFileButton.Back].Y = _windowY + 340f;
            _buttons[SelectFileButton.Start].X = _windowX + 360f;
            _buttons[SelectFileButton.Start].Y = _windowY + 50f;
            _buttons[SelectFileButton.Delete].X = _windowX + 360f;
            _buttons[SelectFileButton.Delete].Y = _windowY + 165f;

            int i = 0;
            foreach (KeyValuePair<SaveSlot, SaveBox> kvp in _saveBoxes)
            {
                kvp.Value.X = _windowX + 30f;
                kvp.Value.Y = _windowY + 40f + 105f * i;
                kvp.Value.CollisionBox = new Rect(kvp.Value.X, kvp.Value.Y, 285f, 95f);
                i++;
            }

            _smileyX = _saveBoxes[_selectedSlot].X + 30f;
            _smileyY = _saveBoxes[_selectedSlot].Y + 45f;
        }

        #endregion
    }

    public class DifficultyPrompt
    {
        public const float DefaultX = 350f;
        public const float DefaultY = 309f;

        #region Private Variables

        private Difficulty _currentSelection;
        private Rect _okBox;
        private Rect _leftBox;
        private Rect _rightBox;
        private float _x;
        private float _y;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new DifficultyPrompt.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public DifficultyPrompt(float x, float y)
        {
            _x = x;
            _y = y;

            _currentSelection = Difficulty.Medium;
            _leftBox = new Rect(_x + 17f, _y + 65f, 32, 32);
            _rightBox = new Rect(_x + 274f, _y + 65f, 32, 32);
            _okBox = new Rect(_x + 147f, _y + 104f, 32, 32);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Whether or not the DifficultyPrompt is visible.
        /// </summary>
        public bool IsVisible
        {
            get;
            set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the difficulty prompt. Returns the user's selection, or null if no selection
        /// was made this frame.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public Difficulty? Update(float dt)
        {
            if (!IsVisible) return null;

            if (SMH.Input.IsDown(Input.Attack))
            {
                if (_leftBox.Contains(SMH.Input.Cursor))
                {
                    if (_currentSelection != Difficulty.VeryEasy)
                    {
                        _currentSelection--;
                        SMH.Sound.PlaySound(Sound.ButtonClick);
                    }
                }
                else if (_rightBox.Contains(SMH.Input.Cursor))
                {
                    if (_currentSelection != Difficulty.VeryHard)
                    {
                        _currentSelection++;
                        SMH.Sound.PlaySound(Sound.ButtonClick);
                    }
                }
                else if (_okBox.Contains(SMH.Input.Cursor))
                {
                    IsVisible = false;
                    SMH.Sound.PlaySound(Sound.ButtonClick);
                    return _currentSelection;
                }
            }
            return null;
        }

        public void Draw()
        {
            if (!IsVisible) return;

            SMH.Graphics.DrawSprite(Sprites.DifficultyPromptBackground, _x, _y);
            SMH.Graphics.DrawString(SmileyFont.AbilityTitle, "Difficulty", _x + 162.5f, _y + 15f, TextAlignment.Center, Color.Black);
            SMH.Graphics.DrawString(SmileyFont.Segoe14, _currentSelection.GetDescription(), _x + 162.5f, _y + 65f, TextAlignment.Center, Color.Black);

            if (_currentSelection != Difficulty.VeryEasy)
            {
                if (_leftBox.Contains(SMH.Input.Cursor))
                    SMH.Graphics.DrawSprite(Sprites.LeftArrowHighlighted, _x + 35f, _y + 80f);
                else
                    SMH.Graphics.DrawSprite(Sprites.LeftArrow, _x + 35f, _y + 80f);
            }

            if (_currentSelection != Difficulty.VeryHard)
            {
                if (_rightBox.Contains(SMH.Input.Cursor))
                    SMH.Graphics.DrawSprite(Sprites.RightArrowHighlighted, _x + 330f - 40f, _y + 80f);
                else
                    SMH.Graphics.DrawSprite(Sprites.RightArrow, _x + 330f - 40f, _y + 80f);
            }

            if (_okBox.Contains(SMH.Input.Cursor))
                SMH.Graphics.DrawSprite(Sprites.OKButtonHighlighted, _x + 162.5f, _y + 120f);
            else
                SMH.Graphics.DrawSprite(Sprites.OKButton, _x + 162.5f, _y + 120f);

            //SMH.Graphics.DrawRect(_okBox, Color.Red);
            //SMH.Graphics.DrawRect(_leftBox, Color.Red);
            //SMH.Graphics.DrawRect(_rightBox, Color.Red);
        }

        #endregion
    }
}
