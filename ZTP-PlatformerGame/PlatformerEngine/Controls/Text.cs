using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Controls
{
    public class Text : IDrawableComponent
    {
        private string _message;
        public bool Hidden { get; set; }
        public Vector2 Position { get; set; }
        protected SpriteFont font;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
            }
        }

        public Point Size
        {
            get
            {
                if (font != null && _message != null)
                    return new Point((int)(font.MeasureString(_message).X * Scale.X), (int)(font.MeasureString(_message).Y * Scale.Y));
                else
                    return new Point(0, 0);
            }
        }

        public Vector2 Scale { get; set; } = new Vector2(1f, 1f);

        public Color Color { get; set; } = Color.White;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            }
        }

        public SpriteEffects SpriteEffects { get; set; }

        public Text(SpriteFont f, String msg)
        {
            font = f;
            Message = msg;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Hidden)
            {
                spriteBatch.DrawString(font, Message, Position, Color, 0f, new Vector2(0, 0), Scale, SpriteEffects.None, 0f);
            }
        }

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
