using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Sprites.PlayerClasses
{
    public class SlowedEffect : PlayerEffect
    {
        public SlowedEffect(Player p) : base(p)
        {
        }

        public override void ManagePlayerInput()
        {
            throw new NotImplementedException();
        }
    }
}
