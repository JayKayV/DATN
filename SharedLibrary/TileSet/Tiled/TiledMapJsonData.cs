using System.Collections.Generic;

namespace SharedLibrary.TileSet.Tiled
{
    public class TiledSetJson
    {
        public int firstgid;
        public string src;
    }

    public class TiledMapJsonData
    {
        public string backgroundcolor;
        public int compressionLevel;
        public int height;
        public int width;
        public bool infinite;
        public List<TiledMapLayer> layers;
        public int nextlayerorderid;
        public int nextojbectid;
        public string orientation;
        public string renderorder;
        public string tiledversion;
        public List<TiledSetJson> tilesets;
        public int tileheight;
        public int tilewidth;
        public string version;
        public string type;
    }
}
