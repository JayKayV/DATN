using Microsoft.Xna.Framework.Graphics;
using SharedLibrary.UIComponents.Base;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using Microsoft.Xna.Framework.Content;

namespace SharedLibrary.UIComponents
{
    /// <summary>
    ///     Determine how uiobjects should be laid out in a frame
    /// </summary>
    public enum FrameLayout
    {
        /// <summary> Frame just contains a list of uiobject for management purpose </summary>
        DEFAULT = 0,
        /// <summary> All uiobjects's position are now related to frame </summary>
        RELATIVE,
        /// <summary> All uiobjects's position are ignored, instead it's handled by frame (in horizontal list style) </summary>
        HORIZONTAL_LIST,
        /// <summary> All uiobjects's position are ignored, instead it's handled by frame (in vertical list style) </summary>
        VERTICAL_LIST,
        /// <summary> All uiobjects's position are ignored, instead it's handled by frame (in grid style) </summary>
        GRID
    }

    public class UiFrame : UiObject<UiFrame>
    {
        private List<AbstractUiObject> uiObjects = new List<AbstractUiObject>();
        private List<Point> relativeDistances = new List<Point>();

        private readonly Texture2D background;
        public FrameLayout Layout { get; set; } = FrameLayout.DEFAULT;

        /// <summary>
        ///     Determine should the frame stretch to fit the content
        /// </summary>
        private bool _fixed = false;
        //public bool Scrollable { get; set; } = false;

        //Used for spacing between objects and between object and frame (and when Layout is list or grid style)
        public int HorizontalSpacing { get; set; } = 5;
        public int VerticalSpacing { get; set; } = 5;

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

        public UiFrame(Point location, Point size, Texture2D image): base(location, size)
        {
            background = image;
        }

        public UiFrame(Rectangle rect, Texture2D image) : base(rect)
        {
            background = image;
        }

        //Draw and update
        public override void Draw(SpriteBatch batch)
        {
            if (_visible)
            {
                batch.Draw(background, _rect.Location.ToVector2(), null, Color.White, Rotation, new Vector2(0, 0), Scale, SpriteEffects.None, 0f);
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
        /// <summary>
        ///     Note: if frameLayout is RELATIVE, uiOBject will be set related to uiFrame's location
        /// </summary>
        /// <param name="uiObject"></param>
        public void AddComponent(AbstractUiObject uiObject)
        {
            switch (Layout) {
                case FrameLayout.RELATIVE:
                    relativeDistances.Add(uiObject.Position);
                    break;
                case FrameLayout.HORIZONTAL_LIST:
                    Point NextHorRelPosition = new Point(uiObjects.Last().GetRectangle().Right + HorizontalSpacing, uiObjects.Last().Position.Y);
                    relativeDistances.Add(NextHorRelPosition);
                    break;
                case FrameLayout.VERTICAL_LIST:
                    Point NextVerRelPosition = new Point(uiObjects.Last().Position.X, uiObjects.Last().GetRectangle().Bottom + VerticalSpacing);
                    relativeDistances.Add(NextVerRelPosition);
                    break;
                case FrameLayout.GRID:
                    break;
                default:
                    break;
            }

            if (Layout != FrameLayout.DEFAULT)
                uiObject.Position = this.Position + relativeDistances.Last();
            uiObjects.Add(uiObject);
        }

        public void RemoveComponent(AbstractUiObject uiObject)
        {
            uiObjects.Remove(uiObject);
        }

        public List<AbstractUiObject> GetObjects() { return uiObjects; }


        protected override void ApplyStyle(UiFrame style)
        {

        } 

        public override Point Position
        {
            get => _rect.Location;
            set
            {
                
                if (Layout != FrameLayout.DEFAULT)
                {
                    int i = 0;
                    foreach (var uiObject in uiObjects)
                    {
                        uiObject.Position = value + relativeDistances[i];
                        i++;
                    }
                }
                _rect.Location = value;
            }
        }

        public static UiFrame LoadFromXml(XmlNode xmlNode, ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            return new UiFrame(Rectangle.Empty, graphicsDevice);
        }


        public override float Scale { 
            get => base.Scale; 
            set
            {
                foreach (var uiObject in uiObjects)
                {
                    uiObject.Scale = value;
                }
            }
        }
    }
}
