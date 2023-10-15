using Microsoft.Xna.Framework.Input;

namespace SharedLibrary.UIComponents.Events.EventArgs
{
    public class OnClickArgs : System.EventArgs
    {
        public MouseState MouseState { get; set; }

        public OnClickArgs(MouseState mouseState)
        {
            MouseState = mouseState;
        }
    }
}
