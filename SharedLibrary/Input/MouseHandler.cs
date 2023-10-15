using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace SharedLibrary.Input
{
    public enum MouseButton : short
    {
        LEFT_BUTTON,
        MIDDLE_BUTTON,
        RIGHT_BUTTON,
    }

    //singleton design
    public class MouseHandler
    {
        private MouseState PrevMouseState;
        private MouseState CurrentMouseState;

        public static readonly MouseHandler Instance = new MouseHandler();

        private MouseHandler()
        {
            CurrentMouseState = Mouse.GetState();
        }

        public static void Update()
        {
            Instance.PrevMouseState = Instance.CurrentMouseState;
            Instance.CurrentMouseState = Mouse.GetState();
        }

        public bool CheckClickState(MouseState mouseState, MouseButton mouseButton)
        {
            if (mouseButton == MouseButton.RIGHT_BUTTON)
            {
                return mouseState.RightButton.HasFlag(ButtonState.Pressed);
            }
            else if (mouseButton == MouseButton.LEFT_BUTTON)
            {
                return mouseState.LeftButton.HasFlag(ButtonState.Pressed);
            }
            return false;
        }

        public bool IsClicked(MouseButton mouseButton)
        {
            return CheckClickState(CurrentMouseState, mouseButton);
        }

        public bool HasClicked(MouseButton mouseButton)
        {
            return IsClicked(mouseButton) && !CheckClickState(PrevMouseState, mouseButton);
        }

        public static Point GetPosition()
        {
            return Instance.CurrentMouseState.Position;
        }

        public static MouseState GetState()
        {
            return Instance.CurrentMouseState;
        }
    }
}
