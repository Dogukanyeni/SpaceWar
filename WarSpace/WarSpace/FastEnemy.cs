using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarSpace
{
    public class FastEnemy : Enemy
    {
        private int Health; // Sağlık puanı

        public FastEnemy(int x, int y, int width, int height, int speed)
            : base(x, y, width, height, 6, "fastEnemy.png")
        {
            Health = 2; // FastEnemy için başlangıç sağlığı
        }

        public void TakeDamage()
        {
            Health--; // Sağlık puanı azaltılır
        }

        public bool IsDestroyed()
        {
            return Health <= 0; // Yok edildi mi?
        }

        public void Move()
        {
            base.Move(); // Temel sınıftaki Move metodunu kullan
        }
    }
}
