using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarSpace
{
    public class Bullet
    {
        public Rectangle Bounds { get; private set; }
        public int Speed;
        private Color BulletColor;

        public Bullet(int x, int y, int width, int height, int speed, Color color)
        {
            Bounds = new Rectangle(x, y, width, height);
            Speed = speed;
            BulletColor = color;
        }

        public void Move(bool isEnemyBullet)
        {
            if (isEnemyBullet)
            {
                // Düşman mermisi sola hareket eder
                Bounds = new Rectangle(Bounds.X - Speed, Bounds.Y, Bounds.Width, Bounds.Height);
            }
            else
            {
                // Uzay gemisi mermisi sağa hareket eder
                Bounds = new Rectangle(Bounds.X + Speed, Bounds.Y, Bounds.Width, Bounds.Height);
            }
        }

    

    public void Draw(Graphics g)
        {
            using (Brush brush = new SolidBrush(BulletColor))
            {
                g.FillRectangle(brush, Bounds);
            }
        }

        public bool IsOffScreen(int screenWidth, int screenHeight)
        {
            return Bounds.X + Bounds.Width < 0 || Bounds.X > screenWidth || Bounds.Y < 0 || Bounds.Y > screenHeight;
        }
    }

}
