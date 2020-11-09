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
    public class Bullet : MovableComponent
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
            List<Enemy> died = new List<Enemy>();
            List<Enemy> allEnemies = (Game as Game1).enemies;
            List<Bullet> bulletRemove = new List<Bullet>();
            foreach (var enemy in allEnemies)
            {
                Rectangle r1 = enemy.texture.Bounds;
                Point p = enemy.getPos().ToPoint();
                r1.Offset(p.X, p.Y);
                Rectangle r2 = texture.Bounds;
                r2.Offset(position);
                if (r1.Intersects(r2))
                {
                    died.Add(enemy);
                    break;
                }
            }
            (Game as Game1).enemies.RemoveAll((Enemy e)=>
             {
                 bool flag = died.Contains(e);
                 if (flag)
                    Game.Components.Remove(e);
                 return flag;
             });
        }
    }
}
