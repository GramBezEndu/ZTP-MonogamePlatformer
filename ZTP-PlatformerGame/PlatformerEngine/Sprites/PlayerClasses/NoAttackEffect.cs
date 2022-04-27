namespace PlatformerEngine.Sprites.PlayerClasses
{
    public class NoAttackEffect : PlayerEffect
    {
        public NoAttackEffect(IPlayer p)
            : base(p)
        {
        }

        public override string Name => "NO ATTACK";

        public override void Attack()
        {
            // Empty method
        }
    }
}
