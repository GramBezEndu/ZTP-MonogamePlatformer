namespace PlatformerEngine.Input
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class InputManager : IComponent
    {
        private static readonly InputManager inputManager = new InputManager();

        private readonly Dictionary<string, IInput> keybindings = new Dictionary<string, IInput>();

        public static InputManager GetInputManager => inputManager;

        private InputManager()
        {
            keybindings = new Dictionary<string, IInput>
            {
                { "MoveUp", new KeyboardInput(Keys.W) },
                { "MoveRight", new KeyboardInput(Keys.D) },
                { "MoveLeft", new KeyboardInput(Keys.A) },
                { "MoveDown", new KeyboardInput(Keys.S) },
                { "Accept", new KeyboardInput(Keys.Space) },
                { "Back", new KeyboardInput(Keys.Escape) },
                { "Attack", new KeyboardInput(Keys.Space) },
            };
        }

        public KeyboardState CurrentKeyboardState { get; private set; }

        public KeyboardState PreviousKeyboardState { get; private set; }

        public MouseState CurrentMouseState { get; private set; }

        public MouseState PreviousMouseState { get; private set; }

        public bool ActionIsPressed(string actionName)
        {
            return keybindings[actionName].IsPressed(this);
        }

        public bool ActionWasJustPressed(string actionName)
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
