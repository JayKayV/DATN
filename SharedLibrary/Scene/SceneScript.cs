using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharedLibrary.BaseGameObject;

namespace SharedLibrary.Scene
{
    public class SceneScript
    {
        protected Scene scene;
        protected int loadMode;
        protected int updateOrder;
        protected int drawOrder;

        public int UpdateOrder { get; }
        public int DrawOrder { get; }
        public SceneScript() { }

        public void Init(Scene scene) { 
            this.scene = scene;
        }

        public virtual void Load()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void Update(GameTime gameTime) 
        { 

        }

        public virtual void Destroy() { 
        }

        public Scene Scene
        {
            get => scene;
            set => scene = value;
        }

        protected GameObjectManager GameObjectManager { get => scene.GetObjectManager(); }

        private void ChangeScene(string name)
        {
            this.scene.GetSceneManager().LoadSceneByName(name);
        }

        public void AddScript(SceneScript script)
        {

        }
    }
}
