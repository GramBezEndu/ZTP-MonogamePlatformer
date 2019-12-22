using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerEngine.Input;
using System.Diagnostics;
using PlatformerEngine.Controls;
using PlatformerEngine.Controls.Buttons;

namespace Engine.Controls.Buttons
{
    public class TextButton : Text, IButton
    {
        protected InputManager inputManager;
        protected bool selected;

        public TextButton(InputManager im, SpriteFont f, string msg, Vector2 scale) : this(im, f, msg)
        {
            Scale = scale;
        }

        public TextButton(InputManager im, SpriteFont f, string msg) : base(f, msg)
        {
            Color = Color.DarkRed;
            inputManager = im;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Hidden)
            {
                //Color red while hovering
                if (Selected)
                    spriteBatch.DrawString(font, Message, Position, Color.Red, 0f, new Vector2(0, 0), Scale, SpriteEffects.None, 0f);
                else
                    spriteBatch.DrawString(font, Message, Position, Color, 0f, new Vector2(0, 0), Scale, SpriteEffects.None, 0f);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!Hidden)
            {
                if (Selected)
                {
                    if (inputManager.ActionWasPressed("Accept"))
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
                    return;
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
