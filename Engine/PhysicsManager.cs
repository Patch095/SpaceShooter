using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lezione_53__22Gennaio2018_Ships
{
    static class PhysicsManager
    {
        public enum ColliderType: uint { Player = 1, Enemy = 2, PlayerBullet = 4, EnemyBullet = 8, PowerUp = 16}
        static List<RigidBody> items;

        public static void Init()
        //static PhysicsManager()
        {
            items = new List<RigidBody>();
        }

        public static void AddItem(RigidBody item)
        {
            items.Add(item);
        }
        public static void RemoveItem(RigidBody item)
        {
            items.Remove(item);
        }
        public static void RemoveAll()
        {
            items.Clear();
        }

        public static void Update()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GameObject.IsActive)
                    items[i].Update();
            }
        }

        public static void CheckCollisions()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GameObject.IsActive && items[i].IsCollisionsAffected)
                {
                    for (int j = i + 1; j < items.Count; j++)
                    {
                        if (items[j].GameObject.IsActive && items[j].IsCollisionsAffected)
                        {
                            bool checkFist = items[i].CheckCollisionWith(items[j]);
                            bool checkSecond = items[j].CheckCollisionWith(items[i]);

                            if ((checkFist || checkSecond) && items[i].Collides(items[j]))
                            {
                                if(checkFist)
                                    items[i].GameObject.OnCollide(items[j].GameObject);
                                if(checkSecond)
                                    items[j].GameObject.OnCollide(items[i].GameObject);
                            }
                        }
                    }
                }
            }
        }
    }
}