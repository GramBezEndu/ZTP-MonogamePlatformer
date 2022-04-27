namespace PlatformerEngine.Controls
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Text : IDrawableComponent
    {
        private string message;

        public Text(SpriteFont f, string msg)
        {
            Font = f;
            Message = msg;
        }

        public bool Hidden { get; set; }

        public Vector2 Position { get; set; }

        public string Message
        {
            get => message;
            set => message = value;
        }

        public Point Size
        {
            get
            {
                if (Font != null && message != null)
                {
                    return new Point((int)(Font.MeasureString(message).X * Scale.X), (int)(Font.MeasureString(message).Y * Scale.Y));
                }
                else
                {
                    return new Point(0, 0);
                }
            }
        }

        public Vector2 Scale { get; set; } = new Vector2(1f, 1f);

        public Color Color { get; set; } = Color.White;

        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

        public SpriteEffects SpriteEffects { get; set; }

        protected SpriteFont Font { get; private set; }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Hidden)
            {
                spriteBatch.DrawString(Font, Message, Position, Color, 0f, new Vector2(0, 0), Scale, SpriteEffects.None, 0f);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}
