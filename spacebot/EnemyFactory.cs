using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using spacebot;

using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public enum EnemyType
    {
        common,
        ghost
    }
    public class EnemyFactory
    {
        static Game game;
        static Texture2D enemyTexture;
        static Texture2D ghostEnemyTexture;
        static Random rand = new Random();
        public List<IColliding> enemies;
        public EnemyFactory(Game g, List<IColliding> collidingList)
        {
            game = g;
            enemyTexture = game.Content.Load<Texture2D>("e1");
            ghostEnemyTexture = game.Content.Load<Texture2D>("ghost");

            enemies = collidingList;
        }

        public Enemy CreateEnemy(Vector2 v, EnemyType type)
        {
            Enemy e;
            if (type == EnemyType.common)
            {
                e = new Enemy(game, enemyTexture, v);
            }
            else
            {
                e = new GhostEnemy(game, ghostEnemyTexture, v);
            }
            enemies.Add(e);
            return e;
        }
    }
}
