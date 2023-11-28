using Microsoft.Xna.Framework;
using System;

namespace SharedLibrary.Ultility
{
    public static class PointHelper
    {
        public static float Distance(Point p1, Point p2)
        {
            int m1 = p1.X - p2.X, m2 = p1.Y - p2.Y;
            return Convert.ToSingle(Math.Sqrt(m1 * m1 + m2 * m2));
        }
    }
}
