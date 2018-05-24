using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    class Rect
    {
        protected Vector2 relativePosition;

        public Vector2 Position { get { return RigidBody.Position + relativePosition; } }
        public RigidBody RigidBody { get; set; }

        public float HalfWidth { get; protected set; }
        public float HalfHeight { get; protected set; }

        public Rect(Vector2 offset, RigidBody owner, float width, float height)
        {
            relativePosition = offset;
            RigidBody = owner;
            HalfWidth = width / 2;
            HalfHeight = height / 2;
        }

        public bool Collides(Rect other)
        {
            Vector2 distance = other.Position - Position;
            bool collision = false;
            float deltaX = Math.Abs(distance.X) - (HalfWidth + other.HalfWidth);
            float deltaY = Math.Abs(distance.Y) - (HalfHeight + other.HalfHeight);
            if (deltaX <= 0 && deltaY <= 0)
                collision = true;
            return collision;
        }
        public bool Collides(Circle circle)
        {
            Vector2 topLeft = Position - new Vector2(HalfWidth, HalfHeight);

            float nearestX = Math.Max(topLeft.X, Math.Min(circle.Position.X, topLeft.X + (HalfWidth * 2)));
            float nearestY = Math.Max(topLeft.Y, Math.Min(circle.Position.Y, topLeft.Y + (HalfHeight * 2)));

            float deltaX = circle.Position.X - nearestX;
            float deltaY = circle.Position.Y - nearestY;

            return ((deltaX * deltaX + deltaY * deltaY) <= circle.Radius * circle.Radius);
        }
    }
}