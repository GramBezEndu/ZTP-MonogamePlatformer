namespace PlatformerEngine.Sprites
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MonoGame.Extended.Animations.SpriteSheets;
    using PlatformerEngine.Physics;
    using PlatformerEngine.Sprites.Enemies;

    public enum Direction
    {
        Left,
        Right,
    }

    public class Boletus : Enemy
    {
        private MoveableBodyStates moveableBodyState;

        private Direction dashDirection = Direction.Left;

        public Boletus(Texture2D spritesheet, Dictionary<string, Rectangle> map, IMoveStrategy strategy)
            : base(spritesheet, map, strategy)
        {
            AddAnimation("Attack", new SpriteSheetAnimationData(new int[] { 1, 2 }, frameDuration: 0.3f));
            AddAnimation("Idle", new SpriteSheetAnimationData(new int[] { 0 }));
            AddAnimation("Walk", new SpriteSheetAnimationData(new int[] { 1, 2 }, frameDuration: 0.3f));
            PlayAnimation("Idle");
        }

        public override MoveableBodyStates MoveableBodyState
        {
            get => moveableBodyState;
            set
            {
                if (value == MoveableBodyStates.Dead)
                {
                    Hidden = true;
                }

                // Allow change state if not attacking
                else if (moveableBodyState != value && !Attacking)
                {
                    moveableBodyState = value;
                    switch (value)
                    {
                    case MoveableBodyStates.Idle:
                        PlayAnimation("Idle");
                        break;
                    case MoveableBodyStates.WalkRight:
                        SpriteEffects = SpriteEffects.None;
                        PlayAnimation("Walk");
                        break;
                    case MoveableBodyStates.WalkLeft:
                        SpriteEffects = SpriteEffects.FlipHorizontally;
                        PlayAnimation("Walk");
                        break;
                    case MoveableBodyStates.InAirRight:
                        SpriteEffects = SpriteEffects.None;
                        PlayAnimation("Walk");
                        break;
                    case MoveableBodyStates.InAirLeft:
                        SpriteEffects = SpriteEffects.FlipHorizontally;
                        PlayAnimation("Walk");
                        break;
                    case MoveableBodyStates.InAir:
                        PlayAnimation("Walk");
                        break;
                    case MoveableBodyStates.Attacking:
                        Attacking = true;
                        Velocity = new Vector2(Velocity.X, -10f);
                        if (dashDirection == Direction.Left)
                        {
                            SpriteEffects = SpriteEffects.FlipHorizontally;
                        }
                        else
                        {
                            SpriteEffects = SpriteEffects.None;
                        }

                        PlayAnimation("Attack", onCompleted: () =>
                        {
                            Attacking = false;

                            // change next dash direction
                            if (dashDirection == Direction.Left)
                            {
                                dashDirection = Direction.Right;
                            }
                            else
                            {
                                dashDirection = Direction.Left;
                            }
                        });
                        break;
                    }
                }
            }
        }

        public override void PrepareMove(GameTime gameTime)
        {
            base.PrepareMove(gameTime);
            if (Attacking)
            {
                if (dashDirection == Direction.Left)
                {
                    Velocity = new Vector2(-9f, Velocity.Y);
                }
                else
                {
                    Velocity = new Vector2(9f, Velocity.Y);
                }
            }
        }
    }
}
