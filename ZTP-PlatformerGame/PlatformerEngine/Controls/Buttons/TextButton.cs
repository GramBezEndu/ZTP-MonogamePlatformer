namespace Engine.Controls.Buttons
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PlatformerEngine.Controls;
    using PlatformerEngine.Controls.Buttons;
    using PlatformerEngine.Input;

    public class TextButton : Text, IButton
    {
        private bool selected;

        protected InputManager inputManager;

        public TextButton(InputManager im, SpriteFont f, string msg, Vector2 scale)
            : this(im, f, msg)
        {
            Scale = scale;
        }

        public TextButton(InputManager im, SpriteFont f, string msg)
            : base(f, msg)
        {
            Color = Color.DarkRed;
            inputManager = im;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Hidden)
            {
                // Color red while hovering
                if (Selected)
                {
                    spriteBatch.DrawString(Font, Message, Position, Color.Red, 0f, new Vector2(0, 0), Scale, SpriteEffects.None, 0f);
                }
                else
                {
                    spriteBatch.DrawString(Font, Message, Position, Color, 0f, new Vector2(0, 0), Scale, SpriteEffects.None, 0f);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!Hidden)
            {
                if (Selected)
                {
                    if (inputManager.ActionWasJustPressed("Accept"))
                    {
                        OnClick?.Invoke(this, new EventArgs());
                    }
                }
            }
        }

        public EventHandler OnClick { get; set; }

        public bool Selected
        {
            get => selected;
            set
            {
                if (selected == value)
                {
                    return;
                }
                else
                {
                    selected = value;
                    OnSelectedChange?.Invoke(this, new EventArgs());
                }
            }
        }

        public EventHandler OnSelectedChange { get; set; }
    }
}
