namespace PlatformerEngine.Controls
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using PlatformerEngine.Controls.Buttons;
    using PlatformerEngine.Input;

    public class VerticalNavigationMenu : NavigationMenu
    {
        private Vector2 position;

        public VerticalNavigationMenu(InputManager im, List<IButton> listButtons, int margin = 0)
            : base(im, listButtons, margin)
        {
        }

        public override Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                Vector2 currentPos = position;
                foreach (IButton button in Buttons)
                {
                    button.Position = currentPos;
                    currentPos += new Vector2(0, button.Size.Y + Margin);
                }
            }
        }

        public override Point Size
        {
            // Size: X equals the biggest width of buttons; Y equals sum of heights + margin heights
            get
            {
                Point size = new Point(0, 0);
                for (int i = 0; i < Buttons.Count; i++)
                {
                    if (Buttons[i].Size.X > size.X)
                    {
                        size.X = Buttons[i].Size.X;
                    }

                    // Last button - do not add margin
                    if (i == Buttons.Count - 1)
                    {
                        size.Y += Buttons[i].Size.Y;
                    }
                    else
                    {
                        size.Y += Buttons[i].Size.Y + Margin;
                    }
                }

                return size;
            }
        }

        protected override void Navigate()
        {
            if (InputManager.ActionWasJustPressed("MoveUp"))
            {
                CurrentlySelectedButtonIndex = (CurrentlySelectedButtonIndex - 1) % Buttons.Count;

                // There is no button higher than this button -> set it to the index of last button
                if (CurrentlySelectedButtonIndex == -1)
                {
                    CurrentlySelectedButtonIndex = Buttons.Count - 1;
                }
            }
            else if (InputManager.ActionWasJustPressed("MoveDown"))
            {
                CurrentlySelectedButtonIndex = (CurrentlySelectedButtonIndex + 1) % Buttons.Count;
            }
        }
    }
}
