using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using SharedLibrary.Input;
using System.Diagnostics;
using SharedLibrary.BaseGameObject;
using SharedLibrary.Ultility;
using Microsoft.Xna.Framework.Content;
using SharedLibrary.TileSet.Tiled;
using System;

namespace SharedLibrary.TileSet
{
    public class TileChangedEvent : EventArgs
    {
        public Point OldTile { get; set; }
        public Point NewTile { get; set; }

        public TileChangedEvent(Point OldTile, Point NewTile)
        {
            this.OldTile = OldTile;
            this.NewTile = NewTile;
        }
    }

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

        public event EventHandler<TileChangedEvent>? OnFocusTileChanged;

        public event EventHandler<TileChangedEvent>? OnHoverTileChanged;

        public bool EnableMapLine { get => tileMapRenderer.DrawLine; set => tileMapRenderer.DrawLine = value; }

        public bool EnableHoverBorder { get; set; } = true;

        //for gameplay
        private Point focusTileLocation = new Point(0, 0);
        private Point hoverTileLocation = new Point(-1, -1);

        public Color TileMapBorderColor { 
            get => tileMapRenderer.LineColor; 
            set => tileMapRenderer.LineColor = value;
        }

        public TileMap TileMap
        {
            get => tileMap;
        }

        public void LoadMap(TileMap tileMap)
        {
            this.tileMap = tileMap;
        }

        public TileMapLayer? GetLayer(string name)
        {
            return tileMap.GetLayerByName(name);
        }

        public Point FocusTilePosition
        {
            get => focusTileLocation;
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

        public void LoadMap(ContentManager contentManager, string tileMapPath)
        {
            TiledMapJsonData mapData = contentManager.Load<TiledMapJsonData>(tileMapPath);
            this.tileMap = new TileMap(mapData);
        }

        public void LoadMap(TiledMapJsonData data)
        {
            this.tileMap = new TileMap(data);
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

                Point newLocation = new Point(y, x);
                if (MouseHandler.Instance.HasClicked(MouseButton.LEFT_BUTTON))
                {
                    Point oldLocation = focusTileLocation;
                    focusTileLocation = newLocation;
                    OnFocusTileChanged?.Invoke(this, new TileChangedEvent(oldLocation, focusTileLocation));
                }

                Point oldHoverLocation = hoverTileLocation;
                hoverTileLocation = new Point(x, y);

                OnHoverTileChanged?.Invoke(this, new TileChangedEvent(oldHoverLocation, hoverTileLocation));
                
            } else
            {
                hoverTileLocation = new Point(-1, -1);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            tileMapRenderer.Draw(spriteBatch, tileMap, tileSet, Location, Scale);
            if (EnableHoverBorder)
            {
                if (hoverTileLocation.X >= 0 && hoverTileLocation.Y >= 0) 
                {
                    tileMapRenderer.DrawTileBorder(spriteBatch, tileMap, hoverTileLocation.X, hoverTileLocation.Y, Location, Scale);
                }
            }
        }

        //Tile operations on TileMap
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

        public void AssignValue(string layerName, int x, int y, int newValue)
        {
            tileMap.AssignValue(layerName, x, y, newValue);
        }
        /// <summary>
        ///     Get tile's actual drawing location from self.Location, row X and column Y
        /// </summary>
        /// <param name="x">row X</param>
        /// <param name="y">column Y</param>
        public Point GetTileLocation(int x, int y)
        {
            int _x = this.Location.X + (int)(this.tileSet.TileWidth * Scale + Spacing) * y;
            int _y = this.Location.Y + (int)(this.tileSet.TileHeight * Scale + Spacing) * x;

            return new Point(_x, _y);
        }

        /// <summary>
        ///     Get tile's actual drawing rectangle from self.Location, row X and column Y
        /// </summary>
        /// <param name="x">row X</param>
        /// <param name="y">column Y</param>
        public Rectangle GetTileRect(int x, int y)
        {
            Point drawLocation = GetTileLocation(x, y);

            return new Rectangle(drawLocation, tileSet.TileSize * new Vector2(Scale, Scale).ToPoint());
        }

        //Ultilities
        public void AlignCenter(Rectangle bound, bool alignHorizontal = true, bool alignVertical = true)
        {
            Point alignedPosition = Helper.AlignCenter(bound, rect.Size, alignHorizontal, alignVertical);
            if (alignHorizontal)
                rect.X = alignedPosition.X;
            if (alignVertical)
                rect.Y = alignedPosition.Y;
        }
    }
}
