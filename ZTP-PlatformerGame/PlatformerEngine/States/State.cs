namespace Engine.States
{
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using PlatformerEngine;
    using PlatformerEngine.Controls;
    using PlatformerEngine.Input;
    using ZTP_PlatformerGame;

    public abstract class State : IComponent
    {
        protected readonly InputManager inputManager;

        public State(Game1 gameReference)
        {
            // Assign variables
            Game = gameReference;
            inputManager = Game.inputManager;
            GraphicsDevice = Game.GraphicsDevice;
            Content = Game.Content;

            UiBatch = new SpriteBatch(GraphicsDevice);

            // Load assets
            LoadFont();
            LoadTextures();
            LoadSounds();
        }

        public Dictionary<string, SoundEffect> Sounds { get; set; } = new Dictionary<string, SoundEffect>();

        protected Game1 Game { get; private set; }

        protected GraphicsDevice GraphicsDevice { get; private set; }

        protected ContentManager Content { get; private set; }

        protected SpriteFont Font { get; private set; }

        protected List<IComponent> UiComponents { get; private set; } = new List<IComponent>();

        protected SpriteBatch UiBatch { get; private set; }

        protected Dictionary<string, Texture2D> Textures { get; private set; } = new Dictionary<string, Texture2D>();

        public void AddUiComponent(IComponent component)
        {
            UiComponents.Add(component);
        }

        public void AddNotification(string msg)
        {
            TextOnTimer message = new TextOnTimer(Font, msg, 2);
            message.Position = new Vector2(
                (Game.LogicalSize.X / 2) - (message.Size.X / 2),
                (Game.LogicalSize.Y / 9) - (message.Size.Y / 2));
            AddUiComponent(message);
        }

        public virtual void Draw(GameTime gameTime)
        {
            UiBatch.Begin();
            DrawUI(gameTime);
            UiBatch.End();
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (IComponent c in UiComponents)
            {
                c.Update(gameTime);
            }
        }

        public void PlaySound(string name)
        {
            Sounds[name].Play();
        }

        protected virtual void DrawUI(GameTime gameTime)
        {
            foreach (IComponent c in UiComponents)
            {
                if (c is IDrawableComponent drawable)
                {
                    drawable.Draw(gameTime, UiBatch);
                }
            }
        }

        private void LoadSounds()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Content.RootDirectory + "/Sounds/");
            if (!directoryInfo.Exists)
            {
                throw new DirectoryNotFoundException();
            }

            FileInfo[] files = directoryInfo.GetFiles("*.*");
            foreach (FileInfo file in files)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);
                Sounds[key] = Content.Load<SoundEffect>(Directory.GetCurrentDirectory() + "/Content/Sounds/" + key);
            }
        }

        private void LoadTextures()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Content.RootDirectory + "/Textures/");
            if (!directoryInfo.Exists)
            {
                throw new DirectoryNotFoundException();
            }

            FileInfo[] files = directoryInfo.GetFiles("*.*");
            foreach (FileInfo file in files)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);
                Textures[key] = Content.Load<Texture2D>(Directory.GetCurrentDirectory() + "/Content/Textures/" + key);
            }
        }

        private void LoadFont()
        {
            Font = Content.Load<SpriteFont>("Fonts/Font");
        }
    }
}
