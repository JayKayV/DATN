using Microsoft.Xna.Framework;
using SharedLibrary.Scene;
using SharedLibrary.UIComponents;
using SharedLibrary.UIComponents.Events.EventArgs;
using SharedLibrary.Ultility;
using System.Diagnostics;
namespace IntoTheDungeon.Scenes
{
    public class MenuSceneScript : SceneScript
    {
        private TextButton gameSceneButton;
        private TextButton settingsButton;
        private TextButton quitButton;
        public override void Load()
        {
            gameSceneButton = GameObjectManager.GetGameObjectByName("gameSceneButton") as TextButton;
            gameSceneButton.Paddings = new int[4] { 5, 30, 5, 30 };
            gameSceneButton.HoverStyle.TextColor = Color.Yellow;

            Point gameSceneAlignedLocation = Helper.AlignCenter(this.Scene.GetGraphicsDevice().Viewport.Bounds, gameSceneButton.Size, true, false);

            gameSceneButton.Position = new Point(
                gameSceneAlignedLocation.X < 0 ? gameSceneButton.Position.X : gameSceneAlignedLocation.X,
                gameSceneAlignedLocation.Y < 0 ? gameSceneButton.Position.Y : gameSceneAlignedLocation.Y);

            gameSceneButton.OnClick += GameSceneButton_OnClick;

            settingsButton = GameObjectManager.GetGameObjectByName("settingsButton") as TextButton;
            settingsButton.Paddings = new int[4] { 5, 30, 5, 30 };

            Point stButtonAlignedLocation = Helper.AlignCenter(this.Scene.GetGraphicsDevice().Viewport.Bounds, settingsButton.Size, true, false);
            settingsButton.Position = new Point(stButtonAlignedLocation.X, settingsButton.Position.Y);
            settingsButton.HoverStyle.TextColor = Color.Yellow;

            quitButton = GameObjectManager.GetGameObjectByName("quitButton") as TextButton;
            quitButton.Paddings = new int[4] { 5, 30, 5, 30 };
            Point qtButtonAlignedLocation = Helper.AlignCenter(this.Scene.GetGraphicsDevice().Viewport.Bounds, quitButton.Size, true, false);
            quitButton.Position = new Point(qtButtonAlignedLocation.X, quitButton.Position.Y);
            quitButton.HoverStyle.TextColor = Color.Yellow;

            quitButton.OnClick += QuitButton_OnClick;
            settingsButton.OnClick += SettingsButton_OnClick;
        }

        private void SettingsButton_OnClick(object sender, OnClickArgs e)
        {
            LoadScene("Settings Scene");
        }

        private void QuitButton_OnClick(object sender, OnClickArgs e)
        {
            scene.QuitGame();
        }

        private void GameSceneButton_OnClick(object sender, OnClickArgs e)
        {
            LoadScene("Loading Scene");
        }
    }
}