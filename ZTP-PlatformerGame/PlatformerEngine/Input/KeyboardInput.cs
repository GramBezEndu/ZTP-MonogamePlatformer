namespace PlatformerEngine.Input
{
    using Microsoft.Xna.Framework.Input;

    public class KeyboardInput : IInput
    {
        private readonly Keys assignedKey;

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
            if (IsPressed(input) && input.PreviousKeyboardState.IsKeyUp(assignedKey))
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
