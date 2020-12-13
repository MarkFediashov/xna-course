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
        protected Game1.Game1 game;
        protected LauncherItem[] items;
        int activeIndex = 0;

        StringBuilder builder = new StringBuilder();

        bool enable = true;
        public Menu(Game1.Game1 game) : base(game)
        {
            this.game = game;
            game.Components.Add(this);
            FillItemList();
        }

        protected virtual void FillItemList()
        {
            items = new LauncherItem[3];
            items[0] = new LauncherItem(game, "Start", Startupt);
            items[1] = new LauncherItem(game, "About", () => { });
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
            LauncherItem.ResetAll();
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
            LauncherItem.ResetAll();
        }

        public override void Update(GameTime gameTime)
        {
            bool updated = false;
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && activeIndex <= items.Length - 2 && activeIndex >=0 && enable)
            {
                updated = true;
                items[++activeIndex].Activate();
                Disable();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && activeIndex <= items.Length - 1 && activeIndex >= 1 && enable)
            {
                updated = true;
                items[--activeIndex].Activate();
                Disable();
            }
            if (updated)
            {
                if(activeIndex < items.Length - 1)
                {
                    items[activeIndex + 1].Deactivatee();
                }
                if (activeIndex > 0)
                {
                    items[activeIndex - 1].Deactivatee();
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && enable)
            {
                items[activeIndex].Select();
                Disable();
                game.Components.Remove(this);
                this.Dispose();
            }

        }
    }
}
