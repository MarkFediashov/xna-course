using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1;
using spacebot.map_service;

using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using spacebot;

namespace Game1
{
    public class Enemy : AbstractMovableComponent, IColliding
    {
        private float velocity = 30;
        float direction = +1;

        public delegate void OnEnemyDispose();
        static public event OnEnemyDispose OnDisposeEvent;

        public Enemy(Game game, Texture2D texture, Vector2 position) : base(game, texture, position)
        {

        }

        protected override void HandleInput(GameTime gameTime)
        {
            if (position.X >= base.rigthScreenBound || position.X <= 0)
            {
                direction *= -1;
                velocity += 1;
            }
            position.X += direction * velocity * gameTime.ElapsedGameTime.Milliseconds / 100;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override bool ShouldDisposeOnCollideWithBullet()
        {
            return true;
        } 

        public override bool ShouldDisposeBullet()
        {
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            var temp = new Vector2(position.X - 50, position.Y - 70);
            AnimationFactory.SpawnAnimation(temp);
            OnDisposeEvent.Invoke();
            base.Dispose(disposing);
        }

    }
}
