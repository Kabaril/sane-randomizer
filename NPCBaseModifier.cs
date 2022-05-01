using System;

namespace SaneRandomizer
{
    public class NPCBaseModifier
    {
        public int MaxLifeModifier;

        public int DamageModifier;

        public int DefenseModifier;

        public NPCBaseModifier(Random random, MinMaxTable table)
        {
            DamageModifier = random.Next(table.NPCDamageMinimum, table.NPCDamageMaximum);
            MaxLifeModifier = random.Next(table.NPCLifeMinimum, table.NPCLifeMaximum);
            DefenseModifier = random.Next(table.NPCArmorMinimum, table.NPCArmorMaximum);
        }
    }
}
