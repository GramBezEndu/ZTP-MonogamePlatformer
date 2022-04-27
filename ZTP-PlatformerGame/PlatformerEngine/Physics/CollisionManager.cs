namespace PlatformerEngine.Physics
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using PlatformerEngine.Sprites.PlayerClasses;

    public class CollisionManager : IComponent
    {
        private List<IPlayer> players;

        private List<IMoveableBody> collidableBodies;

        private List<Rectangle> staticBodies = new List<Rectangle>();

        private List<Rectangle> staticSpikes = new List<Rectangle>();

        public void Update(GameTime gameTime)
        {
            foreach (IMoveableBody c in collidableBodies)
            {
                c.PrepareMove(gameTime);
            }

            foreach (IMoveableBody c in collidableBodies)
            {
                foreach (Rectangle s in staticBodies)
                {
                    CheckForCollision(c, s);
                }

                foreach (Rectangle spike in staticSpikes)
                {
                    CheckForCollisionAndLoseHeart(c, spike);
                }
            }

            // player with enemies collision
            foreach (IPlayer player in players)
            {
                foreach (IMoveableBody c in collidableBodies)
                {
                    // do not check for collsion with other player (if this collidable is in player list then skip)
                    if (players.Contains(c) || c.Hidden)
                    {
                        continue;
                    }

                    if (CheckForCollision(player, c))
                    {
                        player.LoseHeart();
                    }
                }
            }

            // player attack
            foreach (IPlayer player in players)
            {
                foreach (IMoveableBody c in collidableBodies)
                {
                    if (c == player)
                    {
                        continue;
                    }

                    if (player.SwordSlash.Hidden == true)
                    {
                        continue;
                    }

                    if (player.SwordSlash.Rectangle.Intersects(c.Rectangle))
                    {
                        c.LoseHeart();
                    }
                }
            }

            // player out of map
            foreach (IPlayer player in players)
            {
                if (player.Position.Y >= 1500)
                {
                    player.Die();
                }
            }
        }

        public void SetCollisionBodies(List<IMoveableBody> collidables)
        {
            collidableBodies = collidables;
            players = collidables.OfType<IPlayer>().ToList();
        }

        public void SetStaticBodies(List<Rectangle> rectangles)
        {
            staticBodies = rectangles;
        }

        public bool InAir(IMoveableBody c)
        {
            Rectangle collidableEnlarged = new Rectangle(
                (int)c.Position.X,
                (int)c.Position.Y,
                (int)c.Size.X,
                (int)c.Size.Y + 1);
            foreach (Rectangle s in staticBodies)
            {
                if (collidableEnlarged.Intersects(s))
                {
                    return false;
                }
            }

            foreach (Rectangle spike in staticSpikes)
            {
                if (collidableEnlarged.Intersects(spike))
                {
                    return false;
                }
            }

            return true;
        }

        public bool PlayerTouching(IPlayer p, Rectangle r)
        {
            return p.Rectangle.Intersects(r);
        }

        public void SetStaticSpikes(List<Rectangle> rectangles)
        {
            staticSpikes = rectangles;
        }

        private void CheckForCollision(IMoveableBody c, Rectangle s)
        {
            if (IsTouchingRight(c, s))
            {
                float distanceX = c.Rectangle.Left - s.Right;
                c.Velocity = new Vector2(-distanceX, c.Velocity.Y);
            }
            else if (IsTouchingLeft(c, s))
            {
                float distanceX = s.Left - c.Rectangle.Right;
                c.Velocity = new Vector2(distanceX, c.Velocity.Y);
            }

            if (IsTouchingBottom(c, s))
            {
                float distanceY = c.Rectangle.Top - s.Bottom;
                c.Velocity = new Vector2(c.Velocity.X, -distanceY);
            }

            if (IsTouchingTop(c, s))
            {
                float distanceY = s.Top - c.Rectangle.Bottom;
                c.Velocity = new Vector2(c.Velocity.X, distanceY);
            }
        }

        private void CheckForCollisionAndLoseHeart(IMoveableBody c, Rectangle s)
        {
            bool touchingAnything = false;
            if (IsTouchingRight(c, s))
            {
                float distanceX = c.Rectangle.Left - s.Right;
                c.Velocity = new Vector2(-distanceX, c.Velocity.Y);
                touchingAnything = true;
            }
            else if (IsTouchingLeft(c, s))
            {
                float distanceX = s.Left - c.Rectangle.Right;
                c.Velocity = new Vector2(distanceX, c.Velocity.Y);
                touchingAnything = true;
            }

            if (IsTouchingBottom(c, s))
            {
                float distanceY = c.Rectangle.Top - s.Bottom;
                c.Velocity = new Vector2(c.Velocity.X, -distanceY);
                touchingAnything = true;
            }

            if (IsTouchingTop(c, s))
            {
                float distanceY = s.Top - c.Rectangle.Bottom;
                c.Velocity = new Vector2(c.Velocity.X, distanceY);
                touchingAnything = true;
            }

            if (touchingAnything)
            {
                c.LoseHeart();
            }
        }

        private bool IsTouchingLeft(IMoveableBody c, Rectangle r)
        {
            return c.Rectangle.Right + c.Velocity.X > r.Left &&
              c.Rectangle.Left < r.Left &&
              c.Rectangle.Bottom > r.Top &&
              c.Rectangle.Top < r.Bottom;
        }

        private bool CheckForCollision(IMoveableBody c, IMoveableBody c2)
        {
            return c.Rectangle.Intersects(c2.Rectangle);
        }

        private bool IsTouchingRight(IMoveableBody c, Rectangle r)
        {
            return c.Rectangle.Left + c.Velocity.X < r.Right &&
              c.Rectangle.Right > r.Right &&
              c.Rectangle.Bottom > r.Top &&
              c.Rectangle.Top < r.Bottom;
        }

        private bool IsTouchingTop(IMoveableBody c, Rectangle r)
        {
            return c.Rectangle.Bottom + c.Velocity.Y > r.Top &&
              c.Rectangle.Top < r.Top &&
              c.Rectangle.Right > r.Left &&
              c.Rectangle.Left < r.Right;
        }

        private bool IsTouchingBottom(IMoveableBody c, Rectangle r)
        {
            return c.Rectangle.Top + c.Velocity.Y < r.Bottom &&
              c.Rectangle.Bottom > r.Bottom &&
              c.Rectangle.Right > r.Left &&
              c.Rectangle.Left < r.Right;
        }
    }
}
