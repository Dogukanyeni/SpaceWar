using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarSpace
{
    public abstract class Enemy
    {
        public Rectangle Position { get; protected set; }
        public Rectangle Bounds => Position;
        protected int Speed;
        protected Image EnemyImage;
        private int shootTimer = 0;
        private int shootInterval = 100; // 100 döngüde bir ateş edecek

        public Enemy(int x, int y, int width, int height, int speed, string imagePath)
        {
            Position = new Rectangle(x, y, width, height);
            Speed = speed;
            shootTimer = 0;
            shootInterval = 100; 
            EnemyImage = Image.FromFile(imagePath);
        }

        public bool CanShoot()
        {
            shootTimer++;
            if (shootTimer >= shootInterval)
            {
                shootTimer = 0; // Zamanlayıcı sıfırlanır
                return true;
            }
            return false;
        }

        public void DecreaseShootInterval()
        {
            if (shootInterval > 20) // Minimum sınır
            {
                shootInterval -= 10; // Zorluk artışıyla ateş sıklığını artır
            }
        }
        public void IncreaseSpeed()
        {
            Speed++;
        }

        public virtual void Move()
        {
            Position = new Rectangle(Position.X - Speed, Position.Y, Position.Width, Position.Height);
        }

        public bool IsOffScreen()
        {
            return Position.X + Position.Width < 0;
        }

        public List<Bullet> Shoot()
        {
            shootTimer++;
            if (shootTimer >= shootInterval)
            {
                shootTimer = 0; // Zamanlayıcıyı sıfırla
                return new List<Bullet>
                {
                    new Bullet(Position.X, Position.Y + Position.Height / 2, 10, 5, -5, Color.Red) // Düşmanın mermisi
                };
            }
            return new List<Bullet>(); // Ateş etmiyor
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(EnemyImage, Position);
        }
    }
}
