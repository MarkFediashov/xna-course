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
    public class CometFactory
    {
        Game game;
        static Texture2D texture;

        public CometFactory(Game game, Texture2D texture)
        {
            CometFactory.texture = texture;
            this.game = game;
        }

        public Comet CreateComet(Vector2 position)
        {
            var e = new Comet(game, texture, position);
            return e;
        }
    }
}
