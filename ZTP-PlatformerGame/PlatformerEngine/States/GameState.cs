﻿using Engine.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Animations.SpriteSheets;
using PlatformerEngine.CameraSystem;
using PlatformerEngine.Controls;
using PlatformerEngine.EffectsManager;
using PlatformerEngine.MapsManager;
using PlatformerEngine.Physics;
using PlatformerEngine.Sprites;
using PlatformerEngine.Sprites.Enemies;
using PlatformerEngine.Sprites.Enemies.Strategies;
using PlatformerEngine.Sprites.PlayerClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ZTP_PlatformerGame;

namespace PlatformerEngine.States
{
    public abstract class GameState : State
    {
        protected SpriteBatch mapBatch;
        protected List<IComponent> gameComponents = new List<IComponent>();
        protected List<IDrawableComponent> gameOverComponents = new List<IDrawableComponent>();
        protected IPlayer player;
        protected MapReader mapReader;
        protected MapBuilder mapBuilder;
        protected PhysicsManager physicsManager;
        protected PlayerEffectsManager playerEffectsManager;
        protected List<Enemy> enemies = new List<Enemy>();
        protected Camera camera;
        protected Song levelThemeSong;
        public GameState(Game1 gameReference) : base(gameReference)
        {
            mapBatch = new SpriteBatch(graphicsDevice);
            CreateMapReader();
            CreateMapBuilder();
            BuildMap();
            SpawnPlayer();
            camera = new Camera(game, inputManager, player);
            physicsManager = new PhysicsManager();
            physicsManager.AddMoveableBody(player);
            physicsManager.SetStaticBodies(mapBuilder.GetCollisionRectangles());
            physicsManager.SetStaticSpikes(mapBuilder.GetSpikes());

            playerEffectsManager = new PlayerEffectsManager(this, graphicsDevice, font, player);
            SpawnAllEnemies();
            LoadThemeSong();
            game.PlaySong(levelThemeSong);

            CreateGameOverComponents();
        }

        internal abstract void LoadThemeSong();

        private void BuildMap()
        {
            var mapSprites = mapReader.BuildMap(mapBuilder);
            gameComponents.AddRange(mapSprites);
        }

        internal abstract void CreateMapBuilder();
        internal abstract void CreateMapReader();
        protected abstract void SpawnAllEnemies();
        private void CreateGameOverComponents()
        {
            var gameOverText = new Text(font, "GAME OVER");
            gameOverText.Position = new Vector2(game.LogicalSize.X / 2 - gameOverText.Size.X / 2, game.LogicalSize.Y / 2 - gameOverText.Size.Y / 2);
            gameOverText.Color = Color.Black;
            gameOverText.Hidden = true;
            gameOverComponents.Add(gameOverText);

            var restartText = new Text(font, "PRESS LMB TO RESTART");
            restartText.Position = new Vector2(game.LogicalSize.X / 2 - restartText.Size.X / 2, gameOverText.Position.Y + gameOverText.Size.Y);
            restartText.Color = Color.Black;
            restartText.Hidden = true;
            gameOverComponents.Add(restartText);

            foreach (var c in gameOverComponents)
                AddUiComponent(c);
        }

        public void AddGameComponent(IComponent component)
        {
            gameComponents.Add(component);
        }

        public override void Draw(GameTime gameTime)
        {
            mapBatch.Begin(transformMatrix: camera.ViewMatrix);
            foreach(var c in gameComponents)
            {
                if(c is IDrawableComponent drawable)
                {
                    drawable.Draw(gameTime, mapBatch);
                }
            }
            foreach (var enemy in enemies)
                enemy.Draw(gameTime, mapBatch);
            player.Draw(gameTime, mapBatch);
            mapBatch.End();
            base.Draw(gameTime);
        }

        protected override void DrawUI(GameTime gameTime)
        {
            base.DrawUI(gameTime);
            playerEffectsManager.Draw(gameTime, uiSpriteBatch);
            foreach (var s in player.HeartSprites)
                s.Draw(gameTime, uiSpriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            //TODO: Refactor
            //Effects management
            physicsManager.DeleteBody(player);
            playerEffectsManager.Update(gameTime);
            player = playerEffectsManager.GetDecoratedPlayer();
            physicsManager.AddMoveableBody(player);
            camera.Update(gameTime);

            //Debug.WriteLine(player);
            //player = playerEffectsManager.GetDecoratedPlayer();
            //physicsManager.DeleteBody(player);
            //physicsManager.AddMoveableBody(player);
            physicsManager.Update(gameTime);
            foreach (var c in gameComponents)
                c.Update(gameTime);
            if(player.MoveableBodyState == MoveableBodyStates.Dead)
            {
                foreach (var c in gameOverComponents)
                    c.Hidden = false;
                if(inputManager.ActionWasPressed("Attack"))
                {
                    Type type = this.GetType();
                    game.ChangeState((GameState)Activator.CreateInstance(type, game));
                }
            }
            else
            {
                foreach (var c in gameOverComponents)
                    c.Hidden = true;
            }
            base.Update(gameTime);
        }

        public void SpawnBoletus(Vector2 position, bool firstMoveToLeft = true)
        {
            var boletus = new Boletus(content.Load<Texture2D>("Boletus/Spritesheet"), content.Load<Dictionary<string, Rectangle>>("Boletus/Map"), new StandardStrategy(firstMoveToLeft))
            { 
                Position = position 
            };
            physicsManager.AddMoveableBody(boletus);
            enemies.Add(boletus);
        }

        public void PlaySound(string name)
        {
            Sounds[name].Play();
        }

        protected void SpawnPlayer()
        {
            SpriteAnimated swordSlash = new SpriteAnimated(content.Load<Texture2D>("Character/SwordSlash/Spritesheet"), content.Load<Dictionary<string, Rectangle>>("Character/SwordSlash/Map"));
            swordSlash.AddAnimation("Slash", new SpriteSheetAnimationData(new int[] { 0, 1, 2, 3, 4, 5, 6 }, frameDuration: 0.05f));
            swordSlash.Hidden = true;

            player = new Player(content.Load<Texture2D>("Character/Spritesheet"),
                content.Load<Dictionary<string, Rectangle>>("Character/Map"), inputManager, content.Load<Texture2D>("Character/Heart"), swordSlash);
            player.Scale = new Vector2(0.15f, 0.15f);
            player.Position = new Vector2(140, game.LogicalSize.Y - 200);
            player.OnLoseHeart = (o, e) => PlaySound("LoseHeart");
        }
    }
}
