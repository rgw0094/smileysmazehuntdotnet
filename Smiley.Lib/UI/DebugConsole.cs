using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Smiley.Lib.Enums;
using Microsoft.Xna.Framework;
using Smiley.Lib.Framework.Drawing;
using Smiley.Lib.Data;

namespace Smiley.Lib.UI
{
    /// <summary>
    /// Toggle-able debug console that lets you do lots of stuff that makes debugging not suck.
    /// </summary>
    public class DebugConsole
    {
        #region Private Variables

        private bool _debugMovePressed;
        private float _lastDebugMoveTime;
        private int _lineNum;

        #endregion

        #region Properties

        /// <summary>
        /// Whether or not the DebugConsole is open.
        /// </summary>
        public bool IsActive
        {
            get;
            private set;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Draws the console if its active.
        /// </summary>
        public void Draw()
        {
            if (!IsActive)
                return;

            SMH.Graphics.DrawRect(new Rect(10, 145, 130, 225), Color.FromNonPremultiplied(0, 0, 0, 75), true);

            _lineNum = 0;
            WriteLine("Console (Toggle with ~)");
            WriteLine("-----------------------");
            WriteLine("F1 - Warp to debug area ");
            WriteLine("F2 - All abilities      ");
            WriteLine("F3 - 5 of each key      ");
            WriteLine("F4 - 10 gems            ");
            WriteLine("F5 - Full Health/Mana   ");
            WriteLine("F6 - Invincibility (" + (SMH.Player.IsInvincible ? "On" : "Off") + ")");
            WriteLine("F7 - Uber Mode (" + (SMH.Player.IsUber ? "On" : "Off") + ")");
            WriteLine("F8 - Hover (hold)");
            WriteLine("NUM8 -  Move up 1 tile     ");
            WriteLine("NUM5 - Move down 1 tile   ");
            WriteLine("NUM4 - Move left 1 tile   ");
            WriteLine("NUM6 - Move right 1 tile  ");


            SMH.Graphics.DrawString(SmileyFont.Console, string.Format("Player: ({0},{1})", SMH.Player.Tile.X, SMH.Player.Tile.Y), 1000, 5, TextAlignment.Right, Color.White);
        }

        /// <summary>
        /// Updates the DebugConsole.
        /// </summary>
        /// <param name="dt"></param>
        public void Update(float dt)
        {
            if (SMH.Input.IsPressed(Keys.OemTilde))
                IsActive = !IsActive;

            if (!IsActive || SMH.State != GameState.Game)
                return;

            //Teleport to warp zone
            if (SMH.Input.IsPressed(Keys.F1) && SMH.SaveManager.CurrentSave.Level != Level.DEBUG_AREA)
            {
                if (!SMH.AreaChanger.IsChangingAreas)
                {
                    SMH.AreaChanger.ChangeArea(1, 1, Level.DEBUG_AREA);
                }
            }

            if (SMH.Input.IsPressed(Keys.F2))
            {
                foreach (Ability ability in Enum.GetValues(typeof(Ability)))
                {
                    SMH.SaveManager.CurrentSave.HasAbility[ability] = true;
                }
            }

            if (SMH.Input.IsPressed(Keys.F3))
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        SMH.SaveManager.CurrentSave.NumKeys[i, j] += 5;
                    }
                }
            }

            if (SMH.Input.IsPressed(Keys.F4))
            {
                SMH.SaveManager.CurrentSave.Money += 10;
            }

            if (SMH.Input.IsPressed(Keys.F5))
            {
                SMH.Player.Health = SMH.Player.MaxHealth;
                SMH.Player.Mana = SMH.Player.MaxMana;
            }
            
            //Toggle invincibility
            if (SMH.Input.IsPressed(Keys.F6))
            {
                SMH.Player.IsInvincible = !SMH.Player.IsInvincible;
            }

            //Toggle uber-ness (really fast, a shitload of damage)
            if (SMH.Input.IsPressed(Keys.F7))
            {
                SMH.Player.IsUber = !SMH.Player.IsUber;
            }

            //Move smiley with num pad
            int xMove = 0;
            int yMove = 0;

            Keys upKey = Keys.NumPad8;
            Keys downKey = Keys.NumPad5;
            Keys leftKey = Keys.NumPad4;
            Keys rightKey = Keys.NumPad6;

            if (SMH.Input.IsDown(upKey) || SMH.Input.IsDown(downKey) || SMH.Input.IsDown(leftKey) || SMH.Input.IsDown(rightKey))
            {
                if (!_debugMovePressed)
                {
                    _debugMovePressed = true;
                    _lastDebugMoveTime = SMH.GameTime;
                }
                if (SMH.Input.IsPressed(upKey) || (SMH.GameTimePassed(_lastDebugMoveTime, 0.5f) && SMH.Input.IsDown(upKey))) yMove = -1;
                if (SMH.Input.IsPressed(downKey) || (SMH.GameTimePassed(_lastDebugMoveTime, 0.5f) && SMH.Input.IsDown(downKey))) yMove = 1;
                if (SMH.Input.IsPressed(leftKey) || (SMH.GameTimePassed(_lastDebugMoveTime, 0.5f) && SMH.Input.IsDown(leftKey))) xMove = -1;
                if (SMH.Input.IsPressed(rightKey) || (SMH.GameTimePassed(_lastDebugMoveTime, 0.5f) && SMH.Input.IsDown(rightKey))) xMove = 1;
            }
            else
            {
                _debugMovePressed = false;
            }

            if (xMove != 0 || yMove != 0)
            {
                SMH.Player.MoveTo(SMH.Player.Tile.X + xMove, SMH.Player.Tile.Y + yMove);
            }
        }

        #endregion

        #region Private Methods

        private void WriteLine(string text)
        {
            SMH.Graphics.DrawString(Data.SmileyFont.Console, text, 15, 150 + _lineNum * 15, TextAlignment.Left, Color.White);
            _lineNum++;
        }

        #endregion
    }
}
