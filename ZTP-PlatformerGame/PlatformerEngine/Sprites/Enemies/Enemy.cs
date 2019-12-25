using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerEngine.Physics;
using PlatformerEngine.Sprites.Enemies.Strategies;

namespace PlatformerEngine.Sprites.Enemies
{
    public abstract class Enemy : SpriteAnimated, IMoveableBody
    {
        private IMoveStrategy currentStrategy;
        protected int maxHealth = 5;
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
            //throw new NotImplementedException();
            currentStrategy.Move(this);
        }
        public override void Update(GameTime gameTime)
        {
            //Got hurt - go into rage mode
            if(currentHealth < maxHealth)
            {
                currentStrategy = new RageStrategy();
            }
            base.Update(gameTime);
        }

        public void LoseHeart()
        {
            throw new NotImplementedException();
        }
    }
}
