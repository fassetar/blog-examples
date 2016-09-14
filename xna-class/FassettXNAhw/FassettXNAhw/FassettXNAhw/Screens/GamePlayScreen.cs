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
using FassettXNAhw.Managers;
using FassettXNAhw.Game3DObjects;
using FassettXNAhw.Mangers;
using FassettXNAhw.Screens;
using FassettXNAhw.Game2DObjects;


namespace FassettXNAhw.Screens
{
    /// <summary>This is a game component that implements IUpdateable.</summary>
    public class GamePlayScreen : GameScreen
    {

        #region Field and Properties Region
        private int i = 0;
        protected Vector3 Position;
        ExplosionSoundManager esm;
        ScreenManager manager;
        //SpaceShip spaceShip;
        Game1 theGame;
        MissileObject launcherBase;
        MissileObject launcherHead;
        MissileObject[] missiles;
        #endregion



        #region Constructors Region

        public GamePlayScreen(Game game, ScreenManager manager, SpaceShip spaceShip)
            : base(game)
        {
            Content = Game.Content;
            this.manager = manager;
            theGame = (Game1)game;
            //this.spaceShip = spaceShip;
            esm = new ExplosionSoundManager(theGame);
            theGame.Components.Add(esm);
            Background bkg = new Background(theGame);
            bkg.Visible = true;
            theGame.Components.Add(bkg);
            Asteroid aster = new Asteroid(theGame);
            aster.Visible = true;
            theGame.Components.Add(aster);
            this.launcherBase = new MissileObject(theGame);
            this.launcherHead = new MissileObject(theGame);

            this.missiles = new MissileObject[15];
            for (int i = 0; i < 15; i++)
            {
                this.missiles[i] = new MissileObject(theGame);
            }

          }

        #endregion


        #region  XNA API methods region

        protected override void LoadContent()
        {  

            launcherBase.flag = 1;
            launcherBase.LoadContent();
            launcherHead.flag = 2;
            launcherHead.LoadContent();
            missiles.Select(x => x.flag = 3);
            foreach (MissileObject mls in this.missiles)
            {
                mls.flag = 3;
                mls.LoadContent();
            }
            base.LoadContent();
        }




        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            foreach (MissileObject ms in missiles)
            {
                if (ms.Fired)
                ms.Update(gameTime);
            }
            if (InputManager.KeyPressed(Keys.Space) || gamePadState.Buttons.LeftShoulder == ButtonState.Pressed)
            {
                i++;
                if (i < 15)
                {
                    missiles[i].Fired = true;
                    esm.PlayExplosion();
                }
   
            } 
         
            //spaceShip.Update(gameTime);
            launcherBase.Update(gameTime);
            launcherHead.Update(gameTime);
            base.Update(gameTime);
        }


        /// <summary>Allows the game component to draw itself.</summary>
        public override void Draw(GameTime gameTime)
        {
            launcherBase.Draw();
            launcherHead.Draw();
            foreach(MissileObject ms in missiles)
            {
                ms.Draw();
            }
            
            base.Draw(gameTime);
        }
        #endregion
    }
}