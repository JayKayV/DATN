using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharedLibrary.UIComponents.Interfaces;

namespace SharedLibrary.UIComponents.Base
{
    public abstract class UiObject<T> : AbstractUiObject where T : UiObject<T>
    {
        protected T originalStyle;
        protected T clickedStyle;
        protected T focusStyle;
        protected T hoverStyle;

        //constructors
        public UiObject(): base() { }
        public UiObject(Point location): base(location) { }
        public UiObject(Rectangle rectangle): base(rectangle) { }
        public UiObject(Point location, Point size): base(location, size) { }

        public virtual T Clone()
        {
            UiObject<T> clone = this.MemberwiseClone() as UiObject<T>;

            clone._rect = new Rectangle(this._rect.Location, this._rect.Size);
            clone.originalStyle = null;
            clone.clickedStyle = null;
            clone.hoverStyle = null;
            return (T) clone;
        }

        protected void SetStyles()
        {
            originalStyle = Clone();
            clickedStyle = Clone();
            hoverStyle = Clone();
        }

        public T HoverStyle
        {
            get { return hoverStyle; }
        }

        public T ClickStyle
        {
            get { return clickedStyle; }
        }

        protected abstract void ApplyStyle(T style);

        public override Point Position { 
            get => base.Position; 
            set
            {
                base.Position = value;
                if (originalStyle != null)
                    originalStyle.Position = value;
                if (hoverStyle != null)
                    hoverStyle.Position = value;
                if (clickedStyle != null)
                    clickedStyle.Position = value;
            }
        }
    }
}
