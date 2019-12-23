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
        NoAttack,
        NoJump,
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
        /// All possible effects with duration
        /// </summary>
        public Dictionary<Effects, double> AllPossibleEffects = new Dictionary<Effects, double>()
        {
            { Effects.Speed, 20 },
            { Effects.MegaJump, 20 },
            //{ Effects.NoJump, 10 },
            { Effects.NoAttack, 20 },
        };

        /// <summary>
        /// Used to draw new effects -> it is a copy of AllPossibleEffects but when new effect is drawn it is deleted from here (so we can not draw it second time while it's active)
        /// After the effect ends, the value is retrieved
        /// </summary>
        private Dictionary<Effects, double> effectsExcludingActive = new Dictionary<Effects, double>();

        private Dictionary<Effects, GameTimer> actualEffects = new Dictionary<Effects, GameTimer>();

        private IPlayer player;
        private SpriteFont font;

        public PlayerEffectsManager(SpriteFont f, IPlayer p)
        {
            player = p;
            font = f;
            effectsExcludingActive = AllPossibleEffects.ToDictionary(entry => entry.Key,
                                               entry => entry.Value);
            random = new Random();
            newEffectTimer = new GameTimer(10.0)
            {
                OnTimedEvent = (o, e) => DrawEffect()
            };
        }

        private void DrawEffect()
        {
            if(effectsExcludingActive.Count > 0)
            {
                var effect = effectsExcludingActive.ElementAt(random.Next(0, effectsExcludingActive.Count));
                ActivateEffect(effect);
                //actualEffects.Add(effect.Key, eff);
                effectsExcludingActive.Remove(effect.Key);
            }
        }

        private void ActivateEffect(KeyValuePair<Effects, double> effect)
        {
            //TODO: finish
            Debug.WriteLine("New effect: " + effect.Key);
            switch(effect.Key)
            {
                case Effects.Speed:
                    player = new SpeedEffect(player);
                    actualEffects.Add(Effects.Speed, new GameTimer(AllPossibleEffects[Effects.Speed])
                    {
                        OnTimedEvent = (o, e) => RemoveEffect(Effects.Speed)
                    });
                    break;
                case Effects.NoAttack:
                    player = new NoAttackEffect(player);
                    actualEffects.Add(Effects.NoAttack, new GameTimer(AllPossibleEffects[Effects.NoAttack])
                    {
                        OnTimedEvent = (o, e) => RemoveEffect(Effects.Speed)
                    });
                    break;
                case Effects.MegaJump:
                    player = new MegaJumpEffect(player);
                    actualEffects.Add(Effects.MegaJump, new GameTimer(AllPossibleEffects[Effects.MegaJump])
                    {
                        OnTimedEvent = (o, e) => RemoveEffect(Effects.Speed)
                    });
                    break;
                case Effects.NoJump:
                    player = new NoJumpEffect(player);
                    actualEffects.Add(Effects.NoJump, new GameTimer(AllPossibleEffects[Effects.NoJump])
                    {
                        OnTimedEvent = (o, e) => RemoveEffect(Effects.Speed)
                    });
                    break;
            }
        }

        private void RemoveEffect(Effects effect)
        {
            //Debug.WriteLine("Removing effect: " + effect);
            //switch(effect)
            //{
            //    case Effects.Speed:
            //        effectsExcludingActive.Add(Effects.Speed, AllPossibleEffects[Effects.Speed]);
            //        ////Build a new chain of decorators, which skips this effect
            //        //Player playerTemp;
            //        //IPlayer temp = player;
            //        //List<IPlayer> playerWithEffects = new List<IPlayer>();
            //        //while (!(temp is Player))
            //        //{
            //        //    temp = temp.GetDecorated();
            //        //}
            //        //playerTemp = temp as Player;

            //        //var temp2 = player;
            //        //while (!(temp2 is SpeedEffect))
            //        //{
            //        //    temp2 = temp2.GetDecorated();
            //        //}

            //        break;
            //    case Effects.NoJump:
            //        effectsExcludingActive.Add(Effects.NoJump, AllPossibleEffects[Effects.NoJump]);
            //        break;
            //    case Effects.MegaJump:
            //        effectsExcludingActive.Add(Effects.MegaJump, AllPossibleEffects[Effects.MegaJump]);
            //        break;
            //    case Effects.NoAttack:
            //        effectsExcludingActive.Add(Effects.NoAttack, AllPossibleEffects[Effects.NoAttack]);
            //        break;
            //}
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var temp = player;
            var pos = Vector2.Zero;
            while (temp is PlayerEffect effect)
            {
                spriteBatch.DrawString(font, effect.Name, pos, Color.Red);
                pos += new Vector2(0, 20);
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
