using IntoTheDungeon.Gameplay;
using IntoTheDungeon.Gameplay.Units;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharedLibrary.Scene;
using SharedLibrary.TileSet;
using SharedLibrary.UIComponents;
using SharedLibrary.Ultility;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SharedLibrary.Event;
using IntoTheDungeon.Gameplay.Effect;
using SharedLibrary.BaseGameObject;

namespace IntoTheDungeon.Scenes
{
    public class MainGameScript : SceneScript
    {
        private readonly StatusEffect QUICKBLOCK = UnitEffects.CreateQuickBlockEffect();

        private const int PLAYER_ID = 1649;

        private CampaignMaster _campaignMaster;

        private bool drawAttackableTile = false;
        private bool drawPlayerMovableTiles = false;
        private List<Point> drawAttackableTiles = new List<Point>();
        private List<Point> drawMovableTiles = new List<Point>();

        private Texture2D markAttackableTexture;

        private Texture2D markMovableTexture;
        private Point markSize;
        private bool playerSelected = false;
        private Point playerLocation;

        private TextLabel turnCounterText;

        private TextLabel unitNameText;
        private TextLabel unitHpText;
        private TextLabel unitAttackText;
        private TextLabel unitArmorText;
        private TextArea unitDescriptionText;

        private TextButton attackButton;
        private TextButton blockButton;
        private TextButton advanceButton;

        private List<Point> playerLocations = new List<Point>();
        private bool playerMoved = false;
        private bool moveUndo = false;
        private TextButton undoButton;

        private PlayerAction playerAction = PlayerAction.None;

        private bool attacked = false;
        private bool prepareAttack = false;

        //private Dictionary<Point, Item> respawnItems;

        public override void Load()
        {
            //playerStates = new PlayerStateController();
            markMovableTexture = new Texture2D(this.Scene.GetGraphicsDevice(), 1, 1);
            Color markColor = ColorHelper.GetColorFrom("CAF0F8");
            markColor.A = 20;
            markMovableTexture.SetData<Color>(new Color[] { markColor });

            Color markAttackColor = ColorHelper.GetColorFrom("CCFF00");
            markAttackColor.A = 20;
            markAttackableTexture = new Texture2D(this.Scene.GetGraphicsDevice(), 1, 1);
            markAttackableTexture.SetData<Color>(new Color[] { markAttackColor });
            

            _campaignMaster = ObjectStorage.GetObject(Constants.CAMPAIGN_MASTER) as CampaignMaster;
            _campaignMaster.AlignCenter(Constants.GAME_AREA);

            markSize = _campaignMaster.MapManager.GetTileRect(0, 0).Size - new Point(1, 1);

            foreach (var kvp in _campaignMaster.MapUnits) {
                if (kvp.Value.TileId == PLAYER_ID - 1)
                {
                    playerLocation = kvp.Key;
                    break;
                }
            }
            playerLocations.Add(playerLocation);

            _campaignMaster.MapManager.OnFocusTileChanged += ProcessPlayer;
            _campaignMaster.MapManager.OnFocusTileChanged += DisplayTileInfo;
            Register(_campaignMaster);
            //Register(_campaignMaster.MapManager);

            unitNameText = GameObjectManager.GetGameObjectByName("unitName") as TextLabel;
            unitHpText = GameObjectManager.GetGameObjectByName("unitHp") as TextLabel;
            unitAttackText = GameObjectManager.GetGameObjectByName("unitAttack") as TextLabel;
            unitArmorText = GameObjectManager.GetGameObjectByName("unitArmor") as TextLabel;
            unitDescriptionText = GameObjectManager.GetGameObjectByName("unitDescription") as TextArea;

            //unitDescriptionText.LineSpacing = 10;
            undoButton = GameObjectManager.GetGameObjectByName("undo") as TextButton;
            undoButton.OnClick += UndoButton_OnClick;

            attackButton = GameObjectManager.GetGameObjectByName("attack") as TextButton;
            blockButton = GameObjectManager.GetGameObjectByName("block") as TextButton;
            advanceButton = GameObjectManager.GetGameObjectByName("advance") as TextButton;

            attackButton.OnClick += AttackButton_OnClick;
            blockButton.OnClick += BlockButton_OnClick;
            advanceButton.OnClick += AdvanceButton_OnClick;

            turnCounterText = GameObjectManager.GetGameObjectByName("turnCounter") as TextLabel;

            attackButton.AddTag(Constants.INGAME_UPDATABLE);
            blockButton.AddTag(Constants.INGAME_UPDATABLE);
            advanceButton.AddTag(Constants.INGAME_UPDATABLE);
            undoButton.AddTag(Constants.INGAME_UPDATABLE);
            _campaignMaster.AddTag(Constants.INGAME_UPDATABLE);

            //respawnItems = new Dictionary<Point, Item>();

            EventBus.Subscribe(Constants.CHANGE_MAP, ChangeMap);
        }

        private void AdvanceButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {
            _campaignMaster.UpdateMapState();

            playerAction = PlayerAction.None;
            playerSelected = false;
            playerMoved = false;
            moveUndo = false;
            attacked = false;

            drawAttackableTile = false;
            drawPlayerMovableTiles = false;
            prepareAttack = false;

            turnCounterText.Text = string.Format("Turn: {0}", _campaignMaster.Turn + 1);
            EventBus.RaiseEvent("test");

            Unit player = _campaignMaster.MapUnits[playerLocation];
            if (player.IsBlocking())
                player.RemoveEffect(QUICKBLOCK);

            if (!player.IsLiving)
            {
                GameObjectManager.ToggleEnabled(o => o.HasTag(Constants.INGAME_UPDATABLE));
                EventBus.RaiseEvent(Constants.PLAYER_LOSE);
            } 
            if (_campaignMaster.PlayerHasWon(playerLocation))
            {
                GameObjectManager.ToggleEnabled(o => o.HasTag(Constants.INGAME_UPDATABLE));
                if (_campaignMaster.IsLastMap())
                {
                    EventBus.RaiseEvent(Constants.PLAYER_FINISH_CAMPAIGN);
                }
                else
                {
                    EventBus.RaiseEvent(Constants.PLAYER_WIN);
                }
            }
        }

        private void BlockButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {
            Unit player = _campaignMaster.MapUnits[playerLocation];
            if (!attacked)
            {
                if (!player.IsBlocking())
                    player.AddEffect(QUICKBLOCK);
            }
            
            if (playerAction == PlayerAction.Block)
            {
                playerAction = PlayerAction.None;
                player.RemoveEffect(QUICKBLOCK);
            }
        }

        private void AttackButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {
            if (!attacked)
            {
                if (!prepareAttack)
                {
                    playerAction = PlayerAction.PrepareAttack;
                    drawAttackableTile = true;
                    prepareAttack = true;
                }
                else
                {
                    playerAction = PlayerAction.None;
                    drawAttackableTile = false;
                    prepareAttack = false;
                }
            }

            Unit player = _campaignMaster.MapUnits[playerLocation];
            if (player.IsBlocking())
                player.RemoveEffect(QUICKBLOCK);
        }

        private void SetAttackableTiles(Point tileLocation)
        {
            drawAttackableTiles.Clear();

            Point movable1 = new Point(tileLocation.X - 1, tileLocation.Y);
            Point movable2 = new Point(tileLocation.X + 1, tileLocation.Y);
            Point movable3 = new Point(tileLocation.X, tileLocation.Y - 1);
            Point movable4 = new Point(tileLocation.X, tileLocation.Y + 1);

            List<Point> testPoints = new List<Point>() { movable1, movable2, movable3, movable4 };

            foreach (Point point in testPoints) { 
                if (_campaignMaster.MapManager.TileMap.WithinBound(point.X, point.Y))
                { 
                    drawAttackableTiles.Add(_campaignMaster.MapManager.GetTileLocation(point.X, point.Y));
                }
            }
        }

        private void UndoButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {
            if (playerMoved && !moveUndo && !attacked)
            {
                playerLocations.RemoveAt(playerLocations.Count - 1);
                Point tileLocation = playerLocations.Last();
                _campaignMaster.MapManager.SwapInLayer("Units", playerLocation.X, playerLocation.Y, tileLocation.X, tileLocation.Y);

                //copy value
                _campaignMaster.MapUnits[tileLocation] = _campaignMaster.MapUnits[playerLocation];
                //then remove old one
                _campaignMaster.MapUnits.Remove(playerLocation);
                playerLocation = tileLocation;

                SetMovableTiles(tileLocation);
                SetAttackableTiles(tileLocation);

                playerAction = PlayerAction.Move;
                playerMoved = false;
                moveUndo = true;

                drawPlayerMovableTiles = true;
            }
        }

        private void ProcessPlayer(object sender, TileChangedEvent e)
        {
            Point tileLocation = e.NewTile;

            //int groundId = _campaignMaster.MapManager.GetLayer("Ground").GetIdRc(tileLocation.X, tileLocation.Y);
            int unitId = _campaignMaster.MapManager.GetLayer("Units").GetIdRc(tileLocation.X, tileLocation.Y);

            if (unitId == PLAYER_ID && !playerSelected)
            {
                SetAttackableTiles(tileLocation);
                SetMovableTiles(tileLocation);
            } else if (playerSelected)
            {
                if (!attacked)
                    if (!prepareAttack)
                    {
                        if (!playerMoved)
                        {
                            bool hasMoved = ProcessPlayerMove(tileLocation);

                            if (hasMoved)
                            {
                                playerMoved = true;
                            } else
                            {
                                drawPlayerMovableTiles = false;
                                playerSelected = false;
                            }
                        }
                    } else
                    {
                        bool hasAttacked = ProcessPlayerAttack(tileLocation);

                        if (hasAttacked)
                        {
                            attacked = true;
                            drawAttackableTile = false;
                            playerAction = PlayerAction.Attack;
                        }
                    }
            } else
            {
                drawPlayerMovableTiles = false;
                drawAttackableTile = false;
                playerSelected = false;
            }
        }

        private bool ProcessPlayerAttack(Point tileLocation)
        {
            Debug.WriteLine("Now processing player attack");
            Point actualTileLocation = _campaignMaster.MapManager.GetTileLocation(tileLocation.X, tileLocation.Y);
            Point attackPoint = drawAttackableTiles.Find(p => p.X == actualTileLocation.X && p.Y == actualTileLocation.Y);

            if (attackPoint.X <= 0 || attackPoint.Y <= 0)
                return false;

            Debug.WriteLine(attackPoint.ToString());

            int unitId = _campaignMaster.MapManager.GetLayer("Units").GetIdRc(tileLocation.X, tileLocation.Y) - 1;

            if (_campaignMaster.DataManager.Units.ContainsKey(unitId))
            {
                Unit player = _campaignMaster.MapUnits[playerLocation];
                Unit unit = _campaignMaster.MapUnits[tileLocation];
                unit.Health -= (player.Attack >= unit.Armor ? player.Attack - unit.Armor : 0);
                unit.CheckHealth();

                if (!unit.IsLiving)
                {
                    _campaignMaster.MapManager.AssignValue("Units", tileLocation.X, tileLocation.Y, unit.DeadTileId);
                    _campaignMaster.MapUnits.Remove(tileLocation);
                }
            }

            return true;
        }

        private bool ProcessPlayerMove(Point tileLocation)
        {
            int groundId = _campaignMaster.MapManager.GetLayer("Ground").GetIdRc(tileLocation.X, tileLocation.Y);
            int unitId = _campaignMaster.MapManager.GetLayer("Units").GetIdRc(tileLocation.X, tileLocation.Y);

            Point actualTileLocation = _campaignMaster.MapManager.GetTileLocation(tileLocation.X, tileLocation.Y);
            Point? moveNextPoint = drawMovableTiles.Find(p => p.X == actualTileLocation.X && p.Y == actualTileLocation.Y);

            if (moveNextPoint != null && moveNextPoint.Value.X > 0 && moveNextPoint.Value.Y > 0)
            {
                Point playerTileLocation = _campaignMaster.MapManager.GetTileLocation(playerLocation.X, playerLocation.Y);
                if (playerTileLocation.X == moveNextPoint.Value.X && playerTileLocation.Y == moveNextPoint.Value.Y)
                    return false;

                _campaignMaster.MapManager.SwapInLayer("Units", playerLocation.X, playerLocation.Y, tileLocation.X, tileLocation.Y);

                //copy value
                _campaignMaster.MapUnits[tileLocation] = _campaignMaster.MapUnits[playerLocation];
                //then remove old one
                _campaignMaster.MapUnits.Remove(playerLocation);
                playerLocation = tileLocation;

                playerLocations.Add(playerLocation);
                SetMovableTiles(tileLocation);
                SetAttackableTiles(tileLocation);

                drawPlayerMovableTiles = false;
                return true;
            }
            return false;
        }

        private void ChangeMap(Message message)
        {
            _campaignMaster.GoToNextMap();

            foreach (var kvp in _campaignMaster.MapUnits)
            {
                if (kvp.Value.TileId == PLAYER_ID - 1)
                {
                    playerLocation = kvp.Key;
                    break;
                }
            }
            playerLocations.Add(playerLocation);

            turnCounterText.Text = "Turn: 1";
            GameObjectManager.ToggleEnabled(o => o.HasTag(Constants.INGAME_UPDATABLE));
            _campaignMaster.DrawOrder = 0;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (drawAttackableTile)
            {
                foreach (var drawPoint in drawAttackableTiles)
                {
                    spriteBatch.Draw(markAttackableTexture, new Rectangle(drawPoint, markSize), Color.White);
                }
            }
            else if (drawPlayerMovableTiles)
            {
                foreach (var drawPoint in drawMovableTiles)
                {
                    spriteBatch.Draw(markMovableTexture, new Rectangle(drawPoint, markSize), Color.White);
                }
            }
        }

        private void DisplayTileInfo(object sender, TileChangedEvent e)
        {
            Point tileLocation = e.NewTile;

            int groundId = _campaignMaster.MapManager.GetLayer("Ground").GetIdRc(tileLocation.X, tileLocation.Y);
            
            if (_campaignMaster.MapUnits.ContainsKey(tileLocation))
            {
                Unit unit = _campaignMaster.MapUnits[tileLocation];

                unitNameText.Text = string.Format("A {0} is on this tile", unit.Name);
                unitHpText.Text = string.Format("Health: {0}", unit.Health);
                if (unitDescriptionText.NumberOfLines > 0)
                    unitDescriptionText.RemoveLine(0);
                unitDescriptionText.WriteLine(null, unit.Description, null);
                unitAttackText.Text = string.Format("Attack: {0}", unit.Attack);
                unitArmorText.Text = string.Format("Armor: {0}", unit.Armor);
                unitDescriptionText.UpdateRenderTexture();
            } else
            {
                unitNameText.Text = "There is no unit on this tile";
                unitHpText.Text = "";
                if (unitDescriptionText.NumberOfLines > 0)
                    unitDescriptionText.RemoveLine(0);
                unitAttackText.Text = "";
                unitArmorText.Text = "";
            }
        }

        public override void Destroy()
        {
            base.Destroy();

            drawMovableTiles.Clear();
        }

        private void SetMovableTiles(Point tileLocation)
        {
            Unit player = _campaignMaster.MapUnits[tileLocation];
            drawMovableTiles.Clear();

            ListMovableTiles(player.DetectRange, 1, tileLocation, new HashSet<Point>());
            drawMovableTiles = drawMovableTiles.Distinct().ToList();
            drawPlayerMovableTiles = true;
            playerSelected = true;
        }

        private List<Point> ListMovableTiles(int maxDepth, int currentDepth, Point currentPoint, HashSet<Point> visited)
        {
            if (currentDepth > maxDepth)
                return new List<Point>();
            List<Point> result = new List<Point>();

            Point movable1 = new Point(currentPoint.X - 1, currentPoint.Y);
            Point movable2 = new Point(currentPoint.X + 1, currentPoint.Y);
            Point movable3 = new Point(currentPoint.X, currentPoint.Y - 1);
            Point movable4 = new Point(currentPoint.X, currentPoint.Y + 1);

            List<Point> testPoints = new List<Point>() { movable1, movable2, movable3, movable4 };

            foreach (Point point in testPoints)
            {
                if (!visited.Contains(point))
                {
                    if (_campaignMaster.MapManager.TileMap.WithinBound(point.X, point.Y))
                        if ((!_campaignMaster.MapGrounds.ContainsKey(point) 
                            || _campaignMaster.MapGrounds[point].walkable) && !_campaignMaster.MapUnits.ContainsKey(point))
                        {
                            drawMovableTiles.Add(_campaignMaster.MapManager.GetTileLocation(point.X, point.Y));
                            visited.Add(point);
                            result.AddRange(ListMovableTiles(maxDepth, currentDepth + 1, point, visited));
                            visited.Remove(point);
                        } else
                        visited.Add(point);
                    else
                        visited.Add(point);
                }
            }       
            return result;
        }
    }
}