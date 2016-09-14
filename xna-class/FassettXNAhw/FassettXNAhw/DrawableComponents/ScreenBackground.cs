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


namespace FassettXNAhw.DrawableComponents
{
    public enum DrawMode { Center, Fill }

    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ScreenBackground : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region Fields and Properties Region

        Rectangle screenRectangle;
        Rectangle destination;
        Texture2D image;
        DrawMode drawMode;

        Game1 theGame;

        
        #endregion


        #region  Constructors Region

        public ScreenBackground(Game game, Texture2D image, DrawMode drawMode)
            : base(game)
        {
            theGame = (Game1)game;

            Visible = true;
            this.image = image;
            this.drawMode = drawMode;
            screenRectangle = new Rectangle(
            0,
            0,
            game.Window.ClientBounds.Width,
            game.Window.ClientBounds.Height);
            switch (drawMode)
            {
                case DrawMode.Center:
                    destination = new Rectangle(
                    (screenRectangle.Width - image.Width) / 2,
                    (screenRectangle.Height - image.Height) / 2,
                    image.Width,
                    image.Height);
                    break;
                case DrawMode.Fill:
                    destination = new Rectangle(
                    screenRectangle.X,
                    screenRectangle.Y,
                    screenRectangle.Width,
                    screenRectangle.Height);
                    break;
            }

            // TODO: Construct any child components here
        }

        #endregion


        #region   XNA API Methods Region

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }



        /// <summary>
        /// Allows the game component to draw itself.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            if (Visible)
            {
                theGame.GameSpriteBatch.Begin();
                theGame.GameSpriteBatch.Draw(image, destination, Color.White);
                theGame.GameSpriteBatch.End();
            }

            base.Draw(gameTime);
        }

        #endregion

    }

}