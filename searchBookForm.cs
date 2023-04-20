using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PROJECT
{
    public partial class searchBookForm : Form
    {
        private Validation validationObj = new Validation();

        public searchBookForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT Title , Author , Edition , SessionNumber , Status FROM BookDetails WHERE Title LIKE '%" + searchBarText.Text + "%' ", connectionObj);
                DataTable dataTableObj = new DataTable();
                dataAdapter.Fill(dataTableObj);
                dataGridView.DataSource = dataTableObj;

                dataGridView.Columns[0].HeaderText = "Book Title";
                dataGridView.Columns[1].HeaderText = "Book Author";
                dataGridView.Columns[2].HeaderText = "Book Edition";
                dataGridView.Columns[3].HeaderText = "Book Session Number";
                dataGridView.Columns[4].HeaderText = "Book Status";
                dataGridView.Sort(dataGridView.Columns[3], ListSortDirection.Ascending);

                connectionObj.Close();
            }
        }

        private void searchBookForm_Load(object sender, EventArgs e)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                SqlDataAdapter dataAdapterObj = new SqlDataAdapter("SELECT Title , Author , Edition , SessionNumber , Status FROM bookDetails", connectionObj);
                DataTable dataTableObj = new DataTable();
                dataAdapterObj.Fill(dataTableObj);
                dataGridView.DataSource = dataTableObj;

                dataGridView.Columns[0].HeaderText = "Book Title";
                dataGridView.Columns[1].HeaderText = "Book Author";
                dataGridView.Columns[2].HeaderText = "Book Edition";
                dataGridView.Columns[3].HeaderText = "Book Session Number";
                dataGridView.Columns[4].HeaderText = "Book Status";
                dataGridView.Sort(dataGridView.Columns[3], ListSortDirection.Ascending);

                connectionObj.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (validationObj.nonEmptyOrNullString(searchBarText.Text))
                {
                    MessageBox.Show("Search field is empty","Search book",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
                else
                {
                    using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
                    {
                        connectionObj.Open();
                        SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT *FROM BookDetails WHERE Title LIKE '%" + searchBarText.Text + "%' ", connectionObj);
                        DataTable dataTableObj = new DataTable();
                        dataAdapter.Fill(dataTableObj);

                        if (dataTableObj.Rows.Count > 0)
                        {
                            MessageBox.Show(dataTableObj.Rows.Count + " result Found " , "Search Book" , MessageBoxButtons.OK , MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No result Found");
                        }

                        connectionObj.Close();
                    }
                }
            }
            catch (Exception obj)
            {
                MessageBox.Show(obj.Message);
            }
        }
    }
}
