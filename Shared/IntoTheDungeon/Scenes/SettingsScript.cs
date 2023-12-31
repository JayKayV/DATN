using SharedLibrary.Scene;
using SharedLibrary.UIComponents;
using Microsoft.Xna.Framework;
using IntoTheDungeon.Gameplay;
using SharedLibrary.Event;

namespace IntoTheDungeon.Scenes
{
    public class SettingsScript : SceneScript
    {
        private TextButton backButton;
        private TextButton audioOnButton;
        private TextButton audioOffButton;
        private TextButton normalDiffButton;
        private TextButton hardDiffButton;

        public override void Load()
        {
            backButton = GameObjectManager.GetGameObjectByName("backButton") as TextButton;
            backButton.HoverStyle.TextColor = Color.Yellow;

            backButton.OnClick += BackButton_OnClick;

            audioOnButton = GameObjectManager.GetGameObjectByName("On") as TextButton;
            audioOffButton = GameObjectManager.GetGameObjectByName("Off") as TextButton;
            normalDiffButton = GameObjectManager.GetGameObjectByName("Normal") as TextButton;
            hardDiffButton = GameObjectManager.GetGameObjectByName("Hard") as TextButton;

            if (Settings.GameDifficulty == GameDifficulty.NORMAL)
                normalDiffButton.TextColor = Color.Red;
            else 
                hardDiffButton.TextColor = Color.Red;

            if (Settings.AudioOn)
                audioOnButton.TextColor = Color.Red;
            else
                audioOffButton.TextColor = Color.Red;

            audioOnButton.OnClick += AudioOnButton_OnClick;
            audioOffButton.OnClick += AudioOffButton_OnClick;

            normalDiffButton.OnClick += NormalDiffButton_OnClick;
            hardDiffButton.OnClick += HardDiffButton_OnClick;
        }

        private void HardDiffButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {
            if (Settings.GameDifficulty == GameDifficulty.NORMAL)
            {
                normalDiffButton.TextColor = Color.Aqua;
                hardDiffButton.TextColor = Color.Red;
                Settings.GameDifficulty = GameDifficulty.HARD;
            }
        }

        private void NormalDiffButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {
            if (Settings.GameDifficulty == GameDifficulty.HARD)
            {
                normalDiffButton.TextColor = Color.Red;
                hardDiffButton.TextColor = Color.Aqua;

                Settings.GameDifficulty = GameDifficulty.NORMAL;
            }
        }

        private void AudioOffButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {
            if (Settings.AudioOn)
            {
                audioOffButton.TextColor = Color.Red;
                audioOnButton.TextColor = Color.Aqua;

                Settings.AudioOn = false;
                EventBus.RaiseEvent(Constants.TOGGLE_AUDIO);
            }
        }

        private void AudioOnButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {
            if (!Settings.AudioOn)
            {
                audioOffButton.TextColor = Color.Aqua;
                audioOnButton.TextColor = Color.Red;

                Settings.AudioOn = true;
                EventBus.RaiseEvent(Constants.TOGGLE_AUDIO);
            }
        }

        private void BackButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {
            LoadScene("Main Menu");
        }
    }
}