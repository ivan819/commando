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


            scene = new Scene();

            
            Initialize_Timer();
        }
        public MainGame(Scene scene)
        {
            InitializeComponent();


            //double buffer za da nema flicker
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty |
               BindingFlags.Instance | BindingFlags.NonPublic, null, panel1, new object[] { true });


            this.scene = scene;


            Initialize_Timer();
        }

        // Paint event for getting PaintEventArgs.Graphics
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            scene.DoAllTheWork(w,a,s,d,space,e.Graphics);
        }

        // framerate timer
        void Initialize_Timer()
        {
            Clock = new Timer();
            Clock.Interval = Interval;
            Clock.Tick += new System.EventHandler(this.ClockTick);
            Clock.Start();           
        }

        void ClockTick(object sender, EventArgs e)
        {
            Invalidate(true);
        }

       

       
        // Handle user input
        // Key press down event sets the flags to true
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

        // Key press up event sets the flags to false
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
