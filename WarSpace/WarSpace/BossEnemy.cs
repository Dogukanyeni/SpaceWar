using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarSpace
{
    public class BossEnemy : Enemy
    {
        private int Health; // Boss'un ekstra sağlık puanı

        public BossEnemy(int x, int y, int width, int height, int speed)
            : base(x, y, width, height, speed, "bossEnemy.png") // Boss görseli
        {
            Health = 20; // Boss için başlangıç sağlığı
        }

        public void Move()
        {
            // Daha yavaş hareket eder
            Position = new Rectangle(Position.X, Position.Y + Speed / 2, Position.Width, Position.Height);
        }

        public void TakeDamage()
        {
            Health--;
        }

        public bool IsDestroyed()
        {
            return Health <= 0;
        }
    }

}
