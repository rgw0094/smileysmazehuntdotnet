﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smiley.Lib.Framework;

namespace Smiley.Lib.UI.Menu
{
    public enum MenuState
    {
        EnteringScreen,
        InScreen,
        ExitingScreen
    }

    public abstract class BaseMenuScreen
    {
        /// <summary>
        /// Constructs a new BaseMenuScreen.
        /// </summary>
        /// <param name="mainMenu"></param>
        public BaseMenuScreen(MainMenu mainMenu)
        {
            MainMenu = mainMenu;
        }

        /// <summary>
        /// Whether or not the mouse should be drawn for this screen.
        /// </summary>
        public abstract bool ShouldDrawMouse
        {
            get;
        }

        /// <summary>
        /// Whether or not the background should be drawn for this screen.
        /// </summary>
        public abstract bool ShouldDrawBackground
        {
            get;
        }

        /// <summary>
        /// The current state.
        /// </summary>
        protected MenuState State
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the MainMenu.
        /// </summary>
        protected MainMenu MainMenu
        {
            get;
            private set;
        }
        
        /// <summary>
        /// The time the state was entered.
        /// </summary>
        protected float TimeEnteredState
        {
            get;
            private set;
        }

        /// <summary>
        /// Draws the screen.
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Updates the screen.
        /// </summary>
        /// <param name="dt"></param>
        public abstract void Update(float dt);

        protected void EnterState(MenuState newState)
        {
            State = newState;
            TimeEnteredState = SMH.Now;
        }
    }
}
