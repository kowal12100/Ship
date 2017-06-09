using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ships.Model;

namespace Ships.View
{
    public partial class Game : Form
    {
        public static int score;
        internal static object _enemyship;
        private Cell[,] Board;
        private Cell[,] EnemyBoard;
        public Game(Cell[,] board,Cell[,] enemyboard)
        {
            Board = board;
            EnemyBoard = enemyboard;
             
            foreach (var cell in board)
            {
                cell.Enabled = false;
                Controls.Add(cell);
            }
            foreach (var cell in enemyboard)
            {
                Controls.Add(cell);
            }
            InitializeComponent();
        }

        public static void UpdateScore(int value)
        {
            score += value;
            score = score < 0 ? 0 : score;
            label2.Text = score.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
