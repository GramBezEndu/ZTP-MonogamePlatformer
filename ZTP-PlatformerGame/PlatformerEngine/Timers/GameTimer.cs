namespace PlatformerEngine.Timers
{
    using System;
    using Microsoft.Xna.Framework;

    public class GameTimer : IComponent
    {
        public GameTimer(double actionInterval)
        {
            Interval = actionInterval;
            CurrentInterval = Interval;
        }

        /// <summary>
        /// Action performed after set time passed.
        /// </summary>
        public event EventHandler OnTimedEvent;

        public double Interval { get; private set; }

        public double CurrentInterval { get; private set; }

        public void Update(GameTime gameTime)
        {
            CurrentInterval -= gameTime.ElapsedGameTime.TotalSeconds;
            if (CurrentInterval <= 0)
            {
                OnTimedEvent?.Invoke(this, new EventArgs());
                CurrentInterval = Interval;
            }
        }
    }
}
