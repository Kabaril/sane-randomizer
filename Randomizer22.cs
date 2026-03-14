using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using System.Linq;
using Terraria.ID;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using log4net;

namespace SaneRandomizer;

public class Randomizer22 : IRandomizer
{
    private ILog _logger;
    private Random _random;

    public Randomizer22(ILog logger, int seed)
    {
        _logger = logger;
        _random = new Random(seed);
    }

    public Dictionary<int, NPCBaseModifier> RandomizeNPCValues(MinMaxTable minMaxTable)
    {
        var npcModifierTable = new Dictionary<int, NPCBaseModifier>();
        var npcCount = NPCLoader.NPCCount;
        for (var i = 0; i < npcCount; i++)
        {
            if (Helpers.ExcludedEnemies.Contains(i))
            {
                continue;
            }
            var npc = new NPC();
            try
            {
                npc.SetDefaults(i);
            }
            catch (Exception e)
            {
                if (npc.FullName is null)
                {
                    //something went horribly wrong, skip this npc
                    _logger.Warn("Sane Randomizer: Skipped NPC " + i + " could not set Defaults");
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
            var mod = new NPCBaseModifier(_random, minMaxTable);
            if(i == NPCID.VoodooDemon)
            {
                continue;
            }
            npcModifierTable.Add(i, mod);
        }

        return npcModifierTable;
    }

    private void AddToDropTable(int npc, IItemDropRule item, ref Dictionary<int, IItemDropRule[]> dropTable)
    {
        if (dropTable.ContainsKey(npc))
        {
            var values = new List<IItemDropRule>(dropTable[npc]);
            values.Add(item);
            dropTable[npc] = values.ToArray();
        }
        else
        {
            dropTable.Add(npc, [item]);
        }
    }

    public Dictionary<int, ItemBaseModifier> RandomizeItemValues(MinMaxTable minMaxTable)
    {
        var itemModifierTable = new Dictionary<int, ItemBaseModifier>();
        var itemCount = ItemLoader.ItemCount;
        for (var i = 1; i < itemCount; i++)
        {
            var mod = new ItemBaseModifier(_random, minMaxTable);
            itemModifierTable.Add(i, mod);
        }

        return itemModifierTable;
    }

    public Dictionary<int, IItemDropRule[]> RandomizeDrops()
    {
        var dropTable = new Dictionary<int, IItemDropRule[]>();
        var npcCount = NPCID.Search.Names.Count();
        var names = new List<string>();
        var preHardmodeNpcs = new List<int>();
        //List<object> pre_hardmode_debug = new List<object>();
        var preHardmodeDrops = new List<IItemDropRule>();
        var postHardmodeNpcs = new List<int>();
        //List<object> post_hardmode_debug = new List<object>();
        var postHardmodeDrops = new List<IItemDropRule>();
        var postPlanteraNpcs = new List<int>();
        var postPlanteraDrops = new List<IItemDropRule>();
        //List<object> post_plantera_debug = new List<object>();
        for (var i = 0; i < npcCount; i++)
        {
            if (Helpers.ExcludedEnemies.Contains(i))
            {
                continue;
            }
            var npc = new NPC();
            try
            {
                var param = new NPCSpawnParams();
                param.strengthMultiplierOverride = 1f;
                param.playerCountForMultiplayerDifficultyOverride = 1;
                npc.SetDefaults(i, param);
            }
            catch (Exception e)
            {
                if (npc.FullName is null)
                {
                    //something went horribly wrong, skip this npc
                    _logger.Warn("Sane Randomizer: Skipped NPC " + i + " could not set Defaults");
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

            var damage = npc.damage;
            if (damage == 0)
            {
                continue;
            }
            var maxLife = npc.lifeMax;
            var defense = npc.defense;
            var combatRating = damage * 5f + maxLife / 5f + defense * 3f;

            var drops = Main.ItemDropsDB.GetRulesForNPCID(i, false);

            names.Add(npc.FullName);

            if (Helpers.OverrideEnemyPreHardmode.Contains(i))
            {
                preHardmodeNpcs.Add(i);
                preHardmodeDrops.AddRange(drops);
                //pre_hardmode_debug.Add(new Tuple<float, string>(combat_rating, npc.FullName));
                continue;
            }

            if (Helpers.OverrideEnemyPostHardmode.Contains(i))
            {
                postHardmodeNpcs.Add(i);
                postHardmodeDrops.AddRange(drops);
                //post_hardmode_debug.Add(new Tuple<float, string>(combat_rating, npc.FullName));
                continue;
            }

            if (Helpers.OverrideEnemyPostPlantera.Contains(i))
            {
                postPlanteraNpcs.Add(i);
                postPlanteraDrops.AddRange(drops);
                //post_plantera_debug.Add(new Tuple<float, string>(combat_rating, npc.FullName));
                continue;
            }

            if (combatRating < 300f)
            {
                preHardmodeNpcs.Add(i);
                preHardmodeDrops.AddRange(drops);
                //pre_hardmode_debug.Add(new Tuple<float, string>(combat_rating, npc.FullName));
                continue;
            }
            if (combatRating < 900f)
            {
                postHardmodeNpcs.Add(i);
                postHardmodeDrops.AddRange(drops);
                //post_hardmode_debug.Add(new Tuple<float, string>(combat_rating, npc.FullName));
                continue;
            }
            postPlanteraNpcs.Add(i);
            postPlanteraDrops.AddRange(drops);
            //post_plantera_debug.Add(new Tuple<float, string>(combat_rating, npc.FullName));
            //npc basierend auf health, damage, etc in gamestage einordnen und drops aus db in gamestage schreiben
        }

        RandomizeDropTables(preHardmodeNpcs, preHardmodeDrops, ref dropTable);
        RandomizeDropTables(postHardmodeNpcs, postHardmodeDrops, ref dropTable);
        RandomizeDropTables(postPlanteraNpcs, postPlanteraDrops, ref dropTable);

        return dropTable;
    }

    private void RandomizeDropTables(List<int> npcs, List<IItemDropRule> drops, ref Dictionary<int, IItemDropRule[]> dropTable)
    {
        var itemCount = drops.Count;
        var npcCount = npcs.Count;

        var selectedItems = new List<IItemDropRule>();

        foreach (var npc in npcs)
        {
            var itemIndex = _random.Next(itemCount);
            var item = drops.ElementAt(itemIndex);
            selectedItems.Add(item);
            AddToDropTable(npc, item, ref dropTable);
        }

        var unassigned = drops.Except(selectedItems);

        foreach (var item in unassigned)
        {
            var npcIndex = _random.Next(npcCount);
            var npc = npcs[npcIndex];
            AddToDropTable(npc, item, ref dropTable);
        }
    }

    public Dictionary<int, int[]> RandomizeTrades()
    {
        var tradeTable = new Dictionary<int, int[]>();
        var preHardmodeTraders = new List<int>(Helpers.PreHardmodeMerchants);
        var preHardmodeItems = new List<int>(Helpers.PreHardmodeItems);
        RandomizeTradeTables(preHardmodeTraders, preHardmodeItems, ref tradeTable);
        var prePlanteraTraders = new List<int>(Helpers.PrePlanteraMerchants);
        var prePlanteraItems = new List<int>(Helpers.PrePlanteraItems);
        RandomizeTradeTables(prePlanteraTraders, prePlanteraItems, ref tradeTable);
        var postPlanteraTraders = new List<int>(Helpers.PostPlanteraMerchants);
        var postPlanteraItems = new List<int>(Helpers.PostPlanteraItems);
        RandomizeTradeTables(postPlanteraTraders, postPlanteraItems, ref tradeTable);

        return tradeTable;
    }

    private void RandomizeTradeTables(List<int> npcs, List<int> items, ref Dictionary<int, int[]> tradeTable)
    {
        var itemCount = items.Count;

        foreach (var npc in npcs)
        {
            if (npc == NPCID.Mechanic)
            {
                continue;
            }
            var store = new List<int>();
            for (var i = 0; i < 8; i++)
            {
                var itemIndex = _random.Next(itemCount);
                var item = items[itemIndex];
                while (store.Contains(item))
                {
                    itemIndex = _random.Next(itemCount);
                    item = items[itemIndex];
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
            tradeTable.Add(npc, store.ToArray());
        }
    }
}