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


namespace FassettXNAhw.Mangers
{
    /// <summary>This is a game component that implements IUpdateable.</summary>
    public class InputManager : Microsoft.Xna.Framework.GameComponent
    {
        #region Fields Region

        static Game1 theGame;
        static KeyboardState keyboardState;
        static KeyboardState lastKeyboardState;

        #endregion

        #region Constructor Region

        public InputManager(Game game)
            : base(game)
        {
            theGame = (Game1)game;
            // TODO: Construct any child components here
            keyboardState = Keyboard.GetState();
        }
        #endregion

        #region XNA API Methods Region
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
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            base.Update(gameTime);
        }

        #endregion

        #region Keyboard Methods Region

        public static KeyboardState KeyboardState
        {
            get { return keyboardState; }
        }


        public static KeyboardState LastKeyboardState
        {
            get { return lastKeyboardState; }
        }


        public static bool KeyReleased(Keys key)
        {
            return keyboardState.IsKeyUp(key) &&
            lastKeyboardState.IsKeyDown(key);
        }


        public static bool KeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key) &&
            lastKeyboardState.IsKeyUp(key);
        }

        #endregion
    }
}
