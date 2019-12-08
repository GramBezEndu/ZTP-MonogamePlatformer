using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Input
{
    public enum MouseButton
    {
        LeftButton,
        RightButton
    }
    public class MouseInput : IInput
    {
        private MouseButton mouseButton;
        public MouseInput(MouseButton mouseBtn)
        {
            mouseButton = mouseBtn;
        }
        public bool IsPressed(InputManager im)
        {
            throw new NotImplementedException();
        }

        public bool WasJustPressed(InputManager im)
        {
            throw new NotImplementedException();
        }
    }
}
