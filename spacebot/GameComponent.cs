
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Game1
{
    public class MovableComponent : DrawableGameComponent
    {
        public Texture2D texture { get;  set; }
        protected Vector2 position;
        protected double leftScreenBound;
        protected double rigthScreenBound;
        protected double bottomScreenBound;
        private float addedVelocity;
        Weapron currentWeapron;
        List<Weapron> weapron;
        float reload = 0f;

        private readonly float baseVelocity = 23;

        public MovableComponent(Game game, Texture2D texture, Vector2 position) : base(game)
        {
            this.texture = texture;
            this.position = position;
            game.Components.Add(this);
            leftScreenBound = 0;
            rigthScreenBound = game.GraphicsDevice.Viewport.Width - 57;
            bottomScreenBound = game.GraphicsDevice.Viewport.Height - 57;
            addedVelocity = 0;
            weapron = new List<Weapron>() { new MachineGun((game as Game1).machinegunSound), new ShotGun((game as Game1).shotgunSound)};
            currentWeapron = weapron[0];
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected virtual void HandleInput(GameTime gameTime)
        {
            if(reload > 0)
            {
                reload -= gameTime.ElapsedGameTime.Milliseconds;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
                addedVelocity = 27;
            else
                addedVelocity = 0;

            float speed = baseVelocity + addedVelocity;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                if (position.X >= leftScreenBound)
                    position.X -= speed * gameTime.ElapsedGameTime.Milliseconds / 100;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                if (position.X <= rigthScreenBound)
                    position.X += speed * gameTime.ElapsedGameTime.Milliseconds / 100;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (position.Y < bottomScreenBound)
                    position.Y += speed * gameTime.ElapsedGameTime.Milliseconds / 100;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (position.Y >= 57)
                    position.Y -= speed * gameTime.ElapsedGameTime.Milliseconds / 100;
            }
            if(Mouse.GetState().LeftButton == ButtonState.Pressed && reload <= 0f)
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

        public Vector2 getPos()
        {
            return this.position;
        }

    }
}