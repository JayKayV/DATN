using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharedLibrary.BaseGameObject;
using SharedLibrary.Event;

namespace SharedLibrary.Scene
{
    public class SceneScript
    {
        protected Scene scene;
        protected int loadMode;

        public int UpdateOrder { get; set;  }
        public int DrawOrder { get; set; }
        public int LoadOrder { get; set; }
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

        protected EventBus EventBus { get => scene.GetSceneManager().EventBus; }

        protected void LoadScene(string name)
        {
            this.scene.GetSceneManager().LoadSceneByName(name);
        }

        public void AddScript(SceneScript script)
        {
            this.scene.AddScript(script);
        }

        /// <summary>
        ///  <para>Add object to objectManager, let other objects from same scene use; and for auto draw, update </para>
        /// </summary>
        /// <param name="gameObject">The gameObject need to be added, must be derived from GameObject class</param>
        protected void Register(GameObject gameObject)
        {
            this.scene.GetObjectManager().AddGameObject(gameObject);
        }

        protected void Unregister(GameObject gameObject)
        {
            this.Scene.GetObjectManager().RemoveGameObject(gameObject);
        }

        protected GObjectStorage ObjectStorage {
            get => this.scene.GetSceneManager().ObjectStorage;
        }
    }
}
