using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Smiley.Lib.Data;
using Smiley.Lib.Services;
using Smiley.Lib.Menu;
using Smiley.Lib.Framework;
using Smiley.Lib.Framework.Drawing;
using Smiley.Lib.Enums;
using Smiley.Lib.GameObjects.Environment;
using Smiley.Lib.GameObjects;
using Smiley.Lib.GameObjects.Player;

namespace Smiley.Lib
{
    public class SMH : Game
    {
        #region Private Variables

        private GraphicsDeviceManager _graphicsDeviceManager;
        private static MainMenu _mainMenu;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new SmileyGame.
        /// </summary>
        public SMH()
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this);
            _graphicsDeviceManager.PreferredBackBufferWidth = 1024;
            _graphicsDeviceManager.PreferredBackBufferHeight = 768;

            Content.RootDirectory = "Smiley.Content";
            Window.Title = "Smiley's Maze Hunt";
        }

        #endregion

        #region Static Shit

        public static InputManager Input { get; private set; }
        public static SmileyData Data { get; private set; }
        public static Graphics2DWrapper Graphics { get; private set; }
        public static SoundManager Sound { get; private set; }
        public static SmileyEnvironment Environment { get; private set; }
        public static Player Player { get; private set; }
        public static SaveManager SaveManager { get; private set; }

        /// <summary>
        /// Shows the main menu.
        /// </summary>
        public static void ShowMenu()
        {
            _mainMenu = new MainMenu();
            SMH.Sound.PlayMusic(Music.Menu);
        }

        /// <summary>
        /// Gets the amount of time that has elapsed while not in menus.
        /// </summary>
        public static float GameTime { get; private set; }

        /// <summary>
        /// Returns the current time in seconds.
        /// </summary>
        public static float Now { get; private set; }

        /// <summary>
        /// Returns whether or not the specified amount of time has passed since the start time.
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static bool TimePassed(float startTime, float amount)
        {
            return Now - startTime >= amount;
        }

        /// <summary>
        /// Returns whether or not the specified amount of time has passed while not in a menu.
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static bool GameTimePassed(float startTime, float amount)
        {
            return GameTime - startTime >= amount;
        }

        #endregion

        #region Game Overrides

        protected override void LoadContent()
        {
            Sound = new SoundManager(Content);
            Graphics = new Graphics2DWrapper(_graphicsDeviceManager);            
            Data = new SmileyData(Content);
            Input = new InputManager();
            Environment = new SmileyEnvironment();
            Player = new Player();
            SaveManager = new SaveManager();
        }

        protected override void UnloadContent()
        {
            //TODO: save settings and shit

            Content.Unload();
        }

        protected override void BeginRun()
        {
            ShowMenu();

            Environment.LoadLevel(Level.FOUNTAIN_AREA, Level.FOUNTAIN_AREA, true);

            base.BeginRun();
        }

        protected override void Update(GameTime gameTime)
        {
            Now = (float)gameTime.TotalGameTime.Ticks / 10000000f;
            float dt = (float)gameTime.ElapsedGameTime.Ticks / 10000000f;
            if (true /* not in menu */)
            {
                GameTime += dt;
            }

            Input.Update(dt);
            if (_mainMenu != null)
            {
                _mainMenu.Update(dt);
            }
            else
            {
            }

        }

        protected override void Draw(GameTime gameTime)
        {
            Graphics.BeginFrame();
            if (_mainMenu != null)
            {
                _mainMenu.Draw();
            }
            else
            {
            }
            Graphics.EndFrame();
        }

        #endregion
    }
}
