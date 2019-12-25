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
            SpawnBoletus(new Vector2(game.LogicalSize.X * (2 / 3f), game.LogicalSize.Y - 90));
            SpawnBoletus(new Vector2(game.LogicalSize.X * (1.2f), game.LogicalSize.Y - 90));
            SpawnBoletus(new Vector2(game.LogicalSize.X * (5 / 3f), game.LogicalSize.Y - 90));
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
