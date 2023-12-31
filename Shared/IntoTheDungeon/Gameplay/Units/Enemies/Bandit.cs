using IntoTheDungeon.Gameplay.Units;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SharedLibrary.TileSet;
using SharedLibrary.Ultility;

namespace Shared.Gameplay.Units.Enemies
{
    public class Bandit : Unit
    {
        public Bandit(UnitData data): base(data, UnitFaction.Neutral) { }

        public Point Move()
        {
            return new Point(0, 0);
        }

        public void DoAction()
        {
        }

        public void Process(TileMap map, Point position)
        {
            TileMapLayer groundLayer = map.GetLayerByName("Grounds");
            TileMapLayer unitsLayer = map.GetLayerByName("Units");

            Point threatLocation = ScanThreat(groundLayer, unitsLayer, position, map.MapSize);
            if (threatLocation.X >= 0 && threatLocation.Y >= 0)
            {
                Point nextLocation = ChaseThreat(groundLayer, unitsLayer, position, threatLocation, map.MapSize);
            }
        }

        public Point ScanThreat(TileMapLayer groundLayer, TileMapLayer unitLayer, Point location, Point mapSize)
        {
            int[][] groundData = groundLayer.GetData(location.X - eyeRange, location.Y - eyeRange, eyeRange, eyeRange);
            int[][] unitsData = unitLayer.GetData(location.X - eyeRange, location.Y - eyeRange, eyeRange, eyeRange);

            bool[,] seeable = new bool[eyeRange, eyeRange];
            Helper.Fill2DArray(ref seeable, false);

            List<Point> threatFounds = new List<Point>();

            //Checking vertical and horizontal eye sight
            /*for (int i = -eyeRange; i <= eyeRange; i++)
            {
                if ()
            }

            //Checking diagonal sight..
            for (int r = 1; r <= this.DetectRange; r++)
            {
                for (int i = -r; i <= r; i++) {
                    for (int j = -r; j <= r; j++)
                    {
                        if (i < 0)
                        {
                            if (groundLayer[i+1] > 0)
                        } 
                        else if (i > 0)
                        {

                        }
                    }
                }
            }*/

            return new Point(0, 0);
        }

        /// <summary>
        ///     Determine next point to move closer to the target
        /// </summary>
        /// <param name="groundLayer"></param>
        /// <param name="unitLayer"></param>
        /// <param name="location"></param>
        /// <param name="mapSize"></param>
        /// <returns></returns>
        public Point ChaseThreat(TileMapLayer groundLayer, TileMapLayer unitLayer, Point location, Point target, Point mapSize)
        {
            return new Point(0, 0);
        }
    }
}
