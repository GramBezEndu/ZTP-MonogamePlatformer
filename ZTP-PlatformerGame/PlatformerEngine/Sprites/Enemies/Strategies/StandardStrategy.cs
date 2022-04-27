namespace PlatformerEngine.Sprites.Enemies.Strategies
{
    using Microsoft.Xna.Framework;
    using PlatformerEngine.Timers;

    /// <summary>
    /// Standard strategy -> patrol area
    /// </summary>
    public class StandardStrategy : IMoveStrategy
    {
        private readonly GameTimer moveInCurrentDirection;

        private bool movingLeft = true;

        public StandardStrategy(bool firstMoveToLeft = true, double timePatrolInOneDirection = 2.5)
        {
            movingLeft = firstMoveToLeft;

            // How long enemy will move in one direction
            moveInCurrentDirection = new GameTimer(timePatrolInOneDirection);
            moveInCurrentDirection.OnTimedEvent += (o, e) => ChangeDirection();
        }

        private void ChangeDirection()
        {
            movingLeft = !movingLeft;
        }

        public void Move(Enemy enemy)
        {
            if (movingLeft)
            {
                enemy.Velocity = new Vector2(-2f, enemy.Velocity.Y);
            }
            else
            {
                enemy.Velocity = new Vector2(2f, enemy.Velocity.Y);
            }
        }

        public void Update(GameTime gameTime)
        {
            moveInCurrentDirection?.Update(gameTime);
        }
    }
}
