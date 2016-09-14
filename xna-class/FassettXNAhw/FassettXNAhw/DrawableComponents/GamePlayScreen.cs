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
using FassettXNAhw.Managers;


namespace FassettXNAhw.DrawableComponents
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GamePlayScreen : GameScreen
    {

        #region Field and Properties Region

        protected Vector3 Position;
        ExplosionSoundManager esm;
        ScreenManager manager;
        SpaceShip spaceShip;
        Game1 theGame;
        SpaceShip.type1 spaceEnemy;
        SpaceShip.type2 spacefriend;
        #endregion



        #region Constructors Region

        public GamePlayScreen(Game game, ScreenManager manager, SpaceShip spaceShip)
            : base(game)
        {
            Content = Game.Content;
            this.manager = manager;
            theGame = (Game1)game;
            this.spaceShip = spaceShip;
            esm = new ExplosionSoundManager(theGame);
            theGame.Components.Add(esm);
            Background bkg = new Background(theGame);
            bkg.Visible = true;
            theGame.Components.Add(bkg);
            Asteroid aster = new Asteroid(theGame);
            aster.Visible = true;
            theGame.Components.Add(aster);
            this.spaceEnemy = new SpaceShip.type1(theGame);
            this.spacefriend = new SpaceShip.type2(theGame);

        }

        #endregion


        #region  XNA API methods region

        protected override void LoadContent()
        {  
            spaceEnemy.LoadContent();
            spacefriend.LoadContent();
            base.LoadContent();
        }




        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {

            
            if (InputManager.KeyPressed(Keys.K))
            {
                esm.PlayExplosion();
            }
            spaceShip.Update(gameTime);
            spaceEnemy.Update(gameTime);
            spacefriend.Update(gameTime);
            base.Update(gameTime);
        }


        /// <summary>
        /// Allows the game component to draw itself.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            //spaceEnemy.Draw();
            spaceShip.Draw();
            //spacefriend.Draw();
            base.Draw(gameTime);
        }

        #endregion
    }
}