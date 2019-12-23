using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerEngine.Sprites.PlayerClasses;
using PlatformerEngine.Timers;

namespace PlatformerEngine.EffectsManager
{
    public enum Effects
    {
        Speed,
        NoActionAllowed,
        NoJump
    }
    /// <summary>
    /// TODO: Implement multiple effects at a time (correct unpacking)
    /// </summary>
    public class PlayerEffectsManager : IDrawableComponent
    {
        public bool Hidden { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Scale { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Point Size => throw new NotImplementedException();

        public Rectangle Rectangle => throw new NotImplementedException();

        public Color Color { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public SpriteEffects SpriteEffects { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private Random random;
        private GameTimer newEffectTimer;
        private Dictionary<PlayerEffect, GameTimer> actualEffects = new Dictionary<PlayerEffect, GameTimer>();

        /// <summary>
        /// All possible effects with duration
        /// </summary>
        public Dictionary<Effects, double> AllPossibleEffects = new Dictionary<Effects, double>()
        {
            { Effects.Speed, 20 },
            { Effects.NoActionAllowed, 5 },
            { Effects.NoJump, 10 },
        };

        /// <summary>
        /// Used to draw new effects -> it is a copy of AllPossibleEffects but when new effect is drawn it is deleted from here (so we can not draw it second time while it's active)
        /// After the effect ends, the value is retrieved
        /// </summary>
        public Dictionary<Effects, double> EffectsExcludingActive = new Dictionary<Effects, double>();

        private IPlayer player;

        public PlayerEffectsManager(IPlayer p)
        {
            player = p;
            EffectsExcludingActive = AllPossibleEffects.ToDictionary(entry => entry.Key,
                                               entry => entry.Value);
            random = new Random();
            newEffectTimer = new GameTimer(15.0)
            {
                OnTimedEvent = (o, e) => DrawEffect()
            };
        }

        private void DrawEffect()
        {
            var effect = EffectsExcludingActive.ElementAt(random.Next(0, EffectsExcludingActive.Count));
            ActivateEffect(effect);
            EffectsExcludingActive.Remove(effect.Key);
        }

        private void ActivateEffect(KeyValuePair<Effects, double> effect)
        {
            //TODO: finish
            Debug.WriteLine("New effect: " + effect.Key);
            switch(effect.Key)
            {
                case Effects.Speed:
                    player = new SpeedEffect(player);
                    break;
                case Effects.NoActionAllowed:
                    player = new NoActionAllowedEffect(player);
                    break;
                case Effects.NoJump:
                    player = new NoJumpEffect(player);
                    break;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //var temp = player;
            //while(temp.GetDecorated() is PlayerEffect)
            //{
            //    temp = temp.GetDecorated();
            //}
        }

        public void Update(GameTime gameTime)
        {
            newEffectTimer.Update(gameTime);
        }

        public IPlayer GetDecoratedPlayer()
        {
            return player;
        }
    }
}
