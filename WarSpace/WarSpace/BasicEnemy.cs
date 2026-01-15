using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarSpace
{
    public class BasicEnemy : Enemy
    {
        private int Health; // Sağlık puanı

        public BasicEnemy(int x, int y, int width, int height, int speed)
            : base(x, y, width, height, speed, "basicEnemy.png")
        {
            Health = 1; // Basit düşmanlar tek vuruşta yok olur
        }

        public void TakeDamage()
        {
            Health--; // Sağlık puanını azalt
        }

        public bool IsDestroyed()
        {
            return Health <= 0; // Yok edildi mi?
        }

        public void Move()
        {
            base.Move(); // Temel sınıftaki Move metodunu kullan
        }
    }
}
