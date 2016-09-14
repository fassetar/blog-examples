using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FassettXNAhw.DrawableComponents
{
    public class Asteroid : Microsoft.Xna.Framework.DrawableGameComponent
    {
        #region  Fields and Properties


        private Game1 theGame;

        private ContentManager Content;
        private static bool hitonce = false;
        private float aspectRatio;
        private Vector3 Position = Vector3.Zero;
        //private float Zoom = 3600;
        private float RotationY = 52.80f;
        private float RotationX = 45.00f;
        private Matrix gameWorldRotation;
        Vector3 cameraPosition = new Vector3(0.0f, 50.0f, 5000.0f);
        Vector3 modelPosition = Vector3.Zero;
        float modelRotation = 0.0f;
        Model asteroid;
        private static Random rnd = new Random();
        private int ran = rnd.Next(1);
        #endregion

        public Asteroid(Game game)
            : base(game)
        {
            this.Content = game.Content;
            theGame = (Game1)game;
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            asteroid = Content.Load<Model>("Asteroid");
            aspectRatio = theGame.GraphicsDevice.Viewport.AspectRatio;
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {


            int bound = theGame.GraphicsDevice.Viewport.Width;
            if (modelPosition.X < bound && hitonce != true)
            {
                modelPosition.X += 15;
                if (Position.X >= bound)
                    hitonce = true;
            }
            else
            {
                if (Position.X > 0)
                    Position.X -= 15;
                if (Position.X <= 0)
                    hitonce = false;
            }

            gameWorldRotation =
                Matrix.CreateRotationX(MathHelper.ToRadians(RotationX)) *
                Matrix.CreateRotationY(MathHelper.ToRadians(RotationY));
             modelRotation += (float)gameTime.ElapsedGameTime.TotalMilliseconds *
        MathHelper.ToRadians(0.1f);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Matrix[] transforms = new Matrix[asteroid.Bones.Count];
            asteroid.CopyAbsoluteBoneTransformsTo(transforms);

            // Draw the model. A model can have multiple meshes, so loop.
            foreach (ModelMesh mesh in asteroid.Meshes)
            {
                // This is where the mesh orientation is set, as well 
                // as our camera and projection.
                Matrix AsteroidScale = Matrix.CreateScale(9.0f);
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = AsteroidScale* transforms[mesh.ParentBone.Index] *
                        Matrix.CreateRotationY(modelRotation)
                        * Matrix.CreateTranslation(modelPosition);
                    effect.View = Matrix.CreateLookAt(cameraPosition,
                        Vector3.Zero, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(
                        MathHelper.ToRadians(45.0f), aspectRatio,
                        1.0f, 10000.0f);
                }
                // Draw the mesh, using the effects set above.
                mesh.Draw();
            }
            base.Draw(gameTime);
        }
    }
}
