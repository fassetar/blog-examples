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
using FassettXNAhw.Utilities;

namespace FassettXNAhw.DrawableComponents
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Menu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region Fields and Properties Region
        string[] menuItems;
        static int selectedIndex = 1;
        float width;
        float height;
        Vector2 position;
        SpriteFont spriteFont;
        Game1 theGame;
        public Color NormalColor
        {
            get;
            set;
        }

        public Color HiliteColor
        {
            get;
            set;
        }

        public float Width
        {
            get { return width; }
        }

        public float Height
        {
            get { return height; }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
        }
        #endregion



        #region Constructors Region

        public Menu(Game game, SpriteFont spriteFont, string[] items)
            : base(game)
        {
            
            theGame = (Game1)game;
            this.spriteFont = spriteFont;
            SetMenuItems(items);
            NormalColor = Color.YellowGreen;

            HiliteColor = Color.Tomato;

            // TODO: Construct any child components here
        }

        #endregion




        #region Support methods Region

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }


        public void SetMenuItems(string[] items)
        {
            menuItems = (string[])items.Clone();
            MeasureMenu();
        }


        private void MeasureMenu()
        {
            width = 0;
            height = 0;
            foreach (string s in menuItems)
            {
                if (width < spriteFont.MeasureString(s).X)
                    width = spriteFont.MeasureString(s).X;
                height += spriteFont.LineSpacing;
            }
        }

        #endregion


        #region   XNA API Region

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
            // Create a new SpriteBatch, which can be used to draw textures.
            // spriteBatch = new SpriteBatch(GraphicsDevice);

            base.LoadContent();
        }


        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            if (InputManager.KeyPressed(Keys.Up))
            {
                if (selectedIndex > 0)
                    selectedIndex -= 1;
                else
                    selectedIndex = 3;
            }
            if (InputManager.KeyPressed(Keys.Down))
            {
                if (selectedIndex < 3)
                    selectedIndex += 1;
                else
                    selectedIndex = 0;
            }
            if(InputManager.KeyPressed(Keys.Enter))
            {
                if (selectedIndex == 3)
                {
                    //Implemented this in the Menu rather than start screen...
                    this.Game.Exit();
                }
            }
            base.Update(gameTime);
        }



        /// <summary>
        /// Allows the game component to draw itself.
        /// </summary>

        public override void Draw(GameTime gameTime)
        {
            
            Vector2 menuPosition = position;

            

            for (int i = 0; i < menuItems.Length; i++)
            {
                theGame.GameSpriteBatch.Begin();

                if (i == selectedIndex)
                    theGame.GameSpriteBatch.DrawString(spriteFont, menuItems[i], menuPosition,NormalColor );
                else
                    theGame.GameSpriteBatch.DrawString(spriteFont, menuItems[i], menuPosition, HiliteColor);

                theGame.GameSpriteBatch.End();

                menuPosition.Y += spriteFont.LineSpacing;
            }

           

            base.Draw(gameTime);
        }

        #endregion

    }
} 
