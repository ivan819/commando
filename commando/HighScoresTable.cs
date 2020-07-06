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
    public partial class HighScoresTable : Form
    {

        public static bool flag = true;
        public HighScoresTable()
        {
            InitializeComponent();
        }

        private void HighScoresTable_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           
                Brush BlackTextBrush = new SolidBrush(Color.Black);
                Font font = new Font(FontFamily.GenericMonospace, 17, FontStyle.Bold);
                String message = "";


                List<ScoreNamePair> list = ScoreHolderClass.getScores();
                for (int i = 0; i < list.Count() && i < 10; i++)
                {
                        
                    message = (i + 1) + ". " + list[i].name + " - " + list[i].score;
                    e.Graphics.DrawString(message, font, BlackTextBrush, 40, 35 + 21 * i);
                }
           
        }

        private void HighScoresTable_Load(object sender, EventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Thread(() => new MenuMain().ShowDialog()).Start();
            this.Close();
        }
    }
}
