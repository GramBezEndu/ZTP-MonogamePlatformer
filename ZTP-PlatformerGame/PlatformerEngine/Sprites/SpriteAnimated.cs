﻿namespace PlatformerEngine.Sprites
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using MonoGame.Extended;
    using MonoGame.Extended.Animations;
    using MonoGame.Extended.Animations.SpriteSheets;
    using MonoGame.Extended.TextureAtlases;

    public class SpriteAnimated : IDrawableComponent
    {
        private readonly AnimatedSprite animatedSprite;

        private readonly SpriteSheetAnimationFactory animationFactory;

        private readonly TextureAtlas spriteAtlas;

        public SpriteAnimated(Texture2D spritesheet, Dictionary<string, Rectangle> map)
        {
            spriteAtlas = new TextureAtlas("animations", spritesheet, map);
            animationFactory = new SpriteSheetAnimationFactory(spriteAtlas);
            animatedSprite = new AnimatedSprite(animationFactory);
        }

        public Vector2 Scale { get; set; } = Vector2.One;

        public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;

        public bool Hidden { get; set; }

        public Vector2 Position { get; set; }

        public Point Size => new Point(
            (int)(animatedSprite.TextureRegion.Width * Scale.X),
            (int)(animatedSprite.TextureRegion.Height * Scale.Y));

        public Color Color { get; set; } = Color.White;

        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!Hidden)
            {
                spriteBatch.Draw(animatedSprite.TextureRegion, Position, Color, 0f, new Vector2(0, 0), Scale, SpriteEffects, 0f);
                if (Debugger.IsAttached)
                {
                    spriteBatch.DrawRectangle(Rectangle, Color.Blue);
                }
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!Hidden)
            {
                animatedSprite.Update(gameTime);
            }
        }

        /// <summary>
        /// Calls Play() on animatedSprite member.
        /// </summary>
        /// <param name="name">Animation name.</param>
        /// <param name="onCompleted">Action performed when animation is completed.</param>
        public void PlayAnimation(string name, Action onCompleted = null)
        {
            animatedSprite.Play(name, onCompleted);
        }

        public void AddAnimation(string name, SpriteSheetAnimationData data)
        {
            animationFactory.Add(name, data);
        }
    }
}
