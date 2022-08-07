using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace SaneRandomizer
{
    public class SaneRandomizer : Mod
    {
        private IRandomizer randomizer;

        public SaneRandomizerConfig Config;

        //NPC Type -> drop rule array
        public Dictionary<int, IItemDropRule[]> DropTable;

        //NPC Type -> item types
        public Dictionary<int, int[]> TradeTable;

        public Dictionary<int, ItemBaseModifier> ItemModifierTable;

        public Dictionary<int, NPCBaseModifier> NPCModifierTable;

        // Automatically set by tModLoader
        public static SaneRandomizer Instance;

        public override void Load()
        {
            Logger.Info("Initializing Sane Randomizer");
            Config = SaneRandomizerConfig.Instance;
            if (Config.Seed == 0)
            {
                Config.Seed = new Random().Next(1, int.MaxValue);
                Helpers.Save(Config);
            }
            Logger.Info($"Loading Sane Randomizer with Seed {Config.Seed}");
            if(Config.LTS22)
            {
                Logger.Info("Loading Sane Randomizer with LTS22");
                randomizer = new Randomizer22(Logger, Config.Seed);
            } else
            {
                randomizer = new RandomizerDev(Logger, Config.Seed);
            }
        }

        public override void Unload()
        {
            Logger.Info("Unloading Sane Randomizer");
            SaneRandomizerConfig.Instance = null;
            Config = null;
            TradeTable = null;
            ItemModifierTable = null;
            Instance = null;
            NPCModifierTable = null;
            randomizer = null;
        }

        public override void PostSetupContent()
        {
            MinMaxTable minMaxTable = new MinMaxTable(Config);

            //start randomizing
            Logger.Info("Creating Drop Tables");
            DropTable = randomizer.RandomizeDrops();
            Logger.Info("Creating Trade Tables");
            TradeTable = randomizer.RandomizeTrades();
            Logger.Info("Creating Item Modification Table");
            ItemModifierTable = randomizer.RandomizeItemValues(minMaxTable);
            Logger.Info("Creating NPC Modification Table");
            //MUST BE AFTER DROPS
            NPCModifierTable = randomizer.RandomizeNPCValues(minMaxTable);

            Instance = this;
        }
    }
}