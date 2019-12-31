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
using PlatformerEngine.Controls;
using Microsoft.Xna.Framework.Audio;

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
        public Dictionary<string, SoundEffect> Sounds { get; set; } = new Dictionary<string, SoundEffect>();

        public void AddUiComponent(IComponent component)
        {
            uiComponents.Add(component);
        }

        public void AddNotification(string msg)
        {
            var message = new TextOnTimer(font, msg, 2);
            message.Position = new Vector2(game.LogicalSize.X / 2 - message.Size.X / 2,
                game.LogicalSize.Y / 5 - message.Size.Y / 2);
            AddUiComponent(message);
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
            LoadTextures();
            LoadSounds();
        }

        private void LoadSounds()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(content.RootDirectory + "/Sounds/");
            if (!directoryInfo.Exists)
                throw new DirectoryNotFoundException();
            FileInfo[] files = directoryInfo.GetFiles("*.*");
            foreach (FileInfo file in files)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);
                Sounds[key] = content.Load<SoundEffect>(Directory.GetCurrentDirectory() + "/Content/Sounds/" + key);
            }
        }

        private void LoadTextures()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(content.RootDirectory + "/Textures/");
            if (!directoryInfo.Exists)
                throw new DirectoryNotFoundException();
            FileInfo[] files = directoryInfo.GetFiles("*.*");
            foreach (FileInfo file in files)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);
                textures[key] = content.Load<Texture2D>(Directory.GetCurrentDirectory() + "/Content/Textures/" + key);
            }
        }

        private void LoadFont()
        {
            font = this.content.Load<SpriteFont>("Fonts/Font");
        }

        public virtual void Draw(GameTime gameTime)
        {
            uiSpriteBatch.Begin();
            DrawUI(gameTime);
            uiSpriteBatch.End();
        }

        protected virtual void DrawUI(GameTime gameTime)
        {
            foreach (var c in uiComponents)
                if (c is IDrawableComponent drawable)
                    drawable.Draw(gameTime, uiSpriteBatch);
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var c in uiComponents)
                c.Update(gameTime);
        }
    }
}
