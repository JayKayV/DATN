using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
namespace SharedLibrary.TileSet
{
    public class TileSet
    {
        private Texture2D tilesetImage;
        private int margin = 0;
        private int spacing = 0;

        private int tileWidth = 0;
        private int tileHeight = 0;

        private int imageWidth = 0;
        private int imageHeight = 0;

        private Color transparentColor;
        public TileSet() { }

        /// <summary>
        ///     Create tileset data
        /// </summary>
        /// <param name="tilesetImage"></param>
        /// <param name="margin">The margin around the tiles in this tileset (applies to the tileset image, defaults to 0)</param>
        /// <param name="spacing">The spacing in pixels between the tiles in this tileset, default is 0</param>
        /// <param name="tileWidth"></param>
        /// <param name="tileHeight"></param>
        /// <param name="transparentColor">Transparent color of tiles (not image behind it) </param>
        public TileSet(Texture2D tilesetImage, int margin, int spacing, int tileWidth, int tileHeight, Color transparentColor)
        {
            this.tilesetImage = tilesetImage;
            this.margin = margin;
            this.spacing = spacing;
            this.tileWidth = tileWidth; 
            this.tileHeight = tileHeight;

            imageWidth = tilesetImage.Width;
            imageHeight = tilesetImage.Height;

            this.transparentColor = transparentColor;
        }

        public void LoadTileSet(ContentManager contentManager, string src) { 
            tilesetImage = contentManager.Load<Texture2D>(src);

            imageWidth = tilesetImage.Width;
            imageHeight = tilesetImage.Height;
        }

        public int Margin { get => margin; set => margin = value; }
        public int Spacing { get => spacing; set => spacing = value; }
        public int TileWidth { get => tileWidth; set => tileWidth = value; }
        public int TileHeight { get => tileHeight; set => tileHeight = value; } 
        public Color TransparentColor { get => transparentColor; set => transparentColor = value; }
        public Point TileSize
        {
            get => new Point(tileWidth, tileHeight);
            set {
                tileWidth = value.X;
                tileHeight = value.Y;
            }
        }

        public int ColumnCount { get => (imageWidth - margin * 2 + spacing) / (spacing + tileWidth); }
        public int RowCount { get => (imageHeight - margin * 2 + spacing) / (spacing + tileHeight); }

        public int TileCount { get => RowCount * ColumnCount; }

        public Texture2D Image
        {
            get => tilesetImage;
        }

        public override string ToString()
        {
            return string.Format("{{ImageSize:{0},{1} | TileSize:{2},{3} | Margin: {4} | Spacing: {5}}}", 
                imageWidth, imageHeight, tileWidth, tileHeight, margin, spacing);
        }
    }
}
