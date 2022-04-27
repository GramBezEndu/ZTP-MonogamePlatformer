namespace PlatformerEngine.EffectsManager
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Engine.States;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using PlatformerEngine.Sprites;
    using PlatformerEngine.Sprites.PlayerClasses;
    using PlatformerEngine.Timers;

    /// <summary>
    /// All possible effects that will be applied to player.
    /// </summary>
    public enum Effects
    {
        Speed,
        NoAttack,
        MegaJump,
        Invincibility,

        // No jump effect is disabled (it is impossible to complete levels without jumping)
        // NoJump,
    }

    public class PlayerEffectsManager : IDrawableComponent
    {
        private readonly Random random;

        private readonly GameTimer newEffectTimer;

        private readonly double newEffectInterval = 10;

        private readonly SpriteFont font;

        private readonly State state;

        private readonly GraphicsDevice graphicsDevice;

        private readonly DrawableFilledRectangle timerCountdownBackground;

        private readonly Sprite timerCountdownCurrent;

        private readonly Texture2D currentTimerTexture;

        /// <summary>
        /// List of all effects without currectly active ones.
        /// </summary>
        private List<Effects> effectsExcludingActive = new List<Effects>();

        private IPlayer player;

        private Vector2 baseScaleTimer = new Vector2(12.255f, 2.27f);

        public PlayerEffectsManager(State s, GraphicsDevice gd, SpriteFont f, IPlayer p, Texture2D timerTexture)
        {
            state = s;
            player = p;
            font = f;
            graphicsDevice = gd;
            currentTimerTexture = timerTexture;

            random = new Random();
            effectsExcludingActive = Enum.GetValues(typeof(Effects)).Cast<Effects>().ToList();
            newEffectTimer = new GameTimer(newEffectInterval);
            newEffectTimer.OnTimedEvent += (o, e) => DrawEffect();
            timerCountdownBackground = new DrawableFilledRectangle(graphicsDevice, new Rectangle(0, 0, 1152, 25))
            {
                Color = Color.Black,
            };
            timerCountdownCurrent = new Sprite(currentTimerTexture, baseScaleTimer);
        }

        public bool Hidden { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 Scale { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Point Size => throw new NotImplementedException();

        public Rectangle Rectangle => throw new NotImplementedException();

        public Color Color { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public SpriteEffects SpriteEffects { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            timerCountdownBackground.Draw(gameTime, spriteBatch);
            timerCountdownCurrent.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(font, "ACTIVE EFFECTS: ", new Vector2(0, 100), Color.BlueViolet);
            Vector2 pos = new Vector2(0, 135);
            IPlayer temp = player;

            // Draw current effects names
            while (temp is PlayerEffect effect)
            {
                spriteBatch.DrawString(font, effect.Name, pos, Color.White);
                pos += new Vector2(0, 35);
                temp = temp.GetDecorated();
            }
        }

        public void Update(GameTime gameTime)
        {
            if (player.MoveableBodyState != Physics.MoveableBodyStates.Dead)
            {
                newEffectTimer.Update(gameTime);
                timerCountdownCurrent.Scale = new Vector2((float)(newEffectTimer.CurrentInterval / newEffectTimer.Interval) * baseScaleTimer.X, timerCountdownCurrent.Scale.Y);
            }
        }

        public IPlayer GetDecoratedPlayer()
        {
            return player;
        }

        private void DrawEffect()
        {
            if (player.MoveableBodyState != Physics.MoveableBodyStates.Dead)
            {
                // Clear effects when there's only one effect left
                if (effectsExcludingActive.Count <= 1)
                {
                    state.AddNotification("CLEARED ALL EFFECTS");
                    ClearEffects();
                }

                // Roll another effect
                else
                {
                    Effects effect = effectsExcludingActive.ElementAt(random.Next(0, effectsExcludingActive.Count));
                    ActivateEffect(effect);
                    effectsExcludingActive.Remove(effect);
                }
            }
        }

        private void ActivateEffect(Effects effect)
        {
            // Create notification (regex is used to add spaces before Camel case)
            state.AddNotification("NEW EFFECT: " + Regex.Replace(effect.ToString(), "([a-z])([A-Z])", "$1 $2"));
            switch (effect)
            {
            case Effects.Speed:
                player = new SpeedEffect(player);
                break;
            case Effects.NoAttack:
                player = new NoAttackEffect(player);
                break;
            case Effects.MegaJump:
                player = new MegaJumpEffect(player);
                break;
            case Effects.Invincibility:
                player = new InvincibilityEffect(player);
                break;
            }
        }

        /// <summary>
        /// Unpacks player.
        /// </summary>
        private void ClearEffects()
        {
            IPlayer temp = player;
            while (!(temp is Player))
            {
                temp = temp.GetDecorated();
            }

            player = temp;
            effectsExcludingActive = Enum.GetValues(typeof(Effects)).Cast<Effects>().ToList();
        }
    }
}
