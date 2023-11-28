using Microsoft.Xna.Framework;

namespace SharedLibrary.Ultility
{
    public static class Helper
    {
        /// <summary>
        ///     Try to align the object size within bound
        /// </summary>
        /// <param name="bound"></param>
        /// <param name="size"></param>
        /// <param name="alignHorizontal"></param>
        /// <param name="alignVertical"></param>
        /// <returns>Return the position of aligned object within bound </returns>
        public static Point AlignCenter(Rectangle bound, Point size, bool alignHorizontal = true, bool alignVertical = true)
        {
            Point result = new Point(-1, -1);
            if (alignHorizontal)
                result.X = bound.X + (bound.Width - size.X) / 2;
            if (alignVertical) 
                result.Y = bound.Y + (bound.Height - size.Y) / 2;
            return result;
        }

        public static void Fill2DArray<T>(ref T[,] data, T value)
        {
            for (int i = 0; i < data.GetLength(0); i++)
            {
                for (int j = 0; j < data.GetLength(1); j++)
                    data[i, j] = value;
            }
        }
    }
}
