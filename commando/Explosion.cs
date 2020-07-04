using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace commando
{
    public class Explosion : MovingObject
    {
        public Timer ExplosionTimer;
        public Scene scene;
        public int size;
        public Explosion(int x, int y, Scene scene) : base(x, y, 0, 0, 40, 40, "explosion", 1)
        {
            this.size = 0;
            this.scene = scene;
            ExplosionTimer = new Timer();
            ExplosionTimer.Interval = 30;
            ExplosionTimer.Tick += new System.EventHandler(this.ClockTickExplosion);
            ExplosionTimer.Start();
        }
        public void ClockTickExplosion(object sender, EventArgs e)
        {
            size++;
            if(size == 10)
            {
                scene.MarkForDeletion(this);
                scene.DeleteMarkedObjects();
                ExplosionTimer.Stop();
            }
            this.Width += 4;
            this.Height += 4;
            this.X -= 2;
            this.Y -= 2;


        }
    }
}
