using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using PlatformerEngine.Sprites.MapSprites;

namespace PlatformerEngine.MapsManager
{
    public class PeacefulMapBuilder : StandardBuilder
    {
        public PeacefulMapBuilder(Texture2D air, Texture2D ground, Texture2D spike, Texture2D groundGrass) : base(air, ground, spike, groundGrass)
        {
            segmentTwo = new GroundSprite(ground);
        }
    }
}
