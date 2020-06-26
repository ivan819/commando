using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace commando
{
    public partial class MainGame : Form
    {
        Scene scene;
        Timer Clock;
        Player player;
        //  MovingObject enemy;
        // MovingObject bullet;
        const int Interval = 1000 / 60;
        private bool w = false;
        private bool a = false;
        private bool s = false;
        private bool d = false;

        private bool space = false;

        public MainGame()
        {
            InitializeComponent();
            Initialize_Timer();
          

            // double buffer za da nema flicker
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty |
               BindingFlags.Instance | BindingFlags.NonPublic, null, panel1, new object[] { true });
            this.Size = new System.Drawing.Size(Utils.FORM_WIDTH, Utils.FORM_HEIGHT);


            player = new Player();
            scene = new Scene(player);
            //scene.AddObj(player);
            // enemy = new MovingObject(50, 50);


            //  scene.AddObj(enemy);
            Initialize_Shoot_Timer();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            scene.DrawAll(e.Graphics);

        }

        void Initialize_Timer()
        {
            Clock = new Timer();
            Clock.Interval = Interval;
            Clock.Tick += new System.EventHandler(this.ClockTick);
            Clock.Start();
        }
        void Initialize_Shoot_Timer()
        {
            Clock = new Timer();
            Clock.Interval = player.ShootRate;
            Clock.Tick += new System.EventHandler(this.ClockTickShoot);
            Clock.Start();
        }
        void ClockTickShoot(object sender, EventArgs e)
        {
            player.CanShoot=true;
        }


        void ClockTick(object sender, EventArgs e)
        {
            if (w) { player.Y -= player.SpeedY; }
            if (s) { player.Y += player.SpeedY; }
            if (d) { player.X += player.SpeedX; }
            if (a) { player.X -= player.SpeedX; }
            if (space && player.CanShoot)
            {

                Bullet bullet = new Bullet(player.X, player.Y);
                player.CanShoot = false;
                scene.AddObj(bullet);
            }
            //enemy.Y += 1;



            // if (enemy.isCollidingWith(player))
            {
                //   scene.RemoveObject(enemy);
            }



            Invalidate(true);

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.W)
                w = true;
            if (e.KeyCode == Keys.A)
                a = true;
            if (e.KeyCode == Keys.S)
                s = true;
            if (e.KeyCode == Keys.D)
                d = true;
            if (e.KeyCode == Keys.Space)
                space = true;



        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == Keys.W)
                w = false;
            if (e.KeyCode == Keys.A)
                a = false;
            if (e.KeyCode == Keys.S)
                s = false;
            if (e.KeyCode == Keys.D)
                d = false;
            if (e.KeyCode == Keys.Space)
            {
                space = false;

            }
        }


    }
}
