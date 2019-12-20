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
            return IsDown(im.CurrentMouseState, this.mouseButton);
        }

        public bool WasJustPressed(InputManager im)
        {
            if (IsDown(im.CurrentMouseState, this.mouseButton) && IsUp(im.PreviousMouseState, this.mouseButton))
                return true;
            else
                return false;
        }

        private bool IsDown(MouseState mouseState, MouseButton mouseButton)
        {
            switch(mouseButton)
            {
                case MouseButton.LeftButton:
                    return mouseState.LeftButton == ButtonState.Pressed;
                case MouseButton.RightButton:
                    return mouseState.RightButton == ButtonState.Pressed;
                default:
                    throw new ArgumentException("Invalid mouse button");
            }
        }

        private bool IsUp(MouseState mouseState, MouseButton mouseButton)
        {
            switch (mouseButton)
            {
                case MouseButton.LeftButton:
                    return mouseState.LeftButton == ButtonState.Released;
                case MouseButton.RightButton:
                    return mouseState.RightButton == ButtonState.Released;
                default:
                    throw new ArgumentException("Invalid mouse button");
            }
        }
    }
}
