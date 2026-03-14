using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SaneRandomizer;

public class RandomGlobalNPC : GlobalNPC
{
    public override void SetDefaults(NPC npc)
    {
        base.SetDefaults(npc);
        if (SaneRandomizer.Instance is null)
        {
            return;
        }
        if (!SaneRandomizer.Instance.NPCModifierTable.ContainsKey(npc.type))
        {
            return;
        }
        var config = SaneRandomizer.Instance.Config;
        var mod = SaneRandomizer.Instance.NPCModifierTable[npc.type];
        if (config.NPCDamage)
        {
            npc.damage = (int)(npc.damage * (mod.DamageModifier / 100f));
        }
        if (config.NPCArmor)
        {
            npc.defense = (int)(npc.defense * (mod.DefenseModifier / 100f));
        }
        if (config.NPCLife)
        {
            npc.lifeMax = (int)(npc.lifeMax * (mod.MaxLifeModifier / 100f));
        }
    }

    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        if (SaneRandomizer.Instance is null)
        {
            return;
        }
        if (!SaneRandomizer.Instance.Config.Drops)
        {
            base.ModifyNPCLoot(npc, npcLoot);
            return;
        }
        if (SaneRandomizer.Instance.DropTable.ContainsKey(npc.type))
        {
            npcLoot.RemoveWhere(_ => true, false);
            foreach (var drop in SaneRandomizer.Instance.DropTable[npc.type])
            {
                npcLoot.Add(drop);
            }
        }
        else
        {
            base.ModifyNPCLoot(npc, npcLoot);
        }
    }

    public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
    {
        if (!SaneRandomizer.Instance.Config.Trade)
        {
            base.ModifyActiveShop(npc, shopName, items);
            return;
        }

        // this is inefficient, but should work even with runtime modifications from other mods
        var namedTradeTable = SaneRandomizer.Instance.TradeTable.Select(x => new Tuple<string, int[]>(NPCShopDatabase.GetShopName(x.Key), x.Value)).ToList();

        var matchingTradeTable = namedTradeTable.Find(x => x.Item1 == shopName);
        if (matchingTradeTable is not null)
        {
            for(var i = 0; i < items.Length; i++)
            {
                items[i] = new Item();
            }
            var index = 0;
            foreach (var item in matchingTradeTable.Item2)
            {
                var shopItem = new Item();
                shopItem.SetDefaults(item);
                items[index] = shopItem;
                index++;
            }
            if(matchingTradeTable.Item1 == NPCShopDatabase.GetShopName(NPCID.Steampunker))
            {
                if(WorldGen.crimson)
                {
                    var shopItem = new Item();
                    shopItem.SetDefaults(ItemID.RedSolution);
                    items[index] = shopItem;
                    index++;
                } else {
                    var shopItem = new Item();
                    shopItem.SetDefaults(ItemID.PurpleSolution);
                    items[index] = shopItem;
                    index++;
                }
            }
            if (NPC.downedPlantBoss && matchingTradeTable.Item1 == NPCShopDatabase.GetShopName(NPCID.WitchDoctor)) {
                var shopItem1 = new Item();
                shopItem1.SetDefaults(ItemID.PygmyNecklace);
                items[index] = shopItem1;
                index++;
                var shopItem2 = new Item();
                shopItem2.SetDefaults(ItemID.HerculesBeetle);
                items[index] = shopItem2;
                index++;
            }
        }
        else
        {
            base.ModifyActiveShop(npc, shopName, items);
        }
    }

    public override void SetupTravelShop(int[] shop, ref int nextSlot)
    {
        if (!SaneRandomizer.Instance.Config.Trade)
        {
            base.SetupTravelShop(shop, ref nextSlot);
            return;
        }
        if (Main.rand is null)
        {
            return;
        }
        var store = new List<int>();
        var items = new List<int>(Helpers.PreHardmodeItems);

        if (Main.hardMode)
        {
            items.AddRange(Helpers.PrePlanteraItems);
        }

        if(NPC.downedPlantBoss)
        {
            items.AddRange(Helpers.PostPlanteraItems);
        }

        var itemCount = items.Count;

        for (var i = 0; i < shop.Length; i++)
        {
            shop[i] = 0;
        }

        for (var i = 0; i < 8; i++)
        {
            var itemIndex = Main.rand.Next(itemCount);
            var item = items[itemIndex];
            store.Add(item);
        }

        var index = 0;
        foreach (var item in store)
        {
            shop[index] = item;
            index++;
        }
        nextSlot = index;
    }
}