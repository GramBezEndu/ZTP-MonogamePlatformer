namespace PlatformerEngine.States
{
    using System;
    using System.Collections.Generic;
    using Engine.States;
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
    using ZTP_PlatformerGame;
    using ZTP_PlatformerGame.States;

    public abstract class GameState : State
    {
        private readonly SpriteBatch mapBatch;

        private readonly List<IComponent> gameComponents = new List<IComponent>();

        private readonly List<IDrawableComponent> gameOverComponents = new List<IDrawableComponent>();

        private readonly Camera camera;

        private readonly List<Enemy> enemies = new List<Enemy>();

        private readonly PlayerEffectsManager playerEffectsManager;

        private PhysicsManager physicsManager;

        private Sprite endLevelFlag;

        private IPlayer player;

        public GameState(Game1 gameReference)
            : base(gameReference)
        {
            mapBatch = new SpriteBatch(GraphicsDevice);
            CreateMapReader();
            CreateMapBuilder();
            BuildMap();
            SpawnPlayer();
            camera = new Camera(Game, InputManager, player);
            CreatePhysicsManager();

            playerEffectsManager = new PlayerEffectsManager(this, GraphicsDevice, Font, player, Textures["Timer"]);
            SpawnAllEnemies();
            LoadThemeSong();
            Game.PlaySong(LevelThemeSong);
            AddEndLevelFlag();

            CreateGameOverComponents();
        }

        protected Song LevelThemeSong { get; set; }

        protected MapReader MapReader { get; set; }

        protected MapBuilder MapBuilder { get; set; }

        public override void Update(GameTime gameTime)
        {
            // Effects management
            playerEffectsManager.Update(gameTime);

            // After updating player effects manager
            // Player reference might change (he can be packed or unpacked)
            // We're updating the reference in GameState and in PhysicsManager
            physicsManager.DeleteBody(player);
            player = playerEffectsManager.GetDecoratedPlayer();
            physicsManager.AddMoveableBody(player);

            camera.Update(gameTime);
            physicsManager.Update(gameTime);
            CheckForLevelComplete();
            foreach (IComponent c in gameComponents)
            {
                c.Update(gameTime);
            }

            if (player.MoveableBodyState == MoveableBodyStates.Dead)
            {
                foreach (IDrawableComponent c in gameOverComponents)
                {
                    c.Hidden = false;
                }

                if (InputManager.ActionWasJustPressed("Attack"))
                {
                    Type type = GetType();
                    Game.ChangeState((GameState)Activator.CreateInstance(type, Game));
                }
            }
            else
            {
                foreach (IDrawableComponent c in gameOverComponents)
                {
                    c.Hidden = true;
                }
            }

            CheckForBackToMenuInput();
            base.Update(gameTime);
        }

        public void SpawnBoletus(Vector2 position, bool firstMoveToLeft = true)
        {
            Boletus boletus = new Boletus(
                Content.Load<Texture2D>("Boletus/Spritesheet"),
                Content.Load<Dictionary<string, Rectangle>>("Boletus/Map"),
                new StandardStrategy(firstMoveToLeft))
            {
                Position = position,
                Scale = new Vector2(2f),
            };
            physicsManager.AddMoveableBody(boletus);
            enemies.Add(boletus);
        }

        public void AddGameComponent(IComponent component)
        {
            gameComponents.Add(component);
        }

        public override void Draw(GameTime gameTime)
        {
            mapBatch.Begin(transformMatrix: camera.ViewMatrix);
            foreach (IComponent c in gameComponents)
            {
                if (c is IDrawableComponent drawable)
                {
                    drawable.Draw(gameTime, mapBatch);
                }
            }

            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(gameTime, mapBatch);
            }

            player.Draw(gameTime, mapBatch);
            mapBatch.End();
            base.Draw(gameTime);
        }

        internal abstract void LoadThemeSong();

        internal abstract void CreateMapBuilder();

        internal abstract void CreateMapReader();

        protected abstract void SpawnAllEnemies();

        protected void AddEndLevelFlag()
        {
            endLevelFlag = new Sprite(Textures["EndFlag"])
            {
                Position = new Vector2(Game.LogicalSize.X * 4.25f, Game.LogicalSize.Y * 0.7f),
            };
            gameComponents.Add(endLevelFlag);
        }

        protected override void DrawUI(GameTime gameTime)
        {
            base.DrawUI(gameTime);
            playerEffectsManager.Draw(gameTime, UiBatch);
            foreach (Sprite s in player.HeartSprites)
            {
                s.Draw(gameTime, UiBatch);
            }
        }

        protected void SpawnPlayer()
        {
            SpriteAnimated swordSlash =
                new SpriteAnimated(
                    Content.Load<Texture2D>("Character/SwordSlash/Spritesheet"),
                    Content.Load<Dictionary<string, Rectangle>>("Character/SwordSlash/Map"));
            swordSlash.AddAnimation(
                "Slash",
                new SpriteSheetAnimationData(
                    new int[] { 0, 1, 2, 3, 4, 5, 6 },
                    frameDuration: 0.05f));
            swordSlash.Hidden = true;

            player = new Player(
                Content.Load<Texture2D>("Character/Spritesheet"),
                Content.Load<Dictionary<string, Rectangle>>("Character/Map"),
                InputManager,
                Content.Load<Texture2D>("Character/Heart"),
                swordSlash)
            {
                Position = new Vector2(140, Game.LogicalSize.Y - 200),
            };
            player.OnLoseHeart += (o, e) => PlaySound("LoseHeart");
        }

        private void CheckForLevelComplete()
        {
            if (physicsManager.CollisionManager.PlayerTouching(player, endLevelFlag.Rectangle))
            {
                Game.ChangeState(new LevelCompleted(Game));
            }
        }

        private void CheckForBackToMenuInput()
        {
            if (InputManager.ActionWasJustPressed("Back"))
            {
                Game.ChangeState(new MainMenu(Game));
            }
        }

        private void BuildMap()
        {
            List<Sprite> mapSprites = MapReader.BuildMap(MapBuilder);
            gameComponents.AddRange(mapSprites);
        }

        private void CreatePhysicsManager()
        {
            physicsManager = new PhysicsManager();
            physicsManager.AddMoveableBody(player);
            physicsManager.SetStaticBodies(MapBuilder.GetCollisionRectangles());
            physicsManager.SetStaticSpikes(MapBuilder.GetSpikes());
        }

        private void CreateGameOverComponents()
        {
            Text gameOverText = new Text(Font, "GAME OVER");
            gameOverText.Position = new Vector2(
                (Game.LogicalSize.X / 2) - (gameOverText.Size.X / 2),
                (Game.LogicalSize.Y / 2) - (gameOverText.Size.Y / 2));
            gameOverText.Color = Color.White;
            gameOverText.Hidden = true;
            gameOverComponents.Add(gameOverText);

            Text restartText = new Text(Font, "PRESS SPACE TO RESTART");
            restartText.Position = new Vector2(
                (Game.LogicalSize.X / 2) - (restartText.Size.X / 2),
                gameOverText.Position.Y + gameOverText.Size.Y);
            restartText.Color = Color.White;
            restartText.Hidden = true;
            gameOverComponents.Add(restartText);

            foreach (IDrawableComponent c in gameOverComponents)
            {
                AddUiComponent(c);
            }
        }
    }
}
