using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Sprites.PlayerClasses
{
    /// <summary>
    /// Increase movement speed effect
    /// </summary>
    public class SpeedEffect : PlayerEffect
    {
        public SpeedEffect(IPlayer p) : base(p)
        {
        }

        public override void MoveLeft()
        {
            Velocity = new Vector2(-8f, Velocity.Y);
        }
        public override void MoveRight()
        {
            Velocity = new Vector2(8f, Velocity.Y);
        }
    }
}
