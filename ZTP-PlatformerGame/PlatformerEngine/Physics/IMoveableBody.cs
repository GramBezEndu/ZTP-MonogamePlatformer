namespace PlatformerEngine.Physics
{
    using System;
    using Microsoft.Xna.Framework;

    public enum MoveableBodyStates
    {
        Idle,
        WalkRight,
        WalkLeft,
        InAirRight,
        InAirLeft,
        InAir,
        Dead,
        Attacking,
    }

    public interface IMoveableBody : IDrawableComponent
    {
        event EventHandler OnLoseHeart;

        MoveableBodyStates MoveableBodyState { get; set; }

        Vector2 Velocity { get; set; }

        void PrepareMove(GameTime gameTime);

        void LoseHeart();
    }
}
