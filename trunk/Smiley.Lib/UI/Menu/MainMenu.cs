using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework;
using Microsoft.Xna.Framework.Graphics;
using Smiley.Lib.Data;
using Microsoft.Xna.Framework;
using Smiley.Lib.Enums;

namespace Smiley.Lib.UI.Menu
{
    public class MainMenu
    {
        #region Private Variables

        private BaseMenuScreen _currentScreen;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new MainMenu.
        /// </summary>
        public MainMenu()
        {
            ShowScreen<TitleScreen>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Changes the menu to a new screen.
        /// </summary>
        /// <typeparam name="TScreen">The type of screen to show.</typeparam>
        public void ShowScreen<TScreen>()
            where TScreen : BaseMenuScreen
        {
            ShowScreen((BaseMenuScreen)typeof(TScreen).GetConstructor(new Type[] { typeof(MainMenu) }).Invoke(new object[] { this }));
        }

        /// <summary>
        /// Changes the menu to a new screen
        /// </summary>
        /// <param name="screen">The new screen to show.</param>
        public void ShowScreen(BaseMenuScreen screen)
        {
            _currentScreen = screen;
        }

        public void OpenLoadScreen(SaveSlot saveSlot, bool fromLoadingScreen)
        {
            //TODO:
            //ShowScreen(new LoadingScreen(file, fromLoadingScreen));
        }

        #endregion

        #region GameObject Overrides

        public void Update(float dt)
        {
            if (_currentScreen != null)
            {
                _currentScreen.Update(dt);
            }
        }

        public void Draw()
        {
            if (_currentScreen.ShouldDrawBackground)
            {
                SMH.Graphics.DrawSprite(Sprites.MenuBackground, 0.0f, 0.0f);
                SMH.Graphics.DrawString(SmileyFont.Controls, "www.smileysmazehunt.com", 1010f, 740f, TextAlignment.Right, Color.Black,  0.9f);
            }

            _currentScreen.Draw();

            if (SMH.Input.IsCursorInWindow && _currentScreen.ShouldDrawMouse)
            {
                SMH.Graphics.DrawSprite(Sprites.MouseCursor, SMH.Input.Cursor.X, SMH.Input.Cursor.Y);
            }
        }

        #endregion
    }
}
