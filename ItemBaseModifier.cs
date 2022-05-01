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
            DamageModifier = random.Next(table.DamageMinimum, table.DamageMaximum);
            ShootSpeedModifier = random.Next(table.ShootSpeedMinimum, table.ShootSpeedMaximum);
            CritModifier = random.Next(table.CritChanceMinimum, table.CritChanceMaximum);
            DefenseModifier = random.Next(table.ArmorValuesMinimum, table.ArmorValuesMaximum);
            KnockBackModifier = random.Next(table.KnockbackMinimum, table.KnockbackMaximum);
            BuffTimeModifier = random.Next(table.PotionBuffDurationMinimum, table.PotionBuffDurationMaximum);
            HealValueModifier = random.Next(table.PotionHealValuesMinimum, table.PotionHealValuesMaximum);
            ManaValueModifier = random.Next(table.PotionManaValuesMinimum, table.PotionManaValuesMaximum);
            ManaCostModifier = random.Next(table.ManaCostMinimum, table.ManaCostMaximum);
            ScaleModifier = random.Next(table.ScaleMinimum, table.ScaleMaximum);
            UseTimeModifier = random.Next(table.UseTimeMinimum, table.UseTimeMaximum);
            ValueModifier = random.Next(table.ItemValueMinimum, table.ItemValueMaximum);
            BaitPowerModifier = random.Next(table.BaitPowerMinimum, table.BaitPowerMaximum);
            FishingRodPowerModifier = random.Next(table.FishingRodPowerMinimum, table.FishingRodPowerMaximum);
        }
    }
}
