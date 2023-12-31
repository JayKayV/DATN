using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharedLibrary.Input;
using SharedLibrary.UIComponents.Base;
using SharedLibrary.UIComponents.Events.EventArgs;
using SharedLibrary.UIComponents.Interfaces;
using SharedLibrary.Ultility;
using System;
using System.Diagnostics;
using System.Xml;

namespace SharedLibrary.UIComponents
{
    public class ImageButton : AbstractButton<ImageLabel, ImageButton>, IClickable, IHoverable
    {
        public ImageButton(GraphicsDevice graphicsDevice, ImageLabel label, Point location, Color bgColor) : base(graphicsDevice, label, location, bgColor)
        { }

        public ImageButton(GraphicsDevice graphicsDevice, ImageLabel label, Point location, Color bgColor, int[] paddings)
            : base(graphicsDevice, label, location, bgColor, paddings)
        { }

        public Texture2D Image
        {
            get => _label.Image;
            set {

                if (!_label.Size.Equals(value.Bounds.Size))
                {
                    _rect.Size = new Point(
                        value.Bounds.Size.X + paddings[1] + paddings[3] + BorderThickness * 2, 
                        value.Bounds.Size.Y + paddings[0] + paddings[2] + BorderThickness * 2);
                }
                _label.Image = value;
                if (originalStyle != null)
                    originalStyle.Image = value;
                if (clickedStyle != null)
                    clickedStyle.Image = value;
                if (hoverStyle != null) 
                    hoverStyle.Image = value;
            }
        }

        public static ImageButton LoadFromXml(XmlNode node, ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            XmlAttributeCollection attributeCollection = node.Attributes;
            string name = XMLHelper.GetAttribute(attributeCollection, "name", "imageButton", false);
            string bgColor = XMLHelper.GetAttribute(attributeCollection, "bgColor", "000000", false);
            string src = XMLHelper.GetAttribute(attributeCollection, "src");
            float scale = float.Parse(XMLHelper.GetAttribute(attributeCollection, "scale", "1", false));
            int x = int.Parse(XMLHelper.GetAttribute(attributeCollection, "x", "0", false));
            int y = int.Parse(XMLHelper.GetAttribute(attributeCollection, "y", "0", false));
            float depth = float.Parse(XMLHelper.GetAttribute(attributeCollection, "depth", "0", false));

            ImageLabel _label = new ImageLabel(contentManager.Load<Texture2D>(src), new Point(0, 0));
            ImageButton _button = new ImageButton(graphicsDevice, _label, new Point(x, y), ColorHelper.GetColorFrom(bgColor));
            _button.Name = name;
            _button.Scale = scale;
            _button.LayerDepth = depth;
            return _button;
        }
    }
}
