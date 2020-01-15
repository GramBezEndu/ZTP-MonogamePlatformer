using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Input
{
    public class InputManager : IComponent
    {
        private static readonly InputManager inputManager = new InputManager();

        public KeyboardState CurrentKeyboardState;
        public KeyboardState PreviousKeyboardState;
        public MouseState CurrentMouseState;
        public MouseState PreviousMouseState;
        private Dictionary<string, IInput> keybindings = new Dictionary<string, IInput>();
        public static InputManager GetInputManager => inputManager;

        private InputManager()
        {
            keybindings = new Dictionary<string, IInput>
            {
                {"MoveUp", new KeyboardInput(Keys.W)},
                {"MoveRight", new KeyboardInput(Keys.D) },
                {"MoveLeft", new KeyboardInput(Keys.A) },
                {"MoveDown", new KeyboardInput(Keys.S)},
                {"Accept", new KeyboardInput(Keys.Enter) },
                {"Back", new KeyboardInput(Keys.Escape)},
                {"Attack", new KeyboardInput(Keys.Space) },
                //{"Attack", new MouseInput(MouseButton.LeftButton) },
            };
        }

        public bool ActionIsPressed(string actionName)
        {
            return keybindings[actionName].IsPressed(this);
        }

        public bool ActionWasPressed(string actionName)
        {
            return keybindings[actionName].WasJustPressed(this);
        }

        public void Update(GameTime gameTime)
        {
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();

            PreviousMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }
    }

}
