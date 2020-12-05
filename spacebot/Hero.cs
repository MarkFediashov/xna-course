
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game1
{
    public class Hero : AbstractMovableComponent
    {
        Weapron currentWeapron;
        private List<Weapron> weapron;
        public List<Weapron> Weapron
        {
            private get
            {
                return weapron;
            }
            set
            {
                weapron = value;
                currentWeapron = weapron[0];
            }
        }
        float reload = 0f;

        private readonly float baseVelocity = 23;

        public Hero(Game game, Texture2D texture, Vector2 position) : base(game, texture, position)
        {
  
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        private bool IsIntersectWithGameObject()
        {
            List<IColliding> gObj = (Game as Game1).collidableItems;
            return gObj.Exists((obj) => { return GetBounds().Intersects(obj.GetBounds()); });
        }

        protected override void HandleInput(GameTime gameTime)
        {
            Vector2 previousPosition = position;
            
            if (reload > 0)
            {
                reload -= gameTime.ElapsedGameTime.Milliseconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                addedVelocity = 27;
            else
                addedVelocity = 0;

            float speed = baseVelocity + addedVelocity;
            float dv = speed * gameTime.ElapsedGameTime.Milliseconds / 100; ;
            if (!IsIntersectWithGameObject())
            {
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    if (position.X >= leftScreenBound)
                        position.X -= dv;
                    if (IsIntersectWithGameObject())
                    {
                        position = previousPosition;
                    }
                    previousPosition = position;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    if (position.X <= rigthScreenBound)
                        position.X += dv;
                    if (IsIntersectWithGameObject())
                    {
                        position = previousPosition;
                    }
                    previousPosition = position;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    if (position.Y < bottomScreenBound)
                        position.Y += dv;
                    if (IsIntersectWithGameObject())
                    {
                        position = previousPosition;
                    }
                    previousPosition = position;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    if (position.Y >= 57)
                        position.Y -= dv;
                    if (IsIntersectWithGameObject())
                    {
                        position = previousPosition;
                    }
                    previousPosition = position;
                }
            }
            
            if (Mouse.GetState().LeftButton == ButtonState.Pressed && reload <= 0f)
            {
                var pos = Mouse.GetState().Position;
                currentWeapron.shot(Game as Game1, position, pos.ToVector2());
                reload = 250;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                currentWeapron = weapron[1];
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                currentWeapron = weapron[0];
            }

        }

        public override bool ShouldDisposeOnCollideWithBullet()
        {
            return false;
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch batch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            batch.Draw(texture, position, color: Color.White);
            base.Draw(gameTime);
        }

        public void SetPosition(Vector2 position)
        {
            this.position = position;
        }

    }
}