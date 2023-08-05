using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace SaneRandomizer
{
    public class SaneRandomizerConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        // Automatically set by tModLoader
        public static SaneRandomizerConfig Instance;

        [Header("General")]

        [ReloadRequired]
        [DefaultValue(false)]
        public bool LTS22;

        [ReloadRequired]
        [Range(int.MinValue, int.MaxValue)]
        [DefaultValue(0)]
        public int Seed;

        [ReloadRequired]
        [Range(-100, 100)]
        [DefaultValue(0)]
        public int FavorCentricPercentage;

        [Header("RandomizerSettings")]
        [DefaultValue(false)]
        public bool Trade;

        [ReloadRequired]
        [DefaultValue(true)]
        public bool Drops;

        [DefaultValue(true)]
        public bool Damage;

        [DefaultValue(true)]
        public bool ShootSpeed;

        [DefaultValue(true)]
        public bool Knockback;

        [DefaultValue(true)]
        public bool CritChance;

        [DefaultValue(true)]
        public bool Scale;

        [DefaultValue(true)]
        public bool ManaCost;

        [DefaultValue(true)]
        public bool UseTime;

        [DefaultValue(true)]
        public bool BaitPower;

        [DefaultValue(true)]
        public bool FishingRodPower;

        [DefaultValue(true)]
        public bool ItemValue;

        [DefaultValue(true)]
        public bool PotionBuffDuration;

        [DefaultValue(true)]
        public bool PotionHealValues;

        [DefaultValue(true)]
        public bool PotionManaValues;

        [DefaultValue(true)]
        public bool ArmorValues;

        [DefaultValue(true)]
        public bool NPCLife;

        [DefaultValue(true)]
        public bool NPCDamage;

        [DefaultValue(true)]
        public bool NPCArmor;

        [Header("Limits")]

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int DamageMinimum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int DamageMaximum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int ShootSpeedMinimum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int ShootSpeedMaximum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int KnockbackMinimum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int KnockbackMaximum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int CritChanceMinimum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int CritChanceMaximum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int ScaleMinimum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int ScaleMaximum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int ManaCostMinimum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int ManaCostMaximum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int UseTimeMinimum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int UseTimeMaximum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int BaitPowerMinimum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int BaitPowerMaximum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int FishingRodPowerMinimum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int FishingRodPowerMaximum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int ItemValueMinimum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int ItemValueMaximum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int PotionBuffDurationMinimum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int PotionBuffDurationMaximum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int PotionHealValuesMinimum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int PotionHealValuesMaximum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int PotionManaValuesMinimum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int PotionManaValuesMaximum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int ArmorValuesMinimum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int ArmorValuesMaximum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int NPCLifeMinimum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int NPCLifeMaximum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int NPCDamageMinimum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int NPCDamageMaximum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int NPCArmorMinimum;

        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int NPCArmorMaximum;
    }
}
