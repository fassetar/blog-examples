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
using FassettXNAhw.Game3DObjects;
using FassettXNAhw.Mangers;


namespace FassettXNAhw.Game3DObjects
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class MissileObject : Game3DObject
    {
        protected float RotationY = 52.80f;
        protected float RotationX = 45.00f;
        private float aspectRatio;
        public bool Fired = false;
        private ContentManager Content;
        private Game1 theGame;
        public int flag;
        float MissileObjectScale;
        public MissileObject(Game game)
            : base(game)
        {
            this.Content = game.Content;
            theGame = (Game1)game;
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {

            if (this.flag == 2 || (flag == 3 && Fired == false))
            {
                RotationX = 12;
                if (InputManager.KeyPressed(Keys.Right))
                {

                    RotationY += 25;
                }
                if (InputManager.KeyPressed(Keys.Left))
                {
                    RotationY -= 25;
                }
            }
                    if (flag == 3 && Fired)
                {
                    Position.X += 12;
                }
             
            gameWorldRotation =
                Matrix.CreateRotationX(MathHelper.ToRadians(RotationX)) *
                Matrix.CreateRotationY(MathHelper.ToRadians(RotationY));
        }
        public override void  LoadContent()
        {
            var yPositionOfShip = -280;
            var xPositionOfShip = -100;
            switch (flag)
            {
                  case 1:
                    {
                        this.model = Content.Load<Model>("launcher_base");
                        Position.X = xPositionOfShip;
                        Position.Y = yPositionOfShip;
                        RotationX = 0f;
                        RotationY = -52.80f;
                        MissileObjectScale = 4.0f;
                        break;
                    }
                  case 2:
                    {
                        this.model = Content.Load<Model>("launcher_head");
                        RotationX = 154f;
                        MissileObjectScale = 4.0f;
                        break;
                    }
                  case 3:
                    {
                        this.model = Content.Load<Model>("missile");
                        RotationX = 154f;
                        MissileObjectScale = 84.0f;
                        break;
                    }
            }
            aspectRatio = theGame.GraphicsDevice.Viewport.AspectRatio;
        }
        public override void Draw()
        {
            Matrix projection =
               Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                                                   aspectRatio, 1.0f, 10000.0f);
            Matrix view = Matrix.CreateLookAt(new Vector3(0.0f, 50.0f, Zoom),
                                              Vector3.Zero, Vector3.Up);
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    effect.World = gameWorldRotation *
                      Matrix.CreateScale(MissileObjectScale) *
                      Matrix.CreateTranslation(Position);

                    effect.Projection = projection;
                    effect.View = view;
                }
                mesh.Draw();
            }
        }
    }
}
