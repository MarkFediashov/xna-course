using Game1;

using Microsoft.Xna.Framework;

namespace spacebot.perk
{
    public interface IPerk<out T> where T: Weapron
    {
        void AlterShootImpl(Game1.Game1 game, Vector2 start, Vector2 end);
        void InjectWeapron(Weapron weapron);
        string GetPerkDescription();
    }
}
