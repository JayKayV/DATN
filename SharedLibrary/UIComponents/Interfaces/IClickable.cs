using SharedLibrary.UIComponents.Events.EventArgs;
using Microsoft.Xna.Framework.Input;
using System;

namespace SharedLibrary.UIComponents.Interfaces
{
    public interface IClickable
    {
        #nullable enable
        public event EventHandler<OnClickArgs>? OnClick;
        public void RaiseOnClickEvent(MouseState mouseState) { }
    }
}
