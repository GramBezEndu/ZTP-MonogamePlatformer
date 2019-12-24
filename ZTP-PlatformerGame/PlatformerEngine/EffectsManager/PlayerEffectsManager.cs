using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Engine.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerEngine.Sprites.PlayerClasses;
using PlatformerEngine.Timers;

namespace PlatformerEngine.EffectsManager
{
    public enum Effects
    {
        Speed,
        NoAttack,
        //NoJump,
        MegaJump
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

        /// <summary>
        /// Used to draw new effects -> it is a copy of Effects enum but when new effect is drawn it is deleted from here (so we can not draw it second time while it's active)
        /// After the effect ends, the value is retrieved
        /// </summary>
        private List<Effects> effectsExcludingActive = new List<Effects>();

        private IPlayer player;
        private SpriteFont font;
        private State state;

        public PlayerEffectsManager(State s, SpriteFont f, IPlayer p)
        {
            state = s;
            player = p;
            font = f;
            effectsExcludingActive = Enum.GetValues(typeof(Effects)).Cast<Effects>().ToList();
            random = new Random();
            newEffectTimer = new GameTimer(10.0)
            {
                OnTimedEvent = (o, e) => DrawEffect()
            };
        }

        private void DrawEffect()
        {
            //Clear effects when there's only one effect left
            if(effectsExcludingActive.Count <= 1)
            {
                state.AddNotification("CLEARED ALL EFFECTS");
                ClearEffects();
            }
            //Roll another effect
            else
            {
                var effect = effectsExcludingActive.ElementAt(random.Next(0, effectsExcludingActive.Count));
                ActivateEffect(effect);
                effectsExcludingActive.Remove(effect);
            }
        }

        private void ActivateEffect(Effects effect)
        {
            state.AddNotification("NEW EFFECT: " + effect);
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
                //case Effects.NoJump:
                //    player = new NoJumpEffect(player);
                //    break;
            }
        }

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

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var temp = player;
            spriteBatch.DrawString(font, "ACTIVE EFFECTS: ", new Vector2(0, 100), Color.Red);
            var pos = new Vector2(0, 135);
            while (temp is PlayerEffect effect)
            {
                spriteBatch.DrawString(font, effect.Name, pos, Color.Red);
                pos += new Vector2(0, 35);
                temp = temp.GetDecorated();
            }
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
