namespace IntoTheDungeon.Gameplay.Effect
{
    public static class ItemEffects
    {
        public static StatusEffect CreateExtraMovementEffect()
        {
            return new StatusEffect(
                EffectType.BUFF,
                "Move blessed",
                "This unit has been blessed with extra movement range!",
                1,
                0);
        }
    }
}