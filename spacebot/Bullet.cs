using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class Bullet : AbstractMovableComponent
    {
        protected float dx;
        protected float dy;
        public Bullet(Game game, Texture2D texture, Vector2 start, Vector2 end, float velocity = 30): base(game, texture, start)
        {
            float d = Vector2.Distance(end, start);
            float sin = (end.X - start.X) / d;
            float cos = (end.Y - start.Y) / d;
            dx = sin * velocity;
            dy = cos * velocity;
        }

        protected override void HandleInput(GameTime gameTime)
        {
            position.X += dx;
            position.Y += dy;
            if (position.X > rigthScreenBound || position.X < 0 || position.Y > bottomScreenBound || position.Y < 0)
            {
                Game.Components.Remove(this);
            }

            List<IColliding> died = new List<IColliding>();
            List<IColliding> allEnemies = (Game as Game1).collidableItems;
            bool bulletHasCollide = false;
            foreach (var enemy in allEnemies)
            {
                Rectangle r1 = enemy.GetBounds();
                Rectangle r2 = GetBounds();
                if (r1.Intersects(r2))
                {
                    enemy.OnHitNotify();
                    if (enemy.ShouldDisposeOnCollideWithBullet())
                    {
                        died.Add(enemy);
                    }
                    if (enemy.ShouldDisposeBullet())
                    {
                        bulletHasCollide = true;
                        break;
                    }
                }
            }
            died.ForEach((element) =>
            {
                (Game as Game1).collidableItems.Remove(element);
                Game.Components.Remove(element as IGameComponent);
                (element as DrawableGameComponent).Dispose();
            });

            if (bulletHasCollide)
            {
                Game.Components.Remove(this);
            }
        }

        public override bool ShouldDisposeOnCollideWithBullet()
        {
            throw new NotImplementedException();
        }
    }
}
