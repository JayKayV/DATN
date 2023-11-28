using SharedLibrary.UIComponents;
using SharedLibrary.UIComponents.Events.EventArgs;
using SharedLibrary.Scene;
using SharedLibrary.TileSet;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using SharedLibrary.TileSet.Tiled;

namespace IntoTheDungeon.Scenes
{
    public class GameSceneScript : SceneScript
    {
        private ImageButton backButton;
        private TileSet tileSet;

        public override void Load()
        {
            backButton = GameObjectManager.GetGameObjectByName("backButton") as ImageButton;
            backButton.OnClick += BackButton_OnClick;        
        }

        private void BackButton_OnClick(object sender, OnClickArgs e)
        {
            this.Scene.GetSceneManager().LoadSceneByName("Main Menu");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}