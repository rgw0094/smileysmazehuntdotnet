using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Smiley.Lib.Data;
using Microsoft.Xna.Framework.Input;
using Smiley.Lib.Framework.Drawing;
using Smiley.Lib.Enums;

namespace Smiley.Lib.Framework.UIControls
{
    public class Button : GameObject
    {
        private Vector2 _position;
        private bool _isHighlighted;
        private bool _soundPlayedThisFrame;
        private Rect _collisionRect;

        /// <summary>
        /// Constructs a new Button.
        /// </summary>
        /// <param name="position">The top left corner of the button</param>
        /// <param name="text">The text to draw in the button</param>
        public Button(Vector2 position, string text)
        {
            _position = position;
            Text = text;
            _collisionRect = new Rect(position, 250, 75);
        }

        /// <summary>
        /// Gets or sets the button's text.
        /// </summary>
        public string Text
        {
            get;
            set;
        }

        /// <summary>
        /// Returns whether or not the button was clicked this frame.
        /// </summary>
        /// <returns></returns>
        public bool IsClicked()
        {
            if (_isHighlighted && (SMH.Input.IsMouseDown || SMH.Input.IsPressed(Input.Attack)))
            {
                if (!_soundPlayedThisFrame)
                {
                    //smh->soundManager->playSound("snd_ButtonClick");//TODO:
                    _soundPlayedThisFrame = true;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_isHighlighted)
                SmileyData.Sprite_ButtonBackgroundHighlighted.Draw(spriteBatch, _position);
            else
                SmileyData.Sprite_ButtonBackground.Draw(spriteBatch, _position);

            spriteBatch.DrawString(SmileyData.Font_Button, Text, new Vector2(_position.X + _collisionRect.Width / 2.0f, _position.Y + 5.0f), Color.Black);
        }

        public override void Update(float dt)
        {
            _isHighlighted = _collisionRect.Contains(SMH.Input.Cursor);
            _soundPlayedThisFrame = false;
        }
    }
}
