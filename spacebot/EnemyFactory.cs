using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1;

using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class EnemyFactory
    {
        static Game game;
        static Texture2D enemyTexture;
        static Random rand = new Random();
        public List<Enemy> enemies;
        public EnemyFactory(Game g)
        {
            game = g;
            enemyTexture = game.Content.Load<Texture2D>("e1");
            enemies = new List<Enemy>();
        }
        public Enemy CreateEnemy()
        {
            Vector2 v = new Vector2(rand.Next() % 500, 15);
            var e = new Enemy(game, enemyTexture, v);
            enemies.Add(e);
            return e;
        }
    }
}
