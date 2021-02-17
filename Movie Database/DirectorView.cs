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
    public partial class DirectorView : Form
    {
        public DirectorView(string director_name)
        {
            InitializeComponent();
            string director_id ="";
            DBConnect db = new DBConnect();
            db.OpenConnection();

            MySqlConnection connection = db.getConnection();
            MySqlCommand command = connection.CreateCommand();
            string query = "SELECT gender, about, name, date(dob), director_id, image FROM director WHERE name LIKE '%" + director_name + "%';";
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
                    label7.Text = reader.GetString(reader.GetOrdinal("date(dob)"));
                    director_id = reader.GetString(reader.GetOrdinal("director_id"));

                    try
                    {
                        Byte[] byteBLOBData = (byte[])reader["image"];
                        MemoryStream ms = new MemoryStream(byteBLOBData);
                        pictureBox1.Image = Image.FromStream(ms);
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox1.Size = new Size(150, 150);
                    }
                    catch(Exception e)
                    {
                        Console.Write(e.Message);
                    }
                    

                }
            }
            query = String.Format(" SELECT  title FROM movie WHERE director_id = " + director_id + ";");
            command = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);


            DataTable data = new DataTable();
            adapter.Fill(data);
            dataGridView1.DataSource = data;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            //MessageBox.Show(value);

            MovieView movie_profile = new MovieView(value);
            movie_profile.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserHome userHome = new UserHome(variables.username);
            userHome.Show();
            this.Hide();
        }
    }
}
