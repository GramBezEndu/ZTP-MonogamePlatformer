using PlatformerEngine.Sprites.Enemies.Strategies;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Sprites.Enemies
{
    public interface IMoveStrategy
    {
        void Move(Enemy enemy);
    }
}
