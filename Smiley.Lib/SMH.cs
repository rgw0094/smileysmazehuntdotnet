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

namespace Smiley.Lib
{
    public class SMH : Game
    {
        #region Private Variables

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new SmileyGame.
        /// </summary>
        public SMH()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Smiley.Content";
        }

        #endregion

        #region Singletons

        public static InputManager Input { get; private set; }
        public static GraphicsDevice VideoDevice { get; private set; }

        #endregion

        #region Game Overrides

        protected override void Initialize()
        {
            Input = new InputManager();
            VideoDevice = this.GraphicsDevice;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SmileyData.Load(Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            //Content.Load<Texture2D>(

            base.Draw(gameTime);
        }

        #endregion
    }
}
