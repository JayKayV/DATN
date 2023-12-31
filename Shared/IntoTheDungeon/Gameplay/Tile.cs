using IntoTheDungeon.Gameplay.Effect;
using IntoTheDungeon.Gameplay.Units;
using System.Collections.Generic;

namespace IntoTheDungeon.Gameplay
{
    public class Tile
    {
        private Unit? unit;
        private Item? item;
        private List<StatusEffect> effects;

        private int lightLevel;

        private string textureName;

        public Tile(string textureName, int lightLevel)
        {
            this.lightLevel = lightLevel;
            this.textureName = textureName;

            effects = new List<StatusEffect>();
        }

        public Unit Unit
        {
            get => unit;
            set => unit = value;
        }

        public Item Item
        {
            get => item;
            set => item = value;
        }

        public bool HasUnit()
        {
            return unit != null;
        }
    }
}