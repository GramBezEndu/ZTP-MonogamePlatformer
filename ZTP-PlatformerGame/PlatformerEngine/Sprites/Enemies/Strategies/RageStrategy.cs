using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Sprites.Enemies.Strategies
{
    /// <summary>
    /// TODO: Maybe make a base class for strategies (see if there is any common code later)
    /// </summary>
    public class RageStrategy : IMoveStrategy
    {
        public void Move(Enemy enemy)
        {
            //Stand

            //Move left
            //enemy.Velocity = new Vector2(-4f, 0);
        }

        public void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }
    }
}
