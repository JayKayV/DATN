using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharedLibrary.BaseGameObject;
using SharedLibrary.UIComponents;
using SharedLibrary.UIComponents.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SharedLibrary.Ultility
{
    internal class LoadHelper
    {
        public static List<AbstractUiObject> LoadUiObjects(XmlNodeList nodes, ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            List<AbstractUiObject> uiObjects = new List<AbstractUiObject>();
            int index = 0;
            foreach (XmlNode node in nodes)
            {
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
                    case "uiFrame":
                        XmlNodeList childNodes = node.ChildNodes;
                        List<AbstractUiObject> frameObjects = LoadUiObjects(childNodes, contentManager, graphicsDevice);
                        uiObject = UiFrame.LoadFromXml(node, contentManager, graphicsDevice, frameObjects);
                        break;
                    default:
                        Debug.WriteLine($"[WARNING]: Incorrect type or bad element while parsing node index {index}");
                        break;
                }
                uiObjects.Add(uiObject);
            }
            return uiObjects;
        }
    }
}
