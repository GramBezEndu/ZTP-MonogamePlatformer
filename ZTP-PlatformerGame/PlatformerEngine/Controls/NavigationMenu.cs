namespace PlatformerEngine.Controls
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PlatformerEngine.Controls.Buttons;
    using PlatformerEngine.Input;

    public abstract class NavigationMenu : IDrawableComponent
    {
        private SpriteEffects spriteEffects;

        private Vector2 scale;

        /// <summary>
        /// Creates a new instance of navigation through buttons. First button is selected by default.
        /// </summary>
        public NavigationMenu(InputManager im, List<IButton> listButtons, int margin = 0)
        {
            Margin = margin;
            InputManager = im;
            if (listButtons == null || listButtons.Count < 1)
            {
                throw new ArgumentException("Invalid list of buttons");
            }

            Buttons = listButtons;
            Buttons[CurrentlySelectedButtonIndex].Selected = true;
        }

        public bool Hidden { get; set; }

        /// <summary>
        /// Position determines beginning of the navigation menu.
        /// </summary>
        public abstract Vector2 Position { get; set; }

        public abstract Point Size { get; }

        public Color Color { get; set; }

        /// <summary>
        /// Indicates spacing between buttons, after setting margin navigation menu should be repositioned manually.
        /// </summary>
        public int Margin { get; private set; }

        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y);

        public Vector2 Scale
        {
            get => scale;
            set
            {
                scale = value;
                foreach (IButton b in Buttons)
                {
                    b.Scale = value;
                }
            }
        }

        public SpriteEffects SpriteEffects
        {
            get => spriteEffects;
            set
            {
                spriteEffects = value;
                foreach (IButton b in Buttons)
                {
                    b.SpriteEffects = value;
                }
            }
        }

        protected InputManager InputManager { get; private set; }

        protected int CurrentlySelectedButtonIndex { get; set; } = 0;

        protected List<IButton> Buttons { get; private set; }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Hidden)
            {
                foreach (IButton button in Buttons)
                {
                    button.Draw(gameTime, spriteBatch);
                }
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!Hidden)
            {
                UpdateButtons(gameTime);
                Navigate();
                ChangeStateOfButtons();
            }
        }

        protected abstract void Navigate();

        private void ChangeStateOfButtons()
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                Buttons[i].Selected = false;
            }

            Buttons[CurrentlySelectedButtonIndex].Selected = true;
        }

        private void UpdateButtons(GameTime gameTime)
        {
            foreach (IButton button in Buttons)
            {
                button.Update(gameTime);
            }
        }
    }
}
