using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game1;
using spacebot;

using spacebot.perk;

namespace spacebot.perk
{
    public abstract class WeapronPerk<T> : IPerk<T> where T: Weapron
    {
        public T Weapron { get; set; }
        public abstract void AlterShootImpl(Game1.Game1 game, Vector2 start, Vector2 end);
        public void InjectWeapron(Weapron weapron)
        {
            Weapron = (T)weapron;
        }
    }
}
