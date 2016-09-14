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
using FassettXNAhw.Utilities;
using FassettXNAhw.Managers;


namespace FassettXNAhw.Screens
{
    /// <summary>This is a game component that implements IUpdateable.</summary>
    public class GameScreen : Microsoft.Xna.Framework.DrawableGameComponent
    {

        #region Fields and Properties Region
        List<GameComponent> childComponents;
        protected ContentManager Content;


        public List<GameComponent> Components
        {
            get
            {  
                return childComponents; 
            }
             
        }

        #endregion


        #region  Constructors Region

        public GameScreen(Game game)
            : base(game)
        {
            childComponents = new List<GameComponent>();
        }

        #endregion


        #region   XNA API Methods Region
        /// <summary>Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.</summary>
        public override void Initialize()
        {
            base.Initialize();
        }



        /// <summary>Allows the game component to update itself.</summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent component in childComponents)
            {
                if (component.Enabled)
                    component.Update(gameTime);
            }
            base.Update(gameTime);
        }



        /// <summary>Allows the game component to draw itself.</summary>

        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent drawComponent;
            foreach (GameComponent component in childComponents)
            {
                if (component is DrawableGameComponent)
                {
                    drawComponent = component as DrawableGameComponent;
                    drawComponent = (DrawableGameComponent)component;
                    if (drawComponent.Visible)
                        drawComponent.Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }

        #endregion


        #region Support Methods Region

        internal protected virtual void ScreenChange(object sender, ScreenEventArgs e)
        {
            if (e.GameScreen == this)
                Show();
            else
                Hide();
        }


        private void Show()
        {
            Visible = true;
            Enabled = true;
            foreach (GameComponent component in childComponents)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = true;
            }
        }


        private void Hide()
        {
            Visible = false;
            Enabled = false;
            foreach (GameComponent component in childComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent)
                    ((DrawableGameComponent)component).Visible = false;
            }
        }

        #endregion

    }
}
