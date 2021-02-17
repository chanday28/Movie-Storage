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
    public partial class ForumPost : Form
    {
        int ques_id;
        string username;
        public ForumPost(int id, string username)
        {
            ques_id = id;
            this.username =username;
            InitializeComponent();
            DBConnect db = new DBConnect();
            db.OpenConnection();

            MySqlConnection connection = db.getConnection();
            MySqlCommand command = connection.CreateCommand();
            string query = "SELECT username, ts, post FROM forum WHERE ques_id =" + id + " ORDER BY ts asc;";
            command.CommandText = query;
            // MessageBox.Show(query);
            //command.ExecuteReader();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string usermane = reader.GetString(reader.GetOrdinal("username"));
                    string timestamp = reader.GetString(reader.GetOrdinal("ts"));
                    string post = reader.GetString(reader.GetOrdinal("post"));
                    string line = usermane + "(" + timestamp + "): " + post;
                    richTextBox1.Text += line + "\n";

                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBConnect db = new DBConnect();
            if (db.OpenConnection())
            {
                MySqlConnection connection = db.getConnection();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO `forum`(`ques_id`,`username`, `ts`, `post`) VALUES ('" + ques_id + "', '" + username + "', current_timestamp(), '" + textBox1.Text + "');";
                command.ExecuteNonQuery();
                richTextBox1.Text = "";
                string query = "SELECT username, ts, post FROM forum WHERE ques_id =" + ques_id + " ORDER BY ts asc;";
                command.CommandText = query;
                // MessageBox.Show(query);
                //command.ExecuteReader();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string usermane = reader.GetString(reader.GetOrdinal("username"));
                        string timestamp = reader.GetString(reader.GetOrdinal("ts"));
                        string post = reader.GetString(reader.GetOrdinal("post"));
                        string line = usermane + "(" + timestamp + "): " + post;
                        richTextBox1.Text += line + "\n";

                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Forum forum = new Forum(username);
            forum.Show();
            this.Hide();
        }

        private void ForumPost_Load(object sender, EventArgs e)
        {

        }
    }
}
