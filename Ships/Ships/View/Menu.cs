using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ships.View;

namespace Ships
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Board a = new Board(true);
              a.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string a = "Stworzył: Kamil Kowalczyk \n Projekt szkolny z obsługi Windows Form Application\n";
            MessageBox.Show(a);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var scoreboard=new Scoreboard();
           scoreboard.Show();
        }
    }
}
