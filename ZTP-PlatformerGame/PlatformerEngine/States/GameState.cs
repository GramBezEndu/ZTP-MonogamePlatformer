﻿using Engine.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerEngine.CameraSystem;
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
        protected IPlayer player;
        protected MapReader mapReader;
        protected MapBuilder mapBuilder;
        protected PhysicsManager physicsManager;
        protected PlayerEffectsManager playerEffectsManager;
        protected List<Enemy> enemies = new List<Enemy>();
        protected Camera camera;
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

            playerEffectsManager = new PlayerEffectsManager(this, font, player);
            SpawnAllEnemies();
        }

        private void BuildMap()
        {
            var mapSprites = mapReader.BuildMap(mapBuilder);
            gameComponents.AddRange(mapSprites);
        }

        internal abstract void CreateMapBuilder();
        internal abstract void CreateMapReader();
        protected abstract void SpawnAllEnemies();

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
            base.Update(gameTime);
        }

        public void SpawnBoletus(Vector2 position)
        {
            var boletus = new Boletus(content.Load<Texture2D>("Boletus/Spritesheet"), content.Load<Dictionary<string, Rectangle>>("Boletus/Map"), new StandardStrategy())
            { 
                Position = position 
            };
            physicsManager.AddMoveableBody(boletus);
            enemies.Add(boletus);
        }

        protected void SpawnPlayer()
        {
            player = new Player(content.Load<Texture2D>("Character/Spritesheet"),
                content.Load<Dictionary<string, Rectangle>>("Character/Map"), inputManager, content.Load<Texture2D>("Character/Heart"));
            player.Scale = new Vector2(0.2f, 0.2f);
            player.Position = new Vector2(140, game.LogicalSize.Y - 200);
        }
    }
}
