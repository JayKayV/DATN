using Microsoft.Xna.Framework;

namespace SharedLibrary.Animation
{
    delegate void UpdateAnimation(ref Rectangle rect);
    public class FuncAnimation : Animation
    {
        private Rectangle rect;
        private UpdateAnimation updateAnimation = (ref Rectangle rect) =>
        {

        };

        public FuncAnimation(Rectangle rect)
        {
            this.rect = rect;
        }

        public void Update() {
            updateAnimation(ref this.rect);
        }
    }
}
