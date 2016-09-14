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

using FassettXNAhw.NonDrawableComponents;


namespace FassettXNAhw.DrawableComponents
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class StartScreen : GameScreen
    {

        #region Field and Properties Region
        Menu menu;
        SpriteFont spriteFont;
        GamePlayScreen gamePlayScreen;
        string[] menuItems =
          { "Show Instructions", "Start New Game", "Load Previous Game", "Exit" };

        ScreenManager manager;

        Game1 theGame;

        ScreenBackground bkg;

        #endregion



        #region Constructors Region

        public StartScreen(Game game, ScreenManager manager, GamePlayScreen gamePlayScreen)
            : base(game)
        {
            Content = Game.Content;
            this.manager = manager;
            this.gamePlayScreen = gamePlayScreen;
            theGame = (Game1)game;


            // TODO: Construct any child components here
        }

        #endregion


        #region  XNA API methods region

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }



        protected override void LoadContent()
        {
            spriteFont = Content.Load<SpriteFont>("menuFont");

            menu = new Menu(theGame, spriteFont, menuItems);
            Vector2 menuPosition = new Vector2(
            (Game.Window.ClientBounds.Width - menu.Width) / 2,
            (Game.Window.ClientBounds.Height - menu.Height) / 2);
            menu.SetPosition(menuPosition);

            bkg = new ScreenBackground(
                theGame,
                Content.Load<Texture2D>("paisaje_artico"), DrawMode.Fill);

            base.LoadContent();
        }




        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            menu.Update(gameTime);
            if (InputManager.KeyPressed(Keys.Enter))
            {
                manager.ChangeScreens(gamePlayScreen);
            }

            
            base.Update(gameTime);
        }


        /// <summary>
        /// Allows the game component to draw itself.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            bkg.Draw(gameTime);
            menu.Draw(gameTime);
            base.Draw(gameTime);
        }

        #endregion
    }
}
