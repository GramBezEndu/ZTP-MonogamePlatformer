using Microsoft.Xna.Framework;
using PlatformerEngine.Timers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Sprites.Enemies.Strategies
{
    /// <summary>
    /// Standard strategy = patrol
    /// </summary>
    public class StandardStrategy : IMoveStrategy
    {
        GameTimer moveTimer;
        bool movingLeft = true;
        public StandardStrategy(bool firstMoveToLeft = true, double timePatrolInOneDirection = 2.5)
        {
            movingLeft = firstMoveToLeft;
            //For how long enemy will move in one direction
            moveTimer = new GameTimer(timePatrolInOneDirection)
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
                enemy.Velocity = new Vector2(-2f, 0);
            else
                enemy.Velocity = new Vector2(2f, 0);
        }

        public void Update(GameTime gameTime)
        {
            moveTimer?.Update(gameTime);
        }
    }
}
