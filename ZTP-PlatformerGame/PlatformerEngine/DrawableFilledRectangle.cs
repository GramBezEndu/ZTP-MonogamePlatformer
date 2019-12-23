using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine
{
    public class DrawableFilledRectangle : IDrawableComponent
    {
        private Texture2D rectangleTexture;
        private readonly GraphicsDevice graphicsDevice;
        public bool Hidden { get; set; }
        public Vector2 Position
        {
            get
            {
                return new Vector2(rectangle.X, rectangle.Y);
            }
            set
            {
                rectangle = new Rectangle((int)Position.X, (int)Position.X, rectangle.Width, rectangle.Height);
                CreateRectangleTexture();
            }
        }

        public Point Size
        {
            get
            {
                return Rectangle.Size;
            }
        }

        private Rectangle rectangle;

        public Rectangle Rectangle
        {
            get
            {
                return rectangle;
            }
        }

        public Color Color { get; set; } = Color.White;
        public Vector2 Scale { get; set; }
        public SpriteEffects SpriteEffects { get; set; }

        public DrawableFilledRectangle(GraphicsDevice gd, Rectangle rec)
        {
            rectangle = rec;
            graphicsDevice = gd;
            CreateRectangleTexture();
        }

        private void CreateRectangleTexture()
        {
            var data = new List<Color>();

            for (int y = 0; y < (int)Size.Y; y++)
            {
                for (int x = 0; x < (int)Size.X; x++)
                {
                    data.Add(Color);
                }
            }
            rectangleTexture = new Texture2D(graphicsDevice, (int)Size.X, (int)Size.Y);
            rectangleTexture.SetData<Color>(data.ToArray());
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Hidden)
            {
                spriteBatch.Draw(rectangleTexture, Position, null, Color, 0f, new Vector2(0, 0), Scale, SpriteEffects, 0f);
            }
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
