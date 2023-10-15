using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharedLibrary.Scene;
using SharedLibrary.UIComponents;
using System.Diagnostics;

namespace IntoTheDungeon.Scenes
{
    public class MainMenuScene : BaseScene
    {
        private ImageButton myButton;
        public MainMenuScene() : base("Main Menu")
        {
        }

        public MainMenuScene(SceneManager sceneManager) : base("Main Menu", sceneManager)
        {
        }

        public override void Destroy()
        {
            //throw new System.NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            myButton.Draw(spriteBatch);
        }

        public override void LoadContent(ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            myButton = new ImageButton(graphicsDevice, 
                new ImageLabel(contentManager.Load<Texture2D>("GUI_TestButtons/RedButton-Active"), 
                new Point(0, 0)), 
                new Point(), 
                Color.White);
            myButton.OnClick += MyButton_OnClick;
        }

        private void MyButton_OnClick(object sender, SharedLibrary.UIComponents.Events.EventArgs.OnClickArgs e)
        {
            Debug.WriteLine("Hello world!");
        }

        public override void Update(GameTime gameTime)
        {
            myButton.Update(gameTime);
        }
    }
}