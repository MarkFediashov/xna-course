using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Audio;

using spacebot.perk;

namespace Game1
{
    delegate void Shot(Game1 g, Vector2 start, Vector2 end);
    public abstract class Weapron
    {
        protected int reloadDelay;
        protected int ammunition;
        private bool reloadFlag = false;
        SoundEffect sound;
        IPerk<Weapron> perk;

        protected Weapron(SoundEffect sound, int ammunition = 10)
        {
            this.sound = sound;
            this.ammunition = ammunition;
        }

        public void InjectPerk(IPerk<Weapron> perk)
        {
            this.perk = perk;
            perk.InjectWeapron(this);
        }

        public void alterShot(Game1 g, Vector2 start, Vector2 end)
        {
            if (perk != null)
            {
                shotActionStrategy(g, start, end, perk.AlterShootImpl);
            }
        }

        public void shot(Game1 g, Vector2 start, Vector2 end)
        {
            shotActionStrategy(g, start, end, shotImpl);
        }

        public void PlaySound()
        {
            sound.Play();
        }

        private void shotActionStrategy(Game1 g, Vector2 start, Vector2 end, Shot shotActionImpl)
        {
            if (!reloadFlag && ammunition > 0)
            {
                reloadFlag = true;
                shotActionImpl(g, start, end);
                PlaySound();
                startReload();

                ammunition--;
            }
        }


        abstract public void shotImpl(Game1 g, Vector2 start, Vector2 end);

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
