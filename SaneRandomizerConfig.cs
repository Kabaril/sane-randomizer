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

        [Label("Long Term Randomization 2022")]
        [Tooltip("Randomize Item Values to not change across versions\nNote: Values can still change with Seed and external Mod/tModLoader updates")]
        [ReloadRequired]
        [DefaultValue(false)]
        public bool LTS22;

        [Label("Randomizer Seed")]
        [Tooltip("Seed used for randomizing everything")]
        [ReloadRequired]
        [Range(int.MinValue, int.MaxValue)]
        [DefaultValue(0)]
        public int Seed;

        [Label("Favor Center Random Values")]
        [Tooltip("Percentage by which default values are favored.\n-100 -> values are likely far from normal\n100 -> values are likely close to normal\nDoes nothing in LTS22")]
        [ReloadRequired]
        [Range(-100, 100)]
        [DefaultValue(0)]
        public int FavorCentricPercentage;

        [Header("Randomizer Settings")]

        [Label("Trade [Deprecated]")]
        [Tooltip("Randomize Items Sold by Vanilla Merchants with a few Exceptions\nWill be removed or overhauled in future version")]
        [DefaultValue(false)]
        public bool Trade;

        [Label("Drops")]
        [Tooltip("Randomize Dropped Loot from enemies, except Bosses")]
        [ReloadRequired]
        [DefaultValue(true)]
        public bool Drops;

        [Label("Damage")]
        [Tooltip("Randomize Damage of all Items")]
        [DefaultValue(true)]
        public bool Damage;

        [Label("Shoot Speed")]
        [Tooltip("Randomize Shoot Speed of all Items")]
        [DefaultValue(true)]
        public bool ShootSpeed;

        [Label("Knockback")]
        [Tooltip("Randomize Knockback of all Items")]
        [DefaultValue(true)]
        public bool Knockback;

        [Label("Crit Chance")]
        [Tooltip("Randomize Crit Chance of all Items")]
        [DefaultValue(true)]
        public bool CritChance;

        [Label("Scale")]
        [Tooltip("Randomize Scale of all Items")]
        [DefaultValue(true)]
        public bool Scale;

        [Label("Mana Cost")]
        [Tooltip("Randomize Mana Cost of all Items")]
        [DefaultValue(true)]
        public bool ManaCost;

        [Label("Use Time")]
        [Tooltip("Randomize Use Time of all Items")]
        [DefaultValue(true)]
        public bool UseTime;

        [Label("Bait Power")]
        [Tooltip("Randomize Bait Power of all Items")]
        [DefaultValue(true)]
        public bool BaitPower;

        [Label("Fishing Rod Power")]
        [Tooltip("Randomize Fishing Rod Power of all Items")]
        [DefaultValue(true)]
        public bool FishingRodPower;

        [Label("Item Value")]
        [Tooltip("Randomize Item Value (coin cost and reforge cost) of all Items")]
        [DefaultValue(true)]
        public bool ItemValue;

        [Label("Potion Buff Duration")]
        [Tooltip("Randomize Potion Buff Duration")]
        [DefaultValue(true)]
        public bool PotionBuffDuration;

        [Label("Potion Heal Values")]
        [Tooltip("Randomize Potion Heal Values")]
        [DefaultValue(true)]
        public bool PotionHealValues;

        [Label("Potion Mana Values")]
        [Tooltip("Randomize Potion Mana Values")]
        [DefaultValue(true)]
        public bool PotionManaValues;

        [Label("Armor Values")]
        [Tooltip("Randomize Armor Values of all Items")]
        [DefaultValue(true)]
        public bool ArmorValues;

        [Label("NPC Life")]
        [Tooltip("Randomize Maximum Life of NPCs (excluding Bosses)")]
        [DefaultValue(true)]
        public bool NPCLife;

        [Label("NPC Damage")]
        [Tooltip("Randomize Damage of NPCs (excluding Bosses)")]
        [DefaultValue(true)]
        public bool NPCDamage;

        [Label("NPC Armor")]
        [Tooltip("Randomize Armor of NPCs (excluding Bosses)")]
        [DefaultValue(true)]
        public bool NPCArmor;

        [Header("Limits")]

        [Label("Damage Minimum Percentage")]
        [Tooltip("Minimum Percentage for Damage of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int DamageMinimum;

        [Label("Damage Maximum Percentage")]
        [Tooltip("Maximum Percentage for Damage of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int DamageMaximum;

        [Label("Shoot Speed Minimum Percentage")]
        [Tooltip("Minimum Percentage for Shoot Speed of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int ShootSpeedMinimum;

        [Label("Shoot Speed Maximum Percentage")]
        [Tooltip("Maximum Percentage for Shoot Speed of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int ShootSpeedMaximum;

        [Label("Knockback Minimum Percentage")]
        [Tooltip("Minimum Percentage for Knockback of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int KnockbackMinimum;

        [Label("Knockback Maximum Percentage")]
        [Tooltip("Maximum Percentage for Knockback of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int KnockbackMaximum;

        [Label("Crit Chance Minimum Percentage")]
        [Tooltip("Minimum Percentage for Crit Chance of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int CritChanceMinimum;

        [Label("Crit Chance Maximum Percentage")]
        [Tooltip("Maximum Percentage for Crit Chance of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int CritChanceMaximum;

        [Label("Scale Minimum Percentage")]
        [Tooltip("Minimum Percentage for Scale of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int ScaleMinimum;

        [Label("Scale Maximum Percentage")]
        [Tooltip("Maximum Percentage for Scale of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int ScaleMaximum;

        [Label("Mana Cost Minimum Percentage")]
        [Tooltip("Minimum Percentage for Mana Cost of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int ManaCostMinimum;

        [Label("Mana Cost Maximum Percentage")]
        [Tooltip("Maximum Percentage for Mana Cost of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int ManaCostMaximum;

        [Label("Use Time Minimum Percentage")]
        [Tooltip("Minimum Percentage for Use Time of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int UseTimeMinimum;

        [Label("Use Time Maximum Percentage")]
        [Tooltip("Maximum Percentage for Use Time of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int UseTimeMaximum;

        [Label("Bait Power Minimum Percentage")]
        [Tooltip("Minimum Percentage for Bait Power of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int BaitPowerMinimum;

        [Label("Bait Power Maximum Percentage")]
        [Tooltip("Maximum Percentage for Bait Power of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int BaitPowerMaximum;

        [Label("Fishing Rod Power Minimum Percentage")]
        [Tooltip("Minimum Percentage for Fishing Rod Power of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int FishingRodPowerMinimum;

        [Label("Fishing Rod Power Maximum Percentage")]
        [Tooltip("Maximum Percentage for Fishing Rod Power of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int FishingRodPowerMaximum;

        [Label("Item Value Minimum Percentage")]
        [Tooltip("Minimum Percentage for Item Value of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int ItemValueMinimum;

        [Label("Item Value Maximum Percentage")]
        [Tooltip("Maximum Percentage for Item Value of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int ItemValueMaximum;

        [Label("Potion Buff Duration Minimum Percentage")]
        [Tooltip("Minimum Percentage for Potion Buff Duration of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int PotionBuffDurationMinimum;

        [Label("Potion Buff Duration Maximum Percentage")]
        [Tooltip("Maximum Percentage for Potion Buff Duration of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int PotionBuffDurationMaximum;

        [Label("Potion Heal Values Minimum Percentage")]
        [Tooltip("Minimum Percentage for Potion Heal Values of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int PotionHealValuesMinimum;

        [Label("Potion Heal Values Maximum Percentage")]
        [Tooltip("Maximum Percentage for Potion Heal Values of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int PotionHealValuesMaximum;

        [Label("Potion Mana Values Minimum Percentage")]
        [Tooltip("Minimum Percentage for Potion Mana Values of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int PotionManaValuesMinimum;

        [Label("Potion Mana Values Maximum Percentage")]
        [Tooltip("Maximum Percentage for Potion Mana Values of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int PotionManaValuesMaximum;

        [Label("Armor Values Minimum Percentage")]
        [Tooltip("Minimum Percentage for Armor Values of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int ArmorValuesMinimum;

        [Label("Armor Values Maximum Percentage")]
        [Tooltip("Maximum Percentage for Armor Values of all Items")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int ArmorValuesMaximum;

        [Label("NPC Life Minimum Percentage")]
        [Tooltip("Minimum Percentage for NPC Life")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int NPCLifeMinimum;

        [Label("NPC Life Maximum Percentage")]
        [Tooltip("Maximum Percentage for NPC Life")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int NPCLifeMaximum;

        [Label("NPC Damage Minimum Percentage")]
        [Tooltip("Minimum Percentage for NPC Damage")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int NPCDamageMinimum;

        [Label("NPC Damage Maximum Percentage")]
        [Tooltip("Maximum Percentage for NPC Damage")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int NPCDamageMaximum;

        [Label("NPC Armor Minimum Percentage")]
        [Tooltip("Minimum Percentage for NPC Armor")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(50)]
        public int NPCArmorMinimum;

        [Label("NPC Armor Maximum Percentage")]
        [Tooltip("Maximum Percentage for NPC Armor")]
        [ReloadRequired]
        [Range(1, int.MaxValue)]
        [DefaultValue(200)]
        public int NPCArmorMaximum;
    }
}
