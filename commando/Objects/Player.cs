using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace commando
{
    public class Player : MovingObject
    {
        // Stats
        public int Damage { get; set; }
        public int ShootRate { get; set; }
        public int MaxHealth { get; set; }
        public int BulletSpeed { get; set; }
        // Set to true from timer, starts 0.5 sec
        public bool CanShoot { get; set; }

        // Powerup flags
        public bool Instakill { get; set; }
        public bool Invincible { get; set; }
        public bool Multishot { get; set; }


        // PowerUp timers
        public Timer ShootRateTimer;
        public Timer InstakillTimer;
        public Timer InvincibleTimer;
        public Timer MultishotTimer;

        public Player() : base(300, 600, 5, 5, 65, 65, "player", 200)
        {
            this.Damage = 30;
            this.ShootRate = 500;
            this.MaxHealth = 200;
            this.BulletSpeed = 10;
            this.CanShoot = false;
            this.Instakill = false;
            this.Multishot = false;

            // Shootrate timer
            ShootRateTimer = new Timer();
            ShootRateTimer.Interval = this.ShootRate;
            ShootRateTimer.Tick += new System.EventHandler(this.ClockTickShoot);
            ShootRateTimer.Start();

            // POWERUP timers
            // Instakill timer
            InstakillTimer = new Timer();
            InstakillTimer.Interval = 10000;
            InstakillTimer.Tick += new EventHandler(this.InstakillTick);

            // Invincible timer
            InvincibleTimer = new Timer();
            InvincibleTimer.Interval = 10000;
            InvincibleTimer.Tick += new EventHandler(this.InvincibleTick);

            // Multishot timer
            MultishotTimer = new Timer();
            MultishotTimer.Interval = 10000;
            MultishotTimer.Tick += new EventHandler(this.MultishotTick);
        }

        // Overloaded becouse of keypress'
        public void Move(bool w, bool a, bool s, bool d)
        {
            this.img = Utils.getImg("player");
            // Movement
            if (w) { this.Y -= (int)this.SpeedY; }
            if (s) { this.Y += (int)this.SpeedY; }
            if (d)
            { 
                this.X += (int)this.SpeedX;
                this.img = Utils.getImg("player-right");
            }
            if (a) 
            {
                this.X -= (int)this.SpeedX;
                this.img = Utils.getImg("player-left");
            }
            if (a && d)
            {
               
                this.img = Utils.getImg("player");
            }
        }

        // Shooting
        public void Shoot(bool space, Scene scene) { 
            
            if (space && this.CanShoot)
            {
                // Cant shoot again , before timer changes to true
                this.CanShoot = false;
                int bulletDmg = this.Damage;
                int tempX = this.X + this.Width / 2;
                int tempY = this.Y - 30;
                int bulletSpeed = this.BulletSpeed;

                // Instakill powerup
                if (this.Instakill)
                {
                    bulletDmg = 10000;
                }

                // Multishot powerup
                if (this.Multishot)
                {
                    scene.Add(new Bullet(tempX, tempY, bulletSpeed * Math.Cos(1.39626), -bulletSpeed * Math.Sin(1.39626), bulletDmg));
                    scene.Add(new Bullet(tempX, tempY, bulletSpeed * Math.Cos(1.309), -bulletSpeed * Math.Sin(1.309), bulletDmg));
                    scene.Add(new Bullet(tempX, tempY, bulletSpeed * Math.Cos(1.74533), -bulletSpeed * Math.Sin(1.74533), bulletDmg));
                    scene.Add(new Bullet(tempX, tempY, bulletSpeed * Math.Cos(1.8326), -bulletSpeed * Math.Sin(1.8326), bulletDmg));
                }
                scene.Add(new Bullet(tempX, tempY, bulletSpeed * Math.Cos(1.5708), -bulletSpeed * Math.Sin(1.5708), bulletDmg));
            }
        }


        // Shoot Tick
        private void ClockTickShoot(object sender, EventArgs e)
        {
            this.CanShoot = true;
        }

        // Timer ticks
        private void InstakillTick(object sender, EventArgs e)
        {
            this.Instakill = false;
            InstakillTimer.Stop();
        }
        private void InvincibleTick(object sender, EventArgs e)
        {
            this.Invincible = false;
            InvincibleTimer.Stop();
        }
        private void MultishotTick(object sender, EventArgs e)
        {
            this.Multishot = false;
            MultishotTimer.Stop();
        }


    }
}
