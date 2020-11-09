using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1;

using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class Enemy : MovableComponent
    {
        private float velocity = 30;
        float direction = +1;
        public Enemy(Game game, Texture2D texture, Vector2 position) : base(game, texture, position)
        {
        }

        protected override void HandleInput(GameTime gameTime)
        {
            /*var mousePos = Mouse.GetState().Position;
            Rectangle rect = texture.Bounds;
            rect.Offset(position.X, position.Y);
            if (rect.Contains(mousePos.X, mousePos.Y) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Game.Components.Remove(this);
            }*/
            if (position.X >= base.rigthScreenBound || position.X <= 0)
            {
                direction *= -1;
                velocity += 1;
            }
            this.position.X += direction * velocity * gameTime.ElapsedGameTime.Milliseconds / 100;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
