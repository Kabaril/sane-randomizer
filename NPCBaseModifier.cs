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
            DamageModifier = GetRandomWithVariance(random, table.NPCDamageMinimum, table.NPCDamageMaximum, table.Variance);
            MaxLifeModifier = GetRandomWithVariance(random, table.NPCLifeMinimum, table.NPCLifeMaximum, table.Variance);
            DefenseModifier = GetRandomWithVariance(random, table.NPCArmorMinimum, table.NPCArmorMaximum, table.Variance);
        }

        private static int GetRandomWithVariance(Random random, int min, int max, float variance)
        {
            float max_shift = (max - min) * (variance + 1f);
            float value_shifted = ((float)random.NextDouble()) * max_shift;
            return (int)(value_shifted / (variance + 1f)) + min;
        }
    }
}
