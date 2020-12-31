using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using spacebot.perk;
using spacebot.ui;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Game1;

namespace spacebot.map_service
{
    public class ShopLevelService: Menu, IMap
    {
        public static List<IPerk<Weapron>> AllPerks = new List<IPerk<Weapron>>() { new MachineGunPairPerk(), new ShotGunDoubleBarreledPerk() };

        public ShopLevelService(Game1.Game1 game) : base(game)
        {

        }

        private Action OnSelect(int index)
        {
            IPerk<Weapron> injectedPerk = AllPerks[index];
            Type weapronGenericPerk = AllPerks[index].GetType().BaseType.GetGenericArguments()[0];
            var weapron = game.wList.Where((Weapron w)=>
            {
                return w.GetType() == weapronGenericPerk;
            }).First();
            Action callback = () =>
            {
                bool called = false;
                if (!called)
                {
                    weapron.InjectPerk(injectedPerk);
                    game.LoadNewLevel();
                    called = true;
                } 
            };
            return callback;
        }

        protected override void FillItemList()
        {
            items = new LauncherItem[AllPerks.Count];
            for(int i = 0; i < items.Length; i++)
            {
                items[i] = ToItem(AllPerks[i], OnSelect(i));
            }
        }

        private LauncherItem ToItem(IPerk<Weapron> perk, Action action)
        {
            return new LauncherItem(game, perk.GetPerkDescription(), action);
        }

        public void DisposeLevel()
        {
            Game.Components.Remove(this);
        }
    }
}
