using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Game1
{
    class MachineGun : Weapron
    {
        public MachineGun(SoundEffect sound, int ammunition) : base(sound, ammunition)
        {
            reloadDelay = 250;
        }
        protected override void shotImpl(Game1 g, Vector2 start, Vector2 end)
        {
           g.bulletFactory.CreateBullet(start, end);
        }
    }
}
