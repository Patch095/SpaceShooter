using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    class Player : Ship
    {
        protected int joystickIndex;

        protected float shootDelay;
        protected bool isZPressed;
        protected Ship.ShootType bullet;
        protected float directionY;

        protected Bar nrgBar;

        private bool poweredUp;
        private float poweredUpTime;
        const float POWER_UP_TIMER = 7f;
        private int counter;

        const float STANDAR_SPEED = 350f;

        public Player(string fileName, Vector2 spritePosition) : base(spritePosition, fileName)
        {
            sprite.scale = new Vector2(0.3f, 0.3f);

            horizontalSpeed = STANDAR_SPEED;
            shootDelay = 0.3f;
            cannonOffset = new Vector2(Width / 2, 0);

            bullet = Ship.ShootType.STANDARD;
            directionY = 120;

            RigidBody = new RigidBody(sprite.position, this)
            {
                Type = (uint)PhysicsManager.ColliderType.Player
            };

            MaxNrg = 100;
            nrgBar = new Bar(new Vector2(20, 20), "playerBar", MaxNrg)
            {
                BarOffset = new Vector2(4, 4)
            };
            Nrg = MaxNrg;            

            poweredUp = false;

            joystickIndex = 0;
        }

        public void Input()
        {
            if (IsActive)
            {
                shootCounter -= Game.DeltaTime;

                if (Game.NumJoysticks > 0)
                {
                    Vector2 axis = Game.Window.JoystickAxisLeft(joystickIndex);

                    RigidBody.Velocity = axis * horizontalSpeed;

                    if (shootCounter <= 0 && Game.Window.JoystickA(joystickIndex))
                    {
                        Shoot();
                        shootCounter = shootDelay;
                    }
                    if (Game.Window.JoystickY(joystickIndex))
                    {
                        if (!isZPressed)
                        {
                            ChangeBullets();
                            isZPressed = true;
                        }
                    }
                    else if (isZPressed)
                        isZPressed = false;
                }
                else
                {
                    if (Game.Window.GetKey(KeyCode.Right))
                    {
                        RigidBody.SetXVelocity(horizontalSpeed);
                    }
                    else if (Game.Window.GetKey(KeyCode.Left))
                    {
                        RigidBody.SetXVelocity(-horizontalSpeed);
                    }
                    else
                    {
                        RigidBody.SetXVelocity(0);
                    }

                    if (Game.Window.GetKey(KeyCode.Up))
                    {
                        RigidBody.SetYVelocity(-horizontalSpeed);
                    }
                    else if (Game.Window.GetKey(KeyCode.Down))
                    {
                        RigidBody.SetYVelocity(horizontalSpeed);
                    }
                    else
                    {
                        RigidBody.SetYVelocity(0);
                    }

                    if (shootCounter <= 0)
                    {
                        if (Game.Window.GetKey(KeyCode.Space))
                        {
                            Shoot();
                            shootCounter = shootDelay;
                        }
                        else if (Game.Window.GetKey(KeyCode.Z))
                        {
                            if (!isZPressed)
                            {
                                ChangeBullets();
                                isZPressed = true;
                            }
                        }
                        else if (isZPressed)
                            isZPressed = false;
                    }
                }
            }
        }

        public override bool Shoot()
        {
            Bullet b = BulletManager.GetBullet(BulletManager.BulletType.RED);
            if (b != null)
            {
                if (bullet == ShootType.STANDARD)
                    b.Shoot(Position + cannonOffset, new Vector2(240, 0));
                if (bullet == ShootType.DIAGONAL)
                {
                    b.Shoot(Position + cannonOffset, new Vector2(200, directionY));
                    directionY = -directionY;
                }
                if (bullet == ShootType.TRISHOOT)
                {
                    counter++;
                    if(counter % 2 != 0)
                    {
                        Bullet b2 = BulletManager.GetBullet(BulletManager.BulletType.RED);
                        Bullet b3 = BulletManager.GetBullet(BulletManager.BulletType.RED);
                    
                        if (b2 != null && b3 != null)
                        {
                            b2.Shoot(Position + cannonOffset, new Vector2(200, directionY));
                            b3.Shoot(Position + cannonOffset, new Vector2(200, -directionY));
                        }
                    }
                    b.Shoot(Position + cannonOffset, new Vector2(240, 0));
                }
                return true;
            }
            return false;
        }
        public void ChangeBullets()
        {
            switch (bullet)
            {
                case ShootType.STANDARD:
                    bullet = ShootType.DIAGONAL;
                    break;
                case ShootType.DIAGONAL:
                    bullet = ShootType.STANDARD;
                    break;
            }
        }

        public override void OnCollide(GameObject other)
        {
            if (other.IsActive)
            {
                if (other is EnemyShip)
                {
                    base.OnCollide(other);
                    AddDamage(50);
                }
            }
        }

        public override bool AddDamage(float damage)
        {
            bool isDead = base.AddDamage(damage);
            if (isDead)
            {
                nrgBar.SetValue(0);
            }
            return isDead;
        }
        protected override void SetNrg(float newValue)
        {
            base.SetNrg(newValue);
            nrgBar.SetValue(nrg);
        }

        public void GetShootPowerUp()
        {
            poweredUp = true;
            counter = 0;
            poweredUpTime = POWER_UP_TIMER;
            bullet = ShootType.TRISHOOT;
        }
        public void GetSpeedPowerUp()
        {
            poweredUp = true;
            poweredUpTime = POWER_UP_TIMER;
            horizontalSpeed += RandomGenerator.GetRandom(70, 150);
        }

        public override void Update()
        {
            if (poweredUp)
            {
                poweredUpTime -= Game.DeltaTime;

                if(poweredUpTime <= 0)
                {
                    poweredUpTime = 0;
                    bullet = ShootType.STANDARD;
                    horizontalSpeed = STANDAR_SPEED;
                    poweredUp = false;
                }
            }

            base.Update();
        }

        public override void OnDie()
        {
            //Game.CurrentScene
            base.OnDie();
        }
    }
}
/*    class Player : Ship
    {
        public float ShootDelay { get; set; }
        protected Bar nrgBar;
        public int Index { get; set; }

        public Player(string fileName, Vector2 spritePosition) : base(spritePosition, fileName)
        {
            //sprite.scale = new Vector2(0.3f, 0.3f);
            RigidBody = new RigidBody(sprite.position, this);
            RigidBody.Type = (uint)PhysicsManager.ColliderType.Player;
            horizontalSpeed = 300;
            ShootDelay = 0.45f;
            cannonOffset = new Vector2(Width / 2, 0);

            nrgBar = new Bar(new Vector2(20, 20), "playerBar", MaxNrg);
            nrgBar.SetValue(100);
            nrgBar.BarOffset = new Vector2(4, 4);

            Nrg = 100;
            currentBulletType = BulletManager.BulletType.RedLaser;

            Index = 0;
        }

        protected override void SetNrg(float newValue)
        {
            base.SetNrg(newValue);
            nrgBar.SetValue(nrg);
        }

        public override bool AddDamage(float damage)
        {
            bool isDead= base.AddDamage(damage);
            if (isDead)
            {
                nrgBar.SetValue(0);
            }
            return isDead;
        }

        public void Input()
        {
            shootCounter -= Game.DeltaTime;

            if (Game.NumJoysticks > 0)
            {
                
                Vector2 axis = Game.window.JoystickAxisLeft(Index);

                RigidBody.Velocity = axis * horizontalSpeed;

                if(shootCounter<=0 && Game.window.JoystickA(Index))
                {
                    Shoot(currentBulletType);
                    shootCounter = ShootDelay;
                }
            }
            else
            {

                if (Game.window.GetKey(KeyCode.Right))
                {

                    RigidBody.SetXVelocity(horizontalSpeed);
                }
                else if (Game.window.GetKey(KeyCode.Left))
                {
                    RigidBody.SetXVelocity(-horizontalSpeed);
                }
                else
                {
                    RigidBody.SetXVelocity(0);
                }

                if (Game.window.GetKey(KeyCode.Up))
                {
                    RigidBody.SetYVelocity(-horizontalSpeed);
                }
                else if (Game.window.GetKey(KeyCode.Down))
                {
                    RigidBody.SetYVelocity(horizontalSpeed);
                }
                else
                {
                    RigidBody.SetYVelocity(0);
                }

                
                if (shootCounter <= 0)
                {
                    if (Game.window.GetKey(KeyCode.Space))
                    {
                        Shoot(currentBulletType);
                        shootCounter = ShootDelay;
                    }


                }
            }
        }

    }

 */
