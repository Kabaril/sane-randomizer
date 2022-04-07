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

        [Label("Randomizer Seed")]
        [Tooltip("Seed used for randomizing everything")]
        [ReloadRequired]
        [DefaultValue(0)]
        public int Seed;

        [Header("Randomizer Settings")]

        [Label("Trade")]
        [Tooltip("Randomize Items Sold by Vanilla Merchants with a few Exceptions")]
        [DefaultValue(false)]
        public bool Trade;

        [Label("Drops")]
        [Tooltip("Randomize Dropped Items by Vanilla Enemies, except Bosses")]
        [ReloadRequired]
        [DefaultValue(false)]
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
    }
}
