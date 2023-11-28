using IntoTheDungeon.Gameplay.Effect;
using IntoTheDungeon.Gameplay.Units;
using System.Collections.Generic;

namespace IntoTheDungeon.Gameplay
{
    public class Tile
    {
        private BaseUnit? unit;
        private BaseItem? item;
        private List<StatusEffect> effects;

        private int lightLevel;

        private string textureName;

        public Tile(string textureName, int lightLevel)
        {
            this.lightLevel = lightLevel;
            this.textureName = textureName;

            effects = new List<StatusEffect>();
        }

        public BaseUnit Unit
        {
            get => unit;
            set => unit = value;
        }

        public BaseItem Item
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