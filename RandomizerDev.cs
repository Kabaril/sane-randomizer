using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using System.Linq;
using Terraria.ID;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using log4net;


namespace SaneRandomizer
{
    public class RandomizerDev : IRandomizer
    {
        private ILog Logger;
        private Random Item_Random;
        private Random Drop_Random;
        private Random Trade_Random;
        private int Seed;

        public RandomizerDev(ILog logger, int seed)
        {
            Logger = logger;
            Item_Random = new Random(Seed);
            Drop_Random = new Random(Seed);
            Trade_Random = new Random(Seed);
            Seed = seed;
        }

        // Fully Deterministic
        public Dictionary<int, NPCBaseModifier> RandomizeNPCValues(MinMaxTable minMaxTable)
        {
            Dictionary<int, NPCBaseModifier> NPCModifierTable = new Dictionary<int, NPCBaseModifier>();
            int npc_count = NPCLoader.NPCCount;
            for (int i = 0; i < npc_count; i++)
            {
                if (Helpers.ExcludedEnemies.Contains(i))
                {
                    continue;
                }
                NPC npc = new NPC();
                try
                {
                    npc.SetDefaults(i);
                }
                catch (Exception e)
                {
                    if (npc.FullName is null)
                    {
                        //something went horribly wrong, skip this npc
                        Logger.Warn("Sane Randomizer: Skipped NPC " + i + " could not set Defaults");
                        continue;
                    }
                }
                if (npc.boss)
                {
                    continue;
                }
                if (npc.lifeMax < 10)
                {
                    continue;
                }
                if (npc.lifeMax > (int.MaxValue / 2))
                {
                    continue;
                }
                if (npc.immortal)
                {
                    continue;
                }
                int npc_specific_seed = unchecked(Seed + npc.FullName.GetHashCode());
                Random random = new Random(npc_specific_seed);
                NPCBaseModifier mod = new NPCBaseModifier(random, minMaxTable);
                if (i == NPCID.VoodooDemon)
                {
                    continue;
                }
                NPCModifierTable.Add(i, mod);
            }

            return NPCModifierTable;
        }

        private void AddToDropTable(int npc, IItemDropRule item, ref Dictionary<int, IItemDropRule[]> DropTable)
        {
            if (DropTable.ContainsKey(npc))
            {
                List<IItemDropRule> values = new List<IItemDropRule>(DropTable[npc]);
                values.Add(item);
                DropTable[npc] = values.ToArray();
            }
            else
            {
                DropTable.Add(npc, new IItemDropRule[] { item });
            }
        }

        // Deterministic for Modded Content
        // Non-Deterministic for Vanilla Content
        public Dictionary<int, ItemBaseModifier> RandomizeItemValues(MinMaxTable minMaxTable)
        {
            Dictionary<int, ItemBaseModifier> ItemModifierTable = new Dictionary<int, ItemBaseModifier>();
            int item_count = ItemLoader.ItemCount;
            for (int i = 1; i < item_count; i++)
            {
                ModItem item = ModContent.GetModItem(i);
                ItemBaseModifier mod;
                if (item is not null)
                {
                    //Modded item
                    //Use the name of the item (should never change) to compute Modifiers
                    int item_specific_seed = unchecked(Seed + item.FullName.GetHashCode());
                    Random random = new Random(item_specific_seed);
                    mod = new ItemBaseModifier(random, minMaxTable);
                }
                else
                {
                    //Vanilla Item
                    mod = new ItemBaseModifier(Item_Random, minMaxTable);
                }
                ItemModifierTable.Add(i, mod);
            }

            return ItemModifierTable;
        }

        // Partially Deterministic
        public Dictionary<int, IItemDropRule[]> RandomizeDrops()
        {
            Dictionary<int, IItemDropRule[]> DropTable = new Dictionary<int, IItemDropRule[]>();
            int npc_count = NPCID.Search.Names.Count();
            List<string> names = new List<string>();
            List<Tuple<int, string>> pre_hardmode_npcs = new List<Tuple<int, string>>();
            //List<object> pre_hardmode_debug = new List<object>();
            List<IItemDropRule> pre_hardmode_drops = new List<IItemDropRule>();
            List<Tuple<int, string>> post_hardmode_npcs = new List<Tuple<int, string>>();
            //List<object> post_hardmode_debug = new List<object>();
            List<IItemDropRule> post_hardmode_drops = new List<IItemDropRule>();
            List<Tuple<int, string>> post_plantera_npcs = new List<Tuple<int, string>>();
            List<IItemDropRule> post_plantera_drops = new List<IItemDropRule>();
            //List<object> post_plantera_debug = new List<object>();
            for (int i = 0; i < npc_count; i++)
            {
                if (Helpers.ExcludedEnemies.Contains(i))
                {
                    continue;
                }
                NPC npc = new NPC();
                try
                {
                    NPCSpawnParams param = new NPCSpawnParams();
                    param.strengthMultiplierOverride = 1f;
                    param.playerCountForMultiplayerDifficultyOverride = 1;
                    npc.SetDefaults(i, param);
                }
                catch (Exception e)
                {
                    if (npc.FullName is null)
                    {
                        //something went horribly wrong, skip this npc
                        Logger.Warn("Sane Randomizer: Skipped NPC " + i + " could not set Defaults");
                        continue;
                    }
                }
                if (npc.boss)
                {
                    continue;
                }
                if (npc.friendly)
                {
                    continue;
                }
                if (npc.immortal)
                {
                    continue;
                }
                if (npc.aiStyle == NPCAIStyleID.Spell)
                {
                    continue;
                }
                if (npc.aiStyle == NPCAIStyleID.Spore)
                {
                    continue;
                }
                if (npc.aiStyle == NPCAIStyleID.Worm)
                {
                    if (names.Contains(npc.FullName))
                    {
                        continue;
                    }
                }

                int damage = npc.damage;
                if (damage == 0)
                {
                    continue;
                }
                int max_life = npc.lifeMax;
                int defense = npc.defense;
                float combat_rating = (float)(damage * 5f) + (max_life / 5f) + (defense * 3f);

                List<IItemDropRule> drops = Main.ItemDropsDB.GetRulesForNPCID(i, false);

                names.Add(npc.FullName);

                if (Helpers.OverrideEnemyPreHardmode.Contains(i))
                {
                    pre_hardmode_npcs.Add(new Tuple<int, string>(i, npc.FullName));
                    pre_hardmode_drops.AddRange(drops);
                    //pre_hardmode_debug.Add(new Tuple<float, string>(combat_rating, npc.FullName));
                    continue;
                }

                if (Helpers.OverrideEnemyPostHardmode.Contains(i))
                {
                    post_hardmode_npcs.Add(new Tuple<int, string>(i, npc.FullName));
                    post_hardmode_drops.AddRange(drops);
                    //post_hardmode_debug.Add(new Tuple<float, string>(combat_rating, npc.FullName));
                    continue;
                }

                if (Helpers.OverrideEnemyPostPlantera.Contains(i))
                {
                    post_plantera_npcs.Add(new Tuple<int, string>(i, npc.FullName));
                    post_plantera_drops.AddRange(drops);
                    //post_plantera_debug.Add(new Tuple<float, string>(combat_rating, npc.FullName));
                    continue;
                }

                if (combat_rating < 300f)
                {
                    pre_hardmode_npcs.Add(new Tuple<int, string>(i, npc.FullName));
                    pre_hardmode_drops.AddRange(drops);
                    //pre_hardmode_debug.Add(new Tuple<float, string>(combat_rating, npc.FullName));
                    continue;
                }
                if (combat_rating < 900f)
                {
                    post_hardmode_npcs.Add(new Tuple<int, string>(i, npc.FullName));
                    post_hardmode_drops.AddRange(drops);
                    //post_hardmode_debug.Add(new Tuple<float, string>(combat_rating, npc.FullName));
                    continue;
                }
                post_plantera_npcs.Add(new Tuple<int, string>(i, npc.FullName));
                post_plantera_drops.AddRange(drops);
                //post_plantera_debug.Add(new Tuple<float, string>(combat_rating, npc.FullName));
                //npc basierend auf health, damage, etc in gamestage einordnen und drops aus db in gamestage schreiben
            }

            RandomizeDropTables(pre_hardmode_npcs, pre_hardmode_drops, ref DropTable);
            RandomizeDropTables(post_hardmode_npcs, post_hardmode_drops, ref DropTable);
            RandomizeDropTables(post_plantera_npcs, post_plantera_drops, ref DropTable);

            return DropTable;
        }

        private void RandomizeDropTables(List<Tuple<int, string>> npcs, List<IItemDropRule> drops, ref Dictionary<int, IItemDropRule[]> DropTable)
        {
            int item_count = drops.Count;
            int npc_count = npcs.Count;

            List<IItemDropRule> selected_items = new List<IItemDropRule>();

            foreach (Tuple<int, string> npc in npcs)
            {
                int drop_specific_seed = unchecked(Seed + npc.Item2.GetHashCode());
                Random random = new Random(drop_specific_seed);
                int item_index = random.Next(item_count);
                IItemDropRule item = drops.ElementAt(item_index);
                selected_items.Add(item);
                AddToDropTable(npc.Item1, item, ref DropTable);
            }

            var unassigned = drops.Except(selected_items);

            foreach (IItemDropRule item in unassigned)
            {
                int npc_index = Drop_Random.Next(npc_count);
                int npc = npcs[npc_index].Item1;
                AddToDropTable(npc, item, ref DropTable);
            }
        }

        // Non-Deterministic
        public Dictionary<int, int[]> RandomizeTrades()
        {
            Dictionary<int, int[]> TradeTable = new Dictionary<int, int[]>();
            List<int> pre_hardmode_traders = new List<int>(Helpers.PreHardmodeMerchants);
            List<int> pre_hardmode_items = new List<int>(Helpers.PreHardmodeItems);
            RandomizeTradeTables(pre_hardmode_traders, pre_hardmode_items, ref TradeTable);
            List<int> pre_plantera_traders = new List<int>(Helpers.PrePlanteraMerchants);
            List<int> pre_plantera_items = new List<int>(Helpers.PrePlanteraItems);
            RandomizeTradeTables(pre_plantera_traders, pre_plantera_items, ref TradeTable);
            List<int> post_plantera_traders = new List<int>(Helpers.PostPlanteraMerchants);
            List<int> post_plantera_items = new List<int>(Helpers.PostPlanteraItems);
            RandomizeTradeTables(post_plantera_traders, post_plantera_items, ref TradeTable);

            return TradeTable;
        }
        
        private void RandomizeTradeTables(List<int> npcs, List<int> items, ref Dictionary<int, int[]> TradeTable)
        {
            int item_count = items.Count;

            foreach (int npc in npcs)
            {
                if (npc == NPCID.Mechanic)
                {
                    continue;
                }
                List<int> store = new List<int>();
                for (int i = 0; i < 8; i++)
                {
                    int item_index = Trade_Random.Next(item_count);
                    int item = items[item_index];
                    while (store.Contains(item))
                    {
                        item_index = Trade_Random.Next(item_count);
                        item = items[item_index];
                    }
                    store.Add(item);
                }
                if (npc == NPCID.SkeletonMerchant)
                {
                    store.Add(ItemID.YoYoGlove);
                }
                if (npc == NPCID.ArmsDealer)
                {
                    store.Add(ItemID.MusketBall);
                    store.Add(ItemID.Minishark);
                    store.Add(ItemID.IllegalGunParts);
                    store.Add(ItemID.StyngerBolt);
                    store.Add(ItemID.CandyCorn);
                    store.Add(ItemID.ExplosiveJackOLantern);
                }
                if (npc == NPCID.WitchDoctor)
                {
                    store.Add(ItemID.Stake);
                    store.Add(ItemID.Nail);
                }
                if (npc == NPCID.GoblinTinkerer)
                {
                    store.Add(ItemID.TinkerersWorkshop);
                    store.Add(ItemID.Toolbelt);
                }
                if (npc == NPCID.Merchant)
                {
                    store.Add(ItemID.Sickle);
                    store.Add(ItemID.BugNet);
                    store.Add(ItemID.PiggyBank);
                    store.Add(ItemID.LesserHealingPotion);
                    store.Add(ItemID.LesserManaPotion);
                }
                if (npc == NPCID.Steampunker)
                {
                    store.Add(ItemID.ExplosivePowder);
                    store.Add(ItemID.Clentaminator);
                    store.Add(ItemID.GreenSolution);
                    store.Add(ItemID.Teleporter);
                }
                if (npc == NPCID.Dryad)
                {
                    store.Add(ItemID.PurificationPowder);
                }
                if (npc == NPCID.Truffle)
                {
                    store.Add(ItemID.DarkBlueSolution);
                    store.Add(ItemID.BlueSolution);
                    store.Add(ItemID.Autohammer);
                }
                if (npc == NPCID.Wizard)
                {
                    store.Add(ItemID.CrystalBall);
                    store.Add(ItemID.SpellTome);
                }
                if (npc == NPCID.Cyborg)
                {
                    store.Add(ItemID.RocketI);
                    store.Add(ItemID.RocketIII);
                }
                TradeTable.Add(npc, store.ToArray());
            }
        }

    }
}
