using System;

namespace SaneRandomizer
{
    public class ItemBaseModifier
    {
        public int DamageModifier;

        public int ShootSpeedModifier;

        public int CritModifier;

        public int DefenseModifier;

        public int KnockBackModifier;

        public int BuffTimeModifier;

        public int HealValueModifier;

        public int ManaValueModifier;

        public int ManaCostModifier;

        public int ScaleModifier;

        public int UseTimeModifier;

        public int ValueModifier;

        public int BaitPowerModifier;

        public int FishingRodPowerModifier;

        public ItemBaseModifier(Random random, MinMaxTable table)
        {
            DamageModifier = GetRandomWithVariance(random, table.DamageMinimum, table.DamageMaximum, table.Variance);
            ShootSpeedModifier = GetRandomWithVariance(random, table.ShootSpeedMinimum, table.ShootSpeedMaximum, table.Variance);
            CritModifier = GetRandomWithVariance(random, table.CritChanceMinimum, table.CritChanceMaximum, table.Variance);
            DefenseModifier = GetRandomWithVariance(random, table.ArmorValuesMinimum, table.ArmorValuesMaximum, table.Variance);
            KnockBackModifier = GetRandomWithVariance(random, table.KnockbackMinimum, table.KnockbackMaximum, table.Variance);
            BuffTimeModifier = GetRandomWithVariance(random, table.PotionBuffDurationMinimum, table.PotionBuffDurationMaximum, table.Variance);
            HealValueModifier = GetRandomWithVariance(random, table.PotionHealValuesMinimum, table.PotionHealValuesMaximum, table.Variance);
            ManaValueModifier = GetRandomWithVariance(random, table.PotionManaValuesMinimum, table.PotionManaValuesMaximum, table.Variance);
            ManaCostModifier = GetRandomWithVariance(random, table.ManaCostMinimum, table.ManaCostMaximum, table.Variance);
            ScaleModifier = GetRandomWithVariance(random, table.ScaleMinimum, table.ScaleMaximum, table.Variance);
            UseTimeModifier = GetRandomWithVariance(random, table.UseTimeMinimum, table.UseTimeMaximum, table.Variance);
            ValueModifier = GetRandomWithVariance(random, table.ItemValueMinimum, table.ItemValueMaximum, table.Variance);
            BaitPowerModifier = GetRandomWithVariance(random, table.BaitPowerMinimum, table.BaitPowerMaximum, table.Variance);
            FishingRodPowerModifier = GetRandomWithVariance(random, table.FishingRodPowerMinimum, table.FishingRodPowerMaximum, table.Variance);
        }

        private static int GetRandomWithVariance(Random random, int min, int max, float variance)
        {
            float max_shift = (max - min) * (variance + 1f);
            float value_shifted = ((float)random.NextDouble()) * max_shift;
            return (int)(value_shifted / (variance + 1f)) + min;
        }
    }
}
