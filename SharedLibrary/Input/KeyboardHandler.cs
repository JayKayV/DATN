using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace SharedLibrary.Input
{
    public class KeyboardHandler
    {
        private KeyboardState prevKeyboardState;
        private KeyboardState curKeyboardState;

        public static readonly KeyboardHandler Instance = new KeyboardHandler();
        private KeyboardHandler() { 
            curKeyboardState = Keyboard.GetState();
        }

        public static void Update()
        {
            Instance.prevKeyboardState = Instance.curKeyboardState;
            Instance.curKeyboardState = Keyboard.GetState();
        }

        public bool IsKeyDown(Keys key) { 
            return curKeyboardState.IsKeyDown(key);
        }

        public bool HasClickedDown(Keys key)
        {
            return curKeyboardState.IsKeyDown(key) && prevKeyboardState.IsKeyUp(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return curKeyboardState.IsKeyUp(key);
        }

        public bool HasClickedUp(Keys key)
        {
            return curKeyboardState.IsKeyUp(key) && prevKeyboardState.IsKeyDown(key);
        }

        public List<Keys> GetPressedKeys()
        {
            return new List<Keys>(curKeyboardState.GetPressedKeys());
        }

        public List<Keys> GetLastKeysUp() 
        {
            List<Keys> keys = new List<Keys>();
            foreach (Keys downKey in prevKeyboardState.GetPressedKeys())
            {
                if (curKeyboardState.IsKeyUp(downKey))
                    keys.Add(downKey);
            }
            return keys;
        }

        public List<Keys> GetLastKeysDown()
        {
            List<Keys> keys = new List<Keys>();
            foreach (Keys downKey in curKeyboardState.GetPressedKeys())
            {
                if (prevKeyboardState.IsKeyUp(downKey))
                    keys.Add(downKey);
            }
            return keys;
        }
    }
}
