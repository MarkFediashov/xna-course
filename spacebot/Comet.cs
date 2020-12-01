using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace spacebot
{
    public class Comet : AbstractMovableComponent
    {
        int hitCounter = 0;
        public Comet(Game game, Texture2D texture, Vector2 position) : base(game, texture, position)
        {
            (Game as Game1.Game1).collidableItems.Add(this);
        }

        public override void OnHitNotify()
        {
            base.OnHitNotify();
            hitCounter--;
        }

        protected override void HandleInput(GameTime gameTime)
        {
            return;
        }

        public override bool ShouldDisposeOnCollideWithBullet()
        {
            return false;
        }

        public override bool ShouldDisposeBullet()
        {
            return true;
        }
    }
}
