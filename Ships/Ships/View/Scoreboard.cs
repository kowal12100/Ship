using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ships.View
{
    public partial class Scoreboard : Form
    {
        ShipConnectDataContext databaseConnection = new ShipConnectDataContext(Properties.Settings.Default.ShipResultBoardConnectionString);

        public Scoreboard()
        {
            InitializeComponent();
       
                var arrayof5TheBestScore  = databaseConnection.Wyniks.OrderBy(x => x.Result).Take(5).ToArray();
            //label1.Text=arrayof5TheBestScore[0].Id.ToString();
            //label2.Text = arrayof5TheBestScore[0].Username;
            //label3.Text = arrayof5TheBestScore[0].Result.ToString();
        }
    }
}
