namespace PlatformerEngine.Sprites.Enemies.Strategies
{
    using Microsoft.Xna.Framework;
    using PlatformerEngine.Timers;

    /// <summary>
    /// Rage strategy -> attack then stand.
    /// </summary>
    public class RageStrategy : IMoveStrategy
    {
        /// <summary>
        /// Time from START of first attack to START of second attack.
        /// </summary>
        private readonly double timeBetweenAttackAction = 1.5;

        private GameTimer attackTimer;

        public void Move(Enemy enemy)
        {
            // attackTimer does not exist -> we can perform another attack
            if (attackTimer == null)
            {
                // Set state to attacking -> enemy will start attacking (if he is not attacking already)
                enemy.MoveableBodyState = Physics.MoveableBodyStates.Attacking;

                // Create timer
                attackTimer = new GameTimer(timeBetweenAttackAction);
                attackTimer.OnTimedEvent += (o, e) => DestroyTimer();
            }
            else
            {
                enemy.MoveableBodyState = Physics.MoveableBodyStates.Idle;
            }
        }

        public void Update(GameTime gameTime)
        {
            attackTimer?.Update(gameTime);
        }

        private void DestroyTimer()
        {
            attackTimer = null;
        }
    }
}
