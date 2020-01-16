using Microsoft.Xna.Framework;
using PlatformerEngine.Timers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace PlatformerEngine.Sprites.Enemies.Strategies
{
    /// <summary>
    /// Rage strategy -> attack then stand
    /// </summary>
    public class RageStrategy : IMoveStrategy
    {
        private GameTimer attackTimer;
        /// <summary>
        /// Time from START of first attack to START of second attack
        /// </summary>
        private double timeBetweenAttackAction = 1.5;
        public void Move(Enemy enemy)
        {
            //attackTimer does not exist -> we can perform another attack
            if (attackTimer == null)
            {
                //Set state to attacking -> enemy will start attacking (if he is not attacking already)
                enemy.MoveableBodyState = Physics.MoveableBodyStates.Attacking;
                //Create timer
                attackTimer = new GameTimer(timeBetweenAttackAction);
                attackTimer.OnTimedEvent = (o, e) => DestroyTimer();
            }
            //stand
            else
            {
                enemy.MoveableBodyState = Physics.MoveableBodyStates.Idle;
            }
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
