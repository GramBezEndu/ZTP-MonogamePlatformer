﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Sprites.PlayerClasses
{
    public class NoJumpEffect : PlayerEffect
    {
        public NoJumpEffect(IPlayer p) : base(p)
        {
        }

        public override void Jump()
        {
            //Empty method
        }
    }
}
