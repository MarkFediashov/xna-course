using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using spacebot;
using Game1;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace spacebot.perk
{
    public interface IPerk<out T> where T: Weapron
    {
       void AlterShootImpl(Game1.Game1 game, Vector2 start, Vector2 end);
       void InjectWeapron(Weapron weapron);
    }
}
