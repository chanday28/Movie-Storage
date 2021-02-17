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
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
            DBConnect db = new DBConnect();
            db.OpenConnection();
            MySqlDataReader myReader;

            MySqlCommand SelectCommand = new MySqlCommand("SELECT * FROM director",db.getConnection());


            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy/MM/dd";

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "yyyy/MM/dd";

            myReader = SelectCommand.ExecuteReader();
            while (myReader.Read())
            {
                comboBox1.Items.Add(myReader.GetString("name"));
            }
            myReader.Close();
            SelectCommand = new MySqlCommand("SELECT * FROM actor", db.getConnection());
            myReader = SelectCommand.ExecuteReader();
           
            while (myReader.Read())
            {
                comboBox4.Items.Add(myReader.GetString("name"));
            }
            myReader.Close();
            SelectCommand = new MySqlCommand("SELECT * FROM movie", db.getConnection());
            myReader = SelectCommand.ExecuteReader();
            while (myReader.Read())
            {
                comboBox5.Items.Add(myReader.GetString("title"));
            }

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = "C:\\ProgramData\\MySQL\\MySQL Server 5.7\\Uploads";
            open.Filter = "Image Files (*.jpg)|*.jpg|All Files(*.*)|*.*";
            open.FilterIndex = 1;
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (open.CheckFileExists)
                {
                    string pathname = System.IO.Path.GetFullPath(open.FileName);
                    pathname = pathname.Replace("\\", "/");
                    label15.Text = pathname;
                }
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string title = textBox1.Text;
            int year = Int32.Parse(textBox5.Text);
            string genre = textBox4.Text;
            string language = textBox6.Text;
            string image = label15.Text;
//            int director_id = comboBox1.Text;
            string about = richTextBox1.Text;


            DBConnect db = new DBConnect();
            if (db.OpenConnection())
            {
                MySqlConnection connection = db.getConnection();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT director_id FROM director WHERE name= '" + comboBox1.Text + "';";
                MySqlDataReader myReader = command.ExecuteReader();
                myReader.Read();
                int dir_id = myReader.GetInt32("director_id");
                myReader.Close();
                command.CommandText = "INSERT INTO `movie`(`title`, `year`, `language`, `genre`,  `image`, `director_id`, `about`) VALUES ('" + title + "', '" + year + "','" + language + "','" + genre + "', LOAD_FILE('" + image + "'),'" + dir_id + "','" + about + "' );";
                command.ExecuteNonQuery();
                MessageBox.Show("Movie Inserted Succesfully");
            }
            else
            {
                MessageBox.Show("NOT Connected");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DBConnect db = new DBConnect();
            if (db.OpenConnection())
            {

                string name = textBox2.Text;
                string gender = comboBox2.Text;
                string dob = dateTimePicker1.Text;
                string about = richTextBox2.Text;
                string image = label23.Text;
                MySqlConnection connection = db.getConnection();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO `director`( `name`, `gender`, `dob`, `about`, `image`) VALUES ('" + name + "','" + gender + "', '" + dob + "','" + about + "', LOAD_FILE('" + image + "'));";
                command.ExecuteNonQuery();
                MessageBox.Show("Director Inserted Succesfully");
            }
            else
            {
                MessageBox.Show("NOT Connected");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = "C:\\ProgramData\\MySQL\\MySQL Server 5.7\\Uploads";
            open.Filter = "Image Files (*.jpg)|*.jpg|All Files(*.*)|*.*";
            open.FilterIndex = 1;
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (open.CheckFileExists)
                {
                    string pathname = System.IO.Path.GetFullPath(open.FileName);
                    pathname=pathname.Replace("\\", "/");
                    label23.Text = pathname;
                    //System.IO.File.Copy(open.FileName, @"D:/C++ Programs(While Learning)/Movie Database/images/directors/" + pathname);
                }
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = "C:\\ProgramData\\MySQL\\MySQL Server 5.7\\Uploads";
            open.Filter = "Image Files (*.jpg)|*.jpg|All Files(*.*)|*.*";
            open.FilterIndex = 1;
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (open.CheckFileExists)
                {
                    string pathname = System.IO.Path.GetFullPath(open.FileName);
                    pathname = pathname.Replace("\\", "/");
                    label21.Text = pathname;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DBConnect db = new DBConnect();
            if (db.OpenConnection())
            {

                string name = textBox3.Text;
                string gender = comboBox3.Text;
                string dob = dateTimePicker2.Text;
                string about = richTextBox3.Text;
                string image = label21.Text;
                MySqlConnection connection = db.getConnection();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO `actor`(`name`, `gender`, `dob`, `about`, `image`) VALUES ('" + name + "','" + gender + "', '" + dob + "','" + about + "', LOAD_FILE('" + image + "'));";
                command.ExecuteNonQuery();
                MessageBox.Show("Actor Inserted Succesfully");
            }
            else
            {
                MessageBox.Show("NOT Connected");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            (new Login()).Show();
            this.Hide();
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
            DBConnect db = new DBConnect();
            db.OpenConnection();
            MySqlDataReader myReader;
            MySqlCommand SelectCommand;

            SelectCommand = new MySqlCommand("SELECT * FROM director", db.getConnection());
            myReader = SelectCommand.ExecuteReader();
            while (myReader.Read())
            {
                comboBox1.Items.Add(myReader.GetString("name"));
            }
            myReader.Close();
            SelectCommand = new MySqlCommand("SELECT * FROM actor", db.getConnection());
            myReader = SelectCommand.ExecuteReader();
            while (myReader.Read())
            {
                comboBox4.Items.Add(myReader.GetString("name"));
            }
            myReader.Close();
            SelectCommand = new MySqlCommand("SELECT * FROM movie", db.getConnection());
            myReader = SelectCommand.ExecuteReader();
            while (myReader.Read())
            {
                comboBox5.Items.Add(myReader.GetString("title"));
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DBConnect db = new DBConnect();
            if (db.OpenConnection())
            {
                MySqlConnection connection = db.getConnection();
                MySqlCommand command = connection.CreateCommand();
                string actor = comboBox4.Text;
                string movie = comboBox5.Text;

                command.CommandText = "SELECT actor_id FROM actor WHERE name= '" + actor + "';";
                MySqlDataReader myReader = command.ExecuteReader();
                myReader.Read();
                int act_id = myReader.GetInt32("actor_id");
                myReader.Close();

                command.CommandText = "SELECT movie_id FROM movie WHERE title= '" + movie + "';";
                myReader = command.ExecuteReader();
                myReader.Read();
                int mov_id = myReader.GetInt32("movie_id");
                myReader.Close();

                command.CommandText = "INSERT INTO `movie_actor`(`movie_id`, `actor_id`) VALUES ( "+mov_id +" , " + act_id + " );";
                try
                {
                    command.ExecuteNonQuery();
                     MessageBox.Show("Inserted Succesfully");
                }
                catch(Exception ef)
                {
                    MessageBox.Show("Insert Unsuccessful");
                }
               
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {

        }
    }
}
