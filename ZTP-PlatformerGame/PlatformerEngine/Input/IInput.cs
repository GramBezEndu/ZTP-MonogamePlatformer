namespace PlatformerEngine.Input
{
    public interface IInput
    {
        bool IsPressed(InputManager input);

        bool WasJustPressed(InputManager input);
    }
}
