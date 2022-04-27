namespace PlatformerEngine.Sprites.PlayerClasses
{
    public class NoJumpEffect : PlayerEffect
    {
        public NoJumpEffect(IPlayer p)
            : base(p)
        {
        }

        public override string Name => "NO JUMP";

        public override bool CanJump()
        {
            return false;
        }
    }
}
