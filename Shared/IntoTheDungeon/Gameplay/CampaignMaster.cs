using IntoTheDungeon.Gameplay.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Shared.Gameplay.Grounds;
using SharedLibrary.BaseGameObject;
using SharedLibrary.TileSet;
using System.Collections.Generic;

namespace IntoTheDungeon.Gameplay
{
    public class CampaignMaster : GameObject
    {
        private const int MAP_SPACING = 1;
        private const float MAP_SCALE = 3f;

        private Dictionary<string, int> _facts;
        private TileMapManager _mapManager;

        //for loading data pregame
        private GameDataLoader _gameDataLoader;
        private GameDataManager _gameDataManager;

        //for handling data ingame
        private Dictionary<Point, BaseUnit> mapUnits;

        public CampaignMaster()
        {
            _gameDataLoader = new GameDataLoader();
            _facts = new Dictionary<string, int>();
        }

        public CampaignMaster(TileMapManager tileMapManager) {
            _gameDataLoader = new GameDataLoader();
            _facts = new Dictionary<string, int>();

            _mapManager = tileMapManager;
        }

        //First Load
        public void Load(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            //Load all gamedata
            _gameDataLoader.Load(contentManager);

            //Load stage
            _gameDataLoader.LoadStage(contentManager, "Test");
            _mapManager = new TileMapManager(graphicsDevice,
                new TileMap(_gameDataLoader.GetTileMapData("TestMap0")),
                _gameDataLoader.GetTileSet(),
                new Point(0, 0),
                MAP_SPACING,
                MAP_SCALE);

            //Save all data in dataManager
            _gameDataManager = new GameDataManager(_gameDataLoader);

            //Setting for data ingame
            TileMapLayer unitsLayer = _mapManager.TileMap.GetLayerByName("Units");
            mapUnits = _gameDataManager.GetMapUnits(unitsLayer);
        }

        public void LoadStage(ContentManager contentManager, GraphicsDevice graphicsDevice, string stageName)
        {
            _gameDataLoader.LoadStage(contentManager, stageName);
            _mapManager.LoadMap(new TileMap(_gameDataLoader.GetTileMapData("First")));
        }

        public void Save()
        {
            
        }

        public void UpdateMapState()
        {
            foreach (KeyValuePair<Point, BaseUnit> unit in mapUnits)
            {
                int eyeRange = unit.Value.DetectRange;
                int x = unit.Key.X, y = unit.Key.Y;

                int[][] unitData = _mapManager.GetLayer("Units").GetData(x - eyeRange, y - eyeRange, eyeRange * 2, eyeRange * 2);
                int[][] groundData = _mapManager.GetLayer("Ground").GetData(x - eyeRange, y - eyeRange, eyeRange * 2, eyeRange * 2);

                UnitFunctionality.SimpleScan(unit.Value, groundData, unitData, _gameDataManager);
            }
        }

        private void StartTurn()
        {

        }

        private void StopTurn()
        {

        }

        private void ProgressTurn()
        {

        }

        public override void Update(GameTime gameTime)
        {
            _mapManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _mapManager.Draw(spriteBatch);
        }

        public Point Location
        {
            get => _mapManager.Location;
            set => _mapManager.Location = value;
        }

        public TileMapManager Manager
        {
            get => _mapManager;
            set => _mapManager = value;
        }

        public void AlignCenter(Rectangle area)
        {
            this._mapManager.AlignCenter(area);
        }

        /// <summary>
        ///     Create general pathmap with location (x, y) : observer being zero if provided
        /// </summary>
        /// <param name="pathMap"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void CreatePathMap(out int[][] pathMap, int x = -1, int y = -1)
        {
            int height = _mapManager.TileMap.MapSize.Y, width = _mapManager.TileMap.MapSize.X;
            pathMap = new int[height][]; 
            for (int i = 0; i < height; i++)
            {
                pathMap[i] = new int[width];
                for (int j = 0; j < width; j++)
                {

                }
            }
        }
    }
}