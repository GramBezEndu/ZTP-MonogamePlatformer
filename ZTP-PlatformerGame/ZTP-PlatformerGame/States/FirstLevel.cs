using Microsoft.Xna.Framework;
using PlatformerEngine.MapsManager;
using PlatformerEngine.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP_PlatformerGame.States
{
    public class FirstLevel : GameState
    {
        public FirstLevel(Game1 gameReference) : base(gameReference)
        {
            
        }

        protected override void SpawnAllEnemies()
        {
            SpawnBoletus(new Vector2(game.LogicalSize.X * (0.9f), game.LogicalSize.Y));
            SpawnBoletus(new Vector2(game.LogicalSize.X * (0.85f), game.LogicalSize.Y), false);
            SpawnBoletus(new Vector2(game.LogicalSize.X * (1.2f), game.LogicalSize.Y));
            SpawnBoletus(new Vector2(game.LogicalSize.X * (1.01f), game.LogicalSize.Y), false);
            //SpawnBoletus(new Vector2(game.LogicalSize.X * (1.2f), game.LogicalSize.Y), false);
            SpawnBoletus(new Vector2(game.LogicalSize.X * (1.35f), game.LogicalSize.Y));
            SpawnBoletus(new Vector2(game.LogicalSize.X * (1.85f), game.LogicalSize.Y));
        }

        internal override void CreateMapBuilder()
        {
            mapBuilder = new StandardBuilder(textures["Air"], textures["Ground"], textures["Spike"]);
        }

        internal override void CreateMapReader()
        {
            mapReader = new MapReader("Map.txt");
        }
    }
}
