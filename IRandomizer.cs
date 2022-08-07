using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;

namespace SaneRandomizer
{
    public interface IRandomizer
    {
        public Dictionary<int, int[]> RandomizeTrades();

        public Dictionary<int, IItemDropRule[]> RandomizeDrops();

        public Dictionary<int, ItemBaseModifier> RandomizeItemValues(MinMaxTable minMaxTable);

        public Dictionary<int, NPCBaseModifier> RandomizeNPCValues(MinMaxTable minMaxTable);
    }
}
