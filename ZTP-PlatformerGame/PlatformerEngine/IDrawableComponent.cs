namespace PlatformerEngine
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public interface IDrawableComponent : IComponent
    {
        bool Hidden { get; set; }

        Vector2 Position { get; set; }

        Vector2 Scale { get; set; }

        Point Size { get; }

        Rectangle Rectangle { get; }

        Color Color { get; set; }

        SpriteEffects SpriteEffects { get; set; }

        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
