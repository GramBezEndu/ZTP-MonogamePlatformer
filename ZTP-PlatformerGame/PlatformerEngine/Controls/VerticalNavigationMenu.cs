using Microsoft.Xna.Framework;
using PlatformerEngine.Controls.Buttons;
using PlatformerEngine.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Controls
{
    public class VerticalNavigationMenu : NavigationMenu
    {
        private Vector2 position;

        public VerticalNavigationMenu(InputManager im, List<IButton> listButtons, int margin = 0) : base(im, listButtons, margin)
        {
        }

        public override Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                Vector2 currentPos = position;
                foreach (var button in buttons)
                {
                    button.Position = currentPos;
                    currentPos += new Vector2(0, button.Size.Y + Margin);
                }
            }
        }

        public override Point Size
        {
            //Size: X equals the biggest width of buttons; Y equals sum of heights + margin heights
            get
            {
                Point size = new Point(0, 0);
                for (int i = 0; i < buttons.Count; i++)
                {
                    if (buttons[i].Size.X > size.X)
                        size.X = buttons[i].Size.X;
                    //Last button - do not add margin
                    if (i == buttons.Count - 1)
                        size.Y += buttons[i].Size.Y;
                    else
                        size.Y += buttons[i].Size.Y + Margin;
                }
                return size;
            }
        }

        protected override void Navigate()
        {
            if (inputManager.ActionWasPressed("MoveUp"))
            {
                currentlySelectedButton = (currentlySelectedButton - 1) % buttons.Count;
                //there is no button higher than this button -> set it to the index of last button
                if (currentlySelectedButton == - 1)
                    currentlySelectedButton = buttons.Count - 1;
            }
            else if (inputManager.ActionWasPressed("MoveDown"))
            {
                currentlySelectedButton = (currentlySelectedButton + 1) % buttons.Count;
            }
        }
    }
}
