using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework;
using Microsoft.Xna.Framework.Graphics;
using Smiley.Lib.Data;

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
            ShowScreen((BaseMenuScreen)typeof(TScreen).GetType().GetConstructor(Type.EmptyTypes).Invoke(null));
        }

        /// <summary>
        /// Changes the menu to a new screen
        /// </summary>
        /// <param name="screen">The new screen to show.</param>
        public void ShowScreen(BaseMenuScreen screen)
        {
            _currentScreen = screen;
        }

        /// <summary>
        /// Closes the current screen.
        /// </summary>
        public void CloseScreen()
        {
            _currentScreen = null;
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            //TODO:
            //        if (currentScreen != MenuScreens::CLOSING_CINEMATIC_SCREEN && 
            //    currentScreen != MenuScreens::CINEMATIC_SCREEN &&
            //    currentScreen != MenuScreens::CREDITS_SCREEN)
            //{
            SmileyData.Sprite_MenuBackground.Draw(spriteBatch, 0.0f, 0.0f);

            //smh->resources->GetFont("controls")->SetScale(0.9);
            //smh->resources->GetFont("controls")->printf(1015.0, 740.0, HGETEXT_RIGHT, "www.smileysmazehunt.com");
            //smh->resources->GetFont("controls")->SetScale(1.0);
            //}

            if (_currentScreen != null)
            {
                _currentScreen.Draw(spriteBatch);
            }
        }

        #endregion
    }
}
