using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    class RigidBody : IUpdatable
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Circle BoundingCircle { get; protected set; }
        public Rect BoundingBox { get; protected set; }
        public GameObject GameObject { get; protected set; }
        public bool IsGravityAffected { get; set; }
        public bool IsCollisionsAffected { get; set; }

        public uint Type { get; set; }
        protected uint CollisionMask;

        public RigidBody(Vector2 position, GameObject owner, Circle boundingCircle = null, Rect boundindBox = null, bool CreatingBoundingBox = true)
        {
            Position = position;
            GameObject = owner;

            IsGravityAffected = false;
            IsCollisionsAffected = true;

            if (boundingCircle == null)
            {
                //need to create a circle
                float hWidth = GameObject.Width * GameObject.GetSprite().scale.X / 2;
                float hHeight = GameObject.Height * GameObject.GetSprite().scale.Y / 2;
                float ray = (float)Math.Sqrt(hWidth * hWidth + hHeight * hHeight);
                BoundingCircle = new Circle(Vector2.Zero, this, ray);
            }
            else
            {
                BoundingCircle = boundingCircle;
                BoundingCircle.RigidBody = this;
            }

            if(boundindBox == null)
            {
                if(CreatingBoundingBox == true)
                {
                    BoundingBox = new Rect(Vector2.Zero, this, GameObject.Width, GameObject.Height);
                }
            }
            else
            {
                BoundingBox = boundindBox;
                BoundingBox.RigidBody = this;
            }

            PhysicsManager.AddItem(this);
        }

        public void SetCollisionMask(uint mask)
        {
            CollisionMask = mask;
        }
        public void AddCollisionMask(uint mask)
        {
            CollisionMask |= mask;//or booleano
        }
        public bool CheckCollisionWith(RigidBody other)
        {
            return (CollisionMask & other.Type) != 0;
        }

        public void AddVelocity(float vX, float vY)
        {
            Velocity += new Vector2(vX, vY);
        }
        public void SetXVelocity(float vX)
        {
            Velocity = new Vector2(vX, Velocity.Y);
        }
        public void SetYVelocity(float vY)
        {
            Velocity = new Vector2(Velocity.X, vY);
        }

        public bool Collides(RigidBody other)
        {
            //circle vs circle collision
            //return BoundingCircle.Collides(other.BoundingCircle);
            if (BoundingCircle.Collides(other.BoundingCircle))
            {
                if (BoundingBox != null && other.BoundingBox != null)
                {
                    //rect vs rect collision
                    return BoundingBox.Collides(other.BoundingBox);
                }
                else
                {
                    if (BoundingBox != null)
                    {
                        //rect vs circle
                        return BoundingBox.Collides(other.BoundingCircle);
                    }
                    else if (other.BoundingBox != null)
                    {
                        return other.BoundingBox.Collides(other.BoundingCircle);
                    }
                    else
                    {
                        //none has rect
                        return true;
                    }
                }
            }
            return false;
        }

        public void Update()
        {
            if (IsGravityAffected)
                AddVelocity(0, Game.Gravity * Game.DeltaTime);

            Position += Velocity * Game.DeltaTime;
        }
    }
}