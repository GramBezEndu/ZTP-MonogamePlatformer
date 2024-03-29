﻿namespace PlatformerEngine.Sprites.PlayerClasses
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PlatformerEngine.Input;
    using PlatformerEngine.Physics;

    public abstract class PlayerEffect : IPlayer
    {
        private readonly IPlayer player;

        public PlayerEffect(IPlayer p)
        {
            player = p;
        }

        public event EventHandler OnLoseHeart
        {
            add => player.OnLoseHeart += value;
            remove => player.OnLoseHeart -= value;
        }

        public abstract string Name { get; }

        public bool Hidden
        {
            get => player.Hidden;
            set => player.Hidden = value;
        }

        public Vector2 Position
        {
            get => player.Position;
            set => player.Position = value;
        }

        public Vector2 Scale
        {
            get => player.Scale;
            set => player.Scale = value;
        }

        public Point Size => player.Size;

        public Rectangle Rectangle => player.Rectangle;

        public Color Color
        {
            get => player.Color;
            set => player.Color = value;
        }

        public SpriteEffects SpriteEffects
        {
            get => player.SpriteEffects;
            set => player.SpriteEffects = value;
        }

        public MoveableBodyStates MoveableBodyState
        {
            get => player.MoveableBodyState;
            set => player.MoveableBodyState = value;
        }

        public Vector2 Velocity
        {
            get => player.Velocity;
            set => player.Velocity = value;
        }

        public InputManager InputManager => player.InputManager;

        public List<Sprite> HeartSprites => player.HeartSprites;

        public SpriteAnimated SwordSlash => player.SwordSlash;

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            player.Draw(gameTime, spriteBatch);
        }

        public IPlayer GetDecorated()
        {
            return player;
        }

        public virtual bool CanJump()
        {
            return player.CanJump();
        }

        public virtual void Jump()
        {
            player.Jump();
        }

        public virtual void Attack()
        {
            player.Attack();
        }

        public virtual void MoveLeft()
        {
            player.MoveLeft();
        }

        public virtual void MoveRight()
        {
            player.MoveRight();
        }

        public virtual void PrepareMove(GameTime gameTime)
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

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
        }

        public virtual void LoseHeart()
        {
            player.LoseHeart();
        }

        public void Die()
        {
            player.Die();
        }
    }
}
