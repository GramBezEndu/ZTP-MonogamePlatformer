namespace PlatformerEngine.CameraSystem
{
    using Microsoft.Xna.Framework;
    using PlatformerEngine.Input;
    using PlatformerEngine.Sprites.PlayerClasses;
    using ZTP_PlatformerGame;

    public enum CameraMode
    {
        FollowPlayer,
        FreeRoam,
        Static,
    }

    public class Camera : IComponent
    {
        private readonly Game1 game;

        private readonly InputManager inputManager;

        private readonly IPlayer player;

        private Vector2 freeroamVelocity = new Vector2(10f, 10f);

        public Camera(Game1 gameReference, InputManager input, IPlayer p)
        {
            game = gameReference;
            inputManager = input;
            player = p;
        }

        public Matrix ViewMatrix { get; private set; } = Matrix.CreateTranslation(0, 0, 0);

        public Vector2 Position { get; private set; } = Vector2.Zero;

        public Vector2 Origin { get; private set; } = Vector2.Zero;

        public CameraMode CameraMode { get; set; } = CameraMode.FollowPlayer;

        public void Update(GameTime gameTime)
        {
            switch (CameraMode)
            {
            case CameraMode.FollowPlayer:
                FollowPlayer();
                break;
            case CameraMode.FreeRoam:
                FreeRoamCamera();
                break;
            case CameraMode.Static:
                StaticCamera();
                break;
            }
        }

        private void FollowPlayer()
        {
            Origin = new Vector2((game.LogicalSize.X / 2) - (player.Size.X / 2), 0);
            Position = new Vector2(player.Position.X, game.LogicalSize.Y * 0.12f);

            // After updating position we can calculate view matrix
            CalculateViewMatrix();
        }

        private void FreeRoamCamera()
        {
            if (inputManager.ActionIsPressed("MoveLeft"))
            {
                Position += new Vector2(-freeroamVelocity.X, 0);
            }
            else if (inputManager.ActionIsPressed("MoveRight"))
            {
                Position += new Vector2(freeroamVelocity.X, 0);
            }

            if (inputManager.ActionIsPressed("MoveUp"))
            {
                Position += new Vector2(0, -freeroamVelocity.Y);
            }
            else if (inputManager.ActionIsPressed("MoveDown"))
            {
                Position += new Vector2(0, freeroamVelocity.Y);
            }

            // After updating position we can calculate view matrix
            CalculateViewMatrix();
        }

        private void StaticCamera()
        {
            CalculateViewMatrix();
        }

        private void CalculateViewMatrix()
        {
            ViewMatrix = Matrix.CreateTranslation(-Position.X, -Position.Y, 0) * Matrix.CreateTranslation(Origin.X, Origin.Y, 0);
        }
    }
}
