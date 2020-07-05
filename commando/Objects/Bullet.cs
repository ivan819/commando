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
        
        public Bullet(int x, int y, double sx, double sy, int damage) : base(x, y, sx, sy, 7, 7, "bullet", 1)
        {
            this.Damage = damage;
        }
    }
}
 