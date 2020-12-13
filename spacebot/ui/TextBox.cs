using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

using spacebot;
using Game1;

namespace spacebot.ui
{
    public delegate void OnEditingComplete(string str);
    public class TextBox: DrawableGameComponent
    {
        StringBuilder builder;
        OnEditingComplete OnComplete;
        bool enable = true;
        bool firstTime = true;
        Game game;
        public TextBox(Game game, OnEditingComplete OnComplete): base(game)
        {
            this.game = game;
            builder = new StringBuilder();
            game.Components.Add(this);
            this.OnComplete = OnComplete;
        }

        private void Disable()
        {
            enable = false;
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(100);
                enable = true;
            });
        }

        public override void Update(GameTime gameTime)
        {
            if (firstTime)
            {
                Thread.Sleep(290);
                firstTime = false;
                return;
            }
            if (enable)
            {
                KeyboardState state = Keyboard.GetState();
                Keys[] keys = state.GetPressedKeys();
                if (keys.Length > 0)
                {
                    if (keys[0] == Keys.Back && builder.Length!=0)
                    {
                        builder.Remove(builder.Length - 1, 1);
                    }
                    else
                    {
                        if (keys[0] == Keys.Space)
                        {
                            builder.Append(" ");
                        }
                        else
                        {
                            builder.Append(keys[0].ToString());
                        }
                    }
                    if (keys[0] == Keys.Enter)
                    {
                        OnComplete(builder.ToString());
                        Game.Components.Remove(this);
                    }
                    Disable();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            SpriteBatch batch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            batch.DrawString((game as Game1.Game1).font, builder.ToString(), new Vector2(300, 150), Color.White);
            base.Draw(gameTime);
        }
    }
}
