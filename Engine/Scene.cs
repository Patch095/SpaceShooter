using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_53__22Gennaio2018_Ships
{
    abstract class Scene
    {
        public bool IsPlaying { get; set; }

        public Scene PreviousScene { get; set; }
        public Scene NextScene { get; set; }

        public Scene() { }

        public virtual void Start()
        {
            IsPlaying = true;
        }
        public virtual void Reset()
        {
            IsPlaying = true;
        }
        public virtual void OnExit() { }

        public abstract void Input();
        public abstract void Update();
        public abstract void Draw();
    }
}
