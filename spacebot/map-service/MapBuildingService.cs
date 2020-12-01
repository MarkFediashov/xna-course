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

namespace spacebot.map_service
{
    
    public class MapBuildingService
    {
        static readonly uint stepX = 80;
        static readonly uint stepY = 80;
        EnemyFactory enemyFactory;
        CometFactory cometFactory;
        LevelDto level;

        public MapBuildingService(StreamReader file, EnemyFactory enemyFactory, CometFactory cometFactory)
        {
            string fileData = file.ReadToEnd();
            level = JsonConvert.DeserializeObject<LevelDto>(fileData);
            this.cometFactory = cometFactory;
            this.enemyFactory = enemyFactory;
        }

        public void BuildMap()
        {
            for(int i = 0; i < level.MapLayout.Length; i++)
            {
                int[] currentRow = level.MapLayout[i];
                for(int j = 0; j < currentRow.Length; j++)
                {
                    Vector2 position = new Vector2(j * stepY, i * stepX);
                    if(currentRow[j] == 1)
                    {
                        cometFactory.CreateComet(position);
                    }
                    if (currentRow[j] == 2)
                    {
                        enemyFactory.CreateEnemy(position);
                    }
                }
            }
        }
    }
}
