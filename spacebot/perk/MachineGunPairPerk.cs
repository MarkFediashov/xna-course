using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Game1;
using Microsoft.Xna.Framework;
using spacebot;
namespace spacebot.perk
{
    public class MachineGunPairPerk : WeapronPerk<MachineGun>
    {
        static Vector2 offset = new Vector2(20, 20);

        public MachineGunPairPerk() { }

        public override void AlterShootImpl(Game1.Game1 game, Vector2 start, Vector2 end)
        {
            game.bulletFactory.CreateBullet(start, end);
            game.bulletFactory.CreateBullet(start, end + offset);
        }

        public override string GetPerkDescription()
        {
            return "Good for destroy whole line of enemy:\nit give you chance for multiple kill at one shot.";
        }
    }
}
