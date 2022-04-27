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
            TextButton play = new TextButton(InputManager, Font, "PLAY");
            play.OnClick += (o, e) => game.ChangeState(new FirstLevel(game));
            TextButton howToPlay = new TextButton(InputManager, Font, "HOW TO PLAY");
            howToPlay.OnClick += (o, e) => game.ChangeState(new HowToPlay(game));
            TextButton exit = new TextButton(InputManager, Font, "EXIT");
            exit.OnClick += (o, e) => game.Exit();
            VerticalNavigationMenu navigation = new VerticalNavigationMenu(game.InputManager, new List<IButton>()
            {
                play,
                howToPlay,
                exit,
            });
            navigation.Position = new Vector2(
                (game.LogicalSize.X / 2) - (navigation.Size.X / 2),
                (game.LogicalSize.Y / 2) - (navigation.Size.Y / 2));
            AddUiComponent(navigation);
        }
    }
}
