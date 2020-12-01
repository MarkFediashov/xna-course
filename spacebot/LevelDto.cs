using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spacebot
{
    public class LevelDto
    {
        public string LevelName;
        public int Timeout;
        [JsonProperty("Weapron")]
        public WeapronBulletAmountDto Weapron;
        public int[][] MapLayout;
    }

    public class WeapronBulletAmountDto
    {
        public int Automata;
        public int Shotgun;
    }
}
