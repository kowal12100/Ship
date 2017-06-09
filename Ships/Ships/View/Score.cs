using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ships.View
{
    public partial class Score : Form
    {
        static ShipConnectDataContext databaseConnection = new ShipConnectDataContext(Properties.Settings.Default.ShipResultBoardConnectionString);
        static string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Build\Ships\Ships\Ships\ShipResultBoard.mdf;Integrated Security=True;Connect Timeout=30";
        static SqlConnection conn= new SqlConnection(connStr);

        private Game a;
        private bool PlayerWin;
        public Score(bool playerWin)
        {
               InitializeComponent();
            PlayerWin = playerWin;
            if (playerWin)
            {
                label1.Text = "Wygrałeś";
            }
            else
            {
                label1.Text = "Przegrałeś";
                label2.Visible = false;
      textBox1.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PlayerWin)
            {
                SaveToBase();
                var menu = new Menu();
                this.Close();
                menu.Show();
            }
            else
            {
    
                this.Close();
                var menu = new Menu();
                menu.Show();
            }
        }
    
        private void SaveToBase()
        {
           
            var rekord = new Wynik
            {
                Id = databaseConnection.Wyniks.Count() + 1,
                Username = textBox1.Text,
                Result = Game.score
            };

        

            AddUser(rekord.Username,rekord.Result,rekord.Id);

        }
        public static void AddUser(string name, int wynik,int Id)
        {
            string insStmt = "INSERT INTO [Wynik] (Id,Username,Result) VALUES (@Id,@Username, @Result)";
     
            SqlCommand insCmd = new SqlCommand(insStmt, conn);
            insCmd.Parameters.AddWithValue("@Id", Id);
            insCmd.Parameters.AddWithValue("@Username", name);
            insCmd.Parameters.AddWithValue("@Result", wynik);
            try { conn.Open(); insCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { conn.Close(); }
        }

       
    }
}
