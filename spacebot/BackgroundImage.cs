using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Game1
{
    class BackgroundImage : DrawableGameComponent
    {
        private Texture2D bgImage;

        public BackgroundImage(Game game, Texture2D texture): base(game)
        {
            bgImage = texture;
            game.Components.Add(this);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch batch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            batch.Draw(bgImage, new Rectangle(0, 0, Game.GraphicsDevice.DisplayMode.Width,
                Game.GraphicsDevice.DisplayMode.Height), Color.White);
            base.Draw(gameTime);
        }
    }
}
