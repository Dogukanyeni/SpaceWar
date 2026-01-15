using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarSpace
{
    public class PowerUp
    {
        public Rectangle Position { get; private set; }
        public string Type { get; private set; } // Tür (health, speed, damage)
        private int Speed;
        private Image PowerUpImage;

        public PowerUp(int x, int y, int width, int height, int speed, string type)
        {
            Position = new Rectangle(x, y, width, height);
            Speed = speed;
            Type = type; // Power-up türü
            PowerUpImage = Image.FromFile("health.png");
        }

        public void Move()
        {
            Position = new Rectangle(Position.X - Speed, Position.Y, Position.Width, Position.Height);
        }

        public bool IsOffScreen(int screenWidth)
        {
            return Position.X + Position.Width < 0; // Power-up ekranın solundan çıktıysa true
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(PowerUpImage, Position);
        }
    }
}
