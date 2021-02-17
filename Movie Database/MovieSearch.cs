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
    public partial class MovieSearch : Form
    {
        public MovieSearch()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = 0;
            string query = "SELECT movie_id, title, director.name, year FROM (movie INNER JOIN director using (director_id)) INNER JOIN (movie_actor NATURAL JOIN actor) using(movie_id) ";
            if(textBox1.Text!=""){
                query += "WHERE ";
                query += "title LIKE '%" + textBox1.Text + "%' ";
                count++;
            }
            if (textBox2.Text != "") {
                if (count != 0)
                {
                    query += "AND ";
                }
                else {

                    query += "WHERE ";

                }
                query += "year =  " + textBox2.Text + " ";
                count++;
            }
            if (textBox3.Text != "") {
                if (count != 0)
                {
                    query += "AND ";
                }
                else
                {

                    query += "WHERE ";

                }
                query += " director.name LIKE '%" + textBox3.Text + "%' ";
                count++;
            }
            if (textBox4.Text != "") {
                if (count != 0)
                {
                    query += "AND ";
                }
                else
                {

                    query += "WHERE ";

                }
                query += " actor.name LIKE '%" + textBox4.Text + "%' ";
                count++;            
            }
            query += ";";

            MovieSearchResult movies = new MovieSearchResult(query);
            movies.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserHome userHome = new UserHome(variables.username);
            userHome.Show();
            this.Hide();
        }
    }
}
