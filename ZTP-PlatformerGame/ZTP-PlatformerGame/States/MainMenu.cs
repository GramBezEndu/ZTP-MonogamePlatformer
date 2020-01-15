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
    public class MainMenu : State
    {
        public MainMenu(Game1 game) : base(game)
        {
            //AddUiComponent(new Text(font, "MAIN MENU"));
            var navigation = new VerticalNavigationMenu(game.inputManager, new List<IButton>()
            {
                new TextButton(inputManager, font, "PLAY")
                {
                    OnClick = (o, e) => game.ChangeState(new FirstLevel(game))
                },
                new TextButton(inputManager, font, "EXIT")
                {
                    OnClick = (o,e) => game.Exit()
                }
            });
            navigation.Position = new Vector2(game.LogicalSize.X / 2 - navigation.Size.X / 2, game.LogicalSize.Y / 2 - navigation.Size.Y / 2);
            AddUiComponent(navigation);
        }
    }
}
