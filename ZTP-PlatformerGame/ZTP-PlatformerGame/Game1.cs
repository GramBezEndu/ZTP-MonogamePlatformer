﻿using Engine.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PlatformerEngine.Input;
using System.Diagnostics;
using ZTP_PlatformerGame.States;

namespace ZTP_PlatformerGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public InputManager inputManager = InputManager.GetInputManager;

        State currentState;
        State nextState;

        Song currentSong;

        public Point LogicalSize = new Point(1152, 648);

        public void ChangeState(State newState)
        {
            nextState = newState;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = LogicalSize.X;
            graphics.PreferredBackBufferHeight = LogicalSize.Y;
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            ChangeState(new MainMenu(this));
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Handle changing states
            if (nextState != null)
            {
                currentState = nextState;
                nextState = null;
            }
            inputManager.Update(gameTime);
            currentState.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            currentState.Draw(gameTime);

            base.Draw(gameTime);
        }

        public void PlaySong(Song s)
        {
            //We do not allow to change song to the same song
            if (IsThisSongPlaying(s))
                return;
            //If parameter is null we stop playing music
            if (s == null)
            {
                currentSong = null;
                MediaPlayer.Stop();
                return;
            }
            //Regular case: stop playing old song and start playing new song
            currentSong = s;
            MediaPlayer.Stop();
            MediaPlayer.Play(s);
        }

        private bool IsThisSongPlaying(Song song)
        {
            if (currentSong == song)
                return true;
            else
                return false;
        }
    }
}
