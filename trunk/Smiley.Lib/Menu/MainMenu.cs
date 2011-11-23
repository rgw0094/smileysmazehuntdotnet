using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework;
using Microsoft.Xna.Framework.Graphics;
using Smiley.Lib.Data;
using Microsoft.Xna.Framework;

namespace Smiley.Lib.Menu
{
    public class MainMenu : GameObject
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
            ShowScreen((BaseMenuScreen)typeof(TScreen).GetConstructor(Type.EmptyTypes).Invoke(null));
        }

        /// <summary>
        /// Changes the menu to a new screen
        /// </summary>
        /// <param name="screen">The new screen to show.</param>
        public void ShowScreen(BaseMenuScreen screen)
        {
            _currentScreen = screen;
        }

        public void OpenLoadScreen(int file, bool fromLoadingScreen)
        {
            //TODO:
            //ShowScreen(new LoadingScreen(file, fromLoadingScreen));
        }

        #endregion

        #region GameObject Overrides

        public override void Update(float dt)
        {
            if (_currentScreen != null)
            {
                _currentScreen.Update(dt);
            }
        }

        public override void Draw()
        {
            if (_currentScreen.ShouldDrawBackground)
            {
                SMH.Graphics.DrawSprite(SMH.Data.Sprite_MenuBackground, 0.0f, 0.0f);

                //smh->resources->GetFont("controls")->SetScale(0.9);
                SMH.Graphics.DrawString(SMH.Data.Font_Controls, "www.smileysmazehunt.com", 760f, 740f);
                //smh->resources->GetFont("controls")->SetScale(1.0);
            }

            _currentScreen.Draw();

            if (SMH.Input.IsCursorInWindow && _currentScreen.ShouldDrawMouse)
            {
                SMH.Graphics.DrawSprite(SMH.Data.Sprite_MouseCursor, SMH.Input.Cursor);
            }
        }

        #endregion
    }
}
