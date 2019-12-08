using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformerEngine.Sprites.PlayerClasses
{
    public interface IPlayer : IDrawableComponent
    {
        void ManagePlayerInput();
    }
}
