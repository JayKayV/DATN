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
            XmlDocument xmlDocument = XMLHelper.LoadScene(path);
            return xmlDocument;
        }

        private static SceneScript? LoadScriptFromAssembly(string name)
        {
            Assembly assembly = Assembly.LoadFrom(Assembly.GetExecutingAssembly().Location);

            if (assembly == null)
                throw new ArgumentNullException("Error loading assembly...");

            Type ex = assembly.GetType(name);

            if (ex == null)
                throw new ArgumentNullException(String.Format("Class {0} doesn\'t exists!", name));
            if (!ex.IsAssignableTo(typeof(SceneScript)))
                throw new ArgumentException(String.Format("Class {0} must be extended from ScriptScene class!", name));

            return Activator.CreateInstance(ex) as SceneScript;
        }
    }
}
