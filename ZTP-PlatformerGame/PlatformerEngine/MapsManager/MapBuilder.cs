using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerEngine.Sprites;
using PlatformerEngine.Sprites.MapSprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.MapsManager
{
    public abstract class MapBuilder
    {
        protected Sprite segmentZero;
        protected Sprite segmentOne;
        protected Sprite segmentTwo;
        private Vector2 position = Vector2.Zero;
        private const int TILE_SIZE_X = 32;
        private const int TILE_SIZE_Y = 32;
        public List<Sprite> MapObjects { get; private set; } = new List<Sprite>();
        public MapBuilder(Texture2D air, Texture2D ground, Texture2D spike)
        {
        }
        /// <summary>
        /// Call after building segment
        /// </summary>
        protected void UpdatePosition()
        {
            position += new Vector2(TILE_SIZE_X, 0);
        }
        /// <summary>
        /// Call to make a new line
        /// </summary>
        public void NewLine()
        {
            position = new Vector2(0, position.Y + TILE_SIZE_Y);
        }
        public void BuildSegmentZero()
        {
            var sprite = (Sprite)segmentZero.Clone();
            sprite.Position = position;
            MapObjects.Add(sprite);
            UpdatePosition();
        }
        public void BuildSegmentOne()
        {
            var sprite = (Sprite)segmentOne.Clone();
            sprite.Position = position;
            MapObjects.Add(sprite);
            UpdatePosition();
        }
        public void BuildSegmentTwo()
        {
            var sprite = (Sprite)segmentTwo.Clone();
            sprite.Position = position;
            MapObjects.Add(sprite);
            UpdatePosition();
        }

        /// <summary>
        /// Use this method to get list of rectangles and add it to static bodies to physics manager (to make collision)
        /// </summary>
        /// <returns></returns>
        public List<Rectangle> GetCollisionRectangles()
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            foreach(var ob in MapObjects)
            {
                if (ob is AirSprite)
                    continue;
                else
                {
                    rectangles.Add(ob.Rectangle);
                }
            }
            return rectangles;
        }
    }
}
