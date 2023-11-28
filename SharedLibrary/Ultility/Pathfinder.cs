using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace SharedLibrary.Ultility
{
    public static class Pathfinder
    {
        /// <summary>
        ///     <para> This algorithm uses A*, and assumes that character can only go horizontal and vertical</para>
        ///     <para> The algorithm will return path with min cost </para>
        /// </summary>
        /// <param name="table"> Represents cost table, the higher the harder it is to go through </param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>List of Point from start to end </returns>
        public static List<Point> FindPath(in int[][] table, Point start, Point end)
        {
            int width = table[0].Length, height = table.Length;
            float[,] heuristicTable = CreateHeuristicTable(table, end);
            
            bool[][] visited = new bool[table.Length][];
            for (int i = 0; i < visited.Length; ++i) {
                visited[i] = new bool[table[0].Length];
                Array.Fill(visited[i], false);
            }

            List<Point> result = new List<Point>();

            PriorityQueue<Node<Point>, float> queue = new PriorityQueue<Node<Point>, float>();

            Node<Point> startNode = new Node<Point>(start, 0, (float)heuristicTable[start.X, start.Y]);
            queue.Enqueue(startNode, 0);

            while (queue.Count > 0)
            {
                //Note: x means row and y means column
                Node<Point> node = queue.Dequeue();
                int x = node.value.X, y = node.value.Y;
                visited[x][y] = true;

                if (x == end.X && y == end.Y)
                {
                    while (node.parent != null)
                    {
                        result.Add(node.value);
                        node = node.parent;
                    }
                    break;
                }

                if (x > 0 && !visited[x - 1][y])
                    queue.Enqueue(new Node<Point>(
                            node, new Point(x - 1, y), table[x - 1][y], heuristicTable[x - 1, y]
                        ), table[x - 1][y] + heuristicTable[x - 1, y]);
                if (y > 0 && !visited[x][y - 1])
                    queue.Enqueue(new Node<Point>(
                            node, new Point(x, y - 1), table[x][y - 1], heuristicTable[x, y - 1]
                        ), table[x][y - 1] + heuristicTable[x, y - 1]);
                if (x < height - 1 && !visited[x + 1][y])
                    queue.Enqueue(new Node<Point>(
                            node, new Point(x + 1, y), table[x + 1][y], heuristicTable[x + 1, y]
                        ), table[x + 1][y] + heuristicTable[x + 1, y]);
                if (y < width - 1 && !visited[x][y + 1])
                    queue.Enqueue(new Node<Point>(
                            node, new Point(x, y + 1), table[x][y + 1], heuristicTable[x, y + 1]
                        ), table[x][y + 1] + heuristicTable[x, y + 1]);
            }
            return result;
        }

        private static float[,] CreateHeuristicTable(in int[][] table, Point end)
        {
            if (table == null || table.Length == 0)
                throw new ArgumentNullException("Table can\'t be null or blank");

            int width = table[0].Length, height = table.Length;
            float[,] costTable = new float[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    costTable[i, j] = PointHelper.Distance(new Point(i, j), end);
                }
            }
            return costTable;
        }
    }

    internal class Node<T> : IComparable<Node<T>>
    {
        public Node<T>? parent;
        public T value;

        public float moveCost = 0;
        public float heuristic;

        public Node (T value, float moveCost, float heuristic) {
            parent = null; 
            this.value = value;
            this.moveCost = moveCost;
            this.heuristic = heuristic;
        }

        public Node(Node<T> parent, T value, float moveCost, float heuristic)
        {
            this.parent = parent;
            this.value = value;
            this.moveCost = moveCost;
            this.heuristic = heuristic;
        }

        public int CompareTo(Node<T>? other)
        {
            int this_h = (int) (moveCost + heuristic);
            int other_h = (int) (other.moveCost + other.heuristic);

            return this_h - other_h;
        }
    }

    internal class NodeComp<T> : IComparer<Node<T>>
    {
        public int Compare(Node<T>? x, Node<T>? y)
        {
            return x.CompareTo(y);
        }
    }
}
