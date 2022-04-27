namespace PlatformerEngine.Sprites.PlayerClasses
{
    public class InvincibilityEffect : PlayerEffect
    {
        public InvincibilityEffect(IPlayer p)
            : base(p)
        {
        }

        public override string Name => "INVINCIBILITY";

        public override void LoseHeart()
        {
            // Empty method
        }
    }
}
