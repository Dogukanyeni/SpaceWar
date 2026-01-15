using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarSpace
{
    public class Spaceship
    {
        public Rectangle Position { get; private set; }
        private int Speed;
        private int Health;
        private int Damage;
        private Image SpaceshipImage;

        public Spaceship(int x, int y, int width, int height, int speed)
        {
            Position = new Rectangle(x, y, width, height);
            Speed = speed;
            SpaceshipImage = Image.FromFile("spaceship.png");
            Health = 10;
            Damage = 1;
        }
        public void IncreaseHealth()
        {
            Health++;
        }

        public void IncreaseSpeed()
        {
            Speed++;
        }

        public void IncreaseDamage()
        {
            Damage++;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                // Oyun bitti
            }
        }

        public bool IsDestroyed()
        {
            return Health <= 0; // Sağlık sıfır veya altındaysa yok edilmiş kabul edilir
        }

        public void Move(string direction)
        {
            switch (direction)
            {
                case "up":
                    if (Position.Y > 0)
                        Position = new Rectangle(Position.X, Position.Y - Speed, Position.Width, Position.Height);
                    break;
                case "down":
                    if (Position.Y + Position.Height < 576)
                        Position = new Rectangle(Position.X, Position.Y + Speed, Position.Width, Position.Height);
                    break;
                case "left":
                    if (Position.X > 0)
                        Position = new Rectangle(Position.X - Speed, Position.Y, Position.Width, Position.Height);
                    break;
                case "right":
                    if (Position.X + Position.Width < 300)
                        Position = new Rectangle(Position.X + Speed, Position.Y, Position.Width, Position.Height);
                    break;
            }
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(SpaceshipImage, Position);
        }
    }
}
