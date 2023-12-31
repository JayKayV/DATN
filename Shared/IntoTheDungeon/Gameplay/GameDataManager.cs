using IntoTheDungeon.Gameplay.Units;
using Microsoft.Xna.Framework;
using Shared.Gameplay.Grounds;
using SharedLibrary.TileSet;
using System.Collections.Generic;

namespace IntoTheDungeon.Gameplay
{
    public class GameDataManager
    {
        private Dictionary<int, Unit> unitsData;
        private Dictionary<int, GroundData> groundData;
        private Dictionary<int, Item> itemsData;

        public Dictionary<int, Item> Items
        {
            get => itemsData;
        }

        public Dictionary<int, GroundData> Grounds
        {
            get => groundData;
        }

        public Dictionary<int, Unit> Units
        {
            get => unitsData;
        }

        public GameDataManager(GameDataLoader dataLoader)
        {
            unitsData = new Dictionary<int, Unit>();
            foreach (Unit unit in dataLoader.GetUnits())
            {
                unitsData[unit.TileId] = unit;
            }

            groundData = new Dictionary<int, GroundData>();
            foreach (GroundData ground in dataLoader.GetGrounds())
            {
                groundData[ground.tileId] = ground;
            }

            itemsData = new Dictionary<int, Item>();
            foreach (Item item in dataLoader.GetItems())
            {
                itemsData[item.TileId] = item;
            }
        }

        public Dictionary<Point, Unit> GetMapUnits(TileMapLayer unitLayer)
        {
            Dictionary<Point, Unit> units = new Dictionary<Point, Unit>();
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

        public Dictionary<Point, GroundData> GetGrounds(TileMapLayer groundLayer)
        {
            Dictionary<Point, GroundData> grounds = new Dictionary<Point, GroundData>();
            if (groundLayer != null)
            {
                int[][] data = groundLayer.GetData2d();

                for (int i = 0; i < data.Length; i++)
                {
                    for (int j = 0; j < data[i].Length; j++)
                    {
                        if (data[i][j] > 0 && groundData.ContainsKey(data[i][j] - 1))
                        {
                            grounds[new Point(i, j)] = groundData[data[i][j] - 1];
                        }
                    }
                }
            }
            return grounds;
        }

        public Dictionary<Point, Item> GetItems(TileMapLayer groundLayer)
        {
            Dictionary<Point, Item> items = new Dictionary<Point, Item>();
            if (groundLayer != null)
            {
                int[][] data = groundLayer.GetData2d();

                for (int i = 0; i < data.Length; i++)
                {
                    for (int j = 0; j < data[i].Length; j++)
                    {
                        if (data[i][j] > 0 && itemsData.ContainsKey(data[i][j] - 1))
                        {
                            items[new Point(i, j)] = itemsData[data[i][j] - 1];
                        }
                    }
                }
            }
            return items;
        }
    }
}
