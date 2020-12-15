using System.IO;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game1;
using spacebot;

using spacebot.perk;

namespace spacebot.map_service
{ 
    public class MapBuildingService : IMap
    {
        static readonly uint stepX = 80;
        static readonly uint stepY = 80;
        DateTime start;
        EnemyFactory enemyFactory;
        CometFactory cometFactory;
        LevelDto level;
        List<IColliding> levelGameObjects = new List<IColliding>();
        Game1.Game1 game;
        int enemyAmount;

        public MapBuildingService(StreamReader file, EnemyFactory enemyFactory, CometFactory cometFactory, Game1.Game1 game)
        {
            string fileData = file.ReadToEnd();
            level = JsonConvert.DeserializeObject<LevelDto>(fileData);
            this.cometFactory = cometFactory;
            this.enemyFactory = enemyFactory;
            Enemy.OnDisposeEvent += OnEnemyDisposeNotify;
            this.game = game;
            start = DateTime.Now;
        }

        public void BuildMap(List<Weapron> weapronList)
        {
            for(int i = 0; i < level.MapLayout.Length; i++)
            {
                int[] currentRow = level.MapLayout[i];
                for(int j = 0; j < currentRow.Length; j++)
                {
                    IColliding gameObject = null; 
                    Vector2 position = new Vector2(j * stepY, i * stepX);
                    if(currentRow[j] == 1)
                    {
                        gameObject = cometFactory.CreateComet(position);
                    }
                    if (currentRow[j] == 2)
                    {
                        gameObject = enemyFactory.CreateEnemy(position, EnemyType.common);
                        game.enemies.Add(gameObject);
                        enemyAmount++;
                    }
                    if (currentRow[j] == 3)
                    {
                        gameObject = enemyFactory.CreateEnemy(position, EnemyType.ghost);
                        game.enemies.Add(gameObject);
                        enemyAmount++;
                    }
                    if (gameObject != null)
                    {
                        levelGameObjects.Add(gameObject);
                    }
                }
            }
            WeapronBulletAmountDto w = level.Weapron;
            if (level.Weapron != null && weapronList.Count == 0)
            {
               
                weapronList.Add(new MachineGun(game.machinegunSound, w.Automata));
                weapronList.Add(new ShotGun(game.shotgunSound, w.Shotgun));
            } else
            {
                weapronList[0].SetAmmunition(w.Automata);
                weapronList[1].SetAmmunition(w.Shotgun);
            }
        }

        private void OnEnemyDisposeNotify()
        {
            if(--enemyAmount == 0)
            {
                DisposeLevel();
            }
        }

        public void DisposeLevel()
        {
            levelGameObjects.ForEach((o) => { game.Components.Remove(o as DrawableGameComponent); });
            game.Components.Remove(game.hud);
            game.Components.Remove(game.hero);

            levelGameObjects.Clear();
            float result = 0;
            for (int i = 0; i < game.wList.Count; i++)
            {
                result += game.wList[i].GetAvailableAmmunition();
            }
            result = result / game.wList.Count * (5.0f / (DateTime.Now - start).Seconds) * 100;
            game.CompleteLevel();
            game.levelScore.Add(result);
            game.LoadNewLevel();
        }
    }
}
