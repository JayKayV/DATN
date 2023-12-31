using SharedLibrary.BaseGameObject;
using SharedLibrary.Ultility;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml;

namespace SharedLibrary.Scene
{
    public static class SceneLoader
    {
        public static XmlDocument LoadAll()
        {
            Debug.WriteLine(Environment.CurrentDirectory);
            string path = Path.Combine(Environment.CurrentDirectory, @"..\..\..\Config\scenes.xml");
            return LoadAll(path);
        }

        public static XmlDocument LoadAll(string path)
        {
            XmlDocument xmlDocument = XMLHelper.LoadScene(path);
            return xmlDocument;
        }

        public static XmlDocument LoadAllFromEntry()
        {
            Debug.WriteLine(Assembly.GetEntryAssembly().Location);
            string path = Path.Combine(Assembly.GetEntryAssembly().Location, @"..\Config\scenes.xml");
            return LoadAll(path);
        }

        public static SceneScript? LoadScript(string name)
        {
            Assembly assembly = Assembly.LoadFrom(Assembly.GetEntryAssembly().GetName().Name);

            if (assembly == null)
                throw new ArgumentNullException($"Error loading assembly!");

            Type ex = assembly.GetType(name);

            if (ex == null)
                throw new ArgumentNullException(string.Format("Class {0} doesn\'t exists!", name));
            if (!ex.IsAssignableTo(typeof(SceneScript)))
                throw new ArgumentException(String.Format("Class {0} must be extended from SceneScript class!", name));

            return Activator.CreateInstance(ex) as SceneScript;
        }
    }
}
