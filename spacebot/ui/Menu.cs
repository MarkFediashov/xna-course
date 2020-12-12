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
    public class Menu : DrawableGameComponent
    {
        Game1.Game1 game;
        LauncherItem[] items = new LauncherItem[3];
        int activeIndex = 0;

        StringBuilder builder = new StringBuilder();

        bool enable = true;
        public Menu(Game1.Game1 game): base(game)
        {
            this.game = game;
            game.Components.Add(this);
            items[0] = new LauncherItem(game, "Start", Startupt);
            items[1] = new LauncherItem(game, "About", ()=> { });
            items[2] = new LauncherItem(game, "Exit", Exit);
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

        private void Exit()
        {
            game.Exit();
        }

        private void Startupt()
        {
            for(int i = 0; i < items.Length; i++)
            {
                Game.Components.Remove(items[i]);
            }
            Game.Components.Remove(this);
            new TextBox(game, (string s) =>
            {
                game.playerName = s;
                game.Startup();
            });
        }

        public override void Update(GameTime gameTime)
        {
            bool updated = false;
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && activeIndex <= 1 && activeIndex >=0 && enable)
            {
                updated = true;
                items[++activeIndex].Activate();
                Disable();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && activeIndex <= 2 && activeIndex >= 1 && enable)
            {
                updated = true;
                items[--activeIndex].Activate();
                Disable();
            }
            if (updated)
            {
                if(activeIndex < 2)
                {
                    items[activeIndex + 1].Deactivatee();
                }
                if (activeIndex > 0)
                {
                    items[activeIndex - 1].Deactivatee();
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                items[activeIndex].Select();
            }

        }
    }
}
