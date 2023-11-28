namespace IntoTheDungeon.Gameplay.Effect
{
    public static class TileEffects
    {
        public static StatusEffect CreateBearTrapEffect()
        {
            return new StatusEffect(
                EffectType.OTHER,
                "Bear Trap",
                "Beware the bear trap, will immobile an unit that come accross",
                9999,
                0);
        }
    }
}