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
    public partial class ActorView : Form
    {
        public ActorView(string actor_name)
        {
            InitializeComponent();
            string actor_id = "";
            DBConnect db = new DBConnect();
            db.OpenConnection();

            MySqlConnection connection = db.getConnection();
            MySqlCommand command = connection.CreateCommand();
            string query = "SELECT * FROM actor WHERE name LIKE '%" + actor_name + "%';";
            command.CommandText = query;
            // MessageBox.Show(query);
            //command.ExecuteReader();
             using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    label1.Text = reader.GetString(reader.GetOrdinal("gender"));
                    richTextBox1.Text = reader.GetString(reader.GetOrdinal("about"));
                    label4.Text = reader.GetString(reader.GetOrdinal("name"));
                    label7.Text = reader.GetString(reader.GetOrdinal("dob"));
                    actor_id = reader.GetString(reader.GetOrdinal("actor_id"));

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
                    
                }
            }
            
            query = String.Format(" SELECT  title, avg(rating) FROM (movie NATURAL JOIN movie_actor NATURAL LEFT OUTER JOIN review) WHERE actor_id = "+ actor_id+" GROUP BY movie_id, title;");
            command = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);


            DataTable data = new DataTable();
            adapter.Fill(data);
            dataGridView1.DataSource = data;



        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            //MessageBox.Show(value);

            MovieView movie_profile = new MovieView(value);
            movie_profile.Show();
            this.Hide();
        }

        private void Home_Click(object sender, EventArgs e)
        {
            UserHome userHome = new UserHome(variables.username);
            userHome.Show();
            this.Hide();
        }
    }
}
