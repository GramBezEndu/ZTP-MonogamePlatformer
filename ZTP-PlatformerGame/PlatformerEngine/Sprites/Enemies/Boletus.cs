using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerEngine.Sprites.Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Sprites
{
    public class Boletus : Enemy
    {
        public Boletus(Texture2D spritesheet, Dictionary<string, Rectangle> map) : base(spritesheet, map)
        {
        }
    }
}
