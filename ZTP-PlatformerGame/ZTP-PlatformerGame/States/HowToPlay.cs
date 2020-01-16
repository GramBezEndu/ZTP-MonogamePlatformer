using Engine.States;
using Microsoft.Xna.Framework;
using PlatformerEngine.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP_PlatformerGame.States
{
    public class HowToPlay : State
    {
        public HowToPlay(Game1 gameReference) : base(gameReference)
        {
            AddKeybindingsTexts();
            AddGoBackText();
        }

        private void AddGoBackText()
        {
            var goBack = new Text(font, "[ESC] GO BACK")
            {
                Color = Color.Red
            };
            goBack.Position = new Vector2(0, game.LogicalSize.Y - goBack.Size.Y);
            AddUiComponent(goBack);
        }

        private void AddKeybindingsTexts()
        {
            var moveUp = new Text(font, "MOVE UP: [W]");
            moveUp.Position = new Vector2(game.LogicalSize.X / 2 - moveUp.Size.X / 2,
                game.LogicalSize.Y / 6 - moveUp.Size.Y / 2);
            AddUiComponent(moveUp);

            var moveLeft = new Text(font, "MOVE LEFT: [A]");
            moveLeft.Position = new Vector2(game.LogicalSize.X / 2 - moveLeft.Size.X / 2,
                moveUp.Rectangle.Bottom);
            AddUiComponent(moveLeft);

            var moveRight = new Text(font, "MOVE RIGHT: [D]");
            moveRight.Position = new Vector2(game.LogicalSize.X / 2 - moveRight.Size.X / 2,
                moveLeft.Rectangle.Bottom);
            AddUiComponent(moveRight);

            var moveDown = new Text(font, "MOVE DOWN: [S]");
            moveDown.Position = new Vector2(game.LogicalSize.X / 2 - moveDown.Size.X / 2,
                moveRight.Rectangle.Bottom);
            AddUiComponent(moveDown);

            var attack = new Text(font, "ATTACK/ACCEPT: [SPACE]");
            attack.Position = new Vector2(game.LogicalSize.X / 2 - attack.Size.X / 2,
                moveDown.Rectangle.Bottom);
            AddUiComponent(attack);

            var back = new Text(font, "BACK: [ESC]");
            back.Position = new Vector2(game.LogicalSize.X / 2 - back.Size.X / 2,
                attack.Rectangle.Bottom);
            AddUiComponent(back);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (inputManager.ActionWasPressed("Back"))
                game.ChangeState(new MainMenu(game));
        }
    }
}
