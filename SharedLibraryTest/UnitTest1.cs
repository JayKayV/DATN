using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using SharedLibrary;

namespace SharedLibraryTest
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
                new int[] { 0, 0, 1, 0, 1 },
                new int[] { 0, 0, 1, 1, 0 },
                new int[] { 0, 0, 0, 0, 0 },
            };

            List<Point> result = PathFinder.FindPath(table);
        }
    }
}
