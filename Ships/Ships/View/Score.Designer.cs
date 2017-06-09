using System;
using System.Data.SqlClient;
using System.IO;

namespace Ships.View
{
    partial class Score
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(119, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(163, 121);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(163, 181);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Zakończ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(57, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Wpisz swój Nick:";
            string playerName = Convert.ToString(this.label2.Text);

            // 
            // Score
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 283);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Name = "Score";
            this.Text = "Score";
            int scoreValue = Convert.ToInt32(this.Text);
            this.ResumeLayout(false);
            this.PerformLayout();
            SaveScore(playerName, scoreValue);

        }

        public static string database = Path.GetFullPath(@"..\..\ShipResultBoard.mdf");


        public static Wynik[] SelectScores()
        {
            var connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + database + "; Integrated Security = True; Connect Timeout = 30");
            connection.Open();

            var select = "SELECT TOP 5 * FROM Results ORDER BY Score desc";
            var selectCmd = new SqlCommand(select, connection);
            var tabResults = new Wynik[5];

            try
            {
                using (SqlDataReader reader = selectCmd.ExecuteReader())
                {
                    var i = 0;

                    while (reader.Read())
                    {
                        tabResults[i] = new Wynik
                        {
                            Id = reader.GetInt32(0),
                            Username = reader.GetString(1),
                            Result = reader.GetInt32(2),
                        };
                        i++;
                    }
                }
                connection.Close();
                return tabResults;
            }
            catch (SqlException ex) { throw ex; }
            finally { connection.Close(); }
        }

        public static void SaveScore(string playerName, int scoreValue)
        {
            SqlConnection connection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + database + "; Integrated Security = True; Connect Timeout = 30");
            string insert = "INSERT INTO Results (Name, Score, Time) VALUES (@playerName, @score, @time)";
            var insertCmd = new SqlCommand(insert, connection);
            insertCmd.Parameters.AddWithValue("@playerName", playerName);
            insertCmd.Parameters.AddWithValue("@score", scoreValue);

            try { connection.Open(); insertCmd.ExecuteNonQuery(); }
            catch (SqlException ex) { throw ex; }
            finally { connection.Close(); }
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
    }
}