using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarSpace
{
    public class Meteor
    {
        public Rectangle Position { get; private set; }
        private int Speed;
        private Image MeteorImage;

        public Meteor(int x, int y, int width, int height, int speed)
        {
            Position = new Rectangle(x, y, width, height);
            Speed = speed;
            MeteorImage = Image.FromFile("meteor.png"); // Meteor görseli
        }

        public void Move()
        {
            Position = new Rectangle(Position.X - Speed, Position.Y, Position.Width, Position.Height);
        }

        public bool IsOffScreen(int screenHeight)
        {
            return Position.X + Position.Width < 0; 
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(MeteorImage, Position);
        }
    }
}
