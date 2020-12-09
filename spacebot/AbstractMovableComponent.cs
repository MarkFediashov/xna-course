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
    public abstract class AbstractMovableComponent : DrawableGameComponent, IColliding
    {
        public Texture2D texture { get; set; }
        protected Vector2 position;
        public Vector2 Position => position;
        protected SpriteBatch batch;

        protected double leftScreenBound;
        protected double rigthScreenBound;
        protected double bottomScreenBound;
        protected float addedVelocity;

        public AbstractMovableComponent(Game game, Texture2D texture, Vector2 position) : base(game)
        {
            this.texture = texture;
            this.position = position;
            leftScreenBound = 0;
            rigthScreenBound = game.GraphicsDevice.Viewport.Width - 57;
            bottomScreenBound = game.GraphicsDevice.Viewport.Height - 57;
            addedVelocity = 0;
            batch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));

            game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        abstract protected void HandleInput(GameTime gameTime);

        public override void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            batch.Draw(texture, position, color: Color.White);
            base.Draw(gameTime);
        }

        protected Rectangle GetRectangleBounds()
        {
            Rectangle r1 = texture.Bounds;
            Point p = Position.ToPoint();
            r1.Offset(p.X, p.Y);
            return r1;
        }

        public abstract bool ShouldDisposeOnCollideWithBullet();
        public virtual void OnHitNotify()
        {

        }
        public virtual bool ShouldDisposeBullet()
        {
            return true;
        }

        public bool IsInsideScreenBound()
        {
            return !(position.X >= rigthScreenBound || position.X <= 0 || position.Y >= bottomScreenBound || position.Y < 0);
        }

        public Rectangle GetBounds()
        {
            return GetRectangleBounds();
        }
    }
}
