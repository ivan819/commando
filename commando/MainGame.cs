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

        Random rand = new Random();

        public MainGame()
        {
            InitializeComponent();


            //double buffer za da nema flicker
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty |
               BindingFlags.Instance | BindingFlags.NonPublic, null, panel1, new object[] { true });


            player = new Player();
            scene = new Scene(player);

            health.Text = player.Health.ToString();
            Initialize_Timer();
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

            Clock = new Timer();
            Clock.Interval = player.ShootRate;
            Clock.Tick += new System.EventHandler(this.ClockTickShoot);
            Clock.Start();

            Clock = new Timer();
            Clock.Interval = 2000;
            Clock.Tick += new System.EventHandler(this.ClockTickEnemySpawn);
            Clock.Start();
        }

        void ClockTickEnemySpawn(object sender, EventArgs e)
        {
            scene.Add(new Enemy(Utils.normal));
        }
        void ClockTickShoot(object sender, EventArgs e)
        {
            player.CanShoot = true;
        }


        void ClockTick(object sender, EventArgs e)
        {
            if (w) { player.Y -= (int)player.SpeedY; }
            if (s) { player.Y += (int)player.SpeedY; }
            if (d) { player.X += (int)player.SpeedX; }
            if (a) { player.X -= (int)player.SpeedX; }
            if (space && player.CanShoot)
            {

                Bullet bullet = new Bullet(player.X + player.Width / 2, player.Y - 10, player.Damage);
                player.CanShoot = false;
                scene.Add(bullet);
            }

            Invalidate(true);
            health.Text = player.Health.ToString();
            health.Update();
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
