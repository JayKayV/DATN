using Microsoft.Xna.Framework.Graphics;
using SharedLibrary.UIComponents.Base;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SharedLibrary.UIComponents
{
    public class UiFrame : UiObject<UiFrame>
    {
        private List<AbstractUiObject> uiObjects = new List<AbstractUiObject>();

        private readonly Texture2D background;

        private bool relativeMode = false;

        //constructors
        public UiFrame(Point location, Point size, GraphicsDevice device) : base(location, size)
        {
            background = new Texture2D(device, 1, 1);
            background.SetData(new Color[] { Color.Gray });
        }

        public UiFrame(Rectangle rect, GraphicsDevice device) : base(rect) 
        {
            background = new Texture2D(device, 1, 1);
            background.SetData(new Color[] { Color.Gray });
        }

        public UiFrame(Point location, Point size, GraphicsDevice device, Color bgColor) : base(location, size)
        {
            background = new Texture2D(device, 1, 1);
            background.SetData(new Color[] { bgColor });
        }

        public UiFrame(Rectangle rect, GraphicsDevice device, Color bgColor) : base(rect)
        {
            background = new Texture2D(device, 1, 1);
            background.SetData(new Color[] { bgColor });
        }

        //Draw and update
        public override void Draw(SpriteBatch batch)
        {
            if (_visible)
            {
                batch.Draw(background, _rect, Color.White);
                foreach (var uiObject in uiObjects)
                {
                    uiObject.Draw(batch);
                    //Debug.WriteLine(string.Format("#_{0}", uiObject.Position));
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var uiObject in uiObjects)
            {
                uiObject.UpdateIfEnabled(gameTime);
            } 
        }

        //useful methods
        public void AddComponent(AbstractUiObject uiObject)
        {
            uiObjects.Add(uiObject);
        }

        public void AddComponent(AbstractUiObject uiObject, Point relative_position)
        {
            uiObjects.Add(uiObject);
            uiObjects.Last().Position = this._rect.Location + relative_position;
        }

        public void RemoveComponent(AbstractUiObject uiObject)
        {
            uiObjects.Remove(uiObject);
        }

        public List<AbstractUiObject> GetObjects() { return uiObjects; }


        protected override void ApplyStyle(UiFrame style)
        {

        } 

        public bool RelativeMode
        {
            get => relativeMode; 
            set => relativeMode = value;
        }

        public override Point Position
        {
            get => _rect.Location;
            set
            {
                if (relativeMode)
                {
                    foreach (var uiObject in uiObjects)
                    {
                        var diff = uiObject.Position - _rect.Location;
                        uiObject.Position = value + diff;
                    }
                }
                _rect.Location = value;
            }
        }
    }
}
