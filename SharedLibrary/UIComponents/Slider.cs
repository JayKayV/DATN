using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharedLibrary.UIComponents.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.UIComponents
{
    public class Slider : UiObject<Slider>
    {
        private Texture2D slideTexture;
        private Texture2D handleTexture;

        private int min;
        private int max;
        private int step;   

        public Slider(Texture2D slideTexture, Texture2D handleTexture, int min, int max, int step)
        {
            this.slideTexture = slideTexture;
            this.handleTexture = handleTexture;
            this.min = min;
            this.max = max;
            this.step = step;
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        protected override void ApplyStyle(Slider style)
        {
            throw new NotImplementedException();
        }
    }
}
