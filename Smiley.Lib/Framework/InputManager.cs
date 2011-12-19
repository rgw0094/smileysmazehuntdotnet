using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework;
using Microsoft.Xna.Framework.Graphics;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Configuration;
using System.IO;

namespace Smiley.Lib.Services
{
    public class InputManager
    {
        #region Private Variables

        private bool _wasMouseDown;
        private Dictionary<Input, bool> _inputIsDown = new Dictionary<Input, bool>();
        private Dictionary<Input, bool> _inputWasDown = new Dictionary<Input, bool>();
        private Keys[] _keysDownLastFrame = new Keys[1];
        private Keys[] _keysDown = new Keys[1];

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new InputManager.
        /// </summary>
        public InputManager()
        {
            foreach (Input input in Enum.GetValues(typeof(Input)))
            {
                _inputIsDown[input] = false;
                _inputWasDown[input] = false;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current location of the cursor.
        /// </summary>
        public Vector2 Cursor
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets whether or not the cursor is currently in the game window. This will always
        /// be true if we are running in an environment with no mouse.
        /// </summary>
        public bool IsCursorInWindow
        {
            get;
            private set;
        }

        #endregion

        #region Public Methods

        public void Update(float dt)
        {
            //Update the input state for this frame.
            KeyboardState keyboardState = Keyboard.GetState();
            _keysDownLastFrame = _keysDown;
            _keysDown = keyboardState.GetPressedKeys();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            foreach (SmileyInputConfig inputConfig in SMH.ConfigManager.Config.Inputs)
            {
                _inputWasDown[inputConfig.Input] = _inputIsDown[inputConfig.Input];
                _inputIsDown[inputConfig.Input] = false;

                if (inputConfig.Device == InputDevice.GamePad)
                {
                    if (gamePadState.IsButtonDown((Buttons)inputConfig.Code))
                    {
                        _inputIsDown[inputConfig.Input] = true;
                    }
                }
                else if (inputConfig.Device == InputDevice.Keyboard)
                {
                    if (keyboardState.IsKeyDown((Keys)inputConfig.Code))
                    {
                        _inputIsDown[inputConfig.Input] = true;
                    }
                }
            }

            //Alow the cursor to move via directional input in case theres no mouse
            if (IsCursorInWindow)
            {
                float mouseX = Cursor.X;
                float mouseY = Cursor.Y;
                if (IsDown(Input.Left)) mouseX -= 700.0f * dt;
                if (IsDown(Input.Right)) mouseX += 700.0f * dt;
                if (IsDown(Input.Up)) mouseY -= 700.0f * dt;
                if (IsDown(Input.Down)) mouseY += 700.0f * dt;

                if (mouseX != Cursor.X || mouseY != Cursor.Y)
                {
                    Cursor = new Vector2(mouseX, mouseY);
#if WINDOWS
                    Mouse.SetPosition((int)mouseX, (int)mouseY);
#endif
                }
            }

#if WINDOWS
            MouseState mouseState = Mouse.GetState();
            Cursor = new Vector2(mouseState.X, mouseState.Y);
            IsCursorInWindow = SMH.Graphics.IsPointInWindow(Cursor);

            //Let clicking the mouse also count as the confirm/attack input
            bool isMouseDown = mouseState.LeftButton == ButtonState.Pressed;
            if (isMouseDown)
            {
                if (!_wasMouseDown)
                    _inputWasDown[Input.Attack] = false;
                _inputIsDown[Input.Attack] = true;
            }
            _wasMouseDown = isMouseDown;
#endif

#if XBOX
            IsCursorInWindow = true;
#endif
        }

        /// <summary>
        /// Gets whether or not an input is currently pressed.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsDown(Input input)
        {
            return _inputIsDown[input];
        }

        /// <summary>
        /// Gets whether or not an input is currently pressed.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsDown(Keys key)
        {
            return !_keysDownLastFrame.Contains(key) && _keysDown.Contains(key);
        }

        /// <summary>
        /// Gets whether or not an input was pressed this frame.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsPressed(Input input)
        {
            return _inputIsDown[input] && !_inputWasDown[input];
        }

        /// <summary>
        /// Gets whether or not the given keyboard key was pressed this frame.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsPressed(Keys key)
        {
            return !_keysDownLastFrame.Contains(key) && _keysDown.Contains(key);
        }

        /// <summary>
        /// Checks for any input this frame, and if there is any, registers that as the new
        /// input method for the given input type.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Whether or not a new input was accepted.</returns>
        public bool ListenForNewInput(Input input)
        {
            SmileyInputConfig config = SMH.ConfigManager.Config.Inputs.Single(i => i.Input == input);

            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            foreach (Buttons button in Enum.GetValues(typeof(Buttons)))
            {
                if (gamePadState.IsButtonDown(button))
                {
                    config.Device = InputDevice.GamePad;
                    config.Code = (int)button;
                    return true;
                }
            }

            KeyboardState state = Keyboard.GetState();
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (state.IsKeyDown(key))
                {
                    config.Device = InputDevice.Keyboard;
                    config.Code = (int)key;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets a user friendly description for the key or button currently configured
        /// for the given input.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string GetInputDescription(Input input)
        {
            SmileyInputConfig config = SMH.ConfigManager.Config.Inputs.Single(i => i.Input == input);

            if (config.Device == InputDevice.Keyboard)
            {
                return ((Keys)config.Code).ToString();
            }
            else //if (_inputs[input].Device == InputDevice.GamePad)
            {
                return ((Buttons)config.Code).ToString();
            }
        }

        #endregion
    }
}
