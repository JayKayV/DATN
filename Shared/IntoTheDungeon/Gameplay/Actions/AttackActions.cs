using IntoTheDungeon.Gameplay.Units;
using System;

namespace IntoTheDungeon.Gameplay.Action
{
    public class AttackInfo : ActionInfo
    {
        public int AttackModifier { get; set; } = 0;
        public bool DestroyItem { get; set; } = false;
        public bool IsEvilAttack { get; set; } = false;
        public int HealthCost { get; set; } = 0;

        //Status Effect Info
        public bool InflictPoisonEffect { get; set; } = false;
        public int PoisonDuration { get; set; } = 0;
        public int PoisonDelay { get; set; } = 0;

        public AttackInfo() { }

        public static AttackInfo DefaultAttackInfo()
        {
            return new AttackInfo();
        }
    }
    public static class AttackActions
    {
        public static GameAction BasicAttackAction = new GameAction(
            ACTION_TYPE.ATTACK, 
            "Basic attack", 
            "A simple basic attack", 
            (owner, info, targets) => {
                DoAttack(owner, AttackInfo.DefaultAttackInfo(), targets);
            }, 
            ActionTarget.UNIT);

        public static GameAction BrutalAttackAction = new GameAction(
            ACTION_TYPE.ATTACK,
            "Brutal attack",
            "A brutal attack that will cost owner\'s health",
            (owner, info, targets) => {
                AttackInfo attackInfo = new AttackInfo();
                attackInfo.HealthCost = 2;
                DoAttack(owner, attackInfo, targets);
            },
            ActionTarget.UNIT);

        public static GameAction EvilAttackAction = new GameAction(
            ACTION_TYPE.ATTACK,
            "Evil attack",
            "An attack that equals to a percentage of target\'s health",
            (owner, info, targets) => {
                AttackInfo attackInfo = new AttackInfo();
                attackInfo.IsEvilAttack = true;
                DoAttack(owner, attackInfo, targets);
            }, ActionTarget.UNIT);

        public static GameAction PoisonAttack = new GameAction(
            ACTION_TYPE.ATTACK,
            "Poison attack",
            "An attack that will leave it\'s target poisoned for a few turn",
            (owner, info, targets) => {
                AttackInfo attackInfo = new AttackInfo();
                attackInfo.InflictPoisonEffect = true;
                attackInfo.PoisonDuration = 1;
                attackInfo.PoisonDelay = 0;
                attackInfo.AttackModifier = -1;
                DoAttack(owner, attackInfo, targets);
            },
            ActionTarget.UNIT);

        public static GameAction BombAttackAction = new GameAction(
            ACTION_TYPE.ATTACK,
            "Bomb attack",
            "An attack that will also destroy items on the ground",
            (owner, info, targets) => {
                AttackInfo attackInfo = new AttackInfo();
                attackInfo.DestroyItem = true;
                DoAttack(owner, attackInfo, targets);
            },
            ActionTarget.UNIT | ActionTarget.ITEM);

        private static void DoAttack(BaseUnit owner, AttackInfo info, Tile[] targets)
        {
            int totalAttack = owner.Attack + info.AttackModifier;
            if (owner != null)
                owner.Health -= info.HealthCost;
            foreach (Tile tile in targets)
            {
                if (tile.HasUnit())
                {
                    int totalArmor = tile.Unit.Armor;
                    if (tile.Unit.HasBlockEffect())
                        totalArmor += tile.Unit.Block;
                    if (totalAttack > totalArmor)
                        tile.Unit.Health -= (owner.Attack - totalArmor);
                }
                if (info.DestroyItem && tile.Item != null)
                {
                    tile.Item = null;
                }
            }
        }
    }
}