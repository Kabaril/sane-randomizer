using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using System.Reflection;

namespace SaneRandomizer
{
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
            SaneRandomizerConfig config = SaneRandomizer.Instance.Config;
            NPCBaseModifier mod = SaneRandomizer.Instance.NPCModifierTable[npc.type];
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
                npcLoot.RemoveWhere((x) => true, false);
                foreach (IItemDropRule drop in SaneRandomizer.Instance.DropTable[npc.type])
                {
                    npcLoot.Add(drop);
                }
            }
            else
            {
                base.ModifyNPCLoot(npc, npcLoot);
            }
        }

        public override void ModifyShop(NPCShop shop)
        {
            if (!SaneRandomizer.Instance.Config.Trade)
            {
                base.ModifyShop(shop);
            }
            if (SaneRandomizer.Instance.TradeTable.ContainsKey(shop.NpcType))
            {
                List<NPCShop.Entry> entries = new List<NPCShop.Entry>();
                foreach (int item in SaneRandomizer.Instance.TradeTable[shop.NpcType])
                {
                    entries.Add(new NPCShop.Entry(item));
                }
                if(shop.NpcType == NPCID.Steampunker)
                {
                    if(WorldGen.crimson)
                    {
                        entries.Add(new NPCShop.Entry(ItemID.RedSolution));
                    } else {
                        entries.Add(new NPCShop.Entry(ItemID.PurpleSolution));
                    }
                }
                if (NPC.downedPlantBoss) {
                    if (shop.NpcType == NPCID.WitchDoctor)
                    {
                        entries.Add(new NPCShop.Entry(ItemID.PygmyNecklace));
                        entries.Add(new NPCShop.Entry(ItemID.HerculesBeetle));
                    }
                }
                FieldInfo info = shop.GetType().GetField("_entries", BindingFlags.NonPublic | BindingFlags.Instance);
                if(info is not null) {
                    info.SetValue(shop , entries);
                } else {
                    SaneRandomizer.Instance.Logger.Error("Could not use reflection to modify shops, skipping");
                }
            } else
            {
                base.ModifyShop(shop);
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
