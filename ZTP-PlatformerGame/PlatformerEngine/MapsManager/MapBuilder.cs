namespace PlatformerEngine.MapsManager
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using PlatformerEngine.Sprites;
    using PlatformerEngine.Sprites.MapSprites;

    public abstract class MapBuilder
    {
        private const int TileSizeX = 32;

        private const int TileSizeY = 32;

        private Vector2 position = Vector2.Zero;

        public List<Sprite> MapObjects { get; private set; } = new List<Sprite>();

        protected Sprite[] Segments { get; set; }

        /// <summary>
        /// Call to make a new line.
        /// </summary>
        public void NewLine()
        {
            position = new Vector2(0, position.Y + TileSizeY);
        }

        public void BuildSegment(int type)
        {
            Sprite sprite = (Sprite)Segments[type].Clone();
            sprite.Position = position;
            MapObjects.Add(sprite);
            UpdatePosition();
        }

        /// <summary>
        /// Use this method to get list of rectangles and add it to static bodies to physics manager (to make collision).
        /// </summary>
        /// <returns>List of map collision.</returns>
        public List<Rectangle> GetCollisionRectangles()
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            foreach (Sprite ob in MapObjects)
            {
                if (ob is AirSprite || ob is SpikeSprite)
                {
                    continue;
                }
                else
                {
                    rectangles.Add(ob.Rectangle);
                }
            }

            return rectangles;
        }

        public List<Rectangle> GetSpikes()
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            foreach (Sprite ob in MapObjects)
            {
                if (ob is SpikeSprite)
                {
                    rectangles.Add(ob.Rectangle);
                }
            }

            return rectangles;
        }

        /// <summary>
        /// Call after building segment.
        /// </summary>
        protected void UpdatePosition()
        {
            position += new Vector2(TileSizeX, 0);
        }
    }
}
