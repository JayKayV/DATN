using SharedLibrary.UIComponents.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SharedLibrary.UIComponents
{
    public class UiBorder : AbstractUiObject
    {
        private Texture2D texture;
        private Rectangle refRectangle;

        private int thickness;

        public UiBorder(GraphicsDevice graphicsDevice, Rectangle rectangle) {
            refRectangle = rectangle;
            texture = new Texture2D(graphicsDevice, 1, 1);

            texture.SetData(new Color[] { Color.Black });
            thickness = 0;
        }

        public UiBorder(GraphicsDevice graphicsDevice, Rectangle rectangle, Color color, int thickness) {
            refRectangle = rectangle;
            texture = new Texture2D(graphicsDevice, 1, 1);

            texture.SetData(new Color[] { color });
            this.thickness = thickness;
        }

        public override void Update(GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        ///     Draw lines clockwise from top->left
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle topLineRect = new Rectangle(refRectangle.Location, new Point(refRectangle.Width, thickness));
            Rectangle rightLineRect = new Rectangle(
                new Point(refRectangle.Right - thickness, refRectangle.Top + thickness), 
                new Point(thickness, refRectangle.Height - thickness));
            Rectangle bottomLineRect = new Rectangle(
                    new Point(refRectangle.X, refRectangle.Bottom - thickness),
                    new Point(refRectangle.Width, thickness)
                );
            Rectangle leftLineRect = new Rectangle(
                    new Point(refRectangle.X, refRectangle.Top + thickness),
                    new Point(thickness, refRectangle.Height - thickness)
                );

            spriteBatch.Draw(texture, topLineRect, Color.White);
            spriteBatch.Draw(texture, rightLineRect, Color.White);
            spriteBatch.Draw(texture, bottomLineRect, Color.White);
            spriteBatch.Draw(texture, leftLineRect, Color.White);
        }

        public UiBorder Clone()
        {
            UiBorder clone = this.MemberwiseClone() as UiBorder;

            clone.texture = new Texture2D(this.texture.GraphicsDevice, refRectangle.Width, refRectangle.Height);
            clone.Color = this.Color;
            return clone;
        }

        public Color Color
        {
            get
            {
                Color[] _color = new Color[1];
                texture.GetData<Color>(_color);
                return _color[0];
            }
            set
            {
                texture.SetData(new Color[] { value });
            }
        }

        public int Thickness
        {
            get => thickness; 
            set
            {
                this.thickness = value;
            }
        }

        public void SetRefRect(Rectangle rect)
        {
            this.refRectangle = rect;
        }
    }
}
