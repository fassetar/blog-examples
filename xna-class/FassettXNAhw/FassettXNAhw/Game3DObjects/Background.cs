#region File Description
//-----------------------------------------------------------------------------
// ScrollingBackground.cs
//
//-----------------------------------------------------------------------------
#endregion


using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FassettXNAhw;


namespace FassettXNAhw.DrawableComponents
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Background : Microsoft.Xna.Framework.DrawableGameComponent
    {

        #region   Fields and Properties

        private ContentManager Content;

        private Game1 theGame;

        private SpriteBatch spriteBatch;

        Texture2D backgrnd;

        SpriteFont Font1;

        Vector2 FontPos;
        Vector2 FontPos2;

        #endregion



        public Background(Game game)
            : base(game)
        {
            // TODO: Construct any child components here

            this.Content = game.Content;

            theGame = (Game1)game;

        }

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
            spriteBatch = new SpriteBatch(theGame.GraphicsDevice);

            backgrnd = Content.Load<Texture2D>("starfield");

            Font1 = Content.Load<SpriteFont>("kootenay");

            // TODO: Load your game content here            
            FontPos = new Vector2( 100/*GraphicsDevice.Viewport.Width / 2*/,
                10/*GraphicsDevice.Viewport.Height / 2*/);
            FontPos2 = new Vector2(GraphicsDevice.Viewport.Width / 2,
                GraphicsDevice.Viewport.Height / 2);
            base.LoadContent();
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

        public override void Draw(GameTime gameTime)
        {
            Viewport vp = theGame.GraphicsDevice.Viewport;

            Rectangle r = new Rectangle(vp.X, vp.Y, vp.Width, vp.Height);
            Vector2 pos = new Vector2(r.Left, r.Top);

            spriteBatch.Begin(); 
            spriteBatch.Draw(backgrnd, pos, Color.White);
            string output = Convert.ToString(theGame.GraphicsDevice.Viewport.Height + " " + theGame.GraphicsDevice.Viewport.Width);
            string output2 = Convert.ToString(theGame.GraphicsDevice.Viewport.Height + " " + theGame.GraphicsDevice.Viewport.Width +"ok");

            Vector2 FontOrigin = Font1.MeasureString(output) / 2;
            Vector2 FontOrigin2 = Font1.MeasureString(output2) / 2;
            spriteBatch.DrawString(Font1, output, FontPos, Color.White,
                           0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            spriteBatch.DrawString(Font1, output2, FontPos2, Color.White,
                          0, FontOrigin2, 1.0f, SpriteEffects.None, 0.5f);
           
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
