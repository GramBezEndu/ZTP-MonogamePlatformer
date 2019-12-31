using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Sprites.PlayerClasses
{
    public class InvincibilityEffect : PlayerEffect
    {
        public InvincibilityEffect(IPlayer p) : base(p)
        {
        }

        public override string Name { get { return "INVINCIBILITY"; } }

        public override void LoseHeart()
        {
            //Empty method
        }
    }
}
