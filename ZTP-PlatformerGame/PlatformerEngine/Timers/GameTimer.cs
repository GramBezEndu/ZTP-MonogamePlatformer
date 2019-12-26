﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace PlatformerEngine.Timers
{
    public class GameTimer : IComponent
    {
        public double Interval { get; private set; }
        public double CurrentInterval { get; private set; }

        public EventHandler OnTimedEvent;

        public GameTimer(double actionInterval)
        {
            Interval = actionInterval;
            CurrentInterval = Interval;
        }
        public void Update(GameTime gameTime)
        {
            CurrentInterval -= gameTime.ElapsedGameTime.TotalSeconds;
            if(CurrentInterval <= 0)
            {
                OnTimedEvent?.Invoke(this, new EventArgs());
                CurrentInterval = Interval;
            }
        }
    }
}
