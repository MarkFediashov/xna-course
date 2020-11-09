using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Game1
{
    public abstract class Weapron
    {
        protected int reloadDelay;
        private bool reloadFlag = false;
        SoundEffect sound;

        protected Weapron(SoundEffect sound)
        {
            this.sound = sound;
        }

        public void shot(Game1 g, Vector2 start, Vector2 end)
        {
            if (!reloadFlag)
            {
                reloadFlag = true;
                shotImpl(g, start, end);
                sound.Play();
                startReload();
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
    }
}
