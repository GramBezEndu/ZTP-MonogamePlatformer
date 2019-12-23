using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Sprites.Enemies.Strategies
{
    public class RageStrategy : IMoveStrategy
    {
        public void Move(Enemy enemy)
        {
            //Move left
            enemy.Velocity = new Vector2(-4f, 0);
        }
    }
}
