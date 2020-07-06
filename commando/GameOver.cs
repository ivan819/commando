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
    public partial class GameOver : Form
    {

        long score;
        MenuMain parent;

        public GameOver(long score, MenuMain parent)
        {
            this.parent = parent;
            InitializeComponent();
            this.score = score;
            label2.Text = score.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)
            {
                ScoreHolderClass.add(textBox1.Text, score);
                new Thread(() => new MainGame(parent).ShowDialog()).Start();
                this.Close();
            }
           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)
            {
                ScoreHolderClass.add(textBox1.Text, score);
                new Thread(() => new MenuMain().ShowDialog()).Start();
                this.Close();
            }
        }
    }
}
