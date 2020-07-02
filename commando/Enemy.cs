using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace commando
{
    public class Enemy : MovingObject
    {
        public int ShootChance { get; set; }
        public int Damage { get; set; }

        public double Angle { get; set; }

        public static Random rand = new Random();
        public override void Move()
        {
            this.Y += (int)this.SpeedY;
            this.X += (int)this.SpeedX;
        }
        public Enemy(EnemyType type) : base(rand.Next(0, Utils.FORM_WIDTH - 40), -50, type.sx, type.sy, type.w, type.h, type.image, type.hp)
        {
            this.Damage = type.dmg;
            this.ShootChance = type.schance;

        }


        public void Shoot(Scene scene, Random rand)
        {

            if (true)
            {
                double deltaX = scene.player.X - X;
                double deltaY = scene.player.Y - Y;
                Angle = Math.Atan2(deltaY, deltaX);
            }
            if (rand.Next(0, ShootChance) == 1)
            {
                Bullet bullet = new Bullet(this.X + this.Width / 2, this.Y + this.Height + 1, 10 * Math.Cos(Angle), 10 * Math.Sin(Angle), this.Damage);

                scene.Add(bullet);
            }

        }
    }
}
