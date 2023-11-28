using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharedLibrary.Scene;
using SharedLibrary.UIComponents;
using System.Diagnostics;

namespace IntoTheDungeon.Scenes
{
    public class TestSceneScript : SceneScript
    {
        private ImageButton myButton;
        private ImageButton otherButton;

        private TextArea myTextArea;

        public override void Load()
        {
            myButton = GameObjectManager.GetGameObjectByName("testButton") as ImageButton;
            otherButton = GameObjectManager.GetGameObjectByName("otherButton") as ImageButton;

            myButton.EnableBackground = false;
            myButton.Scale = 0.8f;
            myButton.OnClick += MyButton_OnClick;

            otherButton.BackgroundColor = Color.White;
            otherButton.BorderColor = Color.Purple;
            otherButton.Rotation = 0.1f;
            otherButton.Scale = 0.5f;
            otherButton.BorderThickness = 8;

            otherButton.OnClick += OtherButton_OnClick;

            myTextArea = GameObjectManager.GetGameObjectByName("myTextArea") as TextArea;
            //myTextArea.LineSpacing = 15;
        }

        private void MyButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {
            this.Scene.GetSceneManager().LoadSceneByName("Main Menu");
        }

        private void OtherButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {
            Debug.WriteLine("Another hello!");
        }
    }
}