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
    public partial class MovieSearchResult : Form
    {
        public MovieSearchResult(string query)
        {

            //MessageBox.Show(query);
            
            InitializeComponent();
            DBConnect db = new DBConnect();
            MySqlConnection connection = db.getConnection();
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);

            
            DataTable data = new DataTable();
            adapter.Fill(data);
            dataGridView1.DataSource = data;
             

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
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
