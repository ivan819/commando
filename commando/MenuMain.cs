using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainGame f2 = new MainGame();
            this.Visible = false;
            f2.ShowDialog();
        }

        private void MenuMain_Load(object sender, EventArgs e)
        {

        }
    }
}
