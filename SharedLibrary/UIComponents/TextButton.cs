using SharedLibrary.Input;
using SharedLibrary.UIComponents.Base;
using SharedLibrary.UIComponents.Events.EventArgs;
using SharedLibrary.UIComponents.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace SharedLibrary.UIComponents
{
    public class TextButton : AbstractButton<TextLabel, TextButton>, IClickable, IHoverable
    {
        public event EventHandler<OnClickArgs>? OnClick;

        public event EventHandler? OnHover;

        public TextButton(GraphicsDevice graphicsDevice, TextLabel label, Point location, Color bgColor) : base(graphicsDevice, label, location, bgColor)
        { }

        public TextButton(GraphicsDevice graphicsDevice, TextLabel label, Point location, Color bgColor, int[] paddings) 
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

        public void RaiseOnMouseLeaveEvent()
        {
            ApplyStyle(originalStyle);
        }

        public string Text
        {
            get => _label.Text;
            set {
                _label.Text = value;
                _rect.Width = _label.Size.X + paddings[1] + paddings[3] + BorderThickness * 2;

                border.SetRefRect(_rect);
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
    } 
}
