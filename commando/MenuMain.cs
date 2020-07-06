using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace commando
{
    public partial class MenuMain : Form
    {
        public MenuMain()
        {
            InitializeComponent();
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatStyle = FlatStyle.Flat;
            button1.ForeColor = BackColor;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.MouseDownBackColor = Color.Transparent;
            button2.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button2.FlatStyle = FlatStyle.Flat;
            button2.ForeColor = BackColor;
            button2.FlatAppearance.MouseOverBackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Thread(() => new MainGame(this).ShowDialog()).Start();
            this.Close();
        }

        private void MenuMain_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Thread(() => new HighScoresTable().ShowDialog()).Start();
            this.Close();
        }
    }
}
