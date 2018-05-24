using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    class BigBlueLaser : EnemyBullet
    {
        public BigBlueLaser(string textureName = "blueLaser") : base(textureName)
        {
            Type = BulletManager.BulletType.BLUE;
            textureOffset = new Vector2(314, 300);
            dmg = 20;
        }

        public override void Shoot(Vector2 startPos, Vector2 shootVelocity)
        {
            base.Shoot(startPos, shootVelocity - new Vector2(400, 0));
            X -= 40;
        }
    }
}