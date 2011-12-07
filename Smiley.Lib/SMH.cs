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
using Smiley.Lib.Windows;
using Smiley.Lib.GameObjects.Enemies;
using Smiley.Lib.GameObjects.NPCs;

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

            Game = this;
            Content.RootDirectory = "Smiley.Content";
            Window.Title = "Smiley's Maze Hunt";
        }

        #endregion

        #region Static Shit

        public static GameState State { get; private set; }
        public static int CurrentFrame { get; private set; }
        public static DebugConsole Console { get; private set; }
        public static Random Random { get; private set; }
        public static InputManager Input { get; private set; }
        public static SmileyData Data { get; private set; }
        public static Game Game { get; private set; }
        public static Graphics2DWrapper Graphics { get; private set; }
        public static SoundManager Sound { get; private set; }
        public static SmileyEnvironment Environment { get; private set; }
        public static Player Player { get; private set; }
        public static GUI GUI { get; private set; }
        public static SaveManager SaveManager { get; private set; }
        public static WindowManager WindowManager { get; private set; }
        public static EnemyManager EnemyManager { get; private set; }
        public static NPCManager NPCManager { get; private set; }
        public static ProjectileManager ProjectileManager { get; private set; }

        /// <summary>
        /// Shows the main menu.
        /// </summary>
        public static void ShowMenu()
        {
            State = GameState.Menu;
            _mainMenu = new MainMenu();
            SMH.Sound.PlayMusic(Music.Menu);
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        public static void StartGame()
        {
            State = GameState.Game;
            _mainMenu = null;
            Environment.LoadLevel(SMH.SaveManager.CurrentSave.Level, SMH.SaveManager.CurrentSave.Level, true);
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

        /// <summary>
        /// Returns the onscreen x coordinate for a global x coordinate.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float GetScreenX(float x)
        {
            return x - SMH.Player.X + 512;
        }

        /// <summary>
        /// Returns the onscreen y coordinate for a global y coordinate.
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        public static float GetScreenY(float y)
        {
            return y - SMH.Player.Y + 384;
        }

        #endregion

        #region Game Overrides

        protected override void LoadContent()
        {
            Console = new DebugConsole();
            Sound = new SoundManager(Content);
            Graphics = new Graphics2DWrapper(_graphicsDeviceManager);            
            Data = new SmileyData(Content);
            Input = new InputManager();
            Environment = new SmileyEnvironment();
            Player = new Player();
            GUI = new GUI();
            SaveManager = new SaveManager();
            WindowManager = new WindowManager();
            EnemyManager = new EnemyManager();
            NPCManager = new NPCManager();
            ProjectileManager = new ProjectileManager();
            Random = new Random();
        }

        protected override void UnloadContent()
        {
            //TODO: save settings and shit

            Content.Unload();
        }

        protected override void BeginRun()
        {
            ShowMenu();
            base.BeginRun();
        }

        protected override void Update(GameTime gameTime)
        {
            CurrentFrame++;
            Now = (float)gameTime.TotalGameTime.Ticks / 10000000f;
            float dt = (float)gameTime.ElapsedGameTime.Ticks / 10000000f;

            Input.Update(dt);

            if (State == GameState.Menu)
            {
                _mainMenu.Update(dt);
            }
            else if (State == GameState.Game)
            {
                GameTime += dt;

                Console.Update(dt);
                Environment.Update(dt);
                Player.Update(dt);
                GUI.Update(dt);
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
                Environment.Draw();
                Player.Draw();
                GUI.Draw();
                Environment.DrawAfterSmiley();
                Console.Draw();
            }
            Graphics.EndFrame();
        }

        #endregion
    }
}
