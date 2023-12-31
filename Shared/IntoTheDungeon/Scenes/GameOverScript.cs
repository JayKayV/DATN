using IntoTheDungeon.Gameplay;
using SharedLibrary.Event;
using SharedLibrary.Scene;
using SharedLibrary.UIComponents;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace IntoTheDungeon.Scenes 
{
    public class GameOverScript : SceneScript
    {
        private UiFrame gameOverFrame;
        private TextLabel gameOverText;
        private TextButton gameOverButton;

        private CampaignMaster _campaignMaster;

        bool backToMenuFlag = true;

        public override void Load()
        {
            _campaignMaster = ObjectStorage.GetObject(Constants.CAMPAIGN_MASTER) as CampaignMaster;
            gameOverFrame = GameObjectManager.GetGameObjectByName("gameOverFrame") as UiFrame;
            gameOverText = GameObjectManager.GetGameObjectByName("gameOverText") as TextLabel; 
            gameOverButton = GameObjectManager.GetGameObjectByName("gameOverButton") as TextButton;

            gameOverButton.HoverStyle.BackgroundColor = Color.Black;
            gameOverButton.HoverStyle.TextColor = Color.White;

            gameOverFrame.Visible = false;
            //gameOverFrame.Enabled = false;
            gameOverText.Visible = false;
            gameOverButton.Enabled = false;
            gameOverButton.Visible = false;

            EventBus.Subscribe(Constants.PLAYER_WIN, ShowWinGameBox);
            EventBus.Subscribe(Constants.PLAYER_LOSE, ShowLoseGameBox);
            EventBus.Subscribe(Constants.PLAYER_FINISH_CAMPAIGN, ShowFinishGameBox);

            gameOverButton.OnClick += GameOverButton_OnClick;

            Debug.WriteLine(gameOverButton.LayerDepth);
        }

        private void GameOverButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {
            gameOverFrame.Visible = false;
            gameOverButton.Enabled = false;

            if (backToMenuFlag)
                LoadScene("Main Menu");
            else
            {
                EventBus.RaiseEvent(Constants.CHANGE_MAP);
            }
        }

        private void ShowWinGameBox(Message message)
        {
            gameOverText.Text = "Map finished!";
            gameOverButton.Text = "Go to next map";

            gameOverText.Position = new Point(gameOverFrame.Position.X + 15, gameOverText.Position.Y);
            gameOverButton.Position = new Point(gameOverFrame.Position.X + 20, gameOverButton.Position.Y);

            backToMenuFlag = false;
            DisableBackground();
        }

        private void ShowFinishGameBox(Message message)
        {
            gameOverText.Text = "Campaign finished";
            gameOverButton.Text = "Return to main menu";

            gameOverText.Position = new Point(gameOverFrame.Position.X + 7, gameOverText.Position.Y);
            gameOverButton.Position = new Point(gameOverFrame.Position.X + 10, gameOverButton.Position.Y);

            backToMenuFlag = true;
            DisableBackground();
        }

        private void ShowLoseGameBox(Message message)
        {
            gameOverText.Text = "You lost!";
            gameOverButton.Text = "Return to main menu";

            gameOverText.Position = new Point(gameOverFrame.Position.X + 15, gameOverText.Position.Y);
            gameOverButton.Position = new Point(gameOverFrame.Position.X + 10, gameOverButton.Position.Y);

            backToMenuFlag = true;
            DisableBackground();
        }

        private void DisableBackground()
        {
            //_campaignMaster.Enabled = false;

            _campaignMaster.DrawOrder = 1;

            gameOverFrame.Visible = true;
            gameOverButton.Enabled = true;
        }
    }
}