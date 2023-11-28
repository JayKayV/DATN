using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharedLibrary.UIComponents.Interfaces;
using SharedLibrary.BaseGameObject;
using SharedLibrary.UIComponents.Events.EventArgs;
using SharedLibrary.Animation;
using System.Xml;

namespace SharedLibrary.UIComponents.Base
{
    public abstract class AbstractUiObject : GameObject, IUiObject
    {
        protected Rectangle _rect;
        protected Rectangle? _bound;
        protected Animator animator;

        protected float _scale = 1f;
        protected float _rotation = 0f;
        protected Color colorMask = Color.White;

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
        public Rectangle GetRectangle()
        {
            return _rect;
        }

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

        public virtual float Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                _rect.Height = (int)(_rect.Height * _scale);
                _rect.Width = (int)(_rect.Width * _scale);
            }
        }

        /// <summary>
        ///     Rotation works clockwise
        /// </summary>
        public virtual float Rotation
        {
            get => _rotation;
            set => _rotation = value;
        }

        public virtual Color ColorMask
        {
            get => colorMask;
            set => colorMask = value;
        }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        public Animator GetAnimator()
        {
            return animator;
        }

        public Rectangle? Bound
        {
            get => _bound;
            set => _bound = value;
        }
    }
}
