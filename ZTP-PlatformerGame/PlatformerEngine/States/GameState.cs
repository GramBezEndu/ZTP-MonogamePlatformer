﻿using Engine.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerEngine.EffectsManager;
using PlatformerEngine.MapsManager;
using PlatformerEngine.Physics;
using PlatformerEngine.Sprites.PlayerClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using ZTP_PlatformerGame;

namespace PlatformerEngine.States
{
    public class GameState : State
    {
        protected SpriteBatch mapBatch;
        protected List<IComponent> gameComponents = new List<IComponent>();
        protected IPlayer player;
        protected MapReader mapReader;
        protected PhysicsManager physicsManager;
        protected PlayerEffectsManager playerEffectsManager;
        public GameState(Game1 gameReference) : base(gameReference)
        {
            mapBatch = new SpriteBatch(graphicsDevice);
            mapReader = new MapReader("Map.txt");
            var builder = new StandardBuilder(textures["Air"], textures["Ground"], textures["Spike"]);
            var mapSprites = mapReader.BuildMap(builder);
            gameComponents.AddRange(mapSprites);
            player = new Player(content.Load<Texture2D>("Character/Spritesheet"),
                content.Load<Dictionary<string, Rectangle>>("Character/Map"), inputManager);
            player.Scale = new Vector2(0.2f, 0.2f);
            ////Effect testing
            //player = new NoJumpEffect(player);
            //player = new SpeedEffect(player);

            physicsManager = new PhysicsManager();
            physicsManager.AddMoveableBody(player);
            physicsManager.SetStaticBodies(builder.GetCollisionRectangles());

            playerEffectsManager = new PlayerEffectsManager(player);
        }
        public void AddGameComponent(IComponent component)
        {
            gameComponents.Add(component);
        }

        public override void Draw(GameTime gameTime)
        {
            mapBatch.Begin();
            foreach(var c in gameComponents)
            {
                if(c is IDrawableComponent drawable)
                {
                    drawable.Draw(gameTime, mapBatch);
                }
            }
            player.Draw(gameTime, mapBatch);
            mapBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            //TODO: Refactor
            //Effects management
            physicsManager.DeleteBody(player);
            playerEffectsManager.Update(gameTime);
            player = playerEffectsManager.GetDecoratedPlayer();
            physicsManager.AddMoveableBody(player);

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

        }
    }
}
