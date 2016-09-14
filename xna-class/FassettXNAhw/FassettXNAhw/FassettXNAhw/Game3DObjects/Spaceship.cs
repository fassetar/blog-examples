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

//i know this isn't in drawable components..
namespace FassettXNAhw.Game3DObjects
{
    /// <summary>This is a game component that implements IUpdateable.</summary>
    public class SpaceShip : Microsoft.Xna.Framework.GameComponent
    {

        #region  Fields and Properties
        protected float RotationY = 52.80f;
        protected float RotationX = 45.00f;
        private Game1 theGame;
        private ContentManager Content;
        #endregion

        #region  Constructors Region
        public SpaceShip(Game game)
            : base(game)
        {
            this.Content = game.Content;
            theGame = (Game1)game;
        }
        #endregion

        #region XNA API Region

        #endregion
    }
}
