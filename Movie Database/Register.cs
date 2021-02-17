using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Movie_Database
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string username = textBox2.Text;
            string password = textBox3.Text;
            string city = textBox1.Text;
            string email = textBox4.Text;
            if (username.Equals("") || password.Equals("") || city.Equals("") || email.Equals("") || email.IndexOf("@")==-1 || email.IndexOf(".com")==-1)
            {
                MessageBox.Show("Form Field Empty or Incorrect");
            }
            else {
                DBConnect db = new DBConnect();
                if (db.OpenConnection())
                {
                    MySqlConnection connection = db.getConnection();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO `user`(`username`, `password`, `city`, `email`) VALUES ('"+ username +"','"+password+"','"+city+"','"+email+"');";
                    command.ExecuteNonQuery();
                    MessageBox.Show("Registration Succesfull");
                    Login login = new Login();
                    login.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("NOT Connected");
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }


    }
}
