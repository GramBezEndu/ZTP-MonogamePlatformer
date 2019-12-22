using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Animations.SpriteSheets;
using PlatformerEngine.Input;
using PlatformerEngine.Physics;

namespace PlatformerEngine.Sprites.PlayerClasses
{
    public class Player : SpriteAnimated, IPlayer
    {
        public Vector2 VelocityConst = new Vector2(5f, 2f);
        private InputManager inputManager;

        public Player(Texture2D spritesheet, Dictionary<string, Rectangle> map, InputManager im) : base(spritesheet, map)
        {
            inputManager = im;
            AddAnimation("Idle", new SpriteSheetAnimationData(new int[] { 0 }));
            AddAnimation("Walk", new SpriteSheetAnimationData(new int[] { 4, 5 }));
            AddAnimation("InAir", new SpriteSheetAnimationData(new int[] { 1 }));
            PlayAnimation("Idle");
        }

        private MoveableBodyStates moveableBodyState;

        public MoveableBodyStates MoveableBodyState
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
                            PlayAnimation("InAir");
                            break;
                        case MoveableBodyStates.InAirLeft:
                            SpriteEffects = SpriteEffects.FlipHorizontally;
                            PlayAnimation("InAir");
                            break;
                        case MoveableBodyStates.InAir:
                            PlayAnimation("InAir");
                            break;
                    }
                }
            }
        }
        public Vector2 Velocity { get; set; }

        public void ManagePlayerInput()
        {
            if (inputManager.ActionIsPressed("MoveRight"))
            {
                MoveRight();
            }
            else if (inputManager.ActionIsPressed("MoveLeft"))
            {
                MoveLeft();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Debug.WriteLine(Position);
        }

        private void MoveRight()
        {
            Velocity = new Vector2(5f, Velocity.Y);
        }

        private void MoveLeft()
        {
            Velocity = new Vector2(-5f, Velocity.Y);
        }

        public void PrepareMove(GameTime gameTime)
        {
            ManagePlayerInput();
        }
    }
}
