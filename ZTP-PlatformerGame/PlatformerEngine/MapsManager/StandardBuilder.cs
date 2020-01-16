using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using PlatformerEngine.Sprites;
using PlatformerEngine.Sprites.MapSprites;

namespace PlatformerEngine.MapsManager
{
    public class StandardBuilder : MapBuilder
    {
        public StandardBuilder(Texture2D air, Texture2D ground, Texture2D spike, Texture2D groundGrass)
        {
            segments = new Sprite[]
            {
                new AirSprite(air),
                new GroundSprite(ground),
                new SpikeSprite(spike),
                new GroundGrassSprite(groundGrass)
            };
        }
    }
}
