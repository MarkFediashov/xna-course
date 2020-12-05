using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Audio;

namespace Game1
{
    public abstract class Weapron
    {
        protected int reloadDelay;
        protected int ammunition;
        private bool reloadFlag = false;
        SoundEffect sound;

        protected Weapron(SoundEffect sound, int ammunition = 10)
        {
            this.sound = sound;
            this.ammunition = ammunition;
        }

        public void shot(Game1 g, Vector2 start, Vector2 end)
        {
            if (!reloadFlag && ammunition > 0)
            {
                reloadFlag = true;
                shotImpl(g, start, end);
                sound.Play();
                startReload();

                ammunition--;
            }
        }

        abstract protected void shotImpl(Game1 g, Vector2 start, Vector2 end);

        private void startReload()
        {
            Action onReload = () =>
            {
                Thread.Sleep(reloadDelay);
                reloadFlag = false;
            };

            Task.Factory.StartNew(onReload);
        }

        public int GetAvailableAmmunition()
        {
            return ammunition;
        }
    }
}
