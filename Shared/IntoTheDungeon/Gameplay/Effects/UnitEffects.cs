namespace IntoTheDungeon.Gameplay.Effect
{
    public static class UnitEffects
    {
        public static StatusEffect CreatePoisonEffect(int duration, int delay)
        {
            return new StatusEffect(
                EffectType.DEBUFF,
                "Poisoned",
                "This unit has been poisoned",
                duration, 
                delay);
        }

        public static StatusEffect CreateBlockEffect(int duration, int delay)
        {
            return new StatusEffect(
                EffectType.BLOCK,
                "Blocked",
                "This unit is going to block incoming damage",
                duration,
                delay);
        }

        public static StatusEffect CreateQuickBlockEffect()
        {
            return new StatusEffect(
                EffectType.BLOCK,
                "Quick block",
                "This unit quickly blocks incoming damage",
                0,
                0);
        }

        public static StatusEffect CreateWetEffect()
        {
            return new StatusEffect(
                EffectType.OTHER,
                "Wet",
                "The water will douse fire away from this unit",
                1,
                0);
        }
    }
}