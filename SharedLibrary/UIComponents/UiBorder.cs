using SharedLibrary.UIComponents.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SharedLibrary.UIComponents
{
    public class UiBorder : AbstractUiObject
    {
        private Texture2D texture;
        private AbstractUiObject refObject;

        private int thickness;

        public UiBorder(GraphicsDevice graphicsDevice, AbstractUiObject _object) {
            refObject = _object;
            texture = new Texture2D(graphicsDevice, 1, 1);

            texture.SetData(new Color[] { Color.Black });
            thickness = 0;
        }

        public UiBorder(GraphicsDevice graphicsDevice, AbstractUiObject _object, Color color, int thickness) {
            refObject = _object;
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
        // t...
        // r**l
        // .**.
        // b...
        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle refRectangle = refObject.GetRectangle();
            float r = refObject.Rotation;

            double t1 = thickness * Math.Sqrt(2);
            double t2 = Math.Sqrt(thickness * thickness + refRectangle.Height * refRectangle.Height);
            const double theta1 = 5 * Math.PI / 4;
            double theta2 = Math.Acos((-thickness) / t2);
            
            Vector2 topPosition = new Vector2(
                (float)(refRectangle.X + t1 * Math.Cos(r + theta1)),
                (float)(refRectangle.Y + t1 * Math.Sin(r + theta1))
            );
            Vector2 rightSidePosition = new Vector2(
                (float)(refRectangle.X + refRectangle.Width * Math.Cos(r)),
                (float)(refRectangle.Y + refRectangle.Width * Math.Sin(r))
            );
            Vector2 bottomPosition = new Vector2(
                (float)(refRectangle.X + t2 * Math.Cos(r + theta2)), 
                (float)(refRectangle.Y + t2 * Math.Sin(r + theta2))
            );
            Vector2 leftSidePosition = new Vector2(
                (float)(refRectangle.X - thickness * Math.Cos(r)), 
                (float)(refRectangle.Y - thickness * Math.Sin(r))
            );

            Vector2 widthBorderScale = new Vector2(refRectangle.Width + thickness * 2, thickness);
            Vector2 heightBorderScale = new Vector2(thickness, refRectangle.Height);

            spriteBatch.Draw(texture, topPosition, null, Color.White, refObject.Rotation, new Vector2(0, 0), widthBorderScale, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, rightSidePosition, null, Color.White, refObject.Rotation, new Vector2(0, 0), heightBorderScale, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, bottomPosition, null, Color.White, refObject.Rotation, new Vector2(0, 0), widthBorderScale, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, leftSidePosition, null, Color.White, refObject.Rotation, new Vector2(0, 0), heightBorderScale, SpriteEffects.None, 0f);
        }

        public UiBorder Clone(AbstractUiObject cloneObject)
        {
            UiBorder clone = this.MemberwiseClone() as UiBorder;

            clone.texture = new Texture2D(this.texture.GraphicsDevice, 1, 1);
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

        public void SetRefObject(AbstractUiObject _object)
        {
            this.refObject = _object;
        }
    }
}
