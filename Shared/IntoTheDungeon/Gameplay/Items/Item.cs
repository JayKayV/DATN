using Gameplay.Items;
using IntoTheDungeon.Gameplay.Units;

namespace IntoTheDungeon.Gameplay
{
    public class Item
    {
        private string name;
        private int tileId;
        private string affect;
        private int value;

        public string Name { get => name; }
        public int TileId { get => tileId; }
        public int Value { get => tileId; set => this.value = value; }

        public Item(ItemData data) { 
            name = data.Name;
            tileId = data.TileId;
            value = data.Value;
            affect = data.Affect;
        }  

        public void AffectUnit(Unit unit)
        {
            switch (affect)
            {
                case "health":
                    unit.Health += value;
                    break;
                case "armor":
                    unit.Armor += value;
                    break;
                case "attack":
                    unit.Attack += value;
                    break;
                case "moveRange":
                    unit.MoveRange += value;
                    break;
            }
        }
    }
}