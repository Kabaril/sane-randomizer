﻿using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SaneRandomizer
{
    public class RandomGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (!SaneRandomizer.Instance.Config.Drops)
            {
                base.NPCLoot(npc);
                return;
            }
            if (SaneRandomizer.Instance.DropTable.ContainsKey(npc.type))
            {
                foreach(var chance in SaneRandomizer.Instance.DropTable[npc.type])
                {
                    if(Main.rand.Next(0, 10000) < chance.Item2)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, chance.Item1);
                    }
                }
            }
            else
            {
                base.NPCLoot(npc);
            }
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (!SaneRandomizer.Instance.Config.Trade)
            {
                base.SetupShop(type, shop, ref nextSlot);
                return;
            }
            if (SaneRandomizer.Instance.TradeTable.ContainsKey(type))
            {
                for(int i = 0; i < shop.item.Length; i++)
                {
                    shop.item[i] = new Item();
                }
                int index = 0;
                foreach (int item in SaneRandomizer.Instance.TradeTable[type])
                {
                    Item shopitem = new Item();
                    shopitem.SetDefaults(item);
                    shop.item[index] = shopitem;
                    index++;
                }
                if(type == NPCID.Steampunker)
                {
                    if(WorldGen.crimson)
                    {
                        Item shopitem = new Item();
                        shopitem.SetDefaults(ItemID.RedSolution);
                        shop.item[index] = shopitem;
                        index++;
                    } else {
                        Item shopitem = new Item();
                        shopitem.SetDefaults(ItemID.PurpleSolution);
                        shop.item[index] = shopitem;
                        index++;
                    }
                }
                if (NPC.downedPlantBoss) {
                    if (type == NPCID.WitchDoctor)
                    {
                        Item shopitem1 = new Item();
                        shopitem1.SetDefaults(ItemID.PygmyNecklace);
                        shop.item[index] = shopitem1;
                        index++;
                        Item shopitem2 = new Item();
                        shopitem2.SetDefaults(ItemID.HerculesBeetle);
                        shop.item[index] = shopitem2;
                        index++;
                    }
                }
                nextSlot = index;
            }
            else
            {
                base.SetupShop(type, shop, ref nextSlot);
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
            List<int> store = new List<int>();
            List<int> items = new List<int>(Helpers.PreHardmodeItems);

            if (Main.hardMode)
            {
                items.AddRange(Helpers.PrePlanteraItems);
            }

            if(NPC.downedPlantBoss)
            {
                items.AddRange(Helpers.PostPlanteraItems);
            }

            int item_count = items.Count;

            for (int i = 0; i < shop.Length; i++)
            {
                shop[i] = 0;
            }

            for (int i = 0; i < 8; i++)
            {
                int item_index = Main.rand.Next(item_count);
                int item = items[item_index];
                store.Add(item);
            }

            int index = 0;
            foreach (int item in store)
            {
                shop[index] = item;
                index++;
            }
            nextSlot = index;
        }
    }
}