using SharedLibrary.Scene;
using SharedLibrary.UIComponents;
using SharedLibrary.UIComponents.Events.EventArgs;

namespace IntoTheDungeon.Scenes
{
    public class TestButtonScript : SceneScript
    {
        private TextButton testSceneButton;
        public override void Load()
        {
            testSceneButton = GameObjectManager.GetGameObjectByName("testSceneButton") as TextButton;

            testSceneButton.OnClick += TestSceneButton_OnClick;
        }

        private void TestSceneButton_OnClick(object sender, OnClickArgs e)
        {
            this.Scene.GetSceneManager().LoadSceneByName("Test Scene");
        }
    }
}