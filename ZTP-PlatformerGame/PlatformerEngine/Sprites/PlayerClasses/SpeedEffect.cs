namespace PlatformerEngine.Sprites.PlayerClasses
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Increase movement speed effect.
    /// </summary>
    public class SpeedEffect : PlayerEffect
    {
        public SpeedEffect(IPlayer p)
            : base(p)
        {
        }

        public override string Name => "SPEEDBOI";

        public override void MoveLeft()
        {
            Velocity = new Vector2(-8f, Velocity.Y);
        }

        public override void MoveRight()
        {
            Velocity = new Vector2(8f, Velocity.Y);
        }
    }
}
