using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace PlatformerEngine.Sprites.PlayerClasses
{
    public class Player : SpriteAnimated, IPlayer
    {
        public Vector2 VelocityConst = new Vector2(5f, 2f);

        public void ManagePlayerInput()
        {
            throw new NotImplementedException();
        }
    }
}
