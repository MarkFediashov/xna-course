using Microsoft.Xna.Framework;

using Game1;

namespace spacebot.perk
{
    public abstract class WeapronPerk<T>: IPerk<T> where T: Weapron
    {
        public T Weapron { get; set; }
        public abstract void AlterShootImpl(Game1.Game1 game, Vector2 start, Vector2 end);
        public abstract string GetPerkDescription();
        public void InjectWeapron(Weapron weapron)
        {
            Weapron = (T)weapron;
        }
    }
}
