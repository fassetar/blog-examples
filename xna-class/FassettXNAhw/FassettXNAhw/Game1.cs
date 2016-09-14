#region File Description
//-----------------------------------------------------------------------------
// Game1.cs
//-----------------------------------------------------------------------------
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using FassettXNAhw.Managers;
using FassettXNAhw.NonDrawableComponents;
using FassettXNAhw.DrawableComponents;


namespace FassettXNAhw
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        //private int preferredWindowWidth = 1280;
        //private int preferredWindowHeight = 720;
        SpriteBatch spriteBatch;
        StartScreen startScreen;
        //Screen manager
        public static ScreenManager screenManager;
        //Game State items
        public GamePlayScreen gamePlayScreen;
        public readonly SpaceShip spaceship;
        public readonly SpaceShip.type1 spaceEnemy;
        
        public SpriteBatch GameSpriteBatch
        {
            get { return spriteBatch; }
        }

        GraphicsDeviceManager graphics;
        InputManager di;
        SoundManager soundManager;
        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            //this.graphics.PreferredBackBufferWidth = preferredWindowWidth;
            //this.graphics.PreferredBackBufferHeight = preferredWindowHeight;   
            Content.RootDirectory = "Content";
            di = new InputManager(this);
            this.Components.Add(di);
            screenManager = new ScreenManager(this);
            Components.Add(screenManager);
            spaceship = new SpaceShip(this);
            spaceEnemy = new SpaceShip.type1(this);
            gamePlayScreen = new GamePlayScreen(this, screenManager, spaceship);
            startScreen = new StartScreen(this, screenManager, gamePlayScreen);
            screenManager.PushScreen(gamePlayScreen);
            screenManager.PushScreen(startScreen);        
          }
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            soundManager = new SoundManager(Content);
            spaceship.LoadContent();
            //soundManager.PlayBackgroundMusic();
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            if (InputManager.KeyPressed(Keys.Escape))
            {    
                screenManager.ChangeScreens(startScreen);
                //if (InputManager.LastKeyboardState.Equals(new KeyboardState(Keys.Escape)))
                //{   //hotkey for exiting the game...if already on main menu.
                //    this.Exit();
                //}
            }
            if (InputManager.KeyPressed(Keys.F10))
            {
                //if (InputManager.LastKeyboardState.Equals(InputManager.KeyPressed(Keys.F10)))
                //{
                //    this.graphics.ToggleFullScreen();
                //when in gamePlayScreen and person want full screen f10 again.
                //}
                screenManager.ChangeScreens(gamePlayScreen);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }   // end of Game1 class

}  // end of namespace
