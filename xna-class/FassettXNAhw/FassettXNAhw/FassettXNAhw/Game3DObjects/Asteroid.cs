using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FassettXNAhw.Game3DObjects
{
    public class Asteroid : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region  Fields and Properties


        private Game1 theGame;

        private ContentManager Content;
        private static bool hitonce = false;
        private Model asteroid;
        private float aspectRatio;
        private Vector3 Position = Vector3.Right;
        //private float Zoom = 3600;
        private float RotationY = 52.80f;
        private float RotationX = 45.00f;
        private Matrix gameWorldRotation;


        private Matrix world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        private Matrix view = Matrix.CreateLookAt(new Vector3(0, 0.001f, 4), new Vector3(0, 0, 0), Vector3.UnitZ);
        private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.1f, 100f);


        private static Random rnd = new Random();
        private int ran = rnd.Next(1);
        #endregion

        #region Constructors Region
        public Asteroid(Game game)
            : base(game)
        {
            this.Content = game.Content;
            theGame = (Game1)game;
        }
        #endregion

        #region XNA API Methods Region

         protected override void LoadContent()
         {
             asteroid = Content.Load<Model>("Asteroid");
             aspectRatio = theGame.GraphicsDevice.Viewport.AspectRatio;
             base.LoadContent();
         }

         public override void Update(GameTime gameTime)
         {

            
             int bound = theGame.GraphicsDevice.Viewport.Width;
                 if (Position.X < bound && hitonce != true)
                 {
                     Position.X += 15;
                     if (Position.X >= bound)
                         hitonce = true;
                 }
                 else
                 {
                     if(Position.X > 0)
                     Position.X -= 15;
                     if (Position.X <= 0)
                         hitonce = false;
                 }
             
             gameWorldRotation =
                 Matrix.CreateRotationX(MathHelper.ToRadians(RotationX)) *
                 Matrix.CreateRotationY(MathHelper.ToRadians(RotationY));

             base.Update(gameTime);
         }

         public override void Draw(GameTime gameTime)
         {
            DrawModel(asteroid, world, view, projection);
            base.Draw(gameTime);
         }
        #endregion

        private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();

                    effect.World = world * gameWorldRotation * Matrix.CreateTranslation(Position);
                    effect.View = view;
                    effect.Projection = projection;
                }
 
                mesh.Draw();
            }
        }
    }
}
