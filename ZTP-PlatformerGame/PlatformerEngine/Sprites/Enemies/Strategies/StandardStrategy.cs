using Microsoft.Xna.Framework;
using PlatformerEngine.Timers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Sprites.Enemies.Strategies
{
    /// <summary>
    /// Standard strategy -> patrol area
    /// </summary>
    public class StandardStrategy : IMoveStrategy
    {
        GameTimer moveInCurrentDirection;
        bool movingLeft = true;
        public StandardStrategy(bool firstMoveToLeft = true, double timePatrolInOneDirection = 2.5)
        {
            movingLeft = firstMoveToLeft;
            //For how long enemy will move in one direction
            moveInCurrentDirection = new GameTimer(timePatrolInOneDirection)
            {
                OnTimedEvent = (o, e) => ChangeDirection()
            };
        }
        private void ChangeDirection()
        {
            movingLeft = !movingLeft;
        }

        public void Move(Enemy enemy)
        {
            if (movingLeft)
                enemy.Velocity = new Vector2(-2f, enemy.Velocity.Y);
            else
                enemy.Velocity = new Vector2(2f, enemy.Velocity.Y);
        }

        public void Update(GameTime gameTime)
        {
            moveInCurrentDirection?.Update(gameTime);
        }
    }
}
