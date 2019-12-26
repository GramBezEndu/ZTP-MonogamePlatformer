using PlatformerEngine.Input;
using PlatformerEngine.Physics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Sprites.PlayerClasses
{
    public interface IPlayer : IDrawableComponent, IMoveableBody
    {
        List<Sprite> HeartSprites { get; }
        InputManager InputManager { get; }

        SpriteAnimated SwordSlash { get; }
        void MoveLeft();
        void MoveRight();
        bool CanJump();
        void Jump();
        void Attack();
        IPlayer GetDecorated();
    }
}
