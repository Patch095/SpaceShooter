﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_53__22Gennaio2018_Ships
{
    interface IDrawable
    {
        DrawManager.Layer Layer { get; }
        void Draw();
    }
}
