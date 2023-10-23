using SharedLibrary.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace SharedLibrary.BaseGameObject
{
    public class GameObjectManager
    {
        private Scene.Scene scene;
        private List<GameObject> gameObjects;
        public GameObjectManager(Scene.Scene scene)
        {
            this.gameObjects = new List<GameObject>();
            this.scene = scene;
        }

        public void Update(GameTime gameTime)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.UpdateIfEnabled(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var gameObject in gameObjects)
            {
                gameObject.DrawIfVisible(spriteBatch);
            }
        }

        //CRUD
        //Read
        public GameObject GetGameObjectByName(string name)
        {
            GameObject? obj = gameObjects.Find(go => go.Name == name);
            if (obj == null)
                throw new ArgumentException("Object name doesn\'t exist");
            return obj;
        }

        public GameObject GetGameObjectByPred(Predicate<GameObject> predicate)
        {
            GameObject? obj = gameObjects.Find(predicate);
            if (obj == null)
                throw new ArgumentException("Object name doesn\'t exist");
            return obj;
        }

        public IEnumerable<GameObject> GetGameObjects()
        {
            return gameObjects;
        }
        public IEnumerable<GameObject> GetGameObjects(Predicate<GameObject> predicate)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (predicate(gameObject)) yield return gameObject;
            }
        }
        public IEnumerable<GameObject> GetGameObjects(Type type, Predicate<GameObject> predicate)
        { 
            if (!type.IsSubclassOf(typeof(GameObject)))
                throw new ArgumentException(string.Format("Type {0} is not subclass of GameObject", type.Name));
            foreach (GameObject gameObject in gameObjects.Where(o => o.GetType() == type))
            {
                if (predicate(gameObject)) yield return gameObject;
            }
        }

        public bool Exists(string name)
        {
            GameObject? obj = gameObjects.Find(go => go.Name == name);
            return obj != null;
        }  

        //Create
        public void AddGameObject(GameObject gameObject)
        {
            string objName = gameObject.Name;
            if (Exists(objName))
                Debug.WriteLine(String.Format("[Warning]: {0} already exists", objName));
            gameObjects.Add(gameObject);
        }
        public void AddGameObjects(params GameObject[] gameObjects)
        {
            foreach (var gameObject in gameObjects)
            {
                AddGameObject(gameObject);
            }
        }

        //Delete
        public void RemoveGameObject(GameObject gameObject)
        {
            gameObjects.Remove(gameObject);
        }
        public void RemoveGameObjectByPred(Predicate<GameObject> predicate)
        {
            gameObjects.RemoveAll(predicate);
        }
        public void RemoveGameObjectByPred<T>(Predicate<T> predicate) where T : GameObject
        {
            gameObjects.RemoveAll((Predicate<GameObject>) predicate);
        }

        public void Clear()
        {
            gameObjects.Clear();
        }

        //Update
        public void ToggleVisible(Predicate<GameObject> predicate)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (predicate(gameObject))
                {
                    gameObject.Visible = !gameObject.Visible;
                }
            }
        }

        public void ToggleVisible<T>(Predicate<T> predicate) where T : GameObject
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.GetType().IsAssignableFrom(typeof(T)))
                    if (predicate((T)gameObject))
                    {
                        gameObject.Visible = !gameObject.Visible;
                    }
            }
        }

        public void ToggleEnabled(Predicate<GameObject> predicate)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (predicate(gameObject))
                {
                    gameObject.Enabled = !gameObject.Enabled;
                }
            }
        }

        public void ToggleEnabled<T>(Predicate<T> predicate) where T : GameObject
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.GetType().IsAssignableFrom(typeof(T)))
                    if (predicate((T)gameObject))
                    {
                        gameObject.Enabled = !gameObject.Enabled;
                    }
            }
        }

        public void SetEnabled(Predicate<GameObject> predicate, bool enabled)
        {
            foreach (GameObject gameObject in gameObjects)
            {
                if (predicate(gameObject))
                {
                    gameObject.Enabled = enabled;
                }
            }
        }

        //Load from XML
    }
}
