using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharedLibrary.UIComponents.Interfaces;
using SharedLibrary.BaseGameObject;
using SharedLibrary.UIComponents.Events.EventArgs;
using SharedLibrary.Animation;

namespace SharedLibrary.UIComponents.Base
{
    public abstract class AbstractUiObject : GameObject, IUiObject
    {
        protected Rectangle _rect;
        protected Animator animator;

        public AbstractUiObject()
        {
            _rect = new Rectangle();
            animator = new Animator(_rect);
        }

        public AbstractUiObject(Point location)
        {
            _rect = new Rectangle();
            _rect.Location = location;

            animator = new Animator(_rect);
        }

        public AbstractUiObject(Rectangle rectangle)
        {
            _rect = rectangle;

            animator = new Animator(_rect);
        }

        public AbstractUiObject(Point location, Point size)
        {
            _rect = new Rectangle(location, size);

            animator = new Animator(_rect);
        }

        //public abstract void Update();

        public virtual Point Position
        {
            get => _rect.Location; 
            set => _rect.Location = value;
        }

        public virtual Point Size
        {
            get => _rect.Size;
            set => _rect.Size = value;
        }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        public Animator GetAnimator()
        {
            return animator;
        }
    }
}
