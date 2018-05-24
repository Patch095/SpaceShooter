using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    class PlayScene : Scene
    {
        static Player player;
        static Background bg;
        static float timer;

        public override void Start()
        {
            base.Start();

            GfxManager.Init();
            GfxManager.Load();
            /*GfxManager.AddTexture("player", "Assets/playerShip.png");
            GfxManager.AddTexture("bg", "Assets/spaceBg.png");
            GfxManager.AddTexture("bullets", "Assets/beams.png");
            GfxManager.AddTexture("fire", "Assets/fireGlobe.png");
            GfxManager.AddTexture("enemy", "Assets/enemy.png");
            GfxManager.AddTexture("redEnemy", "Assets/enemy2.png");
            GfxManager.AddTexture("botEnemy", "Assets/omega.png");
            GfxManager.AddTexture("bonus", "Assets/FF8_UFO.png");
            GfxManager.AddTexture("explosion", "Assets/explosion.png");
            GfxManager.AddTexture("playerBar", "Assets/loadingBar_bar.png");
            GfxManager.AddTexture("barFrame", "Assets/loadingBar_frame.png");*/

            UpdateManager.Init();
            DrawManager.Init();
            PhysicsManager.Init();
            BulletManager.Init();
            SpawnManager.Init();

            player = new Player("player", new Vector2(Game.Window.Width / 2, Game.Window.Height / 2));
            bg = new Background("bg", Vector2.Zero, -300);

            timer = 2f;
        }
        
        public override void Input()
        {
            if (!player.IsActive)
            {
                timer -= Game.DeltaTime;
                if (timer <= 0)
                    IsPlaying = false;
                //break;
            }
            if (player.IsActive)
                player.Input();
        }

        public override void Update()
        {
            PhysicsManager.Update();
            UpdateManager.Update();
            PhysicsManager.CheckCollisions();
            SpawnManager.EnemiesSpawn();
        }

        public override void Draw()
        {
            DrawManager.Draw();
        }

        public override void OnExit()
        {
            UpdateManager.RemoveAll();
            DrawManager.RemoveAll();
            PhysicsManager.RemoveAll();
            GfxManager.RemoveAll();
            BulletManager.RemoveAll();
            SpawnManager.RemoveAll();
        }
    }
}

