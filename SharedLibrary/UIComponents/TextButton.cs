using SharedLibrary.Input;
using SharedLibrary.UIComponents.Base;
using SharedLibrary.UIComponents.Events.EventArgs;
using SharedLibrary.UIComponents.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;
using System.Xml;
using SharedLibrary.Ultility;

namespace SharedLibrary.UIComponents
{
    public class TextButton : AbstractButton<TextLabel, TextButton>, IClickable, IHoverable
    {

        public TextButton(GraphicsDevice graphicsDevice, TextLabel label, Point location, Color bgColor) : base(graphicsDevice, label, location, bgColor)
        { }

        public TextButton(GraphicsDevice graphicsDevice, TextLabel label, Point location, Color bgColor, int[] paddings) 
            : base(graphicsDevice, label, location, bgColor, paddings)
        { }

        public string Text
        {
            get => _label.Text;
            set {
                _label.Text = value;
                originalStyle._label.Text = value;
                hoverStyle._label.Text = value;
                clickedStyle._label.Text = value;
                _rect.Width = (int)(_label.Size.X * this.Scale) + paddings[1] + paddings[3] + BorderThickness * 2;

                border.SetRefObject(this);
            }
        }

        public Color TextColor
        {
            get => _label.Color;
            set
            {
                _label.Color = value;
                if (originalStyle != null)
                    originalStyle.TextColor = value;
                if (hoverStyle != null)
                    hoverStyle.TextColor = value;
                if (clickedStyle != null)
                    clickedStyle.TextColor = value;
            }
        }

        public static TextButton LoadFromXml(XmlNode node, ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            XmlAttributeCollection attributeCollection = node.Attributes;
            string name = XMLHelper.GetAttribute(attributeCollection, "name", "textLabel", false);
            string bgColor = XMLHelper.GetAttribute(attributeCollection, "bgColor", "000000", false);
            string font = XMLHelper.GetAttribute(attributeCollection, "font");
            string text = XMLHelper.GetAttribute(attributeCollection, "text", node.InnerText, false);
            string textColor = XMLHelper.GetAttribute(attributeCollection, "color", "00FFFF", false);

            float scale = float.Parse(XMLHelper.GetAttribute(attributeCollection, "scale", "1", false));
            float depth = float.Parse(XMLHelper.GetAttribute(attributeCollection, "depth", "0", false));
            int x = int.Parse(XMLHelper.GetAttribute(attributeCollection, "x", "0", false));
            int y = int.Parse(XMLHelper.GetAttribute(attributeCollection, "y", "0", false));

            TextLabel _label = new TextLabel(contentManager.Load<SpriteFont>(font), text, new Point(0, 0));
            TextButton _button = new TextButton(graphicsDevice, _label, new Point(x, y), ColorHelper.GetColorFrom(bgColor));
            _button.Name = name;
            _button.Scale = scale;
            _button.TextColor = ColorHelper.GetColorFrom(textColor);
            _button.LayerDepth = depth;
            return _button;
        }
    } 
}
