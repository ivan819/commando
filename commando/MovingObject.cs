using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commando
{
    class MovingObject : Drawable
    {
        Image img;
        public int X { get; set; }
        public int Y { get; set; }

        public int SpeedX { get; set; }

        public int SpeedY { get; set; }

        public MovingObject(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.SpeedX = 0;
            this.SpeedY = 5;
            img = Image.FromFile(@"C:\Users\Duck\Desktop\vp\player.jpg");

        }
        public void Draw(Graphics g)
        {

            SolidBrush br = new SolidBrush(Color.Black);
            //g.FillEllipse(br, X + SpeedX - 5, Y + SpeedY - 5, 20, 20);
            
            g.DrawImage(img, X + SpeedX - 5, Y + SpeedY - 5, 30, 30);
            br.Dispose();
        }


    }
}
