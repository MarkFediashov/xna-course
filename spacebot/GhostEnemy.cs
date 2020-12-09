using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using Game1;

namespace spacebot
{
    class GhostEnemy: Enemy
    {
        double dx, dy;
        static Random random = new Random();
        double angle;

        public GhostEnemy(Game game, Texture2D texture, Vector2 position):base(game, texture, position)
        {
            velocity = 18;
            DefineDirection();
        }

        protected override void HandleInput(GameTime gameTime)
        {
            bool defined = false;
            while (!IsInsideScreenBound())
            {
                DefineDirection();
                position.X += (float)dx;
                position.Y += (float)dy;
                defined = true;
            }
            if (!defined)
            {
                position.X += (float)dx;
                position.Y += (float)dy;
            }

        }

        private void ChangeDirection()
        {
            angle = -angle;
            dx = velocity * Math.Sin(angle);
            dy = velocity * Math.Cos(angle);
        }

        private void DefineDirection()
        {
            angle = random.NextDouble() * Math.PI * 2;
            dx = velocity * Math.Sin(angle);
            dy = velocity * Math.Cos(angle);
        }
    }
}
