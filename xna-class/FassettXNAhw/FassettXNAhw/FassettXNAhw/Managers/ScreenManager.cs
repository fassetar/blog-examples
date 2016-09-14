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
using FassettXNAhw.Screens;


namespace FassettXNAhw.Mangers
{
    /// <summary>This is a game component that implements IUpdateable. </summary>
    public class ScreenManager : Microsoft.Xna.Framework.GameComponent
    {
        #region Fields and Properties Region

        Stack<GameScreen> gameScreens = new Stack<GameScreen>();
        public event EventHandler<ScreenEventArgs> OnScreenChange;
        const int startDrawOrder = 5000;
        const int drawOrderInc = 100;
        int drawOrder;


        public GameScreen CurrentScreen
        {
            get { return gameScreens.Peek(); }
        }

        #endregion

        #region  Constructors region

        public ScreenManager(Game game)
            : base(game)
        {
            drawOrder = startDrawOrder;
            // TODO: Construct any child components here
        }

        #endregion

        #region Support Methods Region

        public void PopScreen()
        {
            GameScreen oldScreen = RemoveScreen();
            drawOrder -= drawOrderInc;
            if (OnScreenChange != null)
                OnScreenChange(this, new ScreenEventArgs(oldScreen));
        }


        private GameScreen RemoveScreen()
        {
           
                GameScreen screen = (GameScreen)gameScreens.Peek();

                OnScreenChange -= screen.ScreenChange;
                Game.Components.Remove(screen);

                return gameScreens.Pop();
        
        }


        public void PushScreen(GameScreen newScreen)
        {
            drawOrder += drawOrderInc;
            newScreen.DrawOrder = drawOrder;
            AddScreen(newScreen);
            if (OnScreenChange != null)
                OnScreenChange(this, new ScreenEventArgs(newScreen));
        }


        private void AddScreen(GameScreen newScreen)
        {
            gameScreens.Push(newScreen);
            Game.Components.Add(newScreen);
            OnScreenChange += newScreen.ScreenChange;
        }


        public void ChangeScreens(GameScreen newScreen)
        {
            while (gameScreens.Count > 0)
                RemoveScreen();
            newScreen.DrawOrder = startDrawOrder;
            drawOrder = startDrawOrder;
            AddScreen(newScreen);
            if (OnScreenChange != null)
                OnScreenChange(this, new ScreenEventArgs(newScreen));
        }


        #endregion

        #region  XNA API methods Region

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

        #endregion
    }
}
