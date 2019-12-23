using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace PlatformerEngine.Timers
{
    public class GameTimer : IComponent
    {
        private double interval;
        private double currentInterval;

        public EventHandler OnTimedEvent;

        public GameTimer(double actionInterval)
        {
            interval = actionInterval;
            currentInterval = interval;
        }
        public void Update(GameTime gameTime)
        {
            currentInterval -= gameTime.ElapsedGameTime.TotalSeconds;
            if(currentInterval <= 0)
            {
                OnTimedEvent?.Invoke(this, new EventArgs());
                currentInterval = interval;
            }
        }
    }
}
