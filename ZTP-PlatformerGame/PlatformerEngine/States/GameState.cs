using Engine.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerEngine.MapsManager;
using PlatformerEngine.Sprites.PlayerClasses;
using System;
using System.Collections.Generic;
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
        public GameState(Game1 gameReference) : base(gameReference)
        {
            mapBatch = new SpriteBatch(graphicsDevice);
            mapReader = new MapReader("Map.txt");
            var mapSprites = mapReader.BuildMap(new StandardBuilder(textures["Air"], textures["Ground"], textures["Spike"]));
            gameComponents.AddRange(mapSprites);
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
            mapBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var c in gameComponents)
                c.Update(gameTime);
            base.Update(gameTime);
        }

        public void SpawnBoletus(Vector2 position)
        {

        }
    }
}
