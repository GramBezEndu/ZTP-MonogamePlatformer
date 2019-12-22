using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Physics
{
    public enum MoveableBodyStates
    {
        Idle,
        WalkRight,
        WalkLeft,
        InAirRight,
        InAirLeft,
        InAir
    }

    public interface IMoveableBody : IDrawableComponent
    {
        MoveableBodyStates MoveableBodyState { get; set; }
        Vector2 Velocity { get; set; }
        void PrepareMove(GameTime gameTime);
    }
}
