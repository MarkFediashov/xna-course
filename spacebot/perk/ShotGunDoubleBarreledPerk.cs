using System.Threading.Tasks;
using System.Threading;

using Game1;
using Microsoft.Xna.Framework;

namespace spacebot.perk
{
    public class ShotGunDoubleBarreledPerk : WeapronPerk<ShotGun>
    {
        public override void AlterShootImpl(Game1.Game1 game, Vector2 start, Vector2 end)
        {
            Weapron.shotImpl(game, start, end);
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(110);
                Weapron.shotImpl(game, start, end);
                Weapron.PlaySound();
            });
        }

        public override string GetPerkDescription()
        {
            return "The most powerful development of galaxy army:\nin open place you can kill half\nof enemies on level.";
        }

    }
}
