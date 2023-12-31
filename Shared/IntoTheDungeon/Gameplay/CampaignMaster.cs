using IntoTheDungeon.Gameplay.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Shared.Gameplay.Grounds;
using SharedLibrary.BaseGameObject;
using SharedLibrary.TileSet;
using SharedLibrary.Ultility;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace IntoTheDungeon.Gameplay
{
    public class CampaignMaster : GameObject
    {
        private readonly List<string> maps = new List<string>
        {
            "TestMap0", "TestMap1", "TestMap2"
        };

        private int mapId = 0;

        private const int MAP_SPACING = 1;
        private const float MAP_SCALE = 4f;

        private Dictionary<string, int> _facts;
        private TileMapManager _mapManager;

        //for loading data pregame
        private GameDataLoader _gameDataLoader;
        private GameDataManager _gameDataManager;

        //for handling data ingame
        private Dictionary<Point, Unit> mapUnits;
        private Dictionary<Point, GroundData> groundData;
        private Dictionary<Point, Item> items;
        private int turn = 0;

        //for pathfinding
        public int[][] PathMap { get; set; }

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
            _mapManager.Name = "Map Manager";

            //Save all data in dataManager
            _gameDataManager = new GameDataManager(_gameDataLoader);

            SetDataIngame();
        }

        public void LoadStage(ContentManager contentManager, GraphicsDevice graphicsDevice, string stageName)
        {
            _gameDataLoader.LoadStage(contentManager, stageName);
            _mapManager.LoadMap(new TileMap(_gameDataLoader.GetTileMapData(stageName)));

            PathMap = CreatePathMap();
        }

        public void GoToNextMap()
        {
            mapId += 1;
            turn = 0;
            _mapManager.LoadMap(_gameDataLoader.GetTileMapData(maps[mapId]));

            SetDataIngame();
        }

        public bool IsLastMap()
        {
            return mapId == maps.Count - 1;
        }

        public void Save()
        {
            
        }

        public override void Destroy()
        {
            mapUnits.Clear();
        }

        private void SetDataIngame()
        {
            TileMapLayer unitsLayer = _mapManager.TileMap.GetLayerByName("Units");
            TileMapLayer groundLayer = _mapManager.TileMap.GetLayerByName("Ground");
            TileMapLayer itemsLayer = _mapManager.TileMap.GetLayerByName("Items");
            mapUnits = _gameDataManager.GetMapUnits(unitsLayer);
            groundData = _gameDataManager.GetGrounds(groundLayer);
            items = _gameDataManager.GetItems(itemsLayer);

            foreach (var unit in mapUnits.Values)
            {
                if (unit.Name == "Player")
                {
                    if (_facts.ContainsKey("health"))
                        unit.Health = _facts["health"];
                    if (_facts.ContainsKey("attack"))
                        unit.Attack = _facts["attack"];
                    if (_facts.ContainsKey("armor"))
                        unit.Armor = _facts["armor"];
                    continue;
                }

                if (Settings.GameDifficulty == GameDifficulty.HARD)
                {
                    unit.Health += 2;
                    unit.Attack += 1;
                }
            }

            PathMap = CreatePathMap();
        }

        public void UpdateMapState()
        {
            int mapSizeX = _mapManager.TileMap.MapSize.X;
            int mapSizeY = _mapManager.TileMap.MapSize.Y;

            Dictionary<Point, Unit> mapUnitsCopy = new Dictionary<Point, Unit>(mapUnits);
            foreach (KeyValuePair<Point, Unit> kvp in mapUnitsCopy)
            {
                Unit unit = kvp.Value;
                if (unit.Name == "Player")
                {
                    //check item tile
                    if (items.ContainsKey(kvp.Key))
                    {
                        items[kvp.Key].AffectUnit(unit);

                        //remove item
                        _mapManager.AssignValue("Items", kvp.Key.X, kvp.Key.Y, 0);
                        items.Remove(kvp.Key);
                    }
                    _facts["health"] = unit.Health;
                    _facts["attack"] = unit.Attack;
                    _facts["armor"] = unit.Armor;
                    continue;
                }

                //unit is already in chase
                if (unit.ChasePoints.Count > 0)
                {
                    Point nextChasePoint = kvp.Key;
                    if (unit.MoveRange >= unit.ChasePoints.Count - 1)
                    {
                        //move in and attack
                        if (unit.ChasePoints.Count > 1)
                            nextChasePoint = unit.ChasePoints[1];
                        _mapManager.SwapInLayer("Units", kvp.Key.X, kvp.Key.Y, nextChasePoint.X, nextChasePoint.Y);

                        Point targetPoint = unit.ChasePoints[0];
                        //check if target still there
                        if (mapUnits.ContainsKey(targetPoint))
                        {
                            Unit targetUnit = mapUnits[targetPoint];
                            if (targetUnit.TileId == unit.ChaseTargetId)
                            {
                                int attack = unit.Attack;
                                if (targetUnit.IsBlocking())
                                    attack -= targetUnit.Block;
                                targetUnit.Health -= (attack > targetUnit.Armor ? attack - targetUnit.Armor : 0);
                                targetUnit.CheckHealth();
                            }
                        }
                        //clear the target point
                        unit.ChaseTargetId = 0;
                        unit.ChasePoints.Clear();
                    }
                    //else continue give chase
                    else
                    {
                        nextChasePoint = unit.ChasePoints[unit.ChasePoints.Count - unit.MoveRange];
                        _mapManager.SwapInLayer("Units", kvp.Key.X, kvp.Key.Y, nextChasePoint.X, nextChasePoint.Y);

                        unit.ChasePoints = unit.ChasePoints.GetRange(0, unit.ChasePoints.Count - unit.MoveRange);
                    }

                    if (nextChasePoint != kvp.Key)
                    {
                        mapUnits[nextChasePoint] = mapUnits[kvp.Key];
                        mapUnits.Remove(kvp.Key);
                    }
                    PathMap = CreatePathMap();
                    continue;
                }
                int eyeRange = unit.DetectRange;

                int x = MathHelper.Max(kvp.Key.X - eyeRange, 0);
                int y = MathHelper.Max(kvp.Key.Y - eyeRange, 0);
                int w = MathHelper.Min(eyeRange * 2, mapSizeX - y);
                int h = MathHelper.Min(eyeRange * 2, mapSizeY - x);
                int[][] unitData = _mapManager.GetLayer("Units").GetData(x, y, w, h);
                int[][] groundData = _mapManager.GetLayer("Ground").GetData(x, y, w, h);

                List<Point> movePoints;

                if (unit.Faction == UnitFaction.Boss)
                    movePoints = UnitFunctionality.SimpleScan(unit, groundData, unitData, _gameDataManager);
                else
                    movePoints = UnitFunctionality.EyeScan(unit, groundData, unitData, _gameDataManager);

                Point nextMovePoint = kvp.Key;
                //Determine if there is a target
                if (movePoints.Count > 0)
                {
                    movePoints.Sort((mp1, mp2) =>
                    {
                        Point actualPoint1 = new Point(x + mp1.X, y + mp1.Y);
                        Point actualPoint2 = new Point(x + mp2.X, y + mp2.Y);

                        UnitFaction f1 = _gameDataManager.Units[unitData[actualPoint1.X][actualPoint1.Y]].Faction;
                        UnitFaction f2 = _gameDataManager.Units[unitData[actualPoint2.X][actualPoint2.Y]].Faction;

                        if (unit.FactionFunc[f1] > unit.FactionFunc[f2])
                            return 1;
                        if (unit.FactionFunc[f1] < unit.FactionFunc[f2])
                            return -1;
                        return 0;
                    });

                    //Debug.WriteLine(string.Format("{0}: {1}",unit.ToString(), (movePoints[0] + new Point(x, y)).ToString()), "DEBUG");
                    Point firstTarget = movePoints[0] + new Point(x, y);
                    Unit targetUnit = mapUnits[firstTarget];

                    List<Point> path = Pathfinder.FindPath(PathMap, kvp.Key, firstTarget);

                    //check if within move range
                    if (unit.MoveRange >= path.Count - 1)
                    {
                        //move in and attack
                        if (path.Count > 1)
                            nextMovePoint = path[1];
                        _mapManager.SwapInLayer("Units", kvp.Key.X, kvp.Key.Y, nextMovePoint.X, nextMovePoint.Y);
                        int attack = unit.Attack;
                        if (targetUnit.IsBlocking())
                            attack -= targetUnit.Block;
                        targetUnit.Health -= (attack > targetUnit.Armor ? attack - targetUnit.Armor : 0);
                        targetUnit.CheckHealth();
                    }
                    //else give chase
                    else
                    {
                        nextMovePoint = path[path.Count - unit.MoveRange];
                        _mapManager.SwapInLayer("Units", kvp.Key.X, kvp.Key.Y, nextMovePoint.X, nextMovePoint.Y);

                        unit.ChasePoints = path.GetRange(0, path.Count - unit.MoveRange);
                        Unit target = mapUnits[path[0]];
                        unit.ChaseTargetId = target.TileId;
                    } 
                } else
                {
                    //If not, move randomly
                    List<Point> nextPoints = new List<Point>()
                    {
                        new Point(kvp.Key.X, kvp.Key.Y),
                        new Point(kvp.Key.X + 1, kvp.Key.Y),
                        new Point(kvp.Key.X - 1, kvp.Key.Y),
                        new Point(kvp.Key.X, kvp.Key.Y + 1),
                        new Point(kvp.Key.X, kvp.Key.Y - 1),
                    };

                    nextPoints.RemoveAll(p => !_mapManager.TileMap.WithinBound(p.X, p.Y));
                    nextPoints.RemoveAll(p => !_gameDataManager.Grounds[_mapManager.GetLayer("Ground").GetIdRc(p.X, p.Y)].walkable);

                    nextMovePoint = nextPoints[new Random().Next(nextPoints.Count)];

                    Debug.WriteLine(string.Format("{0},{1}", nextPoints.Count, nextMovePoint), "DEBUG");
                    _mapManager.SwapInLayer("Units", kvp.Key.X, kvp.Key.Y, nextMovePoint.X, nextMovePoint.Y);

                    //PathMap = CreatePathMap();
                }

                if (nextMovePoint != kvp.Key)
                {
                    mapUnits[nextMovePoint] = mapUnits[kvp.Key];
                    mapUnits.Remove(kvp.Key);
                }
                PathMap = CreatePathMap();
            }
            
            turn += 1;
        }
        
        public bool PlayerHasWon(Point playerLocation)
        {
            //GroundData ground = 
            int groundId = _mapManager.GetLayer("Ground").GetIdRc(playerLocation.X, playerLocation.Y);
            if (!_gameDataManager.Grounds.ContainsKey(groundId))
                return false;
            return _gameDataManager.Grounds[groundId].name == Constants.EXIT_TILE;
        }

        public bool PlayerHasLost(Point playerLocation)
        {
            return mapUnits[playerLocation].Health <= 0;
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

        public void AlignCenter(Rectangle area)
        {
            this._mapManager.AlignCenter(area);
        }
        public int Turn { get => turn; }

        /// <summary>
        ///     Create general pathmap with location (x, y) : observer being zero if provided
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private int[][] CreatePathMap(int x = -1, int y = -1)
        {
            const int PATH_INF = 9999999;
            int height = _mapManager.TileMap.MapSize.Y, width = _mapManager.TileMap.MapSize.X;

            int[][] groundData = _mapManager.GetLayer("Ground").GetData(width, height);
            int[][] unitData = _mapManager.GetLayer("Units").GetData(width, height);

            int[][] pathMap = new int[height][]; 
            for (int i = 0; i < height; i++)
            {
                pathMap[i] = new int[width];
                for (int j = 0; j < width; j++)
                {
                    if (unitData[i][j] > 0)
                        pathMap[i][j] = PATH_INF;
                    else if (groundData[i][j] > 0)
                    {
                        int gd = groundData[i][j];
                        if (_gameDataManager.Grounds.ContainsKey(gd) && !_gameDataManager.Grounds[gd].walkable)
                        {
                            pathMap[i][j] = PATH_INF;
                        }
                        else
                            pathMap[i][j] = 0;
                    }
                    else
                        pathMap[i][j] = 0;
                }
            }
            return pathMap;
        }

        public TileMapManager MapManager { get => _mapManager; }

        public GameDataManager DataManager { get => _gameDataManager; }

        public Dictionary<Point, Unit> MapUnits { get => mapUnits; }

        public Dictionary<Point, Item> MapItems { get => items; }
 
        public Dictionary<Point, GroundData> MapGrounds { get => groundData; }
    }
}