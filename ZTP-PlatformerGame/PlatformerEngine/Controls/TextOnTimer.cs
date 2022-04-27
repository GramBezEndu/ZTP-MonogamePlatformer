namespace PlatformerEngine.Controls
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PlatformerEngine.Timers;

    public class TextOnTimer : Text
    {
        private readonly GameTimer displayTimer;

        public TextOnTimer(SpriteFont f, string msg, double displayTime = 3)
            : base(f, msg)
        {
            displayTimer = new GameTimer(displayTime);
            displayTimer.OnTimedEvent += (o, e) => Hidden = true;
        }

        public override void Update(GameTime gameTime)
        {
            displayTimer.Update(gameTime);
            base.Update(gameTime);
        }
    }
}
