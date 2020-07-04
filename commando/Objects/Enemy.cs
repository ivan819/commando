using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace commando
{
    public class Enemy : MovingObject
    {
        // Stats
        public int Damage { get; set; }
        public int ShootChance { get; set; }
        // Score values
        public int Score { get; set; }
        // Homing bullets
        public bool Homing { get; set; }

        // Random for spawn location
        public static Random rand = new Random();
       
        public Enemy(EnemyType type,int level) : base(rand.Next(0, Utils.FORM_WIDTH - 40), -50, type.sx, type.sy, type.w, type.h, type.image, type.hp)
        {
            this.Damage = type.dmg*level;
            this.ShootChance = type.schance + level;
            this.Homing = false;
            this.Score = type.score*level;
            this.Health *= level;

            if (type == Utils.normal && level>=3)
            {
                
            }

            // If mine set random x and y for position instead of upper bound
            if(type == Utils.mine)
            {
                this.X = rand.Next(0, 600 - this.Width);
                this.Y = rand.Next(0, 700 - this.Height);
                this.ShootChance = 0;
            }

            // If homing bullets
            if (type == Utils.homing)
            {
                Homing = true;
            }
        }
        // Shooting
        public void Shoot(Scene scene)
        {
            double sxMult = 0;
            double syMult = 1;

            // If homing bullet, find multipliers for SpeedX and SpeedY
            if (this.Homing)
            {
                double deltaX = scene.player.X - X;
                double deltaY = scene.player.Y - Y;
                double Angle = Math.Atan2(deltaY, deltaX);
                sxMult = Math.Cos(Angle);
                syMult = Math.Sin(Angle);
            }

            // ShootChance 1/60
            if (rand.Next(0, ShootChance) == 1)
            {
                scene.Add(new Bullet(this.X + this.Width / 2, this.Y + this.Height + 1, 10 * sxMult, 10 * syMult, this.Damage));
            }

        }
    }
}
