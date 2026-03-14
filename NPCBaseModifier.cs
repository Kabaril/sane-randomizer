using System;

namespace SaneRandomizer;

public class NPCBaseModifier
{
    public int MaxLifeModifier;

    public int DamageModifier;

    public int DefenseModifier;

    public NPCBaseModifier(Random random, MinMaxTable table)
    {
        DamageModifier = GetRandomWithVariance(random, table.NPCDamageMinimum, table.NPCDamageMaximum, table.Variance);
        MaxLifeModifier = GetRandomWithVariance(random, table.NPCLifeMinimum, table.NPCLifeMaximum, table.Variance);
        DefenseModifier = GetRandomWithVariance(random, table.NPCArmorMinimum, table.NPCArmorMaximum, table.Variance);
    }

    private static int GetRandomWithVariance(Random random, int min, int max, float variance)
    {
        var maxShift = (max - min) * (variance + 1f);
        var valueShifted = ((float)random.NextDouble()) * maxShift;
        return (int)(valueShifted / (variance + 1f)) + min;
    }
}