using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Animations.SpriteSheets;
using PlatformerEngine.Physics;
using PlatformerEngine.Sprites.Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Sprites
{
    public enum Direction
    {
        Left,
        Right
    };
    public class Boletus : Enemy
    {
        private MoveableBodyStates moveableBodyState;
        private Direction dashDirection = Direction.Left;

        public Boletus(Texture2D spritesheet, Dictionary<string, Rectangle> map, IMoveStrategy strategy) : base(spritesheet, map, strategy)
        {
            AddAnimation("Attack", new SpriteSheetAnimationData(new int[] { 0, 1 }, frameDuration: 0.3f, isLooping: false));
            AddAnimation("Idle", new SpriteSheetAnimationData(new int[] { 2 }));
            AddAnimation("Walk", new SpriteSheetAnimationData(new int[] { 0, 1 }, frameDuration: 0.3f));
            PlayAnimation("Idle");
        }

        public override void PrepareMove(GameTime gameTime)
        {
            base.PrepareMove(gameTime);
            if (attacking)
            {
                if(dashDirection == Direction.Left)
                {
                    Velocity = new Vector2(9f, Velocity.Y);
                }
                else
                {
                    Velocity = new Vector2(-9f, Velocity.Y);
                }
            }
        }

        public override MoveableBodyStates MoveableBodyState 
        { 
            get => moveableBodyState;
            set
            {
                //Change to death state always
                if(value == MoveableBodyStates.Dead)
                {
                    Hidden = true;
                }
                //Allow change state if not attacking
                else if (moveableBodyState != value && !attacking)
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
                            //PlayAnimation("InAir");
                            PlayAnimation("Walk");
                            break;
                        case MoveableBodyStates.InAirLeft:
                            SpriteEffects = SpriteEffects.FlipHorizontally;
                            //PlayAnimation("InAir");
                            PlayAnimation("Walk");
                            break;
                        case MoveableBodyStates.InAir:
                            //PlayAnimation("InAir");
                            PlayAnimation("Walk");
                            break;
                        //case MoveableBodyStates.Dead:
                        //    Hidden = true;
                        //    break;
                        case MoveableBodyStates.Attacking:
                            attacking = true;
                            PlayAnimation("Attack", onCompleted: () =>
                            {
                                attacking = false;
                                //change next dash direction
                                if (dashDirection == Direction.Left)
                                    dashDirection = Direction.Right;
                                else
                                    dashDirection = Direction.Left;
                            });
                            break;
                    }
                }
            }
        }
    }
}
