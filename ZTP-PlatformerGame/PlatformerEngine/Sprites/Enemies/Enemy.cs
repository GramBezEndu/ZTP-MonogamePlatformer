namespace PlatformerEngine.Sprites.Enemies
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PlatformerEngine.Physics;
    using PlatformerEngine.Sprites.Enemies.Strategies;
    using PlatformerEngine.Timers;

    public abstract class Enemy : SpriteAnimated, IMoveableBody
    {
        private IMoveStrategy currentStrategy;

        private GameTimer healthTimer;

        private int maxHealth = 2;

        private int currentHealth;

        public Enemy(Texture2D spritesheet, Dictionary<string, Rectangle> map, IMoveStrategy strategy)
            : base(spritesheet, map)
        {
            currentStrategy = strategy;
            currentHealth = maxHealth;
        }

        public abstract MoveableBodyStates MoveableBodyState { get; set; }

        public Vector2 Velocity { get; set; }

        public EventHandler OnLoseHeart { get; set; }

        protected bool Attacking { get; set; }

        public virtual void PrepareMove(GameTime gameTime)
        {
            // Update strategy and make move
            currentStrategy.Update(gameTime);
            currentStrategy.Move(this);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Hidden)
            {
                healthTimer?.Update(gameTime);

                // Got hurt - go into rage mode (if we are not already in rage mode)
                if (currentHealth < maxHealth)
                {
                    if (currentStrategy.GetType() != typeof(RageStrategy))
                    {
                        currentStrategy = new RageStrategy();
                    }
                }

                if (Attacking)
                {
                    MoveableBodyState = MoveableBodyStates.Attacking;
                }

                base.Update(gameTime);
            }
        }

        public void LoseHeart()
        {
            if (MoveableBodyState != MoveableBodyStates.Dead)
            {
                if (healthTimer == null)
                {
                    OnLoseHeart?.Invoke(this, new EventArgs());
                    currentHealth -= 1;
                    if (currentHealth <= 0)
                    {
                        MoveableBodyState = MoveableBodyStates.Dead;
                    }

                    healthTimer = new GameTimer(0.5f);
                    healthTimer.OnTimedEvent += (o, e) => DestroyTimer();
                }
            }
        }

        private void DestroyTimer()
        {
            healthTimer = null;
        }
    }
}
