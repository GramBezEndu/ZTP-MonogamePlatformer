using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace PlatformerEngine.Sprites.PlayerClasses
{
    public class NoAttackEffect : PlayerEffect
    {
        public NoAttackEffect(IPlayer p) : base(p)
        {
        }

        public override string Name { get { return "NO ATTACK"; } }

        public override void Attack()
        {
            //Empty method
        }
    }
}
