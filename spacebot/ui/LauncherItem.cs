using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace spacebot.ui
{
    public class LauncherItem : DrawableGameComponent
    {
        private string text;
        private bool isActive = false;
        private Game game;
        private Vector2 position;
        private static bool isFirst = true;
        private Action onSelect;

        private static Vector2 startPosition = new Vector2(30, 30);
        private static Vector2 offset = new Vector2(0, 40);

        public LauncherItem(Game game, string text, Action onSelect) : base(game)
        {
            this.game = game;
            this.text = text;

            this.onSelect = onSelect;

            position = startPosition + offset;
            if (isFirst)
            {
                isActive = true;
                isFirst = false;
            }

            offset.Y += 80;

            game.Components.Add(this);
        }

        public void Activate()
        {
            isActive = true;
        }

        public void Deactivatee()
        {
            isActive = false;
        }

        public void Select()
        {
            onSelect();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch batch = (SpriteBatch)game.Services.GetService(typeof(SpriteBatch));
            Color c = Color.White;
            if (isActive)
            {
                c = Color.Red;
            }
            batch.DrawString((game as Game1.Game1).font, text, position, c);
            base.Draw(gameTime);
        }

        public static void ResetAll()
        {
            isFirst = true;
            offset = new Vector2(0, 40);
        }
    }
}
