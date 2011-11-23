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
    public class InputManager : GameObject
    {
        private const string ConfigFileName = "Smiley.INI";
        private const string InputSection = "Input";

        #region Private Variables

        private Dictionary<Input, InputState> _inputs = new Dictionary<Input, InputState>();

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new InputManager.
        /// </summary>
        public InputManager()
        {
            LoadInputs();
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

        /// <summary>
        /// Gets whether or not the left mouse button is currently down. This will always be false
        /// if we are running in an environment with no mouse.
        /// </summary>
        public bool IsMouseDown
        {
            get;
            private set;
        }

        #endregion

        #region Public Methods

        public override void Update(float dt)
        {
#if WINDOWS
            MouseState mouseState = Mouse.GetState();
            Cursor = new Vector2(mouseState.X, mouseState.Y);
            IsCursorInWindow = SMH.Graphics.IsPointInWindow(Cursor);
            IsMouseDown = mouseState.LeftButton == ButtonState.Pressed;

            if (IsMouseDown)
            {
            }
#endif

#if XBOX
            IsCursorInWindow = true;
#endif
            
            //Update the input state for this frame.
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();
            foreach (InputState input in _inputs.Values)
            {
                input.WasDownLastFrame = input.IsDown;
                input.IsDown = false;
                if (input.Device == InputDevice.GamePad)
                {
                    if (gamePadState.IsButtonDown(input.Button))
                    {
                        input.IsDown = true;
                    }
                }
                else if (input.Device == InputDevice.Keyboard)
                {
                    if (keyboardState.IsKeyDown(input.Key))
                    {
                        input.IsDown = true;
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
        }

        /// <summary>
        /// Gets whether or not an input is currently pressed.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsDown(Input input)
        {
            return _inputs[input].IsDown;
        }

        /// <summary>
        /// Gets whether or not an input was pressed this frame.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsPressed(Input input)
        {
            return _inputs[input].IsDown && !_inputs[input].WasDownLastFrame;
        }

        /// <summary>
        /// Checks for any input this frame, and if there is any, registers that as the new
        /// input method for the given input type.
        /// </summary>
        /// <param name="input"></param>
        public void ListenForNewInput(Input input)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            foreach (Buttons button in Enum.GetValues(typeof(Buttons)))
            {
                if (gamePadState.IsButtonDown(button))
                {
                    _inputs[input].Button = button;
                    _inputs[input].Device = InputDevice.GamePad;
                    return;
                }
            }

            KeyboardState state = Keyboard.GetState();
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                _inputs[input].Key = key;
                _inputs[input].Device = InputDevice.Keyboard;
            }
        }

        public void SaveInputs()
        {
            //TODO:
        }

        public void LoadInputs()
        {
            //TODO: load from config

            foreach (Input input in Enum.GetValues(typeof(Input)))
            {
                _inputs[input] = new InputState { Device = InputDevice.Keyboard };
            }

            _inputs[Input.Ability].Key = Keys.Space;
            _inputs[Input.Aim].Key = Keys.LeftAlt;
            _inputs[Input.Attack].Key = Keys.Space;
            _inputs[Input.Up].Key = Keys.Up;
            _inputs[Input.Down].Key = Keys.Down;
            _inputs[Input.Left].Key = Keys.Left;
            _inputs[Input.Right].Key = Keys.Right;
            _inputs[Input.PreviousAbility].Key = Keys.Z;
            _inputs[Input.NextAbility].Key = Keys.X;
            _inputs[Input.Pause].Key = Keys.I;
        }

        #endregion

        #region Private Classes

        [Serializable]
        private class InputState
        {
            public bool IsDown { get; set; }
            public bool WasDownLastFrame { get; set; }
            public bool InEditMode { get; set; }
            public InputDevice Device { get; set; }
            public Keys Key { get; set; }
            public Buttons Button { get; set; }
        }

        #endregion
    }
}
