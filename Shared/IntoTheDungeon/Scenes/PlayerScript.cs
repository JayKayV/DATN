using IntoTheDungeon.Gameplay;
using SharedLibrary.Scene;
using SharedLibrary.TileSet;
using SharedLibrary.UIComponents;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

namespace IntoTheDungeon.Scenes
{
    public class PlayerScript : SceneScript
    {
        //MainGameScript
        private CampaignMaster _campaignMaster;

        private TextButton advanceButton;

        private TextButton attackButton;
        private ImageButton pauseButton;
        private TextButton blockButton;
        private TextButton useObjButton;

        public override void Load()
        {
            _campaignMaster = GameObjectManager.GetGameObjectByName(Constants.CAMPAIGN_MASTER) as CampaignMaster;

            advanceButton = GameObjectManager.GetGameObjectByName("advance") as TextButton;

            advanceButton.OnClick += AdvanceButton_OnClick;

            //_campaignMaster.MapManager.OnHoverTileChanged += _tileMapManager_OnHoverTileChanged;

            attackButton = GameObjectManager.GetGameObjectByName("attack") as TextButton;
            blockButton = GameObjectManager.GetGameObjectByName("block") as TextButton;

            attackButton.OnClick += AttackButton_OnClick;
            blockButton.OnClick += BlockButton_OnClick;
        }

        private void BlockButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {
            
        }

        private void AttackButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {

        }

        private void AdvanceButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {
            //Debug.WriteLine("Hello there!");
            _campaignMaster.UpdateMapState();
        }
    }
}