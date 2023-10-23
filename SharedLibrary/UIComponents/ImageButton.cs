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
        public event EventHandler<OnClickArgs>? OnClick;

        public event EventHandler? OnHover;

        public ImageButton(GraphicsDevice graphicsDevice, ImageLabel label, Point location, Color bgColor) : base(graphicsDevice, label, location, bgColor)
        { }

        public ImageButton(GraphicsDevice graphicsDevice, ImageLabel label, Point location, Color bgColor, int[] paddings)
            : base(graphicsDevice, label, location, bgColor, paddings)
        { }

        public override void Update(GameTime gameTime)
        {
            Point mousePosition = MouseHandler.GetPosition();
            if (this._rect.Contains(mousePosition))
            {
                if (MouseHandler.Instance.HasClicked(MouseButton.LEFT_BUTTON))
                {
                    MouseState mouseState = MouseHandler.GetState();

                    if (this._rect.Contains(mousePosition))
                    {
                        RaiseOnClickEvent(mouseState);
                    }
                }
                else
                    RaiseOnHoverEvent();
            }
            else
            {
                ApplyStyle(originalStyle);
            }
        }

        protected void RaiseOnClickEvent(MouseState mouseState)
        {
            ApplyStyle(clickedStyle);
            OnClick?.Invoke(this, new OnClickArgs(mouseState));
        }

        public void RaiseOnHoverEvent()
        {
            ApplyStyle(hoverStyle);
            OnHover?.Invoke(this, EventArgs.Empty);
        }

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
            int x = int.Parse(XMLHelper.GetAttribute(attributeCollection, "x", "0", false));
            int y = int.Parse(XMLHelper.GetAttribute(attributeCollection, "y", "0", false));

            ImageLabel _label = new ImageLabel(contentManager.Load<Texture2D>(src), new Point(0, 0));
            ImageButton _button = new ImageButton(graphicsDevice, _label, new Point(x, y), ColorHelper.GetColorFrom(bgColor));
            _button.Name = name;
            return _button;
        }
    }
}
