using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SharedLibrary.Animation
{
    public class Animator
    {
        private Dictionary<string, Animation> states = new Dictionary<string, Animation>();
        private string currentState;
        private int delay;

        public Animator(Rectangle rect) {
            states.Add("None", new FuncAnimation(rect));
            currentState = "None";
        }

        public void AddState(string state, Animation animation)
        {
            states.Add(state, animation);
        }

        public void RemoveState(string state)
        {
            if (state != "None")
            {
                states.Remove(state);
            }
            else
                throw new ArgumentException("State can't be \"None\"");
        }

        public void ChangeState(string state)
        {
            currentState = state;
        }

        public Animation GetState(string state)
        {
            return states[state];
        }

        public Animation GetCurrentState()
        {
            return states[currentState];
        }
    }
}
