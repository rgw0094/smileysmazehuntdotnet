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

namespace Smiley.Lib
{
    public class SMH : Game
    {
        #region Private Variables

        private GraphicsDeviceManager _graphicsDeviceManager;

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
        public static MainMenu MainMenu { get; private set; }

        /// <summary>
        /// Returns the current time in seconds.
        /// </summary>
        public static float Now
        {
            get;
            private set;
        }

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

        #endregion

        #region Game Overrides

        protected override void LoadContent()
        {
            Graphics = new Graphics2DWrapper(_graphicsDeviceManager);            
            Data = new SmileyData(Content);
            Input = new InputManager();
            MainMenu = new MainMenu();
        }

        protected override void UnloadContent()
        {
            //TODO: save settings and shit

            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            Now = (float)gameTime.TotalGameTime.Ticks / 10000000f;
            float dt = (float)gameTime.ElapsedGameTime.Ticks / 10000000f;

            MainMenu.Update(dt);
            Input.Update(dt);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Graphics.BeginFrame();
            MainMenu.Draw();
            Graphics.EndFrame();
        }

        #endregion
    }
}
