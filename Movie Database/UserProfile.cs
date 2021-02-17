using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Movie_Database
{
    public partial class UserProfile : Form
    {
        public UserProfile()
        {
            InitializeComponent();
            pictureBox1.Image = Image.FromFile("C:\\Users\\Nikhil Gupta\\Downloads\\Compressed\\Movie-Database-master\\Movie-Database-master\\Movie Database\\user.png");

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Size = new Size(150, 150);
            label5.Text = variables.username;
            DBConnect db = new DBConnect();
            if (db.OpenConnection())
            {
                MySqlCommand SelectCommand = new MySqlCommand("SELECT * FROM user WHERE username='" + variables.username + "';", db.getConnection());

                MySqlDataReader reader;
                reader = SelectCommand.ExecuteReader();
                reader.Read();
                label3.Text=reader.GetString(reader.GetOrdinal("city"));
                label7.Text=reader.GetString(reader.GetOrdinal("email"));
            }



        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserHome userHome = new UserHome(variables.username);
            userHome.Show();
            this.Hide();
        }
    }
}
