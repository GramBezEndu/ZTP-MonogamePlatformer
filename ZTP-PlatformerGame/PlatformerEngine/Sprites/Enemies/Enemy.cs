using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Sprites.Enemies
{
    public class Enemy : SpriteAnimated
    {
        private IMoveStrategy currentStrategy;
    }
}
