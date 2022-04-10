using System;

namespace SaneRandomizer
{
    public class NPCBaseModifier
    {
        public int MaxLifeModifier;

        public int DamageModifier;

        public int DefenseModifier;

        public NPCBaseModifier(Random random)
        {
            DamageModifier = random.Next(50, 200);
            MaxLifeModifier = random.Next(50, 200);
            DefenseModifier = random.Next(50, 200);
        }
    }
}
