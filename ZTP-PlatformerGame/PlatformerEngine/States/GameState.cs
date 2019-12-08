using Engine.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public GameState(Game1 gameReference) : base(gameReference)
        {
        }
        public void AddGameComponent(IComponent component)
        {
            gameComponents.Add(component);
        }
        public void SpawnBoletus(Vector2 position)
        {

        }
    }
}
