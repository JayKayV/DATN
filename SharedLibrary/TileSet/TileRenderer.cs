using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SharedLibrary.TileSet
{
    public static class TileRenderer
    {
        public static void Draw(SpriteBatch spriteBatch, TileSet tileSet, int id, Vector2 location, float scale = 1f, float layerDepth = 0)
        {
            if (id < 0 || id > tileSet.TileCount)
                throw new ArgumentException(string.Format("Invalid id: {0}", id));
            if (id > 0)
            {
                int r = (id - 1) / tileSet.ColumnCount, c = (id - 1) % tileSet.ColumnCount;
                int tileSetLocationX = tileSet.Margin + (tileSet.Spacing + tileSet.TileWidth) * c;
                int tileSetLocationY = tileSet.Margin + (tileSet.Spacing + tileSet.TileHeight) * r;

                Rectangle srcRect = new Rectangle(tileSetLocationX, tileSetLocationY, tileSet.TileWidth, tileSet.TileHeight);
                spriteBatch.Draw(tileSet.Image, location, srcRect, Color.White, 0f, new Vector2(0, 0), scale, SpriteEffects.None, layerDepth);
            }
        }
    }
}
