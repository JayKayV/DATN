using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharedLibrary.UIComponents.Interfaces;
using System;
using System.Diagnostics;

namespace SharedLibrary.UIComponents.Base
{
    public abstract class AbstractButton<T, U> : UiObject<U> where T : AbstractUiObject
                                                             where U : AbstractButton<T, U> //allow us to use uiobject template
    {
        protected T _label;
        protected UiBorder border;
        protected Texture2D backgroundTexture;

        /// <summary>
        ///     top->right->bottom->left
        /// </summary>
        protected int[] paddings = new int[4] { 5, 5, 5, 5 };

        protected AbstractButton(GraphicsDevice graphicsDevice, T label, Point location, Color bgColor)
        {
            _label = label;

            this._rect = new Rectangle();
            this._rect.Location = location;
            this._rect.Size = _label.Size + new Point(paddings[1] + paddings[3], paddings[0] + paddings[2]);

            this._label.Position = location + new Point(paddings[3], paddings[0]);

            this.backgroundTexture = new Texture2D(graphicsDevice, 1, 1);
            this.backgroundTexture.SetData(new Color[] { bgColor });

            this.border = new UiBorder(graphicsDevice, this._rect);
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

            this.border = new UiBorder(graphicsDevice, this._rect);
            base.SetStyles();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.backgroundTexture, this._rect, Color.White);
            this.border.Draw(spriteBatch);
            this._label.Draw(spriteBatch);
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
                _rect.Size = _label.Size + new Point(paddings[1] + paddings[3] + BorderThickness * 2, paddings[0] + paddings[2] + BorderThickness * 2);
                border.SetRefRect(_rect);
            }
        }

        public int PaddingTop
        {
            get => paddings[0];
            set
            {
                paddings[0] = value;
                _rect.Height = paddings[0] + paddings[2] + _label.Size.Y + BorderThickness * 2;

                _label.Position = _rect.Location + new Point(this.BorderThickness + paddings[3], paddings[0] + this.BorderThickness);
                border.SetRefRect(_rect);
            }
        }

        public int PaddingRight
        {
            get => paddings[1];
            set
            {
                paddings[1] = value;
                _rect.Width = paddings[1] + paddings[3] + _label.Size.X + BorderThickness * 2;

                border.SetRefRect(_rect);
            }
        }

        public int PaddingBottom
        {
            get => paddings[2];
            set
            {
                paddings[2] = value;
                _rect.Height = paddings[0] + paddings[2] + _label.Size.Y + BorderThickness * 2;

                border.SetRefRect(_rect);
            }
        }

        public int PaddingLeft
        {
            get => paddings[3];
            set
            {
                paddings[3] = value;
                _rect.Width = paddings[1] + paddings[3] + _label.Size.X + BorderThickness * 2;

                _label.Position = _rect.Location + new Point(this.BorderThickness + paddings[3], paddings[0] + this.BorderThickness);
                border.SetRefRect(_rect);
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
                this.border.SetRefRect(this._rect);
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
                this._label.Position = this._rect.Location + new Point(paddings[3] + value, paddings[0] + value);
                border.SetRefRect(this._rect);
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
            clone.border = new UiBorder(this.backgroundTexture.GraphicsDevice, this._rect, BorderColor, BorderThickness);
            Debug.WriteLine(this._rect.ToString());
            Debug.WriteLine(clone._rect.ToString());

            return clone;
        }
    }
}

