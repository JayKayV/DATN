using IntoTheDungeon.Gameplay.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Shared.Gameplay.Grounds;
using SharedLibrary.TileSet;
using SharedLibrary.TileSet.Tiled;
using SharedLibrary.Ultility;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace IntoTheDungeon.Gameplay
{
    //All data file readings appear here
    public class GameDataLoader
    {
        private const string TILESET_PATH = "TileSet/urizen_tileset";
        private const string MAPS_FOLDER = "Stages";
        private const string GAMEDATA_FOLDER = "Gamedata";

        private TileSet tileSet;
        private Dictionary<string, TiledMapJsonData> mapsData;
        private List<BaseUnit> baseUnits;
        private List<GroundData> groundData;
        //private List

        public GameDataLoader() {
            mapsData = new Dictionary<string, TiledMapJsonData>();
        }
        public void Load(ContentManager contentManager)
        {
            tileSet = new TileSet(
                contentManager.Load<Texture2D>(TILESET_PATH),
                1,
                1,
                12,
                12,
                Color.Black);

            List<UnitData> unitData = new List<UnitData>();
            unitData = (List<UnitData>)JsonHelper.ReadJsonFile(GAMEDATA_FOLDER + "/units.json", unitData.GetType());
            baseUnits = unitData.Select(u => new BaseUnit(u)).ToList();

            groundData = new List<GroundData>();
            groundData = (List<GroundData>)JsonHelper.ReadJsonFile(GAMEDATA_FOLDER + "/grounds.json", groundData.GetType());

            LoadStage(contentManager, "Test");
        }

        public void LoadStage(ContentManager contentManager, string stageName)
        {
            if (stageName.Length == 0)
                throw new System.Exception("Stage name can\'t be null!");

            DirectoryInfo directoryInfo = new DirectoryInfo(contentManager.RootDirectory + "/" + MAPS_FOLDER + "/" + stageName);
            Debug.WriteLine(directoryInfo.FullName);
            if (!directoryInfo.Exists)
                throw new DirectoryNotFoundException(stageName + " is not found!");

            mapsData.Clear();
            FileInfo[] files = directoryInfo.GetFiles("*.*");
            foreach (var item in files)
            {
                string key = Path.GetFileNameWithoutExtension(item.Name);

                string fullpath = MAPS_FOLDER + "/" + stageName + "/" + key;
                mapsData[key] = contentManager.Load<TiledMapJsonData>(fullpath);   
            }
        }

        //Getter, setter
        public TileSet GetTileSet()
        {
            return tileSet;
        }

        public TiledMapJsonData GetTileMapData(string mapName)
        {
            return mapsData[mapName];
        }

        public List<BaseUnit> GetUnits()
        {
            return baseUnits;
        }

        public List<GroundData> GetGrounds()
        {
            return groundData;
        }
    }
}
