﻿
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using System.Threading;
using spacebot;

namespace Game1
{
    public class Hero : AbstractMovableComponent
    {
        Weapron currentWeapron;
        public int health { get; private set;  }
        bool canBeDamaged = true;
        public double Fuel { get; private set; }
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

        private readonly float baseVelocity = 23;

        public Hero(Game game, Texture2D texture, Vector2 position) : base(game, texture, position)
        {
            health = 10;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        private void Damage()
        {
            if (canBeDamaged)
            {
                health--;
                canBeDamaged = false;
                
                Task.Factory.StartNew(() =>
                {
                    if (health > 0)
                    {
                        Thread.Sleep(500);
                        canBeDamaged = true;
                    }
                    else
                    {
                        AnimationFactory.SpawnAnimation(position);
                        Game.Components.Remove(this);
                        Thread.Sleep(1500);
                        Game.Exit();
                    }
                });
            }
        }

        private bool IsIntersectWithGameObject()
        {
            List<IColliding> gObj = (Game as Game1).collidableItems;
            return gObj.Exists((obj) => { return GetBounds().Intersects(obj.GetBounds()); });
        }

        private bool IsIntersectsWithEnemy()
        {
            List<IColliding> gObj = (Game as Game1).enemies;
            return gObj.Exists((obj) => { return GetBounds().Intersects(obj.GetBounds()); });
        }

        protected override void HandleInput(GameTime gameTime)
        {
            Vector2 previousPosition = position;
            
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && Fuel > 0)
            {
                addedVelocity = 27;
                Fuel -= gameTime.ElapsedGameTime.Milliseconds / 15;
            }
            else
                addedVelocity = 0;

            float speed = baseVelocity + addedVelocity;
            float dv = speed * gameTime.ElapsedGameTime.Milliseconds / 100;
            if (IsIntersectsWithEnemy())
            {
                Damage();
            }
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
                        if (IsIntersectsWithEnemy())
                        {
                            Damage();
                        }
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
                        if (IsIntersectsWithEnemy())
                        {
                            Damage();
                        }
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
                        if (IsIntersectsWithEnemy())
                        {
                            Damage();
                        }
                        position = previousPosition;
                    }
                    previousPosition = position;
                }
            }
            
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                var pos = Mouse.GetState().Position;
                currentWeapron.shot(Game as Game1, position, pos.ToVector2());
            }
            if (Mouse.GetState().RightButton == ButtonState.Pressed)
            {
                var pos = Mouse.GetState().Position;
                currentWeapron.alterShot(Game as Game1, position, pos.ToVector2());
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

        public void Refueling()
        {
            Fuel = 1000;
        }

    }
}