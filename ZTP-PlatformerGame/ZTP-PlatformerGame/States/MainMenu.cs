namespace ZTP_PlatformerGame.States
{
    using System.Collections.Generic;
    using Engine.Controls.Buttons;
    using Engine.States;
    using Microsoft.Xna.Framework;
    using PlatformerEngine.Controls;
    using PlatformerEngine.Controls.Buttons;

    public class MainMenu : State
    {
        public MainMenu(Game1 game)
            : base(game)
        {
            VerticalNavigationMenu navigation = new VerticalNavigationMenu(game.inputManager, new List<IButton>()
            {
                new TextButton(inputManager, Font, "PLAY")
                {
                    OnClick = (o, e) => game.ChangeState(new FirstLevel(game)),
                },
                new TextButton(inputManager, Font, "HOW TO PLAY")
                {
                    OnClick = (o, e) => game.ChangeState(new HowToPlay(game)),
                },
                new TextButton(inputManager, Font, "EXIT")
                {
                    OnClick = (o, e) => game.Exit(),
                },
            });
            navigation.Position = new Vector2(
                (game.LogicalSize.X / 2) - (navigation.Size.X / 2),
                (game.LogicalSize.Y / 2) - (navigation.Size.Y / 2));
            AddUiComponent(navigation);
        }
    }
}
