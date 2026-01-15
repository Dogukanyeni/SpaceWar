using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarSpace
{
    public class StrongEnemy : Enemy
    {
        private int Health; // Ekstra sağlık

        public StrongEnemy(int x, int y, int width, int height, int speed)
            : base(x, y, width, height, speed, "strongEnemy.png")
        {
            Health = 10; // Sağlık puanı
        }

        public void Move()
        {
            base.Move(); // Temel sınıftaki Move metodunu kullan
        }

        public void TakeDamage()
        {
            Health--; // Hasar alır
        }

        public bool IsDestroyed()
        {
            return Health <= 0; // Yok edildi mi?
        }
    }
}
