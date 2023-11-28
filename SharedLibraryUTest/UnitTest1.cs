using SharedLibrary.Ultility;
using Microsoft.Xna.Framework;

namespace SharedLibraryUTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestFindPath()
        {
            int[][] table =
            {
                new int[] { 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 99, 0, 99 },
                new int[] { 0, 0, 99, 99, 0 },
                new int[] { 0, 0, 0, 0, 0 },
            };

            List<Point> result = Pathfinder.FindPath(table, new Point(1, 3), new Point(3, 4));

            int totalCost = result.Count;
            Assert.AreEqual(9, totalCost); 
        }
    }
}