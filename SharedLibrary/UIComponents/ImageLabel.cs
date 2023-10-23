using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharedLibrary.UIComponents.Base;
using SharedLibrary.UIComponents.Events.EventArgs;
using SharedLibrary.UIComponents.Interfaces;
using System;
using SharedLibrary.Input;
using System.Xml;
using SharedLibrary.Ultility;
using Microsoft.Xna.Framework.Content;

namespace SharedLibrary.UIComponents
{
    public class ImageLabel : UiObject<ImageLabel>, IClickable, IHoverable
    {
        private Texture2D _image;

        #nullable enable
        public EventHandler<OnClickArgs>? OnClick;
        
        #nullable enable
        public EventHandler? OnHover;

        #nullable enable
        public EventHandler? OnMouseLeave;

        //constructors
        public ImageLabel(Texture2D image) : base()
        {
            this._image = image;

            this._rect.Size = image.Bounds.Size;

            base.SetStyles();
        }
        public ImageLabel(Texture2D image, Point location) : base(location) {
            this._image = image;

            this._rect.Size = image.Bounds.Size;

            base.SetStyles();
        }

        event EventHandler<OnClickArgs> IClickable.OnClick
        {
            add
            {
                OnClick += value;
            }

            remove
            {
                OnClick -= value;
            }
        }

        //--------------------------------------------------------------

        //Getter and setter
        public Texture2D Image
        {
            get => _image;
            set {
                this._image = value;
                if (originalStyle != null)
                    originalStyle.Image = value;
                if (hoverStyle != null)
                    hoverStyle.Image = value;
                if (clickedStyle != null)
                    clickedStyle.Image = value;
            } 
        }

        //Draw and update
        public override void Draw(SpriteBatch batch)
        {
            if (this._visible) 
                batch.Draw(this._image, _rect, null, Color.White);
        }

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
            } else
            {
                ApplyStyle(originalStyle);
            }
        }
        //----------------------------------------------------------------

        //Events
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
        //-------------------------------------------------------------------

        //styles
        protected override void ApplyStyle(ImageLabel style)
        {
            this._image = style.Image;
        }

        public static ImageLabel LoadFromXml(XmlNode node, ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            XmlAttributeCollection attributeCollection = node.Attributes;
            string name = XMLHelper.GetAttribute(attributeCollection, "name", "imageButton", false);
            string src = XMLHelper.GetAttribute(attributeCollection, "src");
            int x = int.Parse(XMLHelper.GetAttribute(attributeCollection, "x", "0", false));
            int y = int.Parse(XMLHelper.GetAttribute(attributeCollection, "y", "0", false));

            ImageLabel _label = new ImageLabel(contentManager.Load<Texture2D>(src), new Point(x, y));
            _label.Name = name;
            return _label;
        }
    }
}
