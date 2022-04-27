namespace PlatformerEngine.MapsManager
{
    using Microsoft.Xna.Framework.Graphics;
    using PlatformerEngine.Sprites.MapSprites;

    public class PeacefulMapBuilder : StandardBuilder
    {
        public PeacefulMapBuilder(Texture2D air, Texture2D ground, Texture2D spike, Texture2D groundGrass)
            : base(air, ground, spike, groundGrass)
        {
            Segments[2] = new GroundSprite(ground);
        }
    }
}
