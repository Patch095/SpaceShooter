using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    class Circle
    {
        protected Vector2 relativePosition;

        public Vector2 Position { get { return RigidBody.Position + relativePosition; } }
        public RigidBody RigidBody { get; set; }
        public float Radius { get; protected set; }

        public Circle(Vector2 offset, RigidBody owner, float ray)
        {
            relativePosition = offset;
            RigidBody = owner;
            Radius = ray;
        }

        public bool Contains(Vector2 point)
        {
            Vector2 dist = point - Position;
            return dist.Length <= Radius;
        }

        public bool Collides(Circle circle)
        {
            Vector2 dist = circle.Position - Position;
            return dist.Length <= Radius + circle.Radius;
        }
    }
}