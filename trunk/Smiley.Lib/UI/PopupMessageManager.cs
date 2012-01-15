using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Smiley.Lib.Enums;

namespace Smiley.Lib.UI
{
    public class PopupMessageManager
    {
        #region Private Variables

        private string _message;
        private float _messageDuration;
        private float _timeMessageStarted;
        private float _messageAlpha;
        private bool _adviceManMessageActive;
        private Advice _advice;

        #endregion

        #region Public Methods

        /// <summary>
        /// Draws any currently active messages.
        /// </summary>
        public void Draw()
        {
            if (!SMH.GameTimePassed(_timeMessageStarted, _messageDuration))
            {
                //Determine text alpha - fade out near the end
                if (SMH.GameTimePassed(_timeMessageStarted, _messageDuration * 0.75f))
                {
                    _messageAlpha -= 255f * (1f / (_messageDuration * 0.25f)) * SMH.DT;
                    if (_messageAlpha < 0.0) { _messageAlpha = 0f; _adviceManMessageActive = false; }
                }

                SMH.Graphics.DrawString(SmileyFont.AbilityTitle, _message, 512, 710, Enums.TextAlignment.Center, Color.FromNonPremultiplied(255, 255, 255, (int)_messageAlpha), 0.9f);

                if (_adviceManMessageActive)
                {
                    SMH.Graphics.DrawSprite(Sprites.AdviceManDown, 80, 725, Color.FromNonPremultiplied(255, 255, 255, (int)_messageAlpha), 0f, 0.8f);
                }
            }
        }

        /// <summary>
        /// Updates the PopupMessageManager.
        /// </summary>
        /// <param name="dt"></param>
        public void Update(float dt)
        {
            if (_adviceManMessageActive && SMH.Input.IsDown(Keys.N))
            {
                SMH.WindowManager.OpenAdviceTextBox(_advice);
                _timeMessageStarted = 0f;
                _adviceManMessageActive = false;
            }
        }

        public void ShowFullHealth()
        {
            if (_adviceManMessageActive) return;
            StartMessage("Your health is already full!", 1.5f);
        }

        public void ShowFullMana()
        {
            if (_adviceManMessageActive) return;
            StartMessage("Your mana is already full!", 1.5f);
        }

        public void ShowNewAdvice(Advice advice)
        {
            StartMessage("New advice is available from Monocle Man. Press N to view it now.", 5.5f);
            _advice = advice;
            _adviceManMessageActive = true;
            SMH.Sound.PlaySound(Sound.HintMan);
        }

        public void ShowSaveConfirmation()
        {
            if (_adviceManMessageActive) return;
            StartMessage("Game saved!", 2.5f);
        }

        #endregion

        #region Private Methods

        private void StartMessage(string message, float duration)
        {
            _message = message;
            _messageAlpha = 255f;
            _messageDuration = duration;
            _timeMessageStarted = SMH.GameTime;
            _adviceManMessageActive = false;
        }

        #endregion
    }
}
