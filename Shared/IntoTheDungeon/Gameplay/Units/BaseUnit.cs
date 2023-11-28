using IntoTheDungeon.Gameplay.Effect;
using System.Collections.Generic; 

namespace IntoTheDungeon.Gameplay.Units
{
    public enum UnitEffectType
    {
        None = 0, ON_BLOCK, POISONED, BLESSED
    }
    public class UnitEffect
    {
        UnitEffectType type;
        int time;
    }

    public class BaseUnit
    {
        protected string name;

        protected int attackRange = 0;
        protected int moveRange = 0;
        protected int eyeRange = 0;
        protected int health = 0;
        protected int armor = 0;
        protected int attack = 0;
        protected int block = 0;
        protected bool isLiving = false;

        protected int tileId = 1;
        protected int deadTileId = 1;
        
        protected bool isAlive = true;

        private UnitFaction faction = UnitFaction.Neutral;
        private List<BaseItem> items = new List<BaseItem>();
        private List<StatusEffect> effects = new List<StatusEffect>();

        public int AttackRange { get => attackRange; set => attackRange = value; }
        public int MoveRange { get => moveRange; set => moveRange = value; }
        public int DetectRange { get => eyeRange; set => eyeRange = value; }
        public int Health { get => health; set => health = value; }
        public int Armor { get => armor; set => armor = value; }
        public int Attack { get => attack; set => attack = value; }
        public int Block { get => block; set => block = value; }
        public bool IsLiving { get => isLiving; set => isLiving = value; }

        public int TileId { get => tileId; }
        public int DeadTileId { get => deadTileId; }

        public BaseUnit()
        {
            
        }
        public BaseUnit(UnitData unitData)
        {
            name = unitData.Name;
            attackRange = unitData.AttackRange;
            moveRange = unitData.MoveRange;
            eyeRange = unitData.EyeRange;
            health = unitData.Health;
            armor = unitData.Armor;
            attack = unitData.Attack;
            block = unitData.Block;
            isLiving = unitData.IsLiving;

            tileId = unitData.TileId;
            deadTileId = unitData.DeadTileId;

            switch (unitData.Faction)
            {
                case "Human":
                    faction = UnitFaction.Human;
                    FactionFunc = UnitFactionFunc.HumanFaction;
                    break;
                case "Undead":
                    faction = UnitFaction.Undead;
                    FactionFunc = UnitFactionFunc.UndeadFaction;
                    break;
                case "Boss":
                    faction = UnitFaction.Boss;
                    FactionFunc = UnitFactionFunc.BossFaction;
                    break;
                case "Animal":
                    faction = UnitFaction.Animal;
                    FactionFunc = UnitFactionFunc.AnimalFaction;
                    break;
                default:
                    FactionFunc = UnitFactionFunc.NeutralFaction;
                    break;
            }
        }

        public BaseUnit(UnitData unitData, UnitFaction faction)
        {
            name = unitData.Name;
            attackRange = unitData.AttackRange;
            moveRange = unitData.MoveRange;
            eyeRange = unitData.EyeRange;
            health = unitData.Health;
            armor = unitData.Armor;
            attack = unitData.Attack;
            block = unitData.Block;
            isLiving = unitData.IsLiving;

            tileId = unitData.TileId;
            deadTileId = unitData.DeadTileId;

            this.faction = faction;
            switch (faction)
            {
                case UnitFaction.Human:
                    FactionFunc = UnitFactionFunc.HumanFaction; 
                    break;
                case UnitFaction.Undead:
                    FactionFunc = UnitFactionFunc.UndeadFaction; 
                    break;
                case UnitFaction.Animal:
                    FactionFunc = UnitFactionFunc.AnimalFaction;
                    break;
                case UnitFaction.Boss:
                    FactionFunc = UnitFactionFunc.BossFaction;
                    break;
                default:
                    FactionFunc = UnitFactionFunc.NeutralFaction;
                    break;
            }
        }

        public void AddEffect(StatusEffect effect)
        {
            effects.Add(effect);
        }

        public void RemoveEffect(StatusEffect effect)
        {
            if (!effects.Remove(effect))
                throw new System.ArgumentException($"Error searching and removing effect: {effect.Name}");
        }

        public bool HasEffect(StatusEffect effect)
        {
            return effects.Contains(effect);
        }

        public bool HasBlockEffect()
        {
            StatusEffect effect = effects.Find(e => e.Type == EffectType.BLOCK);
            return effect != null;
        }

        public Dictionary<UnitFaction, int> FactionFunc = UnitFactionFunc.NeutralFaction;

        public BaseUnit Clone()
        {
            return this.MemberwiseClone() as BaseUnit;
        }
    }
}