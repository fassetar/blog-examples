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

namespace FassettXNAhw
{
    /// <summary>This is a game component that implements IUpdateable. </summary>
    public class Game3DObject
    {

        private Game1 theGame;
        private ContentManager Content;


        protected Model model;
        private float aspectRatio;
        protected Vector3 Position;
        protected float Zoom = 3600;
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
        { }

        public virtual void Draw()
        { }
        public virtual void LoadContent()
        {
            aspectRatio = theGame.GraphicsDevice.Viewport.AspectRatio;
        }
    }
}
