using IntoTheDungeon.Gameplay.Effect;

namespace IntoTheDungeon.Gameplay.Action
{
    public static class BlockActions
    {
        public static GameAction BasicBlockAction = new GameAction(
            ACTION_TYPE.BLOCK,
            "Basic block",
            "A simple block",
            (owner, info, targets) => {
                if (owner != null && !owner.HasBlockEffect())
                {
                    owner.AddEffect(UnitEffects.CreateBlockEffect(1, 0));
                }
            },
            ActionTarget.UNIT);

        public static GameAction PrepareBlockAction = new GameAction(
            ACTION_TYPE.BLOCK,
            "Quick block",
            "Block with your hands",
            (owner, info, targets) => {
                if (owner != null)
                {
                    owner.AddEffect(UnitEffects.CreateBlockEffect(1, 1));
                }
            },
            ActionTarget.UNIT);
    }
}