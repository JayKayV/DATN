using IntoTheDungeon.Gameplay.Units;
using System;

namespace IntoTheDungeon.Gameplay.Action
{
    /// <summary>
    ///     Tell game master that unit do action on desTiles
    /// </summary>
    /// <param name="unit"></param>
    /// <param name="actionInfo"></param>
    /// <param name="_desTile"></param>
    public delegate void DoAction(BaseUnit unit, ActionInfo actionInfo, params Tile[] desTiles);

    public enum ACTION_TYPE
    {
        NONE = 0, 
        ATTACK,
        BLOCK,
        DEBUFF,
        BLESS,
        REVIVE,
        SUMMON,
    }

    public static class ActionTarget
    {
        public const int NONE = 0;
        public const int UNIT = 1;
        public const int ITEM = 2;
        public const int TILE = 4;
    }

    public class GameAction
    {
        private ACTION_TYPE type;
        private string name;
        private string description;
        private DoAction action;

        /// <summary>
        ///     <para>Represent three bit mask for Tile, Item, Unit in that order</para>
        ///     <para>For example, 5 => 101, means the action targeted Tile and Unit but not Item</para>
        /// </summary>
        private int targets;

        public int Targets { get => targets; }

        public GameAction()
        {
            type = ACTION_TYPE.NONE;
            name = "DEFAULT ACTION";
            description = "DEFAULT DESCRIPTION";
            action = (u, a, d) => { };
            targets = 0;
        }

    public GameAction(ACTION_TYPE type, string name, string description, DoAction action, int targets)
    {
        this.type = type;
        this.name = name;
        this.description = description;
        this.action = action;
        this.targets = targets;
    }
}

    public interface ActionInfo
    {

    }

    public class GameActionEvent : EventArgs
    {

    }
}