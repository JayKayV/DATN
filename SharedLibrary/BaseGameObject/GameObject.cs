using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharedLibrary.BaseGameObject
{
    public abstract class GameObject
    {
        protected string name;
        protected HashSet<string> tags = new HashSet<string>();
        protected bool _visible = true;
        protected bool _enabled = true;

        public int DrawOrder { get; set; } = 0;
        public int UpdateOrder { get; set; } = 0;

        #nullable enable
        public event EventHandler<bool>? OnVisibleChanged;

        #nullable enable
        public event EventHandler<bool>? OnEnabledChanged;

        public event EventHandler? OnDestroyed;

        public GameObject()
        {
            name = "GameObject";
        }
        public GameObject(string name)
        {
            this.name = name;
        }
        public string Name { get; set; }
        public bool Visible { 
            get { return _visible; } 
            set {
                if (_visible != value)
                    OnVisibleChanged?.Invoke(this, value);
                _visible = value; 
            } 
        }
        public bool Enabled { 
            get { return _enabled; } 
            set { 
                if (_enabled != value)
                    OnEnabledChanged?.Invoke(this, value);
                _enabled = value; 
            } 
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);

        public void DestroySelf()
        {
            Destroy();
            OnDestroyed?.Invoke(this, null);
        }

        public virtual void Destroy()
        {

        }

        public void UpdateIfEnabled(GameTime gameTime)
        {
            if (_enabled)
                Update(gameTime);
        }

        public void DrawIfVisible(SpriteBatch spriteBatch)
        {
            if (_visible)
                Draw(spriteBatch);
        }

        public bool HasTag(string tag)
        {
            return tags.Contains(tag);
        }

        public void AddTag(string tag)
        {
            if (tags.Contains(tag))
                throw new ArgumentException(string.Format("Tag {0} already exists in object"));
            tags.Add(tag);
        }

        public bool RemoveTag(string tag)
        {
            return tags.Remove(tag);
        }
    }
}
