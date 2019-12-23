using Microsoft.Xna.Framework;
using PlatformerEngine.Input;
using PlatformerEngine.Sprites.PlayerClasses;
using System;
using System.Collections.Generic;
using System.Text;
using ZTP_PlatformerGame;

namespace PlatformerEngine.CameraSystem
{
    public class Camera : IComponent
    {
        public enum CameraModes
        {
            FollowPlayer,
            FreeRoam,
            Static
        }
        public CameraModes CameraMode = CameraModes.FollowPlayer;
        readonly Game1 game;
        readonly InputManager inputManager;
        readonly IPlayer player;
        public Matrix ViewMatrix { get; private set; } = Matrix.CreateTranslation(0, 0, 0);
        public Vector2 Position { get; private set; } = Vector2.Zero;

        public Vector2 Origin { get; private set; } = Vector2.Zero;

        private Vector2 freeroamVelocity = new Vector2(10f, 10f);
        public Camera(Game1 gameReference, InputManager input, IPlayer p)
        {
            game = gameReference;
            inputManager = input;
            player = p;
        }
        public void Update(GameTime gameTime)
        {
            switch (CameraMode)
            {
                case CameraModes.FollowPlayer:
                    FollowPlayer();
                    break;
                case CameraModes.FreeRoam:
                    FreeRoamCamera();
                    break;
                case CameraModes.Static:
                    StaticCamera();
                    break;
            }
        }

        private void FollowPlayer()
        {
            //Origin = new Vector2(game.LogicalSize.X / 2 - player.Size.X / 2, game.LogicalSize.Y * (2 / 3f) - player.Size.Y / 2);
            //Position = new Vector2(player.Position.X, player.Position.Y);
            Origin = new Vector2(game.LogicalSize.X / 2 - player.Size.X / 2, 0);
            Position = new Vector2(player.Position.X, Position.Y);

            //After updating position we can calculate view matrix
            CalculateViewMatrix();
        }

        private void FreeRoamCamera()
        {
            if (inputManager.ActionIsPressed("MoveLeft"))
            {
                Position += new Vector2(-freeroamVelocity.X, 0);
            }
            if (inputManager.ActionIsPressed("MoveRight"))
            {
                Position += new Vector2(freeroamVelocity.X, 0);
            }
            if (inputManager.ActionIsPressed("MoveUp"))
            {
                Position += new Vector2(0, -freeroamVelocity.Y);
            }
            if (inputManager.ActionIsPressed("MoveDown"))
            {
                Position += new Vector2(0, freeroamVelocity.Y);
            }
            //After updating position we can calculate view matrix
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
