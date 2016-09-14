using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace FassettXNAhw.Managers
{
    public class SoundManager
    {
        private SoundEffect backgroundMusic;

        public SoundManager(ContentManager content)
        {
            backgroundMusic = content.Load<SoundEffect>("Theme");
        }

        public void PlayBackgroundMusic()
        {
            backgroundMusic.Play();
        }
    }
}