using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Lezione_53__22Gennaio2018_Ships
{
    static class BulletManager
    {
        public enum BulletType { FIRE, BLUE, RED }

        static Queue<Bullet>[] bullets;
        const int MAX_CAPACITY = 30;

        public static void Init()
        //static BulletManager()
        {
            bullets = new Queue<Bullet>[(int)BulletType.RED + 1];
            for (int i = 0; i < bullets.Length; i++)
            {
                bullets[i] = new Queue<Bullet>(MAX_CAPACITY);
                switch ((BulletType)i)
                {
                    case BulletType.RED:
                        for (int j = 0; j < MAX_CAPACITY; j++)
                        {
                            bullets[i].Enqueue(new RedLaserBullet());
                        }
                        break;
                    case BulletType.BLUE:
                        for (int j = 0; j < MAX_CAPACITY; j++)
                        {
                            bullets[i].Enqueue(new BigBlueLaser());
                        }
                        break;
                    case BulletType.FIRE:
                        for (int j = 0; j < MAX_CAPACITY; j++)
                        {
                            bullets[i].Enqueue(new FireBall());
                        }
                        break;
                }
            }
        }

        public static Bullet GetBullet(BulletType type)
        {
            int queueList = (int)type;
            if (bullets[queueList].Count > 0)
                return bullets[queueList].Dequeue(); 
            return null;
        }

        public static void RestoreBullet(Bullet b)
        { 
            bullets[(int)b.Type].Enqueue(b);
        }

        public static void RemoveAll()
        {
            for(int i = 0; i < bullets.Length; i++)
                bullets[i].Clear();
        }
    }
}