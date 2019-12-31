using Microsoft.Xna.Framework;
using PlatformerEngine.Timers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PlatformerEngine.Sprites.Enemies.Strategies
{
    /// <summary>
    /// TODO: Maybe make a base class for strategies (see if there is any common code later)
    /// </summary>
    public class RageStrategy : IMoveStrategy
    {
        GameTimer attackTimer;
        public void Move(Enemy enemy)
        {
            //Stand
            if (attackTimer == null)
            {
                enemy.MoveableBodyState = Physics.MoveableBodyStates.Attacking;
                attackTimer = new GameTimer(6f);
                attackTimer.OnTimedEvent = (o, e) => DestroyTimer();
            }
            else
            {
                enemy.MoveableBodyState = Physics.MoveableBodyStates.Idle;
            }
            //Move left
            //enemy.Velocity = new Vector2(-4f, 0);
        }

        private void DestroyTimer()
        {
            attackTimer = null;
        }

        public void Update(GameTime gameTime)
        {
            attackTimer?.Update(gameTime);
        }
    }
}
