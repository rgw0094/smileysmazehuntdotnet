using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Data;
using Microsoft.Xna.Framework;
using Smiley.Lib.Enums;

namespace Smiley.Lib.UI.Windows
{
    /// <summary>
    /// The 3 pages that you can cycle through in the in-game menu.
    /// </summary>
    public enum GameMenuWindow
    {
        Inventory = 0,
        AreaMap = 1,
        WorldMap = 2
    }

    public class WindowManager
    {
        #region Private Variables

        private GameMenuWindow _currentGameMenuWindow;
        private BaseWindow _activeWindow;

        #endregion

        #region Properties

        public bool IsTextBoxOpen
        {
            get;
            private set;
        }

        public bool IsWindowOpen
        {
            get { return _activeWindow != null; }
        }

        public int FrameLastWindowClosed
        {
            get;
            private set;
        }

        public bool IsGameMenuOpen
        {
            get;
            private set;
        }

        #endregion

        #region Public Methods

        public void Update(float dt)
        {
            if (SMH.Input.IsPressed(Input.Pause))
            {
                if (IsGameMenuOpen)
                    CloseWindow();
                else if (!IsWindowOpen)
                    OpenGameMenu();
                return;
            }

            //Handle input for scrolling through game menu windows
            if (IsGameMenuOpen)
            {
                if (SMH.Input.IsPressed(Input.PreviousAbility))
                {
                    if (_currentGameMenuWindow == GameMenuWindow.Inventory)
                        _currentGameMenuWindow = GameMenuWindow.WorldMap;
                    else if (_currentGameMenuWindow == GameMenuWindow.WorldMap)
                        _currentGameMenuWindow = GameMenuWindow.AreaMap;
                    else if (_currentGameMenuWindow == GameMenuWindow.AreaMap)
                        _currentGameMenuWindow = GameMenuWindow.Inventory;
                    OpenGameMenu(_currentGameMenuWindow);
                    SMH.Sound.PlaySound(Sound.ChangeMenu);
                }
                else if (SMH.Input.IsPressed(Input.NextAbility))
                {
                    if (_currentGameMenuWindow == GameMenuWindow.Inventory)
                        _currentGameMenuWindow = GameMenuWindow.AreaMap;
                    else if (_currentGameMenuWindow == GameMenuWindow.AreaMap)
                        _currentGameMenuWindow = GameMenuWindow.WorldMap;
                    else if (_currentGameMenuWindow == GameMenuWindow.WorldMap)
                        _currentGameMenuWindow = GameMenuWindow.Inventory;
                    OpenGameMenu(_currentGameMenuWindow);
                    SMH.Sound.PlaySound(Sound.ChangeMenu);
                }
            }

            //When the text box is open keep updating Smiley's tongue
            if (IsTextBoxOpen)
            {
                SMH.Player.Tongue.Update(dt);
            }

            //If the active window returns false, close it

            if (_activeWindow != null && !_activeWindow.Update(dt)) CloseWindow();
        }

        public void Draw()
        {
            if (_activeWindow != null)
                _activeWindow.Draw();

            //Draw flashing arrows for the menu windows
            if (IsGameMenuOpen)
            {
                float flashingAlpha = 255f;
                float n = 0.6f;
                float x = SMH.Now;
                while (x > n) x -= n;
                if (x < n / 2.0)
                {
                    flashingAlpha = 255f * (x / (n / 2f));
                }
                else
                {
                    flashingAlpha = 255f - 255f * ((x - n / 2f) / (n / 2f));
                }

                SMH.Graphics.DrawSprite(Sprites.WindowArrowLeft, 160, 384, Color.FromNonPremultiplied(255, 255, 255, (int)flashingAlpha));
                SMH.Graphics.DrawSprite(Sprites.WindowArrowRight, 864, 384, Color.FromNonPremultiplied(255, 255, 255, (int)flashingAlpha));
            }
        }

        /// <summary>
        /// Opens the game menu to the most recent window.
        /// </summary>
        public void OpenGameMenu()
        {
            OpenGameMenu(_currentGameMenuWindow);
        }

        /// <summary>
        /// Opens the game menu to the specified menu window.
        /// </summary>
        /// <param name="whichWindow"></param>
        public void OpenGameMenu(GameMenuWindow window)
        {
            if (window == GameMenuWindow.Inventory)
            {
                _activeWindow = new Inventory();
            }
            else if (window == GameMenuWindow.AreaMap)
            {
                _activeWindow = new AreaMap();
            }
            else if (window == GameMenuWindow.WorldMap)
            {
                _activeWindow = new WorldMap();
            }

            IsGameMenuOpen = true;
        }

        /// <summary>
        /// If there is currently a window open, closes it.
        /// </summary>
        public void CloseWindow()
        {
            IsGameMenuOpen = false;
            IsTextBoxOpen = false;
            _activeWindow = null;
        }

        /// <summary>
        /// Opens the minimenu.
        /// </summary>
        /// <param name="mode"></param>
        public void OpenMiniMenu(MiniMenuMode mode)
        {
            _activeWindow = new MiniMenu(mode);
        }

        /// <summary>
        /// Opens the options window.
        /// </summary>
        public void OpenOptionsWindow()
        {
            _activeWindow = new OptionsWindow();
        }

        /// <summary>
        /// Opens the shop.
        /// </summary>
        public void OpenShop()
        {
            _activeWindow = new Shop();
        }

        /// <summary>
        /// Opens the hint text box.
        /// </summary>
        public void OpenHintTextBox()
        {
            //TODO:_activeWindow = new TextBox();
        }

        /// <summary>
        /// Opens the advice window.
        /// </summary>
        public void OpenAdviceWindow()
        {
            _activeWindow = new AdviceWindow();
        }

        /// <summary>
        /// Opens the textbox to display a sign.
        /// </summary>
        /// <param name="signID"></param>
        public void SignTextBox(int signID)
        {
        }

        /// <summary>
        /// Opens a TextBox to indicate that a new ability has been acquired.
        /// </summary>
        /// <param name="ability"></param>
        public void OpenNewAbilityTextBox(Ability ability)
        {
        }

        /// <summary>
        /// Opens a TextBox to display dialog with an NPC.
        /// </summary>
        /// <param name="npcID"></param>
        /// <param name="textID"></param>
        public void OpenDialogTextBox(int npcID, int textID)
        {
        }

        /// <summary>
        /// Opens a TextBox to display advice from the MonocleMan.
        /// </summary>
        /// <param name="advice"></param>
        public void OpenAdviceTextBox(Advice advice)
        {
        }

        /// <summary>
        /// Opens a TextBox to display a sign.
        /// </summary>
        /// <param name="signID"></param>
        public void OpenSignTextBox(int signID)
        {
        }

        #endregion
    }
}
