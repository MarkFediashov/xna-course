using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Game1
{
   
    public class BulletFactory
    {
        static Game game;
        static Texture2D bulletTexture;
        static Texture2D shrepnelTexture;
        static Random rand = new Random();

        public BulletFactory(Game g)
        {
            game = g;
            bulletTexture = game.Content.Load<Texture2D>("shot1");
            shrepnelTexture = game.Content.Load<Texture2D>("4");
        }
        public Bullet CreateBullet(Vector2 start, Vector2 end)
        {
            return new Bullet(game, bulletTexture, start, end);
        }
        public Bullet CreateShrapneel(Vector2 start, Vector2 end)
        {
            return new Bullet(game, shrepnelTexture, start, end, 25f);
        }
    }
}
