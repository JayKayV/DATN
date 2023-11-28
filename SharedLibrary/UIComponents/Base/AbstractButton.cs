using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharedLibrary.Input;
using SharedLibrary.UIComponents.Events.EventArgs;
using SharedLibrary.UIComponents.Interfaces;
using System;
using System.Diagnostics;

namespace SharedLibrary.UIComponents.Base
{
    public abstract class AbstractButton<T, U> : UiObject<U> where T : AbstractUiObject
                                                             where U : AbstractButton<T, U>  //allow us to use uiobject template
                                                , IClickable, IHoverable 
    {
        protected T _label;
        protected UiBorder border;
        protected Texture2D backgroundTexture;

        protected bool enableBackground = true;

        /// <summary>
        ///     top->right->bottom->left
        /// </summary>
        protected int[] paddings = new int[4] { 5, 5, 5, 5 };
        protected bool Fixed { get; set; } = false;

        public event EventHandler<OnClickArgs>? OnClick;

        public event EventHandler? OnHover;

        protected AbstractButton(GraphicsDevice graphicsDevice, T label, Point location, Color bgColor)
        {
            _label = label;

            this._rect = new Rectangle();
            this._rect.Location = location;
            this._rect.Size = _label.Size + new Point(paddings[1] + paddings[3], paddings[0] + paddings[2]);

            this._label.Position = location + new Point(paddings[3], paddings[0]);

            this.backgroundTexture = new Texture2D(graphicsDevice, 1, 1);
            this.backgroundTexture.SetData(new Color[] { bgColor });

            this.border = new UiBorder(graphicsDevice, this);
            base.SetStyles();
        }

        protected AbstractButton(GraphicsDevice graphicsDevice, T label, Point location, Color bgColor, int[] paddings)
        {
            if (paddings.Length < 4)
                throw new ArgumentException("Length of paddings not enough, have to be at least 4 numbers");

            this.paddings = paddings;
            _label = label;

            this._rect = new Rectangle();
            this._rect.Location = location;
            this._rect.Size = _label.Size + new Point(paddings[1] + paddings[3], paddings[0] + paddings[2]);

            this._label.Position = location + new Point(paddings[3], paddings[0]);

            this.backgroundTexture = new Texture2D(graphicsDevice, 1, 1);
            this.backgroundTexture.SetData(new Color[] { bgColor });

            this.border = new UiBorder(graphicsDevice, this);
            base.SetStyles();
        }

        public override void Update(GameTime gameTime)
        {
            Point mousePosition = MouseHandler.GetPosition();
            //rotate mouse position
            int dx = _rect.X - mousePosition.X, dy = _rect.Y - mousePosition.Y;
            double dist = Math.Sqrt(dx * dx + dy * dy);
            double theta;
            Point _rotatedMousePosition;
            if (dist != 0)
            {
                if (dx != 0)
                    theta = Math.Acos(-dx / dist);
                else
                    theta = ((_rect.Y < mousePosition.Y) ? 1 : 3) * Math.PI / 2;
                _rotatedMousePosition = new Point(
                    (int)(_rect.X + dist * Math.Cos(theta + _rotation)),
                    (int)(_rect.Y + dist * Math.Sin(theta + _rotation))
                );
                /*if (this.Name == "otherButton")
                    Debug.WriteLine(string.Format("{0}: {1}, {2}, {3}", Name, _rect.Size.ToString(), mousePosition.ToString(), _rotatedMousePosition.ToString()));*/
            }
            else
                _rotatedMousePosition = mousePosition;
            if (this._rect.Contains(_rotatedMousePosition))
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (enableBackground)
            {
                spriteBatch.Draw(this.backgroundTexture, this._rect.Location.ToVector2(), null, Color.White, _rotation, new Vector2(0, 0), _rect.Size.ToVector2(), SpriteEffects.None, 0f);
                this.border.Draw(spriteBatch);
            }
            this._label.Draw(spriteBatch);

            /*if (this.Name == "gameSceneButton")
                Debug.WriteLine($"{this.Name}: {this._label.Position}, {this._rect.Location}");*/
        }

        protected override void ApplyStyle(U style)
        {
            border = style.border;
            this.backgroundTexture = style.backgroundTexture;
            this._label = style._label;
        }

        //Getter, Setter
        /// <summary>
        ///     <para>For getter, paddings return int[4] and goes from top->right->bottom->left</para>
        ///     <para>For setter, the int[] have to length of at least 4, and goes from the same order as getter</para>
        /// </summary>
        public int[] Paddings
        {
            get => paddings;
            set
            {
                if (value.Length < 4)
                    throw new InsufficientMemoryException();
                paddings = value;

                if (!Fixed)
                    _rect.Size = _label.Size + new Point(paddings[1] + paddings[3] + BorderThickness * 2, paddings[0] + paddings[2] + BorderThickness * 2);
                else
                    _label.Bound = new Rectangle(
                        _rect.X + paddings[3],
                        _rect.Y + paddings[0],
                        _rect.Width - paddings[1] - paddings[3],
                        _rect.Height - paddings[0] - paddings[2]);
                RecalculateLabelPosition();

                if (originalStyle != null)
                    originalStyle.Paddings = value;
                if (hoverStyle != null)
                    hoverStyle.Paddings = value;
                if (clickedStyle != null)
                    clickedStyle.Paddings = value;
            }
        }

        public int PaddingTop
        {
            get => paddings[0];
            set
            {
                paddings[0] = value;
                _rect.Height = paddings[0] + paddings[2] + _label.Size.Y + BorderThickness * 2;

                RecalculateLabelPosition();

                if (originalStyle != null)
                    originalStyle.PaddingTop = value;
                if (hoverStyle != null)
                    hoverStyle.PaddingTop = value;
                if (clickedStyle != null)
                    clickedStyle.PaddingTop = value;
            }
        }

        public int PaddingRight
        {
            get => paddings[1];
            set
            {
                paddings[1] = value;
                _rect.Width = paddings[1] + paddings[3] + _label.Size.X + BorderThickness * 2;

                if (originalStyle != null)
                    originalStyle.PaddingRight = value;
                if (hoverStyle != null)
                    hoverStyle.PaddingRight = value;
                if (clickedStyle != null)
                    clickedStyle.PaddingRight = value;
            }
        }

        public int PaddingBottom
        {
            get => paddings[2];
            set
            {
                paddings[2] = value;
                _rect.Height = paddings[0] + paddings[2] + _label.Size.Y + BorderThickness * 2;

                if (originalStyle != null)
                    originalStyle.PaddingBottom = value;
                if (hoverStyle != null)
                    hoverStyle.PaddingBottom = value;
                if (clickedStyle != null)
                    clickedStyle.PaddingBottom = value;
            }
        }

        public int PaddingLeft
        {
            get => paddings[3];
            set
            {
                paddings[3] = value;
                _rect.Width = paddings[1] + paddings[3] + _label.Size.X + BorderThickness * 2;

                RecalculateLabelPosition();
                border.SetRefObject(this);

                if (originalStyle != null)
                    originalStyle.PaddingLeft = value;
                if (hoverStyle != null)
                    hoverStyle.PaddingLeft = value;
                if (clickedStyle != null)
                    clickedStyle.PaddingLeft = value;
            }
        }

        public Color BackgroundColor
        {
            get
            {
                Color[] bgColor = new Color[1];
                backgroundTexture.GetData<Color>(bgColor);
                return bgColor[0];
            }
            set
            {
                backgroundTexture.SetData(new Color[] { value });
                if (originalStyle != null)
                    originalStyle.BackgroundColor = value;
                if (hoverStyle != null)
                    hoverStyle.BackgroundColor = value;
                if (clickedStyle != null)
                    clickedStyle.BackgroundColor = value;
            }
        }

        public override Point Position
        {
            get => base.Position;
            set
            {
                var diff = this._label.Position - _rect.Location;
                this._rect.Location = value;
                this._label.Position = value + diff;
                border.SetRefObject(this);
                if (originalStyle != null)
                    originalStyle.Position = value;
                if (hoverStyle != null)
                    hoverStyle.Position = value;
                if (clickedStyle != null)
                    clickedStyle.Position = value;
            }
        }

        public int BorderThickness
        {
            get => this.border.Thickness;
            set
            {
                this.border.Thickness = value;

                this._rect.Size = _label.Size + new Point(paddings[1] + paddings[3], paddings[0] + paddings[2]) + new Point(value * 2, value * 2);
                RecalculateLabelPosition();
                border.SetRefObject(this);
                if (originalStyle != null)
                    originalStyle.BorderThickness = value;
                if (hoverStyle != null)
                    hoverStyle.BorderThickness = value;
                if (clickedStyle != null)
                    clickedStyle.BorderThickness = value;
            }
        }

        public Color BorderColor
        {
            get => this.border.Color;
            set
            {
                this.border.Color = value;
                if (originalStyle != null)
                    originalStyle.border.Color = value;
                if (hoverStyle != null)
                    hoverStyle.border.Color = value;
                if (clickedStyle != null)
                    clickedStyle.border.Color = value;
            }
        }

        public override U Clone()
        {
            U clone = base.Clone();

            clone._rect = this._rect;
            clone.backgroundTexture = new Texture2D(this.backgroundTexture.GraphicsDevice, 1, 1);
            clone.backgroundTexture.SetData(new Color[] { BackgroundColor });

            clone._label = clone._label.Clone() as T;
            clone.border = new UiBorder(this.backgroundTexture.GraphicsDevice, this, BorderColor, BorderThickness);

            return clone;
        }

        public bool EnableBackground
        {
            get => enableBackground;
            set
            {
                enableBackground = value;
                if (originalStyle != null)
                    originalStyle.EnableBackground = value;
                if (hoverStyle != null)
                    hoverStyle.EnableBackground = value;
                if (clickedStyle != null)
                    clickedStyle.EnableBackground = value;
            }
        }

        public override float Rotation {
            get => _rotation;
            set
            {
                _rotation = value;
                RecalculateLabelPosition();
                _label.Rotation = value;
                if (originalStyle != null)
                    originalStyle.Rotation = value;
                if (hoverStyle != null)
                    hoverStyle.Rotation = value;
                if (clickedStyle != null)
                    clickedStyle.Rotation = value;
            }
        }

        public override float Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                _label.Scale = value;
                _rect.Size = _label.Size + new Point(paddings[1] + paddings[3] + BorderThickness * 2, paddings[0] + paddings[2] + BorderThickness * 2);
                if (originalStyle != null)
                    originalStyle.Scale = value;
                if (hoverStyle != null)
                    hoverStyle.Scale = value;
                if (clickedStyle != null)
                    clickedStyle.Scale = value;
            }
        }

        private void RecalculateLabelPosition()
        {
            int l = paddings[3] + BorderThickness, t = paddings[0] + BorderThickness;
            double dist = Math.Sqrt(l * l + t * t);
            double theta = Math.Acos(l / dist);
            _label.Position = new Point(
                (int)(_rect.X + dist * Math.Cos(_rotation + theta)),
                (int)(_rect.Y + dist * Math.Sin(_rotation + theta))
            );
        }
    }
}

