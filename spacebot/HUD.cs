using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace spacebot
{
    public class HUD: DrawableGameComponent
    {
        Game1.Game1 game;
        Vector2 posForShotgun = new Vector2(0, 680);
        Vector2 posForAutomata = new Vector2(40, 680);
        Vector2 posForFuel = new Vector2(180, 680);
        Vector2 fillOffset = new Vector2(9, 8);
        Texture2D energyBar;
        Texture2D energyBarFill;

        public HUD(Game1.Game1 game): base(game)
        {
            this.game = game;
            Game.Components.Add(this);

            energyBar = game.Content.Load<Texture2D>("energy-bar");
            energyBarFill = game.Content.Load<Texture2D>("energy-bar-fill");
        }

        public override void Draw(GameTime gameTime)
        {
            string shotgunStr = game.wList[1].GetAvailableAmmunition().ToString();
            string automataStr = game.wList[0].GetAvailableAmmunition().ToString();
            string fuelStr = game.hero.Fuel.ToString();

            SpriteBatch batch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            batch.DrawString(game.font, shotgunStr, posForShotgun, Color.White);
            batch.DrawString(game.font, automataStr, posForAutomata, Color.White);
            batch.Draw(energyBar, posForFuel, Color.White);
            float scale = (float)game.hero.Fuel / 1000;
            Vector2 scaleVector = new Vector2(scale, 1f);
            batch.Draw(energyBarFill, posForFuel + fillOffset, scale: scaleVector);
            base.Draw(gameTime);
            
        }
    }
}
