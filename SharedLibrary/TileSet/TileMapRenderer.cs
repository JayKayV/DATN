using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace SharedLibrary.TileSet
{
    public class TileMapRenderer
    {
        private Texture2D lineTexture;
        private Texture2D hoverBorderTexture;

        /// <summary>
        ///     Enable DrawLine to draw lines
        /// </summary>
        private int spacing = 0;
        
        public TileMapRenderer(GraphicsDevice graphicsDevice)
        {
            lineTexture = new Texture2D(graphicsDevice, 1, 1);
            lineTexture.SetData<Color>(new Color[] { Color.White });

            hoverBorderTexture = new Texture2D(graphicsDevice, 1, 1);
            hoverBorderTexture.SetData<Color>(new Color[] { Color.Aqua });
        }
        /// <summary>
        ///     Draw line between tiles, remember to let spacing greater than 0
        /// </summary>
        public bool DrawLine { get; set; }
        public int Spacing { get => spacing; set => spacing = value; }
        public Color LineColor
        {
            get
            {
                Color[] color = new Color[1];
                lineTexture.GetData<Color>(color);
                return color[0];
            }
            set
            {
                lineTexture.SetData<Color>(new Color[] { value });
            }
        }

        /// <summary>
        ///     Draw tileMap with the tileSet provided, at location x, y
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="tileMap"></param>
        /// <param name="tileSet"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Draw(SpriteBatch spriteBatch, TileMap tileMap, TileSet tileSet, int x, int y, float scale = 1f)
        {
            //Draw all the tiles in tile map
            List<TileMapLayer> layers = tileMap.GetLayers();
            foreach (TileMapLayer layer in layers)
            {
                for (int i = 0; i < layer.Size.Y; ++i)
                    for (int j = 0; j < layer.Size.X; ++j)
                    {
                        TileRenderer.Draw(spriteBatch, 
                            tileSet, 
                            layer.GetId(j, i), 
                            new Vector2(x + j * (scale * tileMap.TileSize.X + spacing), y + i * (scale * tileMap.TileSize.Y + spacing)), 
                            scale, 
                            layer.Depth);
                    }
            }
            //Then draw the lines (if DrawLine sets to true)
            if (DrawLine && spacing > 0)
            {
                Point actualMapSize = tileMap.GetActualSize(spacing, scale);

                //Draw horizontal lines
                for (int i = 0; i < tileMap.MapSize.Y - 1; ++i)
                    spriteBatch.Draw(lineTexture, 
                        new Rectangle(x - spacing, y + (int)((i+1) * tileMap.TileSize.Y * scale) + i * spacing, actualMapSize.X, spacing),
                        Color.White);
                //Draw vertical lines
                for (int i = 0; i < tileMap.MapSize.X - 1; ++i)
                    spriteBatch.Draw(lineTexture,
                        new Rectangle(x + (int)((i + 1) * tileMap.TileSize.X * scale) + i * spacing, y, spacing, actualMapSize.Y),
                        Color.White);
            }
        }

        public void Draw(SpriteBatch spriteBatch, TileMap tileMap, TileSet tileSet, Point location, float scale = 1f)
        {
            Draw(spriteBatch, tileMap, tileSet, location.X, location.Y, scale);
        }

        /// <summary>
        ///     Draw the border arount the tile at location (x, y) in tilemap, Draw order: top->right->bottom->left
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="tileMap"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="scale"></param>
        public void DrawTileBorder(SpriteBatch spriteBatch, TileMap tileMap, int x, int y, Point offsetLocation, float scale = 1f)
        {
            if (x < 0 || y < 0 || x >= tileMap.MapSize.X || y >= tileMap.MapSize.Y)
                return;
            if (spacing > 0)
            {
                Point tileLocation = new Point(
                    x * (int)(tileMap.TileSize.X * scale + spacing) + offsetLocation.X, 
                    y * (int)(tileMap.TileSize.Y * scale + spacing) + offsetLocation.Y
                );

                Point p1 = new Point(tileLocation.X - spacing, tileLocation.Y - spacing);
                Point p2 = new Point(tileLocation.X + (int)(tileMap.TileSize.X * scale), tileLocation.Y);
                Point p3 = new Point(tileLocation.X - spacing, (int)(tileLocation.Y + tileMap.TileSize.Y * scale));
                Point p4 = new Point(tileLocation.X - spacing, tileLocation.Y);

                Point borderWidthSize = new Point((int)(tileMap.TileSize.X * scale) + spacing * 2, spacing);
                Point borderHeightSize = new Point(spacing, (int)(tileMap.TileSize.Y * scale));

                spriteBatch.Draw(hoverBorderTexture, new Rectangle(p1, borderWidthSize), Color.White);
                spriteBatch.Draw(hoverBorderTexture, new Rectangle(p2, borderHeightSize), Color.White);
                spriteBatch.Draw(hoverBorderTexture, new Rectangle(p3, borderWidthSize), Color.White);
                spriteBatch.Draw(hoverBorderTexture, new Rectangle(p4, borderHeightSize), Color.White);
            }
        }
    }
}
