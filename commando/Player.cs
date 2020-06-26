using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace commando
{
    public class Player : MovingObject
    {
        public int ShootRate { get; set; }
        public bool CanShoot { get; set; }

        public override void Move()
        {
           
        }
        public Player() : base(300, 600 , 5, 5, 30, 30 , "player", 100)
        {
            this.ShootRate = 500;
            this.CanShoot = true;
        }
    }
}
