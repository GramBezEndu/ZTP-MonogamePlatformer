using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace PlatformerEngine.Sprites.PlayerClasses
{
    public abstract class PlayerEffect : IPlayer
    {
        private readonly Player player;
        public PlayerEffect(Player p)
        {
            player = p;
        }

        public bool Hidden { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 Position { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Vector2 Scale { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Size Size => throw new NotImplementedException();

        public Rectangle Rectangle => throw new NotImplementedException();

        public Color Color { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public SpriteEffects SpriteEffects { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public abstract void ManagePlayerInput();

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
