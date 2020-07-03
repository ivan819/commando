using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commando
{
    public class PowerUp : MovingObject
    {
        public string powerType;
        public static Random rand = new Random();
        public PowerUp(string type) : base(0, 0, 0, 0, 20, 20, type, 1)
        {
            this.powerType = type;
            this.X = rand.Next(0, 600 - this.Width);
            this.Y = rand.Next(0, 700 - this.Height);
        }
        public override void Move()
        {

        }
    }
}
