using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Movie_Database
{
    public partial class Forum : Form
    {
        string username;
        public Forum(string username)
        {
            this.username = username;
            InitializeComponent();
            DBConnect db = new DBConnect();
            if (db.OpenConnection())
            {
                string query = String.Format("SELECT count_threads() from dual;");
                MySqlConnection connection = db.getConnection();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = query;
                var reader = command.ExecuteReader();
                int count;
                reader.Read();
                count = int.Parse(reader.GetString(reader.GetOrdinal("count_threads()")));
                reader.Close();
                DataTable data = new DataTable();
                query = String.Format("SELECT ques_id, post, username, ts FROM forum group by ques_id LIMIT "+count+";");
                command.CommandText = query;
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(data);
                dataGridView1.DataSource = data;
                
                //reader = command.ExecuteReader();
                //reader.Read();

                //    //System.Windows.Forms.DataGridViewButtonColumn view;
                //    data.Rows.Add(reader["ques_id"].ToString(), reader["post"].ToString(), reader["username"].ToString(), reader["ts"].ToString());//, new DataGridViewButtonColumn("View"));
                

            }
        }
            
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            //MessageBox.Show(value);

            ForumPost forum_post = new ForumPost(Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()), username);
            forum_post.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBConnect db = new DBConnect();
            db.OpenConnection();

            MySqlConnection connection = db.getConnection();
            MySqlCommand command = connection.CreateCommand();
            string query = "SELECT MAX(ques_id) from forum;";
            command.CommandText = query;
            var reader = command.ExecuteReader();
            int newid;
            reader.Read();
            try{
            //if (reader.GetString(reader.GetOrdinal("MAX(ques_id)")))
                newid = int.Parse(reader.GetString(reader.GetOrdinal("MAX(ques_id)"))) + 1;
            }
            catch(Exception eg)
            {          newid = 1;
            }
            ForumPost f_post = new ForumPost(newid, username);
            this.Hide();
            f_post.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserHome userHome = new UserHome(variables.username);
            userHome.Show();
            this.Hide();
        }

    }
}
