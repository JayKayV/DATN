﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharedLibrary.UIComponents.Base;
using SharedLibrary.UIComponents.Events.EventArgs;
using SharedLibrary.UIComponents.Interfaces;
using System;
using SharedLibrary.Input;

namespace SharedLibrary.UIComponents
{
    public class TextLabel : UiObject<TextLabel>, IClickable, IHoverable
    {
        private SpriteFont _font;
        private string _text;
        private Color color = Color.Aqua;

        #nullable enable
        public EventHandler<OnClickArgs>? OnClick;
        
        #nullable enable
        public EventHandler? OnHover;

        #nullable enable
        public EventHandler? OnMouseLeave;

        //constructors
        public TextLabel(SpriteFont font, string text) : base()
        {
            this._text = text;
            this._font = font;

            this._rect.Size = font.MeasureString(text).ToPoint();

            base.SetStyles();
        }
        public TextLabel(SpriteFont font, string text, Point location) : base(location) {
            this._text = text;
            this._font = font;

            this._rect.Size = font.MeasureString(text).ToPoint();

            base.SetStyles();
        }

        public TextLabel(SpriteFont font, string text, Point location, Color color) : base(location)
        {
            this._text = text;
            this._font = font;
            this.color = color;

            this._rect.Size = font.MeasureString(text).ToPoint();

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
        public Color Color 
        { 
            get => color; 
            set { 
                this.color = value;
                if (originalStyle != null)
                    originalStyle.Color = value;
                if (hoverStyle != null)
                    hoverStyle.Color = value;
                if (clickedStyle != null)
                    clickedStyle.Color = value;
            } 
        }

        public string Text 
        { 
            get => _text; 
            set
            {
                this._text = value;
                this._rect.Width = (int) _font.MeasureString(value).X;
                if (originalStyle != null)
                    originalStyle.Text = value;
                if (hoverStyle != null)
                    hoverStyle.Text = value;
                if (clickedStyle != null)
                    clickedStyle.Text = value;
            }
        }

        //Draw and update
        public override void Draw(SpriteBatch batch)
        {
            if (this._visible) 
                batch.DrawString(_font, _text, _rect.Location.ToVector2(), color);
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

        public void RaiseOnMouseLeaveEvent()
        {
            OnMouseLeave?.Invoke(this, EventArgs.Empty);
        }
        //-------------------------------------------------------------------

        //styles
        protected override void ApplyStyle(TextLabel style)
        {
            this._text = style.Text;
            this.color = style.Color;
            this._rect = new Rectangle(style._rect.Location, style._rect.Size);
        }
    }
}