using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SharedLibrary.Input;
using System.Diagnostics;
using SharedLibrary.BaseGameObject;
using SharedLibrary.Ultility;
using MonoGame.Extended.Tiled;
using Microsoft.Xna.Framework.Content;
using SharedLibrary.TileSet.Tiled;
using System;

namespace SharedLibrary.TileSet
{
    public class TileMapManager : GameObject
    {
        private TileMap tileMap;
        private TileSet tileSet;
        private TileMapRenderer tileMapRenderer;

        private Rectangle rect;
        public Point Location { get => rect.Location; set => rect.Location = value; }
        public float Scale { get; set; } = 1f;
        public int Spacing { 
            get => tileMapRenderer.Spacing;
            set
            {
                tileMapRenderer.Spacing = value;
                rect.Size = tileMap.GetActualSize(value, Scale);
            }
        }

        public bool EnableMapLine { get => tileMapRenderer.DrawLine; set => tileMapRenderer.DrawLine = value; }

        public bool EnableHoverBorder { get; set; } = true;

        //for gameplay
        private Point focusTileLocation = new Point(-1, -1);
        public Color TileMapBorderColor { 
            get => tileMapRenderer.LineColor; 
            set => tileMapRenderer.LineColor = value;
        }

        public TileMapManager(GraphicsDevice graphicsDevice, TileMap tileMap, TileSet tileSet, Point location)
        {
            this.tileMap = tileMap;
            this.tileSet = tileSet;
            tileMapRenderer = new TileMapRenderer(graphicsDevice);
            Location = location;

            rect = new Rectangle(location, tileMap.GetActualSize());
        }

        public TileMapManager(GraphicsDevice graphicsDevice, TileMap tileMap, TileSet tileSet, Point location, int spacing, float scale)
        {
            this.tileMap = tileMap;
            this.tileSet = tileSet;
            tileMapRenderer = new TileMapRenderer(graphicsDevice);
            Location = location;

            Spacing = spacing;
            Scale = scale;
            rect = new Rectangle(location, tileMap.GetActualSize(Spacing, Scale));
        }

        public TileMap TileMap { 
            get => tileMap;
        }

        public void LoadMap(TileMap tileMap)
        {
            this.tileMap = tileMap;
        }

        public void LoadMap(ContentManager contentManager, string tileMapPath)
        {
            TiledMapJsonData mapData = contentManager.Load<TiledMapJsonData>(tileMapPath);
            this.tileMap = new TileMap(mapData);
        }

        public override void Update(GameTime gameTime)
        {
            //Hover update
            Point mouseLocation = MouseHandler.GetPosition();
            if (rect.Contains(mouseLocation))
            {
                Point tileRectSize = new Point(
                        (int)(tileMap.TileSize.X * Scale + Spacing),
                        (int)(tileMap.TileSize.Y * Scale + Spacing)
                    );
                int x = (mouseLocation.X - Location.X) / tileRectSize.X;
                int y = (mouseLocation.Y - Location.Y) / tileRectSize.Y;

                focusTileLocation = new Point(x, y);
            } else
            {
                focusTileLocation = new Point(-1, -1);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            tileMapRenderer.Draw(spriteBatch, tileMap, tileSet, Location, Scale);
            if (EnableHoverBorder)
            {
                if (focusTileLocation.X >= 0 && focusTileLocation.Y >= 0) 
                {
                    tileMapRenderer.DrawTileBorder(spriteBatch, tileMap, focusTileLocation.X, focusTileLocation.Y, Location, Scale);
                }
            }
        }

        public void AlignCenter(Rectangle bound, bool alignHorizontal = true, bool alignVertical = true)
        {
            Point alignedPosition = Helper.AlignCenter(bound, rect.Size, alignHorizontal, alignVertical);
            if (alignHorizontal)
                rect.X = alignedPosition.X;
            if (alignVertical)
                rect.Y = alignedPosition.Y;
        }

        public TileMapLayer? GetLayer(string name)
        {
            return tileMap.GetLayerByName(name);
        }

        public Point FocusTilePosition
        {
            get => focusTileLocation;
        }

        public void SwapInLayer(string layerName, int x1, int y1, int x2, int y2)
        {
            TileMapLayer? layer = tileMap.GetLayerByName(layerName);
            if (layer == null)
                throw new ArgumentException("Layer name doesn\'t exists in tile map");
            tileMap.SwapValue(layerName, x1, y1, x2, y2);   
        }

        public void SwapTile(int x1, int y1, int x2, int y2)
        {
            Rectangle testRect = new Rectangle(new Point(0, 0), tileMap.MapSize);
            if (!testRect.Contains(x1, y1) || !testRect.Contains(x2, y2))
                throw new ArgumentException("Either coordinates must be within bounds!");

            tileMap.SwapTile(x1, y1, x2, y2);
        }
    }
}
