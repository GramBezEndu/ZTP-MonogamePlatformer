namespace ZTP_PlatformerGame.States
{
    using Engine.States;
    using Microsoft.Xna.Framework;
    using PlatformerEngine.Controls;

    public class LevelCompleted : State
    {
        public LevelCompleted(Game1 gameReference)
            : base(gameReference)
        {
            Text congratulationsText = AddCongratulationsText();
            AddGoToMenuText(congratulationsText);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (InputManager.ActionWasJustPressed("Attack"))
            {
                Game.ChangeState(new MainMenu(Game));
            }
        }

        private Text AddCongratulationsText()
        {
            Text congratulationsText = new Text(Font, "COGRATULATIONS! YOU HAVE COMPLETED THE GAME!");
            congratulationsText.Position = new Vector2(
                (Game.LogicalSize.X / 2) - (congratulationsText.Size.X / 2),
                (Game.LogicalSize.Y / 2) - (congratulationsText.Size.Y / 2));
            AddUiComponent(congratulationsText);
            return congratulationsText;
        }

        private void AddGoToMenuText(Text congratulationsText)
        {
            Text pressSpace = new Text(Font, "PRESS SPACE")
            {
                Color = Color.Red,
            };
            pressSpace.Position = new Vector2(
                (Game.LogicalSize.X / 2) - (pressSpace.Size.X / 2),
                congratulationsText.Position.Y + congratulationsText.Size.Y);
            AddUiComponent(pressSpace);
        }
    }
}
