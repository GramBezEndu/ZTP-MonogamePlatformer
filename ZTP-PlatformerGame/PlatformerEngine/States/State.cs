using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PlatformerEngine.Input;
using Microsoft.Xna.Framework.Input;
using PlatformerEngine;
using ZTP_PlatformerGame;

namespace Engine.States
{
    public abstract class State : IComponent
    {
        protected readonly Game1 game;
        protected readonly InputManager inputManager;
        protected readonly GraphicsDevice graphicsDevice;
        protected readonly ContentManager content;
        protected SpriteFont font;
        protected List<IComponent> uiComponents = new List<IComponent>();
        protected SpriteBatch uiSpriteBatch;
        protected Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public void AddUiComponent(IComponent component)
        {
            uiComponents.Add(component);
        }

        public State(Game1 gameReference)
        {
            //Assign variables
            game = gameReference;
            inputManager = game.inputManager;
            graphicsDevice = game.GraphicsDevice;
            content = game.Content;

            uiSpriteBatch = new SpriteBatch(graphicsDevice);
            //Load assets
            LoadFont();
        }

        private void LoadFont()
        {
            font = this.content.Load<SpriteFont>("Fonts/Font");
        }

        public virtual void Draw(GameTime gameTime)
        {
            uiSpriteBatch.Begin();
            foreach (var c in uiComponents)
                if (c is IDrawableComponent drawable)
                    drawable.Draw(gameTime, uiSpriteBatch);
            uiSpriteBatch.End();
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var c in uiComponents)
                c.Update(gameTime);
        }
    }
}
