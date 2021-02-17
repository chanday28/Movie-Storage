using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using MySql.Data.MySqlClient;

namespace Movie_Database
{
    public partial class UserHome : Form
    {
        string username;
        public UserHome(string username)
        {
            this.username = username;
            InitializeComponent();
            variables.username = username;
            DBConnect db = new DBConnect();
            if (db.OpenConnection())
            {
                string query = String.Format("SELECT title, avg(rating) as rating FROM movie natural LEFT OUTER JOIN review GROUP BY movie_id, title LIMIT 10");

                MySqlConnection connection = db.getConnection();
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);


                DataTable data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            string value = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            //MessageBox.Show(value);

            MovieView movie_profile = new MovieView(value);
            movie_profile.Show();
            this.Hide();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            ActorSearch actor_search = new ActorSearch();
            actor_search.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserProfile details = new UserProfile();
            details.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MovieSearch movie_search = new MovieSearch();
            movie_search.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DirectorSearch director_search = new DirectorSearch();
            director_search.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Forum fp = new Forum(username);
            fp.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string query = "SELECT movie_id, title, director.name, year FROM favourites NATURAL JOIN (movie INNER JOIN director USING (director_id)) WHERE username= '" + variables.username + "';";
            MovieSearchResult msr = new MovieSearchResult(query);
            msr.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Login login= new Login();
            login.Show();
            this.Hide();
            variables.username = "";
        }
    }
}
