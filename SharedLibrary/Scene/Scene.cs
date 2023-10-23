using SharedLibrary.BaseGameObject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Xml;
using SharedLibrary.UIComponents.Base;
using SharedLibrary.UIComponents;
using System.Diagnostics;
using System.Linq;

namespace SharedLibrary.Scene
{
    public class Scene
    {
        protected string name;
        protected SceneManager sceneManager; //must be registered by SceneManager.AddScene(scene) if created outside of loading xml
        protected GameWindow gameWindow; //must be registered by SceneManager.AddScene(scene) if created outside of loading xml

        protected GameObjectManager gameObjectManager;
        protected SceneScriptManager scriptsManager;
        public string Name { get { return name; } }
        public Scene(string name)
        {
            this.name = name;
            this.sceneManager = null;
            this.gameObjectManager = new GameObjectManager(this);
            this.scriptsManager = new SceneScriptManager(this);
        }
        public Scene(string name, SceneManager sceneManager)
        {
            this.name = name;
            this.sceneManager = sceneManager;
            this.gameObjectManager = new GameObjectManager(this);
            this.scriptsManager = new SceneScriptManager(this);
        }

        public void LoadUiContent(XmlNodeList nodes, ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            int index = 0;
            foreach (XmlNode node in nodes) {
                index += 1;
                if (node.Attributes.GetNamedItem("type") == null)
                    continue;
                string uiType = node.Attributes.GetNamedItem("type").Value;
                AbstractUiObject uiObject = null;
                switch (uiType)
                {
                    case "imageButton":
                        uiObject = ImageButton.LoadFromXml(node, contentManager, graphicsDevice);
                        break;
                    case "imageLabel":
                        uiObject = ImageLabel.LoadFromXml(node, contentManager, graphicsDevice);
                        break;
                    default:
                        Debug.WriteLine($"[WARNING]: Incorrect type or bad element while parsing node index {index}");
                        break;
                }
                gameObjectManager.AddGameObject(uiObject);
            }
        }

        public void LoadScripts(XmlNodeList nodes, ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            //scriptsManager.
            Debug.WriteLine("check..." + gameObjectManager.GetGameObjects().Count());
        }
        public void Update(GameTime gameTime)
        {
            foreach (var uiObject in gameObjectManager.GetGameObjects().OfType<AbstractUiObject>())
            {
                uiObject.Update(gameTime);
            }
            scriptsManager.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var uiObject in gameObjectManager.GetGameObjects().OfType<AbstractUiObject>())
            {
                uiObject.Draw(spriteBatch);
            }
            scriptsManager.Draw(spriteBatch);
        }
        public void Destroy()
        {
            scriptsManager.Clear();
            gameObjectManager.Clear();
        }

        public SceneManager GetSceneManager()
        {
            return sceneManager;
        }
        public void SetSceneManager(SceneManager sceneManager)
        {
            this.sceneManager = sceneManager;
        }

        public void SetWindow(GameWindow gameWindow)
        {
            this.gameWindow = gameWindow;
        }

        public GameObjectManager GetObjectManager()
        {
            return gameObjectManager;
        }

        public void AddScript(SceneScript script)
        {
            scriptsManager.AddScript(script);
        }
    }
}
