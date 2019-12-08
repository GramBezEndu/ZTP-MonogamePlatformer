using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Input
{
    public class InputManager
    {
        private static readonly InputManager inputManager = new InputManager();

        public static InputManager GetInputManager => inputManager;

        private InputManager()
        {

        }
    }

}
