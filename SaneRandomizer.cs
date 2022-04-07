using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using System.Linq;
using Terraria.ID;

namespace SaneRandomizer
{
    public class SaneRandomizer : Mod
    {
        public Random Random;
        public SaneRandomizerConfig Config;

        //NPC Type -> drop type, weight (how many in 10.000)
        public Dictionary<int, Tuple<int, int>[]> DropTable;

        //NPC Type -> item types
        public Dictionary<int, int[]> TradeTable;

        public Dictionary<int, ItemBaseModifier> ItemModifierTable;

        // Automatically set by tModLoader
        public static SaneRandomizer Instance;

        public override void Load()
        {
            Logger.Info("Initializing Sane Randomizer");
            Config = SaneRandomizerConfig.Instance;
            if (Config.Seed == 0)
            {
                Config.Seed = new Random().Next(1, int.MaxValue);
                Helpers.Save(Config);
            }
            Random = new Random(Config.Seed);

            //start randomizing
            ItemModifierTable = new Dictionary<int, ItemBaseModifier>();
            Logger.Info("Creating Drop Tables");
            RandomizeDrops();
            Logger.Info("Creating Trade Tables");
            RandomizeTrades();
            Logger.Info("Creating Item Modification Table");
            RandomizeItemValues();

            Instance = this;
        }

        public override void Unload()
        {
            Logger.Info("Unloading Sane Randomizer");
            Random = null;
            Config = null;
            DropTable = null;
            TradeTable = null;
            ItemModifierTable = null;
            Instance = null;
        }

        private void AddToDropTable(int npc, int item, int chance)
        {
            if (DropTable.ContainsKey(npc))
            {
                List<Tuple<int, int>> values = new List<Tuple<int, int>>(DropTable[npc]);
                values.Add(new Tuple<int, int>(item, chance));
                DropTable[npc] = values.ToArray();
            }
            else
            {
                DropTable.Add(npc, new Tuple<int, int>[] { new Tuple<int, int>(item, chance) });
            }
        }

        private void RandomizeItemValues()
        {
            int item_count = ItemLoader.ItemCount;
            for(int i = 1; i < item_count; i++)
            {
                ItemBaseModifier mod = new ItemBaseModifier(Random);
                ItemModifierTable.Add(i, mod);
            }
        }

        private void RandomizeDrops()
        {
            DropTable = new Dictionary<int, Tuple<int, int>[]>();
            List<int> pre_hardmode_npcs = new List<int>(Helpers.PreHardmodeNPCs);
            List<int> pre_hardmode_items = new List<int>(Helpers.PreHardmodeItems);
            RandomizeDropTables(pre_hardmode_npcs, pre_hardmode_items);
            List<int> pre_plantera_npcs = new List<int>(Helpers.PrePlanteraNPCs);
            List<int> pre_plantera_items = new List<int>(Helpers.PrePlanteraItems);
            RandomizeDropTables(pre_plantera_npcs, pre_plantera_items);
            List<int> post_plantera_npcs = new List<int>(Helpers.PostPlanteraNPCs);
            List<int> post_plantera_items = new List<int>(Helpers.PostPlanteraItems);
            RandomizeDropTables(post_plantera_npcs, post_plantera_items);
        }

        private void RandomizeDropTables(List<int> npcs, List<int> items)
        {
            int item_count = items.Count;
            int npc_count = npcs.Count;

            List<int> selected_items = new List<int>();

            foreach (int npc in npcs)
            {
                int item_index = Random.Next(item_count);
                int item = items[item_index];
                selected_items.Add(item);
                int chance = Random.Next(3, 1000);
                AddToDropTable(npc, item, chance);
            }

            var unassigned = items.Except(selected_items);

            foreach (int item in unassigned)
            {
                int chance = Random.Next(3, 1000);
                int npc_index = Random.Next(npc_count);
                int npc = npcs[npc_index];
                AddToDropTable(npc, item, chance);
            }
        }

        private void RandomizeTrades()
        {
            TradeTable = new Dictionary<int, int[]>();
            List<int> pre_hardmode_traders = new List<int>(Helpers.PreHardmodeMerchants);
            List<int> pre_hardmode_items = new List<int>(Helpers.PreHardmodeItems);
            RandomizeTradeTables(pre_hardmode_traders, pre_hardmode_items);
            List<int> pre_plantera_traders = new List<int>(Helpers.PrePlanteraMerchants);
            List<int> pre_plantera_items = new List<int>(Helpers.PrePlanteraItems);
            RandomizeTradeTables(pre_plantera_traders, pre_plantera_items);
            List<int> post_plantera_traders = new List<int>(Helpers.PostPlanteraMerchants);
            List<int> post_plantera_items = new List<int>(Helpers.PostPlanteraItems);
            RandomizeTradeTables(post_plantera_traders, post_plantera_items);
        }

        private void RandomizeTradeTables(List<int> npcs, List<int> items)
        {
            int item_count = items.Count;

            foreach (int npc in npcs)
            {
                if(npc == NPCID.Mechanic)
                {
                    continue;
                }
                List<int> store = new List<int>();
                for (int i = 0; i < 8; i++)
                {
                    int item_index = Random.Next(item_count);
                    int item = items[item_index];
                    while(store.Contains(item))
                    {
                        item_index = Random.Next(item_count);
                        item = items[item_index];
                    }
                    store.Add(item);
                }
                if(npc == NPCID.SkeletonMerchant)
                {
                    store.Add(ItemID.YoYoGlove);
                }
                if(npc == NPCID.ArmsDealer)
                {
                    store.Add(ItemID.MusketBall);
                    store.Add(ItemID.Minishark);
                    store.Add(ItemID.IllegalGunParts);
                    store.Add(ItemID.StyngerBolt);
                    store.Add(ItemID.CandyCorn);
                    store.Add(ItemID.ExplosiveJackOLantern);
                }
                if(npc == NPCID.WitchDoctor)
                {
                    store.Add(ItemID.Stake);
                    store.Add(ItemID.Nail);
                }
                if(npc == NPCID.GoblinTinkerer)
                {
                    store.Add(ItemID.TinkerersWorkshop);
                    store.Add(ItemID.Toolbelt);
                }
                if(npc == NPCID.Merchant)
                {
                    store.Add(ItemID.Sickle);
                    store.Add(ItemID.BugNet);
                    store.Add(ItemID.PiggyBank);
                    store.Add(ItemID.LesserHealingPotion);
                    store.Add(ItemID.LesserManaPotion);
                }
                if(npc == NPCID.Steampunker)
                {
                    store.Add(ItemID.ExplosivePowder);
                    store.Add(ItemID.Clentaminator);
                    store.Add(ItemID.GreenSolution);
                    store.Add(ItemID.Teleporter);
                }
                if(npc == NPCID.Dryad)
                {
                    store.Add(ItemID.PurificationPowder);
                }
                if(npc == NPCID.Truffle)
                {
                    store.Add(ItemID.DarkBlueSolution);
                    store.Add(ItemID.BlueSolution);
                    store.Add(ItemID.Autohammer);
                }
                if(npc == NPCID.Wizard)
                {
                    store.Add(ItemID.CrystalBall);
                    store.Add(ItemID.SpellTome);
                }
                if(npc == NPCID.Cyborg)
                {
                    store.Add(ItemID.RocketI);
                    store.Add(ItemID.RocketIII);
                }
                TradeTable.Add(npc, store.ToArray());
            }
        }
    }
}