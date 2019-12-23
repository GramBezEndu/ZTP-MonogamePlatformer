using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerEngine.Timers;

namespace PlatformerEngine.Controls
{
    public class TextOnTimer : Text
    {
        GameTimer displayTimer;
        public TextOnTimer(SpriteFont f, string msg, double displayTime = 3) : base(f, msg)
        {
            displayTimer = new GameTimer(displayTime)
            {
                OnTimedEvent = (o, e) => this.Hidden = true
            };
        }

        public override void Update(GameTime gameTime)
        {
            displayTimer.Update(gameTime);
            base.Update(gameTime);
        }
    }
}
