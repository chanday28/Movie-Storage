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
    public partial class ProvideReview : Form
    {
        MovieView mv;
        string movie_name;
        public ProvideReview(MovieView mv, string movie_name)
        {
            InitializeComponent();
            label2.Text = movie_name;
            this.mv = mv;
            this.movie_name = movie_name;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            int movie_id = 0;
            DBConnect db = new DBConnect();
            db.OpenConnection();

            MySqlCommand SelectCommand = new MySqlCommand("SELECT movie_id FROM movie WHERE movie.title LIKE '%"+label2.Text+"%';", db.getConnection());

            MySqlDataReader myReader;
            myReader = SelectCommand.ExecuteReader();
            int count = 0;
            while (myReader.Read())
            {
                movie_id = int.Parse(myReader.GetString(myReader.GetOrdinal("movie_id")));
                count = count + 1;
            }
            myReader.Close();
            MySqlConnection connection = db.getConnection();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO `review`(`movie_id`, `username`, `review_provided`, `rating`) VALUES ('" + movie_id + "','" + variables.username + "','" + richTextBox1.Text + "','" + comboBox1.Text + "')";
            command.ExecuteNonQuery();
            MessageBox.Show("Movie Reviewed");
            this.Hide();
            MovieView mv = new MovieView(movie_name);
            mv.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
