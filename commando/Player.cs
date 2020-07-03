using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace commando
{
    public class Player : MovingObject
    {
        public int ShootRate { get; set; }
        public bool CanShoot { get; set; }

        public int Damage { get; set; }

        public int MaxHealth { get; set; }

        public bool Instakill { get; set; }
        public bool Invincible { get; set; }
        public bool Multishot { get; set; }
        public override void Move()
        {

        }
        public Player() : base(300, 600, 5, 5, 50, 50, "player", 100)
        {
            this.Damage = 30;
            this.ShootRate = 500;
            this.CanShoot = false;
            this.Instakill = false;
            this.Multishot = false;
            this.MaxHealth = 200;
        }
        public void Kill()
        {
            this.SpeedX = 0;
            this.SpeedY = 0;
        }
    }
}
