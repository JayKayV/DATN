using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace SharedLibrary.Scene
{
    public class SceneScriptManager
    {
        private List<SceneScript> scripts;
        private Scene scene;

        public SceneScriptManager(Scene scene) { 
            scripts = new List<SceneScript>();
            this.scene = scene;
        }

        public void Load()
        {
            foreach (var script in scripts.OrderBy(s => s.LoadOrder)) { 
                script.Load();
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (SceneScript script in scripts.OrderBy(s => s.UpdateOrder)) { 
                script.Update(gameTime);
                if (!scene.GetSceneManager().ContinueUpdate)
                    return;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (SceneScript script in scripts.OrderBy(s => s.DrawOrder))
            {
                script.Draw(spriteBatch);
            }
        }

        public List<SceneScript> GetScripts() { 
            return scripts; 
        }

        public void AddScript(SceneScript script) {
            script.Init(scene);
            scripts.Add(script);
        }

        public void RemoveScript(SceneScript script) { 
            scripts.Remove(script);
        }

        public void Clear()
        {
            foreach (SceneScript script in scripts)
            {
                script.Destroy();
            }
            scripts.Clear();
        }

    }
}
