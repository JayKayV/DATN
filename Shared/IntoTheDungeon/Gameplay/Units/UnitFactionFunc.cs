using System.Collections.Generic;

namespace IntoTheDungeon.Gameplay.Units
{
    public enum UnitFaction
    {
        Human = 0, Undead, Boss, Animal, Neutral
    }

    /// <summary>
    ///     Detailing how factions fight against each other, 
    ///     <para>The higher the score, the more friendlier faction that towards</para>
    ///     <para>Will mostly stay in 0-5 range </para>
    /// </summary>
    public static class UnitFactionFunc
    {
        public static readonly Dictionary<UnitFaction, int> UndeadFaction = new Dictionary<UnitFaction, int> {
            { UnitFaction.Human, 0 },
            { UnitFaction.Undead, 5 },
            { UnitFaction.Boss, 5 },
            { UnitFaction.Animal, 5 },
            { UnitFaction.Neutral, 3 },
        };

        public static readonly Dictionary<UnitFaction, int> HumanFaction = new Dictionary<UnitFaction, int> {
            { UnitFaction.Human, 5 },
            { UnitFaction.Undead, 0 },
            { UnitFaction.Boss, 0 },
            { UnitFaction.Animal, 3 },
            { UnitFaction.Neutral, 3 },
        };

        public static readonly Dictionary<UnitFaction, int> BossFaction = new Dictionary<UnitFaction, int> {
            { UnitFaction.Human, 0 },
            { UnitFaction.Undead, 5 },
            { UnitFaction.Boss, 5 },
            { UnitFaction.Animal, 5 },
            { UnitFaction.Neutral, 5 },
        };

        public static readonly Dictionary<UnitFaction, int> AnimalFaction = new Dictionary<UnitFaction, int> {
            { UnitFaction.Human, 2 },
            { UnitFaction.Undead, 0 },
            { UnitFaction.Boss, 0 },
            { UnitFaction.Animal, 5 },
            { UnitFaction.Neutral, 3 },
        };

        public static readonly Dictionary<UnitFaction, int> NeutralFaction = new Dictionary<UnitFaction, int> {
            { UnitFaction.Human, 5 },
            { UnitFaction.Undead, 5 },
            { UnitFaction.Boss, 5 },
            { UnitFaction.Animal, 5 },
            { UnitFaction.Neutral, 5 },
        };
    }
}
