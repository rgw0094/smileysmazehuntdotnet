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
    public class Button : BaseControl
    {
        private bool _isHighlighted;
        private bool _soundPlayedThisFrame;
        private Rect _collisionRect;

        /// <summary>
        /// Constructs a new Button.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="text">The text to draw in the button</param>
        public Button(float x, float y, string text)
        {
            X = x;
            Y = y;
            Text = text;
            _collisionRect = new Rect(x, y, 250, 75);
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

        public override void Draw()
        {
            if (_isHighlighted)
                SMH.Graphics.DrawSprite(SMH.Data.Sprite_ButtonBackgroundHighlighted, X, Y);
            else
                SMH.Graphics.DrawSprite(SMH.Data.Sprite_ButtonBackground, X, Y);

            SMH.Graphics.DrawStringCentered(SMH.Data.Font_Button, Text, X + _collisionRect.Width / 2.0f, Y + 5.0f);
        }

        public override void Update(float dt)
        {
            _isHighlighted = _collisionRect.Contains(SMH.Input.Cursor);
            _soundPlayedThisFrame = false;
        }
    }
}
