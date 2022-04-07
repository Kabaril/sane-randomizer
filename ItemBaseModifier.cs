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

        public ItemBaseModifier(Random random)
        {
            DamageModifier = random.Next(50, 200);
            ShootSpeedModifier = random.Next(50, 200);
            CritModifier = random.Next(50, 200);
            DefenseModifier = random.Next(50, 200);
            KnockBackModifier = random.Next(50, 200);
            BuffTimeModifier = random.Next(50, 200);
            HealValueModifier = random.Next(50, 200);
            ManaValueModifier = random.Next(50, 200);
            ManaCostModifier = random.Next(50, 200);
            ScaleModifier = random.Next(50, 200);
            UseTimeModifier = random.Next(50, 200);
            ValueModifier = random.Next(50, 200);
            BaitPowerModifier = random.Next(50, 200);
        }
    }
}
