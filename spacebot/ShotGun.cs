using System;


using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Game1
{
    public class ShotGun : Weapron
    {
        Random rand;
        public ShotGun(SoundEffect sound, int ammunition) : base(sound, ammunition)
        {
            rand = new Random();
            reloadDelay = 1500;
        }

        protected override void shotImpl(Game1 g, Vector2 start, Vector2 end)
        {
            int random = rand.Next() % 7 + 5;
            for(int i = 0; i < random; i++)
            {
                int direction = rand.Next() % 2 > 0 ? 1 : -1;
                Vector2 v = new Vector2(end.X + (rand.Next() % 160 * direction), end.Y + (rand.Next() % 120 * direction));
                g.bulletFactory.CreateShrapneel(start, v);
            }
        }
    }
}
