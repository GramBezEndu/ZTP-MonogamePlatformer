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
        private int maxHealth = 5;
        private int currentHealth;
        public Vector2 VelocityConst = new Vector2(5f, 2f);
        private InputManager inputManager;
        public List<Sprite> heartSprites = new List<Sprite>();

        public Player(Texture2D spritesheet, Dictionary<string, Rectangle> map, InputManager im, Texture2D heartTexture) : base(spritesheet, map)
        {
            HeartManage(heartTexture);
            inputManager = im;
            AddAnimation("Idle", new SpriteSheetAnimationData(new int[] { 0 }));
            AddAnimation("Walk", new SpriteSheetAnimationData(new int[] { 4, 5 }));
            AddAnimation("InAir", new SpriteSheetAnimationData(new int[] { 1 }));
            PlayAnimation("Idle");
        }

        private void HeartManage(Texture2D heartTexture)
        {
            currentHealth = maxHealth;
            var pos = Vector2.Zero;
            for (int i = 0; i < maxHealth; i++)
            {
                var heart = new Sprite(heartTexture, new Vector2(3f, 3f))
                {
                    Position = pos,
                };
                heartSprites.Add(heart);
                pos = new Vector2(pos.X + heart.Size.X, pos.Y);
            }
        }

        private MoveableBodyStates moveableBodyState;

        public MoveableBodyStates MoveableBodyState
        {
            get => moveableBodyState;
            set
            {
                if (moveableBodyState != value)
                {
                    //Do not allow changing states when player is dead
                    if(moveableBodyState != MoveableBodyStates.Dead)
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
                            case MoveableBodyStates.Dead:
                                break;
                        }
                    }
                }
            }
        }
        public Vector2 Velocity { get; set; }

        public InputManager InputManager { get { return inputManager; } }

        public List<Sprite> HeartSprites { get { return heartSprites; } }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //Debug.WriteLine(Position);
        }

        public void MoveRight()
        {
            Velocity = new Vector2(4f, Velocity.Y);
        }

        public void MoveLeft()
        {
            Velocity = new Vector2(-4f, Velocity.Y);
        }

        public void Jump()
        {
            Velocity = new Vector2(Velocity.X, -20f);
        }

        public void PrepareMove(GameTime gameTime)
        {
            if (InputManager.ActionIsPressed("MoveRight"))
            {
                MoveRight();
            }
            else if (InputManager.ActionIsPressed("MoveLeft"))
            {
                MoveLeft();
            }
            if (InputManager.ActionIsPressed("MoveUp"))
            {
                if(CanJump())
                    Jump();
            }
        }

        public bool CanJump()
        {
            if (MoveableBodyStates.Idle <= MoveableBodyState && MoveableBodyState <= MoveableBodyStates.WalkLeft)
                return true;
            else
                return false;
        }

        public IPlayer GetDecorated()
        {
            return this;
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void LoseHeart()
        {
            currentHealth -= 1;
            for(int i = currentHealth; i < maxHealth ; i++)
            {
                heartSprites[i].Hidden = true;
            }
            if (currentHealth <= 0)
                MoveableBodyState = MoveableBodyStates.Dead;
        }
    }
}
