using Microsoft.Xna.Framework.Graphics;
using SharedLibrary.UIComponents.Base;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using Microsoft.Xna.Framework.Content;
using SharedLibrary.Ultility;
using System.ComponentModel;
using System.Reflection.Emit;
using Autofac.Core;

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

        private UiBorder border;

        private readonly Texture2D background;
        public FrameLayout Layout { get; set; } = FrameLayout.RELATIVE;

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

            border = new UiBorder(device, this);
        }

        public UiFrame(Rectangle rect, GraphicsDevice device) : base(rect) 
        {
            background = new Texture2D(device, 1, 1);
            background.SetData(new Color[] { Color.Gray });

            border = new UiBorder(device, this);
        }

        public UiFrame(Point location, Point size, GraphicsDevice device, Color bgColor) : base(location, size)
        {
            background = new Texture2D(device, 1, 1);
            background.SetData(new Color[] { bgColor });

            border = new UiBorder(device, this);
        }

        public UiFrame(Rectangle rect, GraphicsDevice device, Color bgColor) : base(rect)
        {
            background = new Texture2D(device, 1, 1);
            background.SetData(new Color[] { bgColor });

            border = new UiBorder(device, this);
        }

        public UiFrame(Point location, Point size, Texture2D image): base(location, size)
        {
            background = image;

            border = new UiBorder(image.GraphicsDevice, this);
        }

        public UiFrame(Rectangle rect, Texture2D image) : base(rect)
        {
            background = image;

            border = new UiBorder(image.GraphicsDevice, this);
        }

        //Draw and update
        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(background, _rect.Location.ToVector2(), null, Color.White, Rotation, new Vector2(0, 0), _rect.Size.ToVector2(), SpriteEffects.None, LayerDepth);
            border.Draw(batch);
            foreach (var uiObject in uiObjects)
            {
                uiObject.Draw(batch);
               //Debug.WriteLine(string.Format("#_{0}", uiObject.Position));
            }
        }

        public override void Update(GameTime gameTime)
        {

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

        public int BorderThickness
        {
            get => this.border.Thickness;
            set
            {
                this.border.Thickness = value;
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

        public static UiFrame LoadFromXml(XmlNode xmlNode, 
            ContentManager contentManager, 
            GraphicsDevice graphicsDevice, 
            List<AbstractUiObject> frameObjects)
        {
            XmlAttributeCollection attributeCollection = xmlNode.Attributes;

            string name = XMLHelper.GetAttribute(attributeCollection, "name", "Frame", false);
            string color = XMLHelper.GetAttribute(attributeCollection, "color", "FFFFFF", false);

            int x = int.Parse(XMLHelper.GetAttribute(attributeCollection, "x", "0", false));
            int y = int.Parse(XMLHelper.GetAttribute(attributeCollection, "y", "0", false));
            int width = int.Parse(XMLHelper.GetAttribute(attributeCollection, "width", "0", false));
            int height = int.Parse(XMLHelper.GetAttribute(attributeCollection, "height", "0", false));
            string layout = XMLHelper.GetAttribute(attributeCollection, "layout", "relative", false);

            float depth = float.Parse(XMLHelper.GetAttribute(attributeCollection, "depth", "0", false));
            UiFrame result = new UiFrame(new Rectangle(x, y, width, height), graphicsDevice, ColorHelper.GetColorFrom(color));
            result.Name = name;
            result.LayerDepth = depth;

            switch (layout)
            {
                case "none":
                    result.Layout = FrameLayout.DEFAULT;
                    break;
                case "relative":
                    result.Layout = FrameLayout.RELATIVE; break;
                case "vertical_list":
                    result.Layout = FrameLayout.VERTICAL_LIST; break;
                case "horizontal_list":
                    result.Layout = FrameLayout.HORIZONTAL_LIST; break;
                default:
                    result.Layout = FrameLayout.RELATIVE;
                    break;
            }
            foreach (AbstractUiObject uiObject in frameObjects)
            {
                result.AddComponent(uiObject);
            }
            return result;
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
