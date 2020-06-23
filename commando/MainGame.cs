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
        MovingObject player;

        const int Interval = 1000/60;
        bool w = false;
        bool a = false;
        bool s = false;
        bool d = false;
       // Graphics g;
        public MainGame()
        {
            InitializeComponent();
            Initialize_Timer();

            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | 
               BindingFlags.Instance | BindingFlags.NonPublic, null, panel1, new object[] { true });
            //this.DoubleBuffered = true;
            //g = panel1.CreateGraphics();
            scene = new Scene();
            player = new MovingObject(50, 50);
            scene.AddObj(player);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
            scene.DrawAll(e.Graphics);
           // g.Dispose();
        }

        void Initialize_Timer()
        {
            Clock = new Timer();
            Clock.Interval = Interval;
            Clock.Tick += new System.EventHandler(this.ClockTick);
            Clock.Start();
        }
        void ClockTick(object sender, EventArgs e)
        {
            if (w) { player.SpeedY -= 5; }
            if (s) { player.SpeedY += 5; }
            if (d) { player.SpeedX += 5; }
            if (a) { player.SpeedX -= 5; }






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
        }


    }
}
