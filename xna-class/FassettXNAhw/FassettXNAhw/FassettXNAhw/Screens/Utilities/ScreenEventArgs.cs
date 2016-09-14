using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FassettXNAhw.Screens;

namespace FassettXNAhw.Utilities
{
    public class ScreenEventArgs : EventArgs
    {
        #region Fields and Properties  Region

        GameScreen gameScreen;

        public GameScreen GameScreen
        {
            get { return gameScreen; }
            private set { gameScreen = value; }
        }


        #endregion



        #region Constructors Region

        public ScreenEventArgs(GameScreen gameScreen)
        {
            GameScreen = gameScreen;
        }

        #endregion

    }
}
