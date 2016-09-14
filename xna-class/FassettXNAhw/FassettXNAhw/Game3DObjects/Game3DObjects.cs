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

namespace FassettXNAhw
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Game3DObject
    {

        private Game1 theGame;

        private ContentManager Content;


        protected Model spaceship;

        protected Vector3 Position;
        protected float Zoom = 3600;
        protected float RotationY = 52.80f;
        protected float RotationX = 45.00f;
        protected Matrix gameWorldRotation;
        public float Zpos;
        protected Random rnd = new Random();

        protected Matrix world = Matrix.Identity;

        public Game3DObject(Game game)
        {
            this.Content = game.Content;

            theGame = (Game1)game;
        }

        public virtual void Update(GameTime gameTime)
        {
        //    double time = gameTime.TotalGameTime.TotalSeconds;
        //    float speedAlongEllipse = MathHelper.PiOver2;
        //    float ellipseCenterX = -250f; // a
        //    float ellipseCenterY = 30f; // b
        //    float ellipseMajorAxis = 500f; // m
        //    float ellipseMinorAxis = 300f; // n
        //    Position.X = ellipseCenterX
        //    + (float)(ellipseMajorAxis
        //    * Math.Cos(speedAlongEllipse * time));
        //    Position.Z = ellipseCenterY
        //    + (float)(ellipseMinorAxis
        //    * Math.Sin(speedAlongEllipse * time));
        //    Position.Y = 50f +
        //    (float)Math.Sqrt((Position.X * Position.X) + (Position.Z * Position.Z)) *
        //    (float)Math.Tan(10.0);
        //    time += (MathHelper.Pi) / 90;
            //Position.Z += 12;
            RotationX = 12; 
            if (InputManager.KeyPressed(Keys.Right))
            
                RotationY += 25;
            
            if (InputManager.KeyPressed(Keys.Left))
                RotationY -= 25;
            gameWorldRotation =
                Matrix.CreateRotationX(MathHelper.ToRadians(RotationX)) *
                Matrix.CreateRotationY(MathHelper.ToRadians(RotationY));
        }

        public virtual void Draw()
        {
            Matrix[] transforms = new Matrix[spaceship.Bones.Count];

            float aspectRatio = theGame.GraphicsDevice.Viewport.Width /
                                theGame.GraphicsDevice.Viewport.Height;

            spaceship.CopyAbsoluteBoneTransformsTo(transforms);

            Matrix projection =
                Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                                                    aspectRatio, 1.0f, 10000.0f);
            Matrix view = Matrix.CreateLookAt(new Vector3(0.0f, 50.0f, Zoom),
                                               Vector3.Zero, Vector3.Up);
            Matrix SpaceShip = Matrix.CreateScale(25f);
            foreach (ModelMesh mesh in spaceship.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();

                    effect.View = view;

                    effect.Projection = projection;


                    effect.World = SpaceShip * gameWorldRotation *
                                     transforms[mesh.ParentBone.Index] *
                                     Matrix.CreateTranslation(Position);
                }
                mesh.Draw();
            } 
        }
        public virtual void LoadContent()
        {
            var yPositionOfShip = 0;//theGame.GraphicsDevice.Viewport.Height / 2;
            var xPositionOfShip = -900;//(theGame.GraphicsDevice.Viewport.Width / 2);
            Position.X = xPositionOfShip;
            Position.Y = yPositionOfShip;
            this.spaceship = Content.Load<Model>("launcher_base");
        }
    }
}
