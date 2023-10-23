using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharedLibrary.Scene;
using SharedLibrary.UIComponents;
using System.Diagnostics;

namespace IntoTheDungeon.Scenes
{
    public class MainMenuScene : SceneScript
    {
        private ImageButton myButton;

        public override void Load()
        {
            myButton = GameObjectManager.GetGameObjectByName("testButton") as ImageButton;
            myButton.EnableBackground = false;
            myButton.OnClick += MyButton_OnClick;
        }

        private void MyButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {
            Debug.WriteLine("Hello world!");
        }
    }
}