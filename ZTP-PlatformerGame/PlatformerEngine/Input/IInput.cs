using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Input
{
    public interface IInput
    {
        bool IsPressed(InputManager input);
        bool WasJustPressed(InputManager input);
    }
}
