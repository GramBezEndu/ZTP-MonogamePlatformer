using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerEngine.Physics;
using PlatformerEngine.Sprites.Enemies.Strategies;
using PlatformerEngine.Timers;

namespace PlatformerEngine.Sprites.Enemies
{
    public abstract class Enemy : SpriteAnimated, IMoveableBody
    {
        private IMoveStrategy currentStrategy;
        private GameTimer healthTimer;
        protected int maxHealth = 2;
        protected int currentHealth;

        public Enemy(Texture2D spritesheet, Dictionary<string, Rectangle> map, IMoveStrategy strategy) : base(spritesheet, map)
        {
            currentStrategy = strategy;
            currentHealth = maxHealth;
        }

        public abstract MoveableBodyStates MoveableBodyState { get; set; }
        public Vector2 Velocity { get; set; }

        public void PrepareMove(GameTime gameTime)
        {
            //Update strategy and make move
            currentStrategy.Update(gameTime);
            currentStrategy.Move(this);
        }
        public override void Update(GameTime gameTime)
        {
            if(!Hidden)
            {
                healthTimer?.Update(gameTime);
                //Got hurt - go into rage mode
                if (currentHealth < maxHealth)
                {
                    currentStrategy = new RageStrategy();
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
                    currentHealth -= 1;
                    if (currentHealth <= 0)
                        MoveableBodyState = MoveableBodyStates.Dead;
                    healthTimer = new GameTimer(2f)
                    {
                        OnTimedEvent = (o, e) => DestroyTimer()
                    };
                }
            }
        }

        private void DestroyTimer()
        {
            healthTimer = null;
        }
    }
}
