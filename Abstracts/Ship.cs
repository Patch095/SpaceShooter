using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    abstract class Ship : GameObject
    {
        public enum ShootType { TRISHOOT, VERTICAL, DIAGONAL, STANDARD };
        protected ShootType shootType;

        protected float horizontalSpeed;
        protected Vector2 cannonOffset;
        protected float shootCounter;

        protected float nrg;

        public float MaxNrg { get; protected set; }

        public float Nrg { get { return nrg; } set { SetNrg(value); } }

        public Ship(Vector2 spritePosition, string spritesheetName, DrawManager.Layer drawLayer = DrawManager.Layer.Playground) : base(spritePosition, spritesheetName, drawLayer)
        {
            sprite.pivot = new Vector2(Width / 2, Height / 2);
        }

        abstract public bool Shoot();
        public override void OnCollide(GameObject other)
        {
            base.OnCollide(other);
        }

        /*public virtual void Shoot(BulletManager.BulletType type)
        {
            Bullet b = BulletManager.GetBullet(type);
            if (b != null)
            {
                float bulletOffsetX = b.Width / 2;
                if (sprite.FlipX)
                    bulletOffsetX = -bulletOffsetX;
                b.Shoot(Position + cannonOffset + new Vector2(bulletOffsetX, 0), b.);
            }
        }*/

        public virtual void OnDie()
        {
            IsActive = false;
            if (Position.X + Width / 2 > 0)
                new Explosion(new Vector2(Position.X - Width / 2, Position.Y - Height));
        }

        public virtual bool AddDamage(float damage)
        {
            Nrg -= damage;
            if (Nrg > MaxNrg)
                Nrg = MaxNrg;
            if (Nrg <= 0)
            {
                OnDie();
                return true;
            }
            return false;
        }
        protected virtual void SetNrg(float newValue)
        {
            nrg = newValue;
            if (nrg > MaxNrg)
            {
                nrg = MaxNrg;
            }
        }
    }
}