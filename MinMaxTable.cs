using System;

namespace SaneRandomizer
{
    public class MinMaxTable
    {
        public int DamageMinimum;
        public int DamageMaximum;
        public int ShootSpeedMinimum;
        public int ShootSpeedMaximum;
        public int KnockbackMinimum;
        public int KnockbackMaximum;
        public int CritChanceMinimum;
        public int CritChanceMaximum;
        public int ScaleMinimum;
        public int ScaleMaximum;
        public int ManaCostMinimum;
        public int ManaCostMaximum;
        public int UseTimeMinimum;
        public int UseTimeMaximum;
        public int BaitPowerMinimum;
        public int BaitPowerMaximum;
        public int FishingRodPowerMinimum;
        public int FishingRodPowerMaximum;
        public int ItemValueMinimum;
        public int ItemValueMaximum;
        public int PotionBuffDurationMinimum;
        public int PotionBuffDurationMaximum;
        public int PotionHealValuesMinimum;
        public int PotionHealValuesMaximum;
        public int PotionManaValuesMinimum;
        public int PotionManaValuesMaximum;
        public int ArmorValuesMinimum;
        public int ArmorValuesMaximum;
        public int NPCLifeMinimum;
        public int NPCLifeMaximum;
        public int NPCDamageMinimum;
        public int NPCDamageMaximum;
        public int NPCArmorMinimum;
        public int NPCArmorMaximum;
        public float Variance;

        public MinMaxTable(SaneRandomizerConfig config)
        {
            if(config.LTS22)
            {
                Variance = 1f;
            } else
            {
                Variance = config.FavorCentricPercentage / 200f;
            }
            DamageMinimum = Math.Min(config.DamageMinimum, config.DamageMaximum);
            DamageMaximum = Math.Max(config.DamageMaximum, config.DamageMinimum);
            ShootSpeedMaximum = Math.Max(config.ShootSpeedMaximum, config.ShootSpeedMinimum);
            ShootSpeedMinimum = Math.Min(config.ShootSpeedMinimum, config.ShootSpeedMaximum);
            KnockbackMaximum = Math.Max(config.KnockbackMaximum, config.KnockbackMinimum);
            KnockbackMinimum = Math.Min(config.KnockbackMinimum, config.KnockbackMaximum);
            CritChanceMaximum = Math.Max(config.CritChanceMaximum, config.CritChanceMinimum);
            CritChanceMinimum = Math.Min(config.CritChanceMinimum, config.CritChanceMaximum);
            ScaleMaximum = Math.Max(config.ScaleMaximum, config.ScaleMinimum);
            ScaleMinimum = Math.Min(config.ScaleMinimum, config.ScaleMaximum);
            ManaCostMaximum = Math.Max(config.ManaCostMaximum, config.ManaCostMinimum);
            ManaCostMinimum = Math.Min(config.ManaCostMinimum, config.ManaCostMaximum);
            UseTimeMaximum = Math.Max(config.UseTimeMaximum, config.UseTimeMinimum);
            UseTimeMinimum = Math.Min(config.UseTimeMinimum, config.UseTimeMaximum);
            BaitPowerMaximum = Math.Max(config.BaitPowerMaximum, config.BaitPowerMinimum);
            BaitPowerMinimum = Math.Min(config.BaitPowerMinimum, config.BaitPowerMaximum);
            FishingRodPowerMaximum = Math.Max(config.FishingRodPowerMaximum, config.FishingRodPowerMinimum);
            FishingRodPowerMinimum = Math.Min(config.FishingRodPowerMaximum, config.FishingRodPowerMinimum);
            ItemValueMaximum = Math.Max(config.ItemValueMaximum, config.ItemValueMinimum);
            ItemValueMinimum = Math.Min(config.ItemValueMinimum, config.ItemValueMaximum);
            PotionBuffDurationMaximum = Math.Max(config.PotionBuffDurationMaximum, config.PotionBuffDurationMinimum);
            PotionBuffDurationMinimum = Math.Min(config.PotionBuffDurationMinimum, config.PotionBuffDurationMaximum);
            PotionHealValuesMaximum = Math.Max(config.PotionHealValuesMaximum, config.PotionHealValuesMinimum);
            PotionHealValuesMinimum = Math.Min(config.PotionHealValuesMinimum, config.PotionHealValuesMaximum);
            PotionManaValuesMaximum = Math.Max(config.PotionManaValuesMaximum, config.PotionManaValuesMinimum);
            PotionManaValuesMinimum = Math.Min(config.PotionManaValuesMinimum, config.PotionManaValuesMaximum);
            ArmorValuesMinimum = Math.Min(config.ArmorValuesMinimum, config.ArmorValuesMaximum);
            ArmorValuesMaximum = Math.Max(config.ArmorValuesMaximum, config.ArmorValuesMinimum);
            NPCLifeMaximum = Math.Max(config.NPCLifeMaximum, config.NPCLifeMinimum);
            NPCLifeMinimum = Math.Min(config.NPCLifeMinimum, config.NPCLifeMaximum);
            NPCDamageMinimum = Math.Min(config.NPCDamageMinimum, config.NPCDamageMaximum);
            NPCDamageMaximum = Math.Max(config.NPCDamageMaximum, config.NPCDamageMinimum);
            NPCArmorMaximum = Math.Max(config.NPCArmorMaximum, config.NPCArmorMinimum);
            NPCArmorMinimum = Math.Min(config.NPCArmorMinimum, config.NPCArmorMaximum);
        }
    }
}
