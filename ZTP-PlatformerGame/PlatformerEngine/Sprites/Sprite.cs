﻿namespace PlatformerEngine.Sprites
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Sprite : IDrawableComponent, ICloneable
    {
        private readonly Texture2D texture;

        public Sprite(Texture2D t, Vector2 objScale)
        {
            texture = t;
            Scale = objScale;
        }

        public Sprite(Texture2D t)
        {
            texture = t;
        }

        public Vector2 Scale { get; set; } = Vector2.One;

        public bool Hidden { get; set; }

        public Vector2 Position { get; set; }

        public Point Size => new Point((int)(texture.Width * Scale.X), (int)(texture.Height * Scale.Y));

        public Color Color { get; set; } = Color.White;

        public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;

        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y);

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Hidden)
            {
                spriteBatch.Draw(texture, Position, null, Color, 0f, new Vector2(0, 0), Scale, SpriteEffects, 0f);
            }
        }

        public void Update(GameTime gameTime)
        {
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
