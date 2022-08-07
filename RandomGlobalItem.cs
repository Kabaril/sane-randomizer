using Terraria;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace SaneRandomizer
{
    public class RandomGlobalItem : GlobalItem
    {
        private static readonly List<int> use_time_excluded = new List<int>
        {
            ItemID.MagicMirror,
            ItemID.BugNet,
            ItemID.FireproofBugNet,
            ItemID.GoldenBugNet,
            ItemID.RecallPotion,
            ItemID.CellPhone,
            ItemID.TeleportationPotion,
            ItemID.IceMirror,
            ItemID.RodofDiscord
        };

        public override void SetDefaults(Item item)
        {
            base.SetDefaults(item);
            if(SaneRandomizer.Instance is null)
            {
                return;
            }
            if(!SaneRandomizer.Instance.ItemModifierTable.ContainsKey(item.type))
            {
                return;
            }
            SaneRandomizerConfig config = SaneRandomizer.Instance.Config;
            ItemBaseModifier mod = SaneRandomizer.Instance.ItemModifierTable[item.type];
            if (item.damage != -1) {
                if (config.Damage)
                {
                    int damage_mod = mod.DamageModifier;
                    item.damage = (int)(item.damage * (damage_mod / 100f));
                }
            }
            if (config.ShootSpeed)
            {
                item.shootSpeed *= (mod.ShootSpeedModifier / 100f);
            }
            if (config.CritChance)
            {
                item.crit = (int)(item.crit * (mod.CritModifier / 100f));
            }
            if (config.ArmorValues)
            {
                item.defense = (int)(item.defense * (mod.DefenseModifier / 100f));
            }
            if (config.Knockback)
            {
                item.knockBack *= (mod.KnockBackModifier / 100f);
            }
            if (config.UseTime && !use_time_excluded.Contains(item.type))
            {
                item.useAnimation = (int)(item.useAnimation * (mod.UseTimeModifier / 100f));
                item.useTime = (int)(item.useTime * (mod.UseTimeModifier / 100f));
            }
            if (config.ItemValue)
            {
                item.value = (int)(item.value * (mod.ValueModifier / 100f));
            }
            if (config.ManaCost)
            {
                item.mana = (int)(item.mana * (mod.ManaCostModifier / 100f));
            }
            if (config.PotionBuffDuration)
            {
                item.buffTime = (int)(item.buffTime * (mod.BuffTimeModifier / 100f));
            }
            if (config.PotionHealValues)
            {
                item.healLife = (int)(item.healLife * (mod.HealValueModifier / 100f));
            }
            if (config.PotionManaValues)
            {
                item.healMana = (int)(item.healMana * (mod.ManaValueModifier / 100f));
            }
            if (config.Scale)
            {
                item.scale *= (mod.ScaleModifier / 100f);
            }
            if (item.bait != 0)
            {
                if (config.BaitPower)
                {
                    item.bait = (int)(item.bait * (mod.BaitPowerModifier / 100f));
                }
            }
            if (item.fishingPole != 0)
            {
                if (config.FishingRodPower)
                {
                    item.fishingPole = (int)(item.fishingPole * (mod.FishingRodPowerModifier / 100f));
                }
            }
        }
    }
}
