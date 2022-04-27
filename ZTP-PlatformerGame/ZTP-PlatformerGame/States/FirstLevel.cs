namespace ZTP_PlatformerGame.States
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Media;
    using PlatformerEngine.MapsManager;
    using PlatformerEngine.States;

    public class FirstLevel : GameState
    {
        public FirstLevel(Game1 gameReference)
            : base(gameReference)
        {
        }

        internal override void CreateMapBuilder()
        {
            MapBuilder = new StandardBuilder(Textures["Air"], Textures["Ground"], Textures["Spike"], Textures["GroundGrass"]);
        }

        internal override void CreateMapReader()
        {
            MapReader = new MapReader(Content.RootDirectory + "/Map.txt");
        }

        internal override void LoadThemeSong()
        {
            LevelThemeSong = Content.Load<Song>("Songs/FirstLevel");
        }

        protected override void SpawnAllEnemies()
        {
            SpawnBoletus(new Vector2(Game.LogicalSize.X * 0.9f, 0.9f * Game.LogicalSize.Y));
            SpawnBoletus(new Vector2(Game.LogicalSize.X * 0.85f, 0.9f * Game.LogicalSize.Y), false);
            SpawnBoletus(new Vector2(Game.LogicalSize.X * 1.2f, 0.9f * Game.LogicalSize.Y));
            SpawnBoletus(new Vector2(Game.LogicalSize.X * 1.01f, 0.9f * Game.LogicalSize.Y), false);
            SpawnBoletus(new Vector2(Game.LogicalSize.X * 1.35f, 0.9f * Game.LogicalSize.Y));
            SpawnBoletus(new Vector2(Game.LogicalSize.X * 1.85f, 0.9f * Game.LogicalSize.Y));
        }
    }
}
