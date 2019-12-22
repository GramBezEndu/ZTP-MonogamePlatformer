using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using PlatformerEngine.Sprites.MapSprites;

namespace PlatformerEngine.MapsManager
{
    public class PeacefulMapBuilder : StandardBuilder
    {
        public PeacefulMapBuilder(Texture2D air, Texture2D ground, Texture2D spike) : base(air, ground, spike)
        {
            segmentTwo = new GroundSprite(ground);
        }
    }
}
