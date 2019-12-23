using PlatformerEngine.Input;
using PlatformerEngine.Physics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Sprites.PlayerClasses
{
    public interface IPlayer : IDrawableComponent, IMoveableBody
    {
        InputManager InputManager { get; }
        void MoveLeft();
        void MoveRight();
        void Jump();

        IPlayer GetDecorated();
    }
}
