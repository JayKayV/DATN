using IntoTheDungeon.Gameplay.Units;
using Microsoft.Xna.Framework;
using Shared.Gameplay.Grounds;
using SharedLibrary.TileSet;
using System.Collections.Generic;

namespace IntoTheDungeon.Gameplay
{
    public class GameDataManager
    {
        private Dictionary<int, BaseUnit> unitsData;
        private Dictionary<int, GroundData> groundData;
        private Dictionary<int, BaseItem> itemsData;

        public Dictionary<int, BaseItem> Items
        {
            get => itemsData;
        }

        public Dictionary<int, GroundData> Grounds
        {
            get => groundData;
        }

        public Dictionary<int, BaseUnit> Units
        {
            get => unitsData;
        }

        public GameDataManager(GameDataLoader dataLoader)
        {
            unitsData = new Dictionary<int, BaseUnit>();
            foreach (BaseUnit unit in dataLoader.GetUnits())
            {
                unitsData[unit.TileId] = unit;
            }
        }

        public Dictionary<Point, BaseUnit> GetMapUnits(TileMapLayer unitLayer)
        {
            Dictionary<Point, BaseUnit> units = new Dictionary<Point, BaseUnit>();
            if (unitLayer != null)
            {
                int[][] data = unitLayer.GetData2d();

                for (int i = 0; i < data.Length; i++)
                {
                    for (int j = 0; j < data[i].Length; j++)
                    {
                        if (data[i][j] > 0 && unitsData.ContainsKey(data[i][j] - 1))
                        {
                            units[new Point(i, j)] = unitsData[data[i][j] - 1].Clone();
                        }
                    }
                }
            }
            return units;
        }
    }
}
