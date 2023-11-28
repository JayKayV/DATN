using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using SharedLibrary.TileSet.Tiled;
using System;

namespace SharedLibrary.TileSet
{
    public class TileMapLayer
    {
        public string Name { get; set; }
        public float Depth { get; set; } = 0;
        public Point Size { get; set; }
        public bool Visible { get; set; }

        private int[] data;
        public TileMapLayer() { }
        public TileMapLayer(TiledMapLayer tiledLayer) { 
            data = tiledLayer.data;
            Size = new Point(tiledLayer.width, tiledLayer.height);  
            Name = tiledLayer.name;
            Depth = tiledLayer.GetProperty<float>("Order");
        }

        public int GetId(int x, int y)
        {
            return data[y * Size.X + x];
        }

        public int[] GetData()
        {
            return data;
        }

        public int[][] GetData2d()
        {
            return GetData(Size.X, Size.Y);
        }

        /// <summary>
        ///     Get a submap of size (w, h) at origin (0,0)
        /// </summary>
        /// <param name="w">Width of submap</param>
        /// <param name="h">Height of submap</param>
        /// <returns></returns>
        public int[][] GetData(int w, int h)
        {
            if (w > Size.X || h > Size.Y)
                throw new ArgumentOutOfRangeException("Submap out of bound!");

            int[][] result = new int[h][];
            for (int i = 0; i < h; i++)
            {
                result[i] = new ArraySegment<int>(data, i * Size.X, w).ToArray();
            }
            return result;
        }

        /// <summary>
        ///     Get a submap of size (w, h) at origin (x, y)
        /// </summary>
        /// <param name="x">Origin.X of submap</param>
        /// <param name="y">Origin.Y of submap</param>
        /// <param name="w">Width of submap</param>
        /// <param name="h">Height of submap</param>
        /// <returns></returns>
        public int[][] GetData(int x, int y, int w, int h)
        {
            if (x + w > Size.X || y + h > Size.Y)
                throw new ArgumentOutOfRangeException("Submap out of bound!");

            int[][] result = new int[h][];
            for (int i = 0; i < h; i++)
            {
                result[i] = new ArraySegment<int>(data, (x + i) * Size.X + y, w).Array;
            }
            return result;
        }

        public void SwapValue(int x1, int y1, int x2, int y2)
        {
            Rectangle testRect = new Rectangle(0, 0, Size.X, Size.Y);
            if (!testRect.Contains(x1, y1) || !testRect.Contains(x2, y2))
                throw new ArgumentException("Either coordinates must be within bounds!");
            
            //location i1, i2 in data
            int i1 = x1 * Size.X + y1, i2 = x2 * Size.X + y2;
            int v = data[i1];
            data[i1] = data[i2];
            data[i2] = v;
        }

        public void AssignValue(int x, int y, int newValue)
        {
            Rectangle testRect = new Rectangle(0, 0, Size.X, Size.Y);
            if (!testRect.Contains(x, y))
                throw new ArgumentException("Either coordinates must be within bounds!");

            //location  in data
            int i = x * Size.X + y;
            data[i] = newValue;
        }
    }

    public class TileMap
    {
        private Point mapSize;
        private Point tileSize;

        private List<TileMapLayer> layers;
        public TileMap() { }

        public TileMap(TiledMapJsonData jsonData) {
            LoadTiledData(jsonData);
        }

        public void LoadTiledData(TiledMapJsonData jsonData)
        {
            this.mapSize = new Point(jsonData.width, jsonData.height);
            this.tileSize = new Point(jsonData.tilewidth, jsonData.tileheight);

            layers = new List<TileMapLayer>();
            foreach (TiledMapLayer layer in jsonData.layers)
            {
                layers.Add(new TileMapLayer(layer));
            }
        }
        public List<TileMapLayer> GetLayers()
        {
            return layers;
        }

        public Point MapSize { get => mapSize; }
        public Point TileSize { get => tileSize; }

        public Point GetActualSize(int spacing = 0, float scale = 1f)
        {
            return new Point(
                (int)((tileSize.X * mapSize.X * scale + spacing * (mapSize.X - 1))),
                (int)((tileSize.Y * mapSize.Y * scale + spacing * (mapSize.Y - 1)))
           );
        }

        public override string ToString()
        {
            return string.Format("{{MapSize: {0}, TileSize: {1}}}", MapSize, TileSize);
        }

        /// <summary>
        ///     If there doesn't exist layerName in map's layers, return -1, otherwise return the id
        /// </summary>
        /// <param name="layerName"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int PeekId(string layerName, int x, int y)
        {
            TileMapLayer? layer = layers.Find(o => o.Name == layerName);
            if (layer == null)
                return -1;
            return layer.GetId(x, y);
        }

        /// <summary>
        ///     <para>Note: lower layerDepth is drawed on top of higher ones</para>
        ///     <para>Another note: layerDepth must be between 0 and 1f, if some are >= 1f, they are all draw on the same depth (1f)</para>
        /// </summary>
        /// <param name="layerName"></param>
        /// <param name="layerDepth"></param>
        public void SetLayerDepth(string layerName, float layerDepth)
        {
            TileMapLayer? layer = layers.Find(o => o.Name == layerName);
            if (layer == null)
            {
                Debug.WriteLine($"[ERROR]: layer {layerName} doesn\'t exist in map");
            } else
            {
                layer.Depth = layerDepth;
            }
        }

        public TileMapLayer? GetLayerByName(string layerName)
        {
            return layers.Find(o => o.Name == layerName);
        }

        public void SwapValue(ref TileMapLayer layer, int x1, int y1, int x2, int y2)
        {
            layer.SwapValue(x1, y1, x2, y2);
        }

        public void SwapValue(string layerName, int x1, int y1, int x2, int y2)
        {
            layers.Find(v => v.Name == layerName)?.SwapValue(x1, y1, x2, y2);
        }

        public void AssignValue(ref TileMapLayer layer, int x, int y, int newValue) 
        {
            layer.AssignValue(x, y, newValue);
        }

        public void AssignValue(string layerName, int x, int y, int newValue)
        {
            layers.Find(v => v.Name == layerName)?.AssignValue(x, y, newValue);
        }

        public void SwapTile(int x1, int y1, int x2, int y2)
        {
            foreach (TileMapLayer layer in layers)
            {

            }
        }

        public void AssignTile(int x, int y, int value)
        {

        }
    }
}
