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


namespace FassettXNAhw.Managers
{
    
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ExplosionSoundManager : Microsoft.Xna.Framework.GameComponent
    {

        #region Fields and Properties

        AudioEngine engine;
        WaveBank waveBank;
        SoundBank soundBank;


        #endregion



        public ExplosionSoundManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }



        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            engine = new AudioEngine("Content\\Example5.xgs");
            waveBank = new WaveBank(engine, "Content\\Wave Bank.xwb");
            soundBank = new SoundBank(engine, "Content\\Sound Bank.xsb");


            base.Initialize();
        }




        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public void PlayTheme()
        {
            Cue cue2 = soundBank.GetCue("theme");
            cue2.Play();
        }


        public void PlayExplosion()
        {
            Cue cue = soundBank.GetCue("explosion");
            cue.Play();
        }


    }
}
