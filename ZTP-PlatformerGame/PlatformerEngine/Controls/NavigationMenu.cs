using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerEngine.Controls.Buttons;
using PlatformerEngine.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Controls
{
    public abstract class NavigationMenu : IDrawableComponent
    {
        protected readonly InputManager inputManager;
        protected int currentlySelectedButton = 0;
        protected List<IButton> buttons;
        private SpriteEffects spriteEffects;
        private Vector2 scale;

        public bool Hidden { get; set; }
        /// <summary>
        /// Position determines beginning of the navigation menu
        /// </summary>
        public abstract Vector2 Position { get; set; }

        public abstract Point Size { get; }

        public Color Color { get; set; }

        /// <summary>
        /// Indicates spacing between buttons, after setting margin navigation menu should be repositioned manually
        /// </summary>
        public int Margin { get; private set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y);
            }
        }

        public Vector2 Scale 
        { 
            get => scale;
            set
            {
                scale = value;
                foreach (var b in buttons)
                    b.Scale = value;
            }
        }
        public SpriteEffects SpriteEffects
        {
            get => spriteEffects;
            set
            {
                spriteEffects = value;
                foreach (var b in buttons)
                    b.SpriteEffects = value;
            }
        }

        /// <summary>
        /// Creates a new instance of navigation through buttons. First button is selected by default.
        /// </summary>
        /// <param name="listButtons"></param>
        public NavigationMenu(InputManager im, List<IButton> listButtons, int margin = 0)
        {
            Margin = margin;
            inputManager = im;
            if (listButtons == null || listButtons.Count < 1)
                throw new ArgumentException("Invalid list of buttons");
            buttons = listButtons;
            buttons[currentlySelectedButton].Selected = true;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Hidden)
            {
                foreach (var button in buttons)
                    button.Draw(gameTime, spriteBatch);
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

        private void ChangeStateOfButtons()
        {
            for (int i = 0; i < buttons.Count; i++)
                buttons[i].Selected = false;
            buttons[currentlySelectedButton].Selected = true;
        }

        private void UpdateButtons(GameTime gameTime)
        {
            foreach (var button in buttons)
                button.Update(gameTime);
        }

        protected abstract void Navigate();
    }
}
