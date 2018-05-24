using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    class RedLaserBullet : PlayerBullet
    {
        public RedLaserBullet(string spritesheetName = "bullets") : base(spritesheetName)
        {
            Type = BulletManager.BulletType.RED;
            textureOffset = new Vector2(247, 38);
        }

    }
}