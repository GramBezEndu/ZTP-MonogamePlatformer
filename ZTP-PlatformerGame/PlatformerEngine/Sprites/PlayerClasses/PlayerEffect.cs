using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using PlatformerEngine.Input;
using PlatformerEngine.Physics;

namespace PlatformerEngine.Sprites.PlayerClasses
{
    public abstract class PlayerEffect : IPlayer
    {
        public abstract string Name { get; }
        private readonly IPlayer player;        

        public PlayerEffect(IPlayer p)
        {
            player = p;
        }

        public bool Hidden 
        { 
            get { return player.Hidden; } 
            set { player.Hidden = value; } 
        }
        public Vector2 Position 
        {
            get { return player.Position; }
            set { player.Position = value; }
        }
        public Vector2 Scale 
        {
            get { return player.Scale; }
            set { player.Scale = value; }
        }

        public Point Size
        {
            get { return player.Size; }
        }

        public Rectangle Rectangle
        {
            get { return player.Rectangle; }
        }

        public Color Color 
        { 
            get { return player.Color; }
            set { player.Color = value; } 
        }
        public SpriteEffects SpriteEffects 
        {
            get { return player.SpriteEffects; }
            set { player.SpriteEffects = value; }
        }
        public MoveableBodyStates MoveableBodyState 
        {
            get { return player.MoveableBodyState; }
            set { player.MoveableBodyState = value; }
        }
        public Vector2 Velocity 
        {
            get { return player.Velocity; }
            set { player.Velocity = value; }
        }

        public InputManager InputManager
        {
            get { return player.InputManager; }
        }

        public List<Sprite> HeartSprites
        {
            get { return player.HeartSprites; }
        }

        public SpriteAnimated SwordSlash
        {
            get { return player.SwordSlash; }
        }

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
                        Jump();
                }
                if (InputManager.ActionWasPressed("Attack"))
                {
                    Attack();
                }
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            player.Update(gameTime);
        }

        public virtual void LoseHeart()
        {
            player.LoseHeart();
        }
    }
}
