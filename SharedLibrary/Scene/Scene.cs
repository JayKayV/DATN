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
using System.Reflection;

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
                    case "textButton":
                        uiObject = TextButton.LoadFromXml(node, contentManager, graphicsDevice); 
                        break;
                    case "textLabel":
                        uiObject = TextLabel.LoadFromXml(node, contentManager);
                        break;
                    case "textArea":
                        uiObject = TextArea.LoadFromXml(node, contentManager, graphicsDevice);
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
            //Debug.WriteLine("check..." + gameObjectManager.GetGameObjects().Count());
            int index = 0;
            foreach (XmlNode node in nodes)
            {
                index += 1;
                SceneScript? script = SceneLoader.LoadScript(node.InnerText);
                if (script == null)
                    Debug.WriteLine($"[WARNING]: Unable to parse script while parsing script index {index}, please check your path");
                else 
                    scriptsManager.AddScript(script);
            }
            scriptsManager.Load();
        }
        public void Update(GameTime gameTime)
        {
            foreach (var gObject in gameObjectManager.GetGameObjects())
            {
                gObject.Update(gameTime);
                if (!sceneManager.ContinueUpdate)
                    return;
            }
            scriptsManager.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var gObject in gameObjectManager.GetGameObjects())
            {
                gObject.Draw(spriteBatch);
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

        public ContentManager GetContentManger()
        {
            return sceneManager.ContentManager;
        }

        public GraphicsDevice GetGraphicsDevice()
        {
            return sceneManager.GraphicsDevice;
        }

        public void AddScript(SceneScript script)
        {
            scriptsManager.AddScript(script);
        }

        public void QuitGame()
        {
            this.Destroy();
            sceneManager.QuitGame();
        }
    }
}
