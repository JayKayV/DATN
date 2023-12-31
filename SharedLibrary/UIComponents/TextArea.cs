using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharedLibrary.UIComponents.Base;
using SharedLibrary.UIComponents.Interfaces;
using SharedLibrary.Ultility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace SharedLibrary.UIComponents
{
    public class TextAreaHoverEvent : EventArgs
    {
        public int LineIndex { get; set; }
        public TextAreaHoverEvent(int lineIndex) { 
            LineIndex = lineIndex;
        }
    }

    public class TextArea : UiObject<TextArea>, IHoverable
    {
        private List<SpriteFont> fonts = new List<SpriteFont>();
        private List<string> lines = new List<string>();
        private List<Color> colors = new List<Color>();

        public SpriteFont? MainFont { get; set; } = null;
        public Color MainColor { get; set; } = Color.White;

        /// <summary>
        ///     <para>regionHint.X is the y-distance from region to origin (0, 0)</para>
        ///     <para>regionHint.Y is the y-size of the region</para>
        ///     <para>The text is draw on a region (a rectangle), regionHint contains Location.Y and Size.Y </para>
        /// </summary>
        private List<Point> regionHints = new List<Point>();

        private GraphicsDevice graphicsDevice;
        private RenderTarget2D renderTarget;
        private Texture2D renderTexture;

        public EventHandler<TextAreaHoverEvent>? OnHover;

        private bool wordWrap = true;
        private int lineSpacing = 2;
        private bool scrollable = false;
        private int scrollY = 0;
        private Color bgColor = Color.Transparent;

        public bool WordWrap { 
            get => wordWrap;
            set
            {
                wordWrap = value;
                UpdateRenderTexture();
            }
        }

        public int LineSpacing
        {
            get => lineSpacing;
            set
            {
                lineSpacing = value;
                if (regionHints.Count >= 2)
                {
                    for (int i = 1; i < regionHints.Count; i++)
                    {
                        Point temp = regionHints[i];
                        temp.X = regionHints[i - 1].X + regionHints[i - 1].Y + lineSpacing;
                        regionHints[i] = temp;
                    }
                    UpdateRenderTexture();
                }
            }
        }
        public bool Scrollable
        {
            get => scrollable;
            set => scrollable = value;
        }
        public int ScrollY { get; set; } = 0;
        public Color BackgroundColor {
            get => bgColor;
            set
            {
                bgColor = value;
                UpdateRenderTexture();
            }
        }

        public TextArea(GraphicsDevice graphicsDevice, Point position) : base(position, new Point(0, 0))
        {
            this.graphicsDevice = graphicsDevice;

            renderTarget = new RenderTarget2D(graphicsDevice, 0, 0);
        }

        public TextArea(GraphicsDevice graphicsDevice, Point position, Point size) : base(position, size) {
            this.graphicsDevice = graphicsDevice;

            renderTarget = new RenderTarget2D(graphicsDevice, size.X, size.Y);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(renderTexture, Position.ToVector2(), null, Color.White, Rotation, new Vector2(0, 0), Scale, SpriteEffects.None, _layerDepth);
        }

        public override void Update(GameTime gameTime)
        {

        }


        public void RaiseOnHoverEvent()
        {
            throw new System.NotImplementedException();
        }

        protected override void ApplyStyle(TextArea style)
        {
            throw new System.NotImplementedException();
        }


        //CRUD
        //----------------------------------------------------------------
        public string ReadLine(int index)
        {
            return lines[index];
        }

        public List<string> ReadAll()
        {
            return lines;
        }

        public string ReadLines(char join = ' ')
        {
            return string.Join(join, lines);
        }

        public void WriteLine(SpriteFont? font, string line, Color? color, int index = -1)
        {
            if (font == null && MainFont == null)
                throw new ArgumentNullException("There are no fonts available, make sure you provided for each line or set the main font");
            SpriteFont _font = font ?? MainFont;
            Color _color = color ?? MainColor;

            if (index < 0) {
                if (NumberOfLines > 0)
                    regionHints.Add(new Point(regionHints.Last().X + regionHints.Last().Y + LineSpacing, CalculateHeight(_font, line)));
                else
                    regionHints.Add(new Point(0, CalculateHeight(_font, line)));
                lines.Add(line);
                colors.Add(_color);
                fonts.Add(_font);
            } /*else
            {
                lines.Insert(index, line);
                colors.Insert(index, color);
                fonts.Insert(index, font);
            }*/
        }

        public void RemoveLine(int index)
        {
            lines.RemoveAt(index);
            colors.RemoveAt(index);
            fonts.RemoveAt(index);

            UpdateRenderTexture();
        }
        public int NumberOfLines { get => lines.Count; }
        //----------------------------------------------------------

        public void UpdateRenderTexture()
        {
            graphicsDevice.SetRenderTarget(renderTarget);
            SpriteBatch batch = new SpriteBatch(graphicsDevice);

            graphicsDevice.Clear(BackgroundColor);
            batch.Begin();

            for (int i = 0; i < lines.Count; i++)
            {
                DrawLineTexture(batch, fonts[i], lines[i], colors[i], regionHints[i]);
            }

            batch.End();
            graphicsDevice.SetRenderTarget(null);

            renderTexture = (Texture2D)renderTarget;
        }

        /// <summary>
        ///     Calculate the size to wrap
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int CalculateHeight(SpriteFont font, string text)
        {
            string[] words = text.Split(' ');
            int blankWidth = (int)font.MeasureString("" + ' ').X;
            int tLength = 0, tHeight = font.LineSpacing;
            foreach (string word in words)
            {
                int wordLength = (int) font.MeasureString(word).X;

                if (wordLength >= Size.X)
                {
                    tHeight += font.LineSpacing;
                    tLength = 0;
                } else if (tLength + wordLength + blankWidth >= Size.X)
                {
                    if (wordLength + blankWidth >= Size.X)
                    {
                        tHeight += font.LineSpacing * 2;
                        tLength = 0;
                    }
                    else
                    {
                        tHeight += font.LineSpacing;
                        tLength = wordLength + blankWidth;
                    }
                } else
                {
                    tLength += wordLength + blankWidth;
                }
            }
            return tHeight;
        }

        private void DrawLineTexture(SpriteBatch batch, SpriteFont font, string text, Color color, Point regionHint)
        {
            if (WordWrap)
            {
                string[] words = text.Split(' ');
                int blankWidth = (int)font.MeasureString("" + ' ').X;
                int cHeight = (int)font.MeasureString("t").Y;
                int tLength = 0, tHeight = regionHint.X;
                foreach (string word in words) { 
                    int wordLength = (int)font.MeasureString(word).X;
                    if (wordLength >= Size.X)
                    {
                        batch.DrawString(font, word, new Vector2(0, tHeight), color, Rotation, new Vector2(0, 0), Scale, SpriteEffects.None, 0f);
                        tHeight += font.LineSpacing;
                        tLength = 0;
                    }
                    else if (tLength + wordLength + blankWidth >= Size.X)
                    {
                        batch.DrawString(font, word, new Vector2(0, tHeight + font.LineSpacing), color, Rotation, new Vector2(0, 0), Scale, SpriteEffects.None, 0f);
                        if (wordLength + blankWidth >= Size.X)
                        {
                            tHeight += font.LineSpacing * 2;
                            tLength = 0;
                        } else
                        {
                            tHeight += font.LineSpacing;
                            tLength = wordLength + blankWidth;
                        }
                    } else
                    {
                        batch.DrawString(font, word, new Vector2(tLength, tHeight), color, Rotation, new Vector2(0, 0), Scale, SpriteEffects.None, 0f);
                        tLength += wordLength + blankWidth;
                    }
                    //batch.DrawString(font, word, new Vector2(tLength, tHeight + regionHint.Y), color, Rotation, new Vector2(0, 0), Scale, SpriteEffects.None, 0f);

                    //line break again incase word's length is greater than bounded size
                    /*if (tLength > Size.X)
                    {
                        tHeight += font.LineSpacing;
                        tLength = 0;
                    }*/
                }
            } else
            {
                batch.DrawString(font, text, new Vector2(0, regionHint.X), color, Rotation, new Vector2(0, 0), Scale, SpriteEffects.None, 0f);
            }
        }


        public Point MeasureSize()
        {
            Point size = new Point(0, 0);
            
            return size;
        }

        public static TextArea LoadFromXml(XmlNode node, ContentManager contentManager, GraphicsDevice graphicsDevice)
        {
            XmlAttributeCollection attributeCollection = node.Attributes;
            string name = XMLHelper.GetAttribute(attributeCollection, "name", "textArea", false);

            int x = int.Parse(XMLHelper.GetAttribute(attributeCollection, "x", "0", false));
            int y = int.Parse(XMLHelper.GetAttribute(attributeCollection, "y", "0", false));
            int width = int.Parse(XMLHelper.GetAttribute(attributeCollection, "width", "0", false));
            int height = int.Parse(XMLHelper.GetAttribute(attributeCollection, "height", "0", false));

            string mainFont = XMLHelper.GetAttribute(attributeCollection, "font", "", false);
            string mainColor = XMLHelper.GetAttribute(attributeCollection, "color", "FFFFFF", false);
            float depth = float.Parse(XMLHelper.GetAttribute(attributeCollection, "depth", "0", false));

            TextArea _area = new TextArea(graphicsDevice, new Point(x, y), new Point(width, height));
            if (mainFont != "")
                _area.MainFont = contentManager.Load<SpriteFont>(mainFont);
            _area.MainColor = ColorHelper.GetColorFrom(mainColor);
            _area.LayerDepth = depth;

            if (node.ChildNodes.Count > 0)
            {
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.NodeType == XmlNodeType.Element && childNode.Name == "line")
                    {
                        XmlAttributeCollection childNodeAttrCollection = childNode.Attributes;

                        string font = XMLHelper.GetAttribute(childNodeAttrCollection, "font", mainFont, mainFont == "");
                        string text = XMLHelper.GetAttribute(childNodeAttrCollection, "text", childNode.InnerText, false);
                        string color = XMLHelper.GetAttribute(childNodeAttrCollection, "color", mainColor, false);

                        _area.WriteLine(
                            contentManager.Load<SpriteFont>(font), 
                            text, 
                            ColorHelper.GetColorFrom(color));
                    }
                }

            }

            _area.UpdateRenderTexture();
            _area.Name = name;
            return _area;
        }
    }
}
