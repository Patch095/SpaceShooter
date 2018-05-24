using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    static class SpawnManager
    {
        static Queue<EnemyShip>[] enemies;

        const int MAX_ENEMIES = 15;

        static float spawnTimer;
        const float MAX_TIMER = 1.5f;

        const int POS_X = 70;

        static float fireTimer;
        static float FIRE_TIMER = 3f;

        public static void Init()
        //static SpawnManager()
        {
            enemies = new Queue<EnemyShip>[(int)EnemyShip.EnemyType.STANDAR + 1];
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = new Queue<EnemyShip>(MAX_ENEMIES);

                switch ((EnemyShip.EnemyType)i)
                {
                    case EnemyShip.EnemyType.STANDAR:
                        for (int j = 0; j < MAX_ENEMIES; j++)
                        {
                            enemies[i].Enqueue(new EnemyShip(EnemyShip.EnemyType.STANDAR));
                        }
                        break;
                    case EnemyShip.EnemyType.RED:
                        for (int j = 0; j < MAX_ENEMIES; j++)
                        {
                            enemies[i].Enqueue(new RedEnemyShip(EnemyShip.EnemyType.RED));
                        }
                        break;
                    case EnemyShip.EnemyType.BOT:
                        for (int j = 0; j < MAX_ENEMIES; j++)
                        {
                            enemies[i].Enqueue(new BottomEnemy(EnemyShip.EnemyType.BOT));
                        }
                        break;
                    case EnemyShip.EnemyType.BONUS:
                        for (int j = 0; j < MAX_ENEMIES; j++)
                        {
                            enemies[i].Enqueue(new PowerUp(EnemyShip.EnemyType.BOT));
                        }
                        break;
                }
                spawnTimer = MAX_TIMER;
                fireTimer = FIRE_TIMER;
            }
        }

        public static void EnemiesSpawn()
        {
            spawnTimer -= Game.Window.deltaTime;
            fireTimer -= Game.Window.deltaTime;

            if (spawnTimer <= 0)
            {
                int randomType = RandomGenerator.GetRandom(0, 11);
                int spanwType;
                if (randomType <= 1)
                    spanwType = 0;//Bonus
                else if (randomType > 1 && randomType < 4)
                    spanwType = 1;//Bot
                else if (randomType >= 4 && randomType < 6)
                    spanwType = 2;//Red
                else
                    spanwType = 3;//Standard
                if(enemies[spanwType].Count > 0)
                {
                    EnemyShip enemy = enemies[spanwType].Dequeue();

                    if (spanwType == 1)
                    {
                        enemy.OnSpawn(new Vector2(Game.Window.Width + POS_X, Game.Window.Height - 30));
                        spawnTimer = MAX_TIMER / 3;
                    }
                    else
                    {
                        enemy.OnSpawn(new Vector2(Game.Window.Width + POS_X, RandomGenerator.GetRandom(enemy.Height / 2, Game.Window.Height - enemy.Height / 2)));
                        if(spanwType == 0)
                            ((PowerUp)enemy).PowerValue = RandomGenerator.GetRandom(0, 3);
                        spawnTimer = MAX_TIMER;
                    }
                }
            }
            if(fireTimer <= 0)
            {
                FireShoot();
                fireTimer = FIRE_TIMER;
            }
        }

        public static void FireShoot()
        {
            Bullet b;
            b = BulletManager.GetBullet(BulletManager.BulletType.FIRE);
            if(b != null)
                 b.Shoot(new Vector2(Game.Window.Width - POS_X, RandomGenerator.GetRandom(b.Height, Game.Window.Height - b.Height)), new Vector2(-500, 0));
        }

        static public void Restore(EnemyShip enemy)
        {
            enemies[(int)enemy.Type].Enqueue(enemy);
        }

        public static void RemoveAll()
        {
            for (int i = 0; i < enemies.Length; i++)
                enemies[i].Clear();
        }
    }
}