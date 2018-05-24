using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    class Ash:GameObject
    {
        enum AnimationType { IDLE, DOWN, LEFT, RIGHT, UP}

        public Ash(Vector2 pos) : base(pos, "ash")
        {

        }

        public void Input()
        {
            if (Game.Window.GetKey(KeyCode.Left))
            {
                Animation = Animations[(int)AnimationType.LEFT];
            }
            else if (Game.Window.GetKey(KeyCode.Right))
            {
                Animation = Animations[(int)AnimationType.RIGHT];
            }
            else if (Game.Window.GetKey(KeyCode.Up))
            {
                Animation = Animations[(int)AnimationType.UP];
            }
            else if (Game.Window.GetKey(KeyCode.Down))
            {
                Animation = Animations[(int)AnimationType.DOWN];
            }
            else
            {
                Animation = Animations[(int)AnimationType.IDLE];
            }
        }

        public override void Update()
        {
            base.Update();
            if (IsActive)
                Input();
        }
    }
}
