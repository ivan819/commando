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
            Clock.Interval = 3000;
            Clock.Tick += new System.EventHandler(this.ClockTickEnemySpawn);
            Clock.Start();

            Clock = new Timer();
            Clock.Interval = 5000;
            Clock.Tick += new System.EventHandler(this.ClockTickPowerupSpawn);
            Clock.Start();
        }

        void ClockTickEnemySpawn(object sender, EventArgs e)
        {
            EnemyType enemy;
            int x = rand.Next(0, 10);
            if (x == 0)
            {
                enemy = Utils.mine;
            }
            else if (x <= 4)
            {
                enemy = Utils.homing;
            }
            else
            {
                enemy = Utils.normal;
            }

            scene.Add(new Enemy(enemy));
        }
        void ClockTickShoot(object sender, EventArgs e)
        {
            player.CanShoot = true;
        }

        void ClockTickPowerupSpawn(object sender, EventArgs e)
        {
            if (scene.powerups.Count <= 2)
            {
                scene.Add(new PowerUp(rand.Next(6, 7).ToString()));
            }
            
        }


        void ClockTick(object sender, EventArgs e)
        {
            if (w) { player.Y -= (int)player.SpeedY; }
            if (s) { player.Y += (int)player.SpeedY; }
            if (d) { player.X += (int)player.SpeedX; }
            if (a) { player.X -= (int)player.SpeedX; }
            if (space && player.CanShoot)
            {
                int bulletDmg = player.Damage;
                if (player.Instakill)
                {
                    bulletDmg = 10000;
                }
                if (player.Multishot)
                {
                    Bullet bullet1;
                    double Angle45 = Math.Atan2(-400, 100);
                    double Angle452 = Math.Atan2(-400, -100);
                    double Angle90 = Math.Atan2(-300, 100);
                    double Angle902 = Math.Atan2(-300, -100);

                    bullet1 = new Bullet(player.X + player.Width / 2, player.Y - 12, 10 * Math.Cos(Angle45), 7 * Math.Sin(Angle45), bulletDmg);
                    scene.Add(bullet1);
                    bullet1 = new Bullet(player.X + player.Width / 2, player.Y - 12, 10 * Math.Cos(Angle452), 7 * Math.Sin(Angle452), bulletDmg);
                    scene.Add(bullet1);
                    bullet1 = new Bullet(player.X + player.Width / 2, player.Y - 10, 10 * Math.Cos(Angle90), 7 * Math.Sin(Angle90), bulletDmg);
                    scene.Add(bullet1);
                    bullet1 = new Bullet(player.X + player.Width / 2, player.Y - 10, 10 * Math.Cos(Angle902), 7 * Math.Sin(Angle902), bulletDmg);
                    scene.Add(bullet1);
                }
                Bullet bullet = new Bullet(player.X + player.Width / 2, player.Y - 10, 0, -7, bulletDmg);
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

        private void MainGame_Load(object sender, EventArgs e)
        {

        }
    }
}
