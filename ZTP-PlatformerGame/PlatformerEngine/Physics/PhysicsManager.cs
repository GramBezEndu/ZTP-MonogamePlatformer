namespace PlatformerEngine.Physics
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;

    public class PhysicsManager : IComponent
    {
        private const float Gravity = 1f;

        private readonly List<IMoveableBody> moveableBodies;

        private readonly List<Rectangle> staticBodies;

        public PhysicsManager()
        {
            CollisionManager = new CollisionManager();
            moveableBodies = new List<IMoveableBody>();
            staticBodies = new List<Rectangle>();
        }

        public CollisionManager CollisionManager { get; private set; }

        public void AddMoveableBody(IMoveableBody c)
        {
            moveableBodies.Add(c);
        }

        public void AddStaticBody(Rectangle r)
        {
            staticBodies.Add(r);
        }

        public void DeleteBody(IMoveableBody c)
        {
            if (moveableBodies.Contains(c))
            {
                moveableBodies.Remove(c);
            }
            else
            {
                throw new ArgumentException("Body not found");
            }
        }

        public void DeleteStaticBlock(Rectangle r)
        {
            if (staticBodies.Contains(r))
            {
                staticBodies.Remove(r);
            }
            else
            {
                throw new ArgumentException("Block not found");
            }
        }

        public void SetStaticBodies(List<Rectangle> rectangles)
        {
            staticBodies.Clear();
            rectangles.ForEach((item) => staticBodies.Add(item));
            CollisionManager.SetStaticBodies(staticBodies);
        }

        public void SetStaticSpikes(List<Rectangle> rectangles)
        {
            CollisionManager.SetStaticSpikes(rectangles);
        }

        public void Update(GameTime gameTime)
        {
            CollisionManager.SetCollisionBodies(moveableBodies);
            CollisionManager.Update(gameTime);
            foreach (IMoveableBody m in moveableBodies)
            {
                UpdateBodyState(m);
                m.Update(gameTime);
                MoveBody(m);
                ApplyDownForce(m, Gravity);
            }
        }

        private void MoveBody(IMoveableBody c)
        {
            c.Position += c.Velocity;
            if (c.Velocity.X > 0)
            {
                c.Velocity = new Vector2(c.Velocity.X - 1f, c.Velocity.Y);
                if (c.Velocity.X < 0)
                {
                    c.Velocity = new Vector2(0, c.Velocity.Y);
                }
            }
            else if (c.Velocity.X < 0)
            {
                c.Velocity = new Vector2(c.Velocity.X + 1f, c.Velocity.Y);
                if (c.Velocity.X > 0)
                {
                    c.Velocity = new Vector2(0, c.Velocity.Y);
                }
            }
        }

        private void ApplyDownForce(IMoveableBody c, float downForce)
        {
            c.Velocity = new Vector2(c.Velocity.X, c.Velocity.Y + downForce);
        }

        private void UpdateBodyState(IMoveableBody c)
        {
            if (c.MoveableBodyState == MoveableBodyStates.Attacking)
            {
                return;
            }

            if (c.Velocity.X > 0 && c.Velocity.Y != 0)
            {
                c.MoveableBodyState = MoveableBodyStates.InAirRight;
            }
            else if (c.Velocity.X < 0 && c.Velocity.Y != 0)
            {
                c.MoveableBodyState = MoveableBodyStates.InAirLeft;
            }
            else if (c.Velocity.Y != 0)
            {
                c.MoveableBodyState = MoveableBodyStates.InAir;
            }
            else if (c.Velocity.X > 0)
            {
                if (CollisionManager.InAir(c))
                {
                    c.MoveableBodyState = MoveableBodyStates.InAirRight;
                }
                else if (c.MoveableBodyState != MoveableBodyStates.Attacking)
                {
                    c.MoveableBodyState = MoveableBodyStates.WalkRight;
                }
            }
            else if (c.Velocity.X < 0)
            {
                if (CollisionManager.InAir(c))
                {
                    c.MoveableBodyState = MoveableBodyStates.InAirLeft;
                }
                else if (c.MoveableBodyState != MoveableBodyStates.Attacking)
                {
                    c.MoveableBodyState = MoveableBodyStates.WalkLeft;
                }
            }
            else
            {
                if (CollisionManager.InAir(c))
                {
                    c.MoveableBodyState = MoveableBodyStates.InAir;
                }
                else
                {
                    c.MoveableBodyState = MoveableBodyStates.Idle;
                }
            }
        }
    }
}
