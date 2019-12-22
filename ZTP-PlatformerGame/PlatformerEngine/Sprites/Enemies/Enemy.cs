using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerEngine.Sprites.Enemies
{
    public class Enemy : SpriteAnimated
    {
        private IMoveStrategy currentStrategy;

        public Enemy(Texture2D spritesheet, Dictionary<string, Rectangle> map) : base(spritesheet, map)
        {
        }
    }
}
