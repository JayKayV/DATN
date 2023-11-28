using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace IntoTheDungeon.Gameplay.Units
{
    public delegate List<Point> DoScan(BaseUnit unit, int[][] groundData, int[][] unitsData, GameDataManager gameData);

    public delegate Point DoRunaway(BaseUnit unit, int[][] groundData, int[][] unitsData, GameDataManager gameData);

    public delegate List<Point> DoChase(BaseUnit unit, int[][] groundData, int[][] unitsData, GameDataManager gameData);

    public static class UnitFunctionality
    {
        /// <summary>
        ///     Scan in n x n matrix, regardless of obstacles
        /// </summary>
        public static DoScan SimpleScan = (BaseUnit unit, int[][] groundData, int[][] unitsData, GameDataManager gameData) =>
        {
            List<Point> result = new List<Point>();
            for (int i = 0; i < unitsData.Length; i++)
            {
                for (int j = 0; j < unitsData[i].Length; j++) {
                    //Always exclude center point
                    if (i == j && i == unit.DetectRange)
                        continue;
                    int tileId = unitsData[i][j];
                    if (tileId > 0 && gameData.Units.ContainsKey(tileId))
                    {
                        result.Add(new Point(i, j));
                    }
                }
            }
            return result;
        };

        /// <summary>
        ///     Scan in account of obstacles
        /// </summary>
        public static DoScan EyeScan = (BaseUnit unit, int[][] groundData, int[][] unitsData, GameDataManager gameData) =>
        {
            List<Point> result = new List<Point>();
            for (int i = 0; i < unitsData.Length; i++)
            {
                for (int j = 0; j < unitsData[i].Length; j++)
                {
                    //Always exclude center point
                    if (i == j && i == unit.DetectRange)
                        continue;
                    int tileId = unitsData[i][j];

                    if (tileId > 0 && gameData.Units.ContainsKey(tileId))
                    {
                        bool seeable = true;

                        int horizontalGroundId = 0, verticalGroundId = 0; 
                        //Above
                        if (i < unit.DetectRange)
                        {
                            verticalGroundId = unitsData[i + 1][j];
                            
                        } 
                        //Below
                        else if (i > unit.DetectRange)
                        {
                            verticalGroundId = unitsData[i + 1][j];
                        }

                        //Left
                        if (j < unit.DetectRange)
                        {
                            horizontalGroundId = unitsData[i][j + 1];
                        }
                        //Right
                        else if (j > unit.DetectRange)
                        {
                            horizontalGroundId = unitsData[i][j - 1];
                        }

                        seeable = gameData.Grounds[horizontalGroundId].seethrough && gameData.Grounds[verticalGroundId].seethrough;
                        if (seeable)
                            result.Add(new Point(i, j));
                    }
                }
            }
            return result;
        };

    }
}
