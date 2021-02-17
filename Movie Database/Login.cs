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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;


            if (username.Equals("") || password.Equals(""))
            {
                MessageBox.Show("Form Field Empty");
            }
            else {
                try
                {
                    DBConnect db = new DBConnect();
                    if (db.OpenConnection())
                    {
                    }
                    else
                    {
                        MessageBox.Show("NOT Connected");
                    }

                    MySqlCommand SelectCommand = new MySqlCommand("SELECT * FROM user WHERE username='" + username + "' AND password='" + password + "' ;", db.getConnection());

                    MySqlDataReader myReader;
                    myReader = SelectCommand.ExecuteReader();

                    if(!myReader.HasRows)
                    {
                        MessageBox.Show("Username and password is incorrect. Please try again.");
                    }
                    else
                    {
                        UserHome home = new UserHome(textBox1.Text);
                        home.Show();
                        this.Hide();
                    }
                    //int count = 0;
                    //while (myReader.Read()){
                    //   int user_id = int.Parse(myReader.GetString(myReader.GetOrdinal("id")));
                    //   variables.user_id = user_id;
                    //    count = count + 1;
                    //}

                    //if (count == 1)
                    //{
                    //    Form3 home = new Form3();
                    //    home.Show();
                    //    this.Hide();
                    //}
                    //else if (count > 1)
                    //    MessageBox.Show("Duplicate username and password. Access is denied.");
                    //else
                    //    MessageBox.Show("Username and password is incorrect. Please try again.");

                    db.CloseConnection();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
