using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commando
{
    public abstract class MovingObject
    {
        public Image img { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }
        public int Health { get; set; }

        public MovingObject(int x, int y, int SpeedX, int SpeedY, int Width, int Height, string type, int Health)
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
        public void Draw(Graphics g)
        {
            g.DrawImage(img, X, Y, Width, Height);
        }
       

        public bool IsCollidingWith(MovingObject o)
        {
            return (this.X <= o.X + o.Width && this.X + this.Width >= o.X) &&
                    (this.Y <= o.Y + o.Height && this.Y + this.Height >= o.Y);
        }

        
        public abstract void Move();



    }
}
