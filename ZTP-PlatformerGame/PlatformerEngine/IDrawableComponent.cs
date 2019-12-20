using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ZTP_PlatformerGame;

namespace PlatformerEngine
{
    public interface IDrawableComponent : IComponent
    {
        bool Hidden { get; set; }
        Vector2 Position { get; set; }
        Vector2 Scale { get; set; }
        Size Size { get; }
        Rectangle Rectangle { get; }
        Color Color { get; set; }
        SpriteEffects SpriteEffects { get; set; }
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
