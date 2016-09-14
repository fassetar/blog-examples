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
namespace FassettXNAhw.DrawableComponents
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class SpaceShip : Game3DObject
    {

        #region  Fields and Properties

        private Game1 theGame;
        private bool hitonce = false;
        private ContentManager Content;
        public int shiptype = 0;
        static float angle = .015f;
        float dirX = (float)Math.Sin(angle);
        float dirZ = (float)Math.Cos(angle);
        #endregion
        public SpaceShip(Game game)
            : base(game)
        {
            this.Content = game.Content;
            theGame = (Game1)game;
        }

        public class type1:SpaceShip
        {
            public type1(Game game):base(game)
            {
                this.Content = game.Content;
                theGame = (Game1)game;
            }
            public override void Update(GameTime gameTime)
            {

                int bound = theGame.GraphicsDevice.Viewport.Width;
                if (Position.X < bound)
                    Position.X += 5;
                //if (Position.X <= bound) //&& hitonce != true)
                {
                  //  Position.X += 15;
                //    if (Position.X > bound)
                //        hitonce = true;
                //}
                //else
                //{
                //    if (Position.X > 0)
                //        Position.X -= 15;
                //    if (Position.X <= 0)
                //        hitonce = false;
                }
                gameWorldRotation =
                    Matrix.CreateRotationX(MathHelper.ToRadians(RotationX)) *
                    Matrix.CreateRotationY(MathHelper.ToRadians(RotationY));
            }         
        }
        public class type2 : SpaceShip
        {
             public type2(Game game):base(game)
            {
                this.Content = game.Content;
                theGame = (Game1)game;
            }
             public override void Update(GameTime gameTime)
             {

                 int bound = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width - 265;
                 if (Position.X < bound && hitonce != true)
                 {
                     Position.X += 5;
                     if (Position.X >= bound)
                         hitonce = true;
                 }
                 else
                 {
                     if (Position.X > 0)
                         Position.X -= 5;
                     if (Position.X <= 0)
                         hitonce = false;
                 }
                 gameWorldRotation =
                   Matrix.CreateRotationX(MathHelper.ToRadians(RotationX)) *
                   Matrix.CreateRotationY(MathHelper.ToRadians(RotationY));
             }
        }
    }
}
