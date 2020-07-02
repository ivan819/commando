using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace commando
{
    public class Bullet : MovingObject
    {
        public int Damage { get; set; }
        public override void Move()
        {
            this.Y += (int)this.SpeedY;
            this.X += (int)this.SpeedX;
        }


        public Bullet(int x, int y, int damage) : base(x, y, 0, -5, 5, 5, "bullet", 1)
        {
            this.Damage = damage;
        }
        public Bullet(int x, int y, double sx, double sy, int damage) : base(x, y, sx, sy, 5, 5, "bullet", 1)
        {
            this.Damage = damage;
        }
    }
}
