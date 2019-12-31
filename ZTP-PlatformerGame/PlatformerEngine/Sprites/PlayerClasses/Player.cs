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
using PlatformerEngine.Timers;

namespace PlatformerEngine.Sprites.PlayerClasses
{
    public class Player : SpriteAnimated, IPlayer
    {
        private GameTimer healthTimer;
        private int maxHealth = 3;
        private int currentHealth;
        public Vector2 VelocityConst = new Vector2(5f, 2f);
        private InputManager inputManager;
        public List<Sprite> heartSprites = new List<Sprite>();
        private SpriteAnimated swordSlash;

        public Player(Texture2D spritesheet, Dictionary<string, Rectangle> map, InputManager im, Texture2D heartTexture, SpriteAnimated swordSlashObj) : base(spritesheet, map)
        {
            swordSlash = swordSlashObj;
            CreateHeartsManager(heartTexture);
            inputManager = im;
            AddAnimation("Idle", new SpriteSheetAnimationData(new int[] { 0 }));
            AddAnimation("Walk", new SpriteSheetAnimationData(new int[] { 4, 5 }));
            AddAnimation("InAir", new SpriteSheetAnimationData(new int[] { 1 }));
            PlayAnimation("Idle");
        }

        private void CreateHeartsManager(Texture2D heartTexture)
        {
            currentHealth = maxHealth;
            var pos = new Vector2(0, 30);
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
                                PlayAnimation("Idle");
                                break;
                            case MoveableBodyStates.Attacking:
                                Attack();
                                break;
                        }
                    }
                }
            }
        }
        public Vector2 Velocity { get; set; }

        public InputManager InputManager { get { return inputManager; } }

        public List<Sprite> HeartSprites { get { return heartSprites; } }

        public SpriteAnimated SwordSlash { get { return swordSlash; } }

        public EventHandler OnLoseHeart { get; set; }

        public override void Update(GameTime gameTime)
        {
            if(MoveableBodyState != MoveableBodyStates.Dead)
            {
                base.Update(gameTime);
                if (swordSlash.Hidden != true)
                    MoveableBodyState = MoveableBodyStates.Attacking;
                healthTimer?.Update(gameTime);
                
                swordSlash.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            if(!Hidden)
            {
                swordSlash.Draw(gameTime, spriteBatch);
            }
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
            Velocity = new Vector2(Velocity.X, -18f);
        }

        public void PrepareMove(GameTime gameTime)
        {
            if(MoveableBodyState != MoveableBodyStates.Dead && MoveableBodyState != MoveableBodyStates.Attacking)
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
                    if (CanJump())
                        Jump();
                }
                if(InputManager.ActionWasPressed("Attack"))
                {
                    Attack();
                }
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
            MoveableBodyState = MoveableBodyStates.Attacking;
            PlayAnimation("Idle");
            if (this.SpriteEffects == SpriteEffects.FlipHorizontally)
            {
                swordSlash.Position = new Vector2(this.Position.X - swordSlash.Size.X, this.Position.Y + this.Size.Y / 3);
                swordSlash.SpriteEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                swordSlash.Position = new Vector2(this.Position.X + this.Size.X, this.Position.Y + this.Size.Y / 3);
                swordSlash.SpriteEffects = SpriteEffects.None;
            }
            swordSlash.Hidden = false;
            swordSlash.PlayAnimation("Slash",
                onCompleted: () => {
                    swordSlash.Hidden = true;
                });
        }

        public void LoseHeart()
        {
            //if(MoveableBodyState != MoveableBodyStates.Dead)
            //{
            //    if (healthTimer == null)
            //    {
            //        OnLoseHeart?.Invoke(this, new EventArgs());
            //        currentHealth -= 1;
            //        for (int i = currentHealth; i < maxHealth; i++)
            //        {
            //            heartSprites[i].Hidden = true;
            //        }
            //        if (currentHealth <= 0)
            //            MoveableBodyState = MoveableBodyStates.Dead;
            //        healthTimer = new GameTimer(2f)
            //        {
            //            OnTimedEvent = (o, e) => DestroyTimer()
            //        };
            //    }
            //}
        }

        private void DestroyTimer()
        {
            healthTimer = null;
        }
    }
}
