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
using System.IO;


namespace Movie_Database
{
    public partial class MovieView : Form
    {
        int movie_id;
        public MovieView(string movie_name)
        {
            InitializeComponent();
            label10.Text = "";
            DBConnect db = new DBConnect();
            db.OpenConnection();

            MySqlConnection connection = db.getConnection();
            MySqlCommand command = connection.CreateCommand();
            string query = "SELECT * FROM movie WHERE movie.title LIKE '%"+ movie_name +"%';";
            command.CommandText = query;
            movie_id =1;
           // MessageBox.Show(query);
           //command.ExecuteReader();
           using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    movie_id = int.Parse(reader.GetString(reader.GetOrdinal("movie_id")));
                    label6.Text = reader.GetString(reader.GetOrdinal("title"));
                    label8.Text = reader.GetString(reader.GetOrdinal("genre"));
                    textBox1.Text = reader.GetString(reader.GetOrdinal("about"));

                    try
                    {
                        Byte[] byteBLOBData = (byte[])reader["image"];
                        MemoryStream ms = new MemoryStream(byteBLOBData);
                        pictureBox1.Image = Image.FromStream(ms);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.Size = new Size(150, 150);
                    }
                    catch (Exception e)
                    {
                        Console.Write(e.Message);
                    }

                    //string imagelink = "D:/C++ Programs(While Learning)/Movie Database/images/movies/" + reader.GetString(reader.GetOrdinal("image"));

                    //pictureBox1.Image = Image.FromFile(imagelink);
                    //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    //pictureBox1.Size = new Size(276, 206);

                }
            }
           query = String.Format("SELECT username, review_provided, rating FROM review WHERE movie_id =" + movie_id + ";");
           command = new MySqlCommand(query, connection);
           MySqlDataAdapter adapter = new MySqlDataAdapter(command);


           DataTable data = new DataTable();
           adapter.Fill(data);
           dataGridView1.DataSource = data;


           query = "SELECT name FROM movie_actor natural join actor WHERE movie_id=" + movie_id + ";";
           command.CommandText = query;
           // MessageBox.Show(query);
           //command.ExecuteReader();
           using (var reader = command.ExecuteReader())
           {
               if (reader.Read())
               {
                   
                   string actor = reader.GetString(reader.GetOrdinal("name"));
                   if (label10.Text == "")
                       label10.Text = actor;
                   else
                       label10.Text += ", " + actor;
           
               }
           }

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form7_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProvideReview review = new ProvideReview(this,label6.Text);
            review.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DBConnect db = new DBConnect();
            if (db.OpenConnection())
            {
                MySqlConnection connection = db.getConnection();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO `favourites`(`movie_id`, `username`) VALUES (" + movie_id + ", '" + variables.username +"');";
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Movie Added to Favourites");
                }
                catch(Exception rt)
                {
                    MessageBox.Show("Already added");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UserHome userHome = new UserHome(variables.username);
            userHome.Show();
            this.Hide();
        }

    }
}
