using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Sprites.PlayerClasses
{
    public class MegaJumpEffect : PlayerEffect
    {
        public override string Name { get { return "KANGAROO"; } }
        public MegaJumpEffect(IPlayer p) : base(p)
        {
        }

        public override void Jump()
        {
            Velocity = new Vector2(Velocity.X, -24f);
        }
    }
}
