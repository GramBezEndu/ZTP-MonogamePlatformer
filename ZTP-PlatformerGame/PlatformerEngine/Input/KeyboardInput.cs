using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Input
{
    public class KeyboardInput : IInput
    {
        private Keys assignedKey;
        public KeyboardInput(Keys key)
        {
            assignedKey = key;
        }
        public bool IsPressed(InputManager input)
        {
            return input.CurrentKeyboardState.IsKeyDown(assignedKey);
        }

        public bool WasJustPressed(InputManager input)
        {
            if(this.IsPressed(input) && input.PreviousKeyboardState.IsKeyUp(assignedKey))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
