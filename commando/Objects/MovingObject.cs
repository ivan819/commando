using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commando
{
    public class MovingObject
    {
        // Object coodrinates
        public int X { get; set; }
        public int Y { get; set; }
        // Object dimensions
        public int Width { get; set; }
        public int Height { get; set; }
        // Object speed
        public double SpeedX { get; set; }
        public double SpeedY { get; set; }
        // Hitpoints
        public int Health { get; set; }
        // Image for drawing
        public Image img { get; set; }

        public MovingObject(int x, int y, double SpeedX, double SpeedY, int Width, int Height, string type, int Health)
        {
            this.X = x;
            this.Y = y;
            this.SpeedX = SpeedX;
            this.SpeedY = SpeedY;
            this.Width = Width;
            this.Height = Height;
            this.Health = Health;
            img = Utils.getImg(type);
        }
        // Draw function with graphics from panel
        public void Draw(Graphics g)
        {
            g.DrawImage(img, X, Y, Width, Height);
        }
        // Collision detection
        public bool IsCollidingWith(MovingObject o)
        {
            return (this.X <= o.X + o.Width && this.X + this.Width >= o.X) &&
                    (this.Y <= o.Y + o.Height && this.Y + this.Height >= o.Y);
        }
        // Diffrent movement for different objects
        public void Move()
        {
            this.Y += (int)this.SpeedY;
            this.X += (int)this.SpeedX;
        }
    }
}
