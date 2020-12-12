using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Game1;
using Microsoft.Xna.Framework;
using spacebot;

namespace spacebot.perk
{
    public class ShotGunDoubleBarreledPerk : WeapronPerk<ShotGun>
    {
        public override void AlterShootImpl(Game1.Game1 game, Vector2 start, Vector2 end)
        {
            Weapron.shotImpl(game, start, end);
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);
                Weapron.shotImpl(game, start, end);
                Weapron.PlaySound();
            });
        }

    }
}
