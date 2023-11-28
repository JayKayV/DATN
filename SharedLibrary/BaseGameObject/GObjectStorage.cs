using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.BaseGameObject
{
    public class GObjectStorage
    {
        private Dictionary<string, GameObject> objects = new Dictionary<string, GameObject>();
        public GObjectStorage() { }

        public GameObject GetObject(string key)
        {
            return objects[key];
        }

        public List<GameObject> GetAll()
        {
            return objects.Values.ToList();
        }

        public void AddObject(string key, GameObject obj)
        {
            objects[key] = obj;
        }

        public void RemoveObject(string key)
        {
            objects.Remove(key);
        }

        public void Clear()
        {
            objects.Clear();
        }

        public bool Contains(string key)
        {
            return objects.ContainsKey(key);
        }

        public int Count { get => objects.Count; }
    }
}
