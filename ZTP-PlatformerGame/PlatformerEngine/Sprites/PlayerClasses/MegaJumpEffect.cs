namespace PlatformerEngine.Sprites.PlayerClasses
{
    using Microsoft.Xna.Framework;

    public class MegaJumpEffect : PlayerEffect
    {
        public MegaJumpEffect(IPlayer p)
            : base(p)
        {
        }

        public override string Name => "KANGAROO";

        public override void Jump()
        {
            Velocity = new Vector2(Velocity.X, -24f);
        }
    }
}
