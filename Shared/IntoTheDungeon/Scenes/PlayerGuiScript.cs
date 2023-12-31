using IntoTheDungeon.Gameplay;
using SharedLibrary.Scene;
using SharedLibrary.UIComponents;
using Microsoft.Xna.Framework;
using SharedLibrary.Event;
using System;

namespace IntoTheDungeon.Scenes
{
    public class PlayerGuiScript : SceneScript
    {
        //MainGameScript
        private CampaignMaster _campaignMaster;

        private TextButton advanceButton;

        private TextButton attackButton;
        private TextButton undoButton;
        private ImageButton pauseButton;
        private TextButton blockButton;
        private TextButton useObjButton;

        public override void Load()
        {
            _campaignMaster = GameObjectManager.GetGameObjectByName(Constants.CAMPAIGN_MASTER) as CampaignMaster;

            advanceButton = GameObjectManager.GetGameObjectByName("advance") as TextButton;
            advanceButton.HoverStyle.TextColor = Color.White;
            advanceButton.OnClick += AdvanceButton_OnClick;

            //_campaignMaster.MapManager.OnHoverTileChanged += _tileMapManager_OnHoverTileChanged;

            attackButton = GameObjectManager.GetGameObjectByName("attack") as TextButton;
            attackButton.HoverStyle.TextColor = Color.White;
            blockButton = GameObjectManager.GetGameObjectByName("block") as TextButton;
            blockButton.HoverStyle.TextColor = Color.White;

            undoButton = GameObjectManager.GetGameObjectByName("undo") as TextButton;
            undoButton.HoverStyle.TextColor = Color.White;

            attackButton.OnClick += AttackButton_OnClick;
            blockButton.OnClick += BlockButton_OnClick;
        }

        private void BlockButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {
            if (blockButton.OriginalStyle.TextColor == Color.Aqua)
            {
                blockButton.OriginalStyle.TextColor = Color.Yellow;
                blockButton.HoverStyle.TextColor = Color.Yellow;

                if (attackButton.OriginalStyle.TextColor != Color.Aqua)
                {
                    attackButton.OriginalStyle.TextColor = Color.Aqua;
                    attackButton.HoverStyle.TextColor = Color.White;
                }
            } else
            {
                blockButton.OriginalStyle.TextColor = Color.Aqua;
                blockButton.HoverStyle.TextColor = Color.White;
            }
        }

        private void AttackButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {
            if (attackButton.OriginalStyle.TextColor == Color.Aqua)
            {
                attackButton.OriginalStyle.TextColor = Color.Red;
                attackButton.HoverStyle.TextColor = Color.Red;

                if (blockButton.OriginalStyle.TextColor != Color.Aqua)
                {
                    blockButton.OriginalStyle.TextColor = Color.Aqua;
                    blockButton.HoverStyle.TextColor = Color.White;
                }
            } else
            {
                attackButton.OriginalStyle.TextColor = Color.Aqua;
                attackButton.HoverStyle.TextColor = Color.White;
            }
        }

        private void AdvanceButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {
            //Debug.WriteLine("Hello there!");

            if (attackButton.OriginalStyle.TextColor != Color.Aqua)
                attackButton.OriginalStyle.TextColor = Color.Aqua;
            if (blockButton.OriginalStyle.TextColor != Color.Aqua)
                blockButton.OriginalStyle.TextColor = Color.Aqua;
        }
    }
}