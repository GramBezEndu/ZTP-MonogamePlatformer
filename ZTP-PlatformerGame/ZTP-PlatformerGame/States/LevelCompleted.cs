using Engine.Controls.Buttons;
using Engine.States;
using Microsoft.Xna.Framework;
using PlatformerEngine.Controls;
using PlatformerEngine.Controls.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP_PlatformerGame.States
{
    public class LevelCompleted : State
    {
        public LevelCompleted(Game1 gameReference) : base(gameReference)
        {
            Text congratulationsText = AddCongratulationsText();
            AddGoToMenuText(congratulationsText);
        }

        private Text AddCongratulationsText()
        {
            var congratulationsText = new Text(font, "COGRATULATIONS! YOU HAVE COMPLETED THE GAME!");
            congratulationsText.Position = new Vector2(game.LogicalSize.X / 2 - congratulationsText.Size.X / 2,
                game.LogicalSize.Y / 2 - congratulationsText.Size.Y / 2);
            AddUiComponent(congratulationsText);
            return congratulationsText;
        }

        private void AddGoToMenuText(Text congratulationsText)
        {
            var pressSpace = new Text(font, "PRESS SPACE")
            {
                Color = Color.Red
            };
            pressSpace.Position = new Vector2(game.LogicalSize.X / 2 - pressSpace.Size.X / 2,
                congratulationsText.Position.Y + congratulationsText.Size.Y);
            AddUiComponent(pressSpace);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(inputManager.ActionWasPressed("Attack"))
            {
                game.ChangeState(new MainMenu(game));
            }
        }
    }
}
