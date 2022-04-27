namespace ZTP_PlatformerGame.States
{
    using Engine.States;
    using Microsoft.Xna.Framework;
    using PlatformerEngine.Controls;

    public class HowToPlay : State
    {
        public HowToPlay(Game1 gameReference)
            : base(gameReference)
        {
            AddKeybindingsTexts();
            AddGoBackText();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (InputManager.ActionWasJustPressed("Back"))
            {
                Game.ChangeState(new MainMenu(Game));
            }
        }

        private void AddGoBackText()
        {
            Text goBack = new Text(Font, "[ESC] GO BACK")
            {
                Color = Color.Red,
            };
            goBack.Position = new Vector2(0, Game.LogicalSize.Y - goBack.Size.Y);
            AddUiComponent(goBack);
        }

        private void AddKeybindingsTexts()
        {
            Text moveUp = new Text(Font, "MOVE UP: [W]");
            moveUp.Position = new Vector2(
                (Game.LogicalSize.X / 2) - (moveUp.Size.X / 2),
                (Game.LogicalSize.Y / 6) - (moveUp.Size.Y / 2));
            AddUiComponent(moveUp);

            Text moveLeft = new Text(Font, "MOVE LEFT: [A]");
            moveLeft.Position = new Vector2(
                (Game.LogicalSize.X / 2) - (moveLeft.Size.X / 2),
                moveUp.Rectangle.Bottom);
            AddUiComponent(moveLeft);

            Text moveRight = new Text(Font, "MOVE RIGHT: [D]");
            moveRight.Position = new Vector2(
                (Game.LogicalSize.X / 2) - (moveRight.Size.X / 2),
                moveLeft.Rectangle.Bottom);
            AddUiComponent(moveRight);

            Text moveDown = new Text(Font, "MOVE DOWN: [S]");
            moveDown.Position = new Vector2(
                (Game.LogicalSize.X / 2) - (moveDown.Size.X / 2),
                moveRight.Rectangle.Bottom);
            AddUiComponent(moveDown);

            Text attack = new Text(Font, "ATTACK/ACCEPT: [SPACE]");
            attack.Position = new Vector2(
                (Game.LogicalSize.X / 2) - (attack.Size.X / 2),
                moveDown.Rectangle.Bottom);
            AddUiComponent(attack);

            Text back = new Text(Font, "BACK: [ESC]");
            back.Position = new Vector2(
                (Game.LogicalSize.X / 2) - (back.Size.X / 2),
                attack.Rectangle.Bottom);
            AddUiComponent(back);
        }
    }
}
