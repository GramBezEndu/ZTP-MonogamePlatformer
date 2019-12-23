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
    public class Boletus : Enemy
    {
        private MoveableBodyStates moveableBodyState;

        public Boletus(Texture2D spritesheet, Dictionary<string, Rectangle> map, IMoveStrategy strategy) : base(spritesheet, map, strategy)
        {
            AddAnimation("Idle", new SpriteSheetAnimationData(new int[] { 0 }));
            AddAnimation("Walk", new SpriteSheetAnimationData(new int[] { 1 }));
            PlayAnimation("Idle");
        }

        public override MoveableBodyStates MoveableBodyState 
        { 
            get => moveableBodyState;
            set
            {
                if (moveableBodyState != value)
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
                    }
                }
            }
        }
    }
}
