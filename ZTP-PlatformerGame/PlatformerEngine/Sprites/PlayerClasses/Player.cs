namespace PlatformerEngine.Sprites.PlayerClasses
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MonoGame.Extended.Animations.SpriteSheets;
    using PlatformerEngine.Input;
    using PlatformerEngine.Physics;
    using PlatformerEngine.Timers;

    public class Player : SpriteAnimated, IPlayer
    {
        private readonly int maxHealth = 3;

        private readonly InputManager inputManager;

        private readonly SpriteAnimated swordSlash;

        private readonly Vector2 movementSpeed = new Vector2(4f, -18f);

        private readonly List<Sprite> heartSprites = new List<Sprite>();

        private MoveableBodyStates moveableBodyState;

        private GameTimer healthTimer;

        private int currentHealth;

        public Player(Texture2D spritesheet, Dictionary<string, Rectangle> map, InputManager im, Texture2D heartTexture, SpriteAnimated swordSlashObj)
            : base(spritesheet, map)
        {
            swordSlash = swordSlashObj;
            CreateHeartsManager(heartTexture);
            inputManager = im;
            AddAnimation("Attack", new SpriteSheetAnimationData(new int[] { 0, 1, 2 }, frameDuration: 0.15f));
            AddAnimation("Idle", new SpriteSheetAnimationData(new int[] { 3 }));
            AddAnimation("Walk", new SpriteSheetAnimationData(new int[] { 4, 5, 6, 7 }));
            AddAnimation("InAir", new SpriteSheetAnimationData(new int[] { 3 }));
            PlayAnimation("Idle");
        }

        public event EventHandler OnLoseHeart;

        public MoveableBodyStates MoveableBodyState
        {
            get => moveableBodyState;
            set
            {
                SetNewBodyState(value);
            }
        }

        public Vector2 Velocity { get; set; }

        public InputManager InputManager => inputManager;

        public List<Sprite> HeartSprites => heartSprites;

        public SpriteAnimated SwordSlash => swordSlash;

        public override void Update(GameTime gameTime)
        {
            if (MoveableBodyState != MoveableBodyStates.Dead)
            {
                base.Update(gameTime);
                healthTimer?.Update(gameTime);

                swordSlash.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            if (!Hidden)
            {
                swordSlash.Draw(gameTime, spriteBatch);
            }
        }

        public void MoveRight()
        {
            Velocity = new Vector2(movementSpeed.X, Velocity.Y);
        }

        public void MoveLeft()
        {
            Velocity = new Vector2(-movementSpeed.X, Velocity.Y);
        }

        public void Jump()
        {
            Velocity = new Vector2(Velocity.X, movementSpeed.Y);
        }

        public void PrepareMove(GameTime gameTime)
        {
            if (MoveableBodyState != MoveableBodyStates.Dead && MoveableBodyState != MoveableBodyStates.Attacking)
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
                    {
                        Jump();
                    }
                }

                if (InputManager.ActionWasJustPressed("Attack"))
                {
                    Attack();
                }
            }
        }

        public bool CanJump()
        {
            if (MoveableBodyState >= MoveableBodyStates.Idle && MoveableBodyState <= MoveableBodyStates.WalkLeft)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IPlayer GetDecorated()
        {
            return this;
        }

        public void Attack()
        {
            if (MoveableBodyState >= MoveableBodyStates.InAirRight && MoveableBodyState <= MoveableBodyStates.InAir)
            {
                return;
            }

            MoveableBodyState = MoveableBodyStates.Attacking;
            PlayAnimation("Attack", onCompleted: () =>
            {
                MoveableBodyState = MoveableBodyStates.Idle;
            });
            if (SpriteEffects == SpriteEffects.FlipHorizontally)
            {
                swordSlash.Position = new Vector2(Position.X - swordSlash.Size.X, Position.Y + (Size.Y / 3));
                swordSlash.SpriteEffects = SpriteEffects.FlipHorizontally;
            }
            else
            {
                swordSlash.Position = new Vector2(Position.X + Size.X, Position.Y + (Size.Y / 3));
                swordSlash.SpriteEffects = SpriteEffects.None;
            }

            swordSlash.Hidden = false;
            swordSlash.PlayAnimation(
                "Slash",
                onCompleted: () =>
                {
                    swordSlash.Hidden = true;
                });
        }

        public void LoseHeart()
        {
            if (MoveableBodyState != MoveableBodyStates.Dead)
            {
                if (healthTimer == null)
                {
                    OnLoseHeart?.Invoke(this, new EventArgs());
                    currentHealth -= 1;
                    for (int i = currentHealth; i < maxHealth; i++)
                    {
                        heartSprites[i].Hidden = true;
                    }

                    if (currentHealth <= 0)
                    {
                        Die();
                    }

                    healthTimer = new GameTimer(2f);
                    healthTimer.OnTimedEvent += (o, e) => DestroyTimer();
                }
            }
        }

        public void Die()
        {
            currentHealth = 0;
            MoveableBodyState = MoveableBodyStates.Dead;
        }

        private void DestroyTimer()
        {
            healthTimer = null;
        }

        private void CreateHeartsManager(Texture2D heartTexture)
        {
            currentHealth = maxHealth;
            Vector2 pos = new Vector2(0, 30);
            for (int i = 0; i < maxHealth; i++)
            {
                Sprite heart = new Sprite(heartTexture, new Vector2(1f, 1f))
                {
                    Position = pos,
                };
                heartSprites.Add(heart);
                pos = new Vector2(pos.X + heart.Size.X, pos.Y);
            }
        }

        private void SetNewBodyState(MoveableBodyStates value)
        {
            if (moveableBodyState != value)
            {
                // Do not allow changing states when player is dead
                if (moveableBodyState != MoveableBodyStates.Dead)
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
}
