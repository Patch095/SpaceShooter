using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    class Explosion : GameObject
    {
        public Explosion(Vector2 spritePosition) : base(spritePosition, "explosion", DrawManager.Layer.Foreground)
        {

        }

        public override void Update()
        {
            base.Update();
            if(Animation.IsActive == false)
            {
                OnExplosionEnds();
            }
        }
        private void OnExplosionEnds()
        {
            DrawManager.RemoveItem(this);
            UpdateManager.RemoveItem(this);
        }
    }
}
