using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace PlatformerEngine.Sprites.PlayerClasses
{
    public class NoActionAllowedEffect : PlayerEffect
    {
        public NoActionAllowedEffect(IPlayer p) : base(p)
        {
        }

        public override void PrepareMove(GameTime gameTime)
        {
            //Empty method
        }
    }
}
