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
    public partial class DirectorSearchResult : Form
    {
        public DirectorSearchResult(string director_name)
        {
            InitializeComponent();
            DBConnect db = new DBConnect();
            if (db.OpenConnection())
            {
                string query = String.Format("SELECT name, gender, dob FROM director WHERE name LIKE '%"+director_name+"%';");

                //MessageBox.Show("SELECT name, gender as Gender, dob FROM director WHERE name LIKE '%" + director_name + "%';");

                MySqlConnection connection = db.getConnection();
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);


                DataTable data = new DataTable();
                adapter.Fill(data);
                dataGridView1.DataSource = data;
            }
        }

        private void Form13_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            DirectorView director_profile = new DirectorView(value);
            director_profile.Show();
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
