using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commando
{
    public class Bullet : MovingObject
    {
        public override void Move()
        {
            this.Y -= this.SpeedY;
        }

       
        public Bullet(int x, int y) : base(x+14, y, 0, 5, 5, 5, "bullet", 1)
        {

        }
    }
}
