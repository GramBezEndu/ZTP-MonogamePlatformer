namespace PlatformerEngine.Sprites.PlayerClasses
{
    using System.Collections.Generic;
    using PlatformerEngine.Input;
    using PlatformerEngine.Physics;

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

        void Die();

        IPlayer GetDecorated();
    }
}
