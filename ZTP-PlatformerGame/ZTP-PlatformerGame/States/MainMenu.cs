using Engine.States;
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
            AddUiComponent(new Text(font, "MAIN MENU"));
            //AddUiComponent(new VerticalNavigationMenu(game.inputManager, new List<IButton>()
            //{
            //    //new TextButton()
            //}));
        }
    }
}
