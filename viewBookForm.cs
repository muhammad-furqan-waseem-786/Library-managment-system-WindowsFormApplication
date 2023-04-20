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
    public partial class viewBookForm : Form
    {
        private Validation validationObj = new Validation();
        public viewBookForm()
        {
            InitializeComponent();
        }

        private void viewBookForm_Load(object sender, EventArgs e)
        {
            LoadGridView();
        }

        public void LoadGridView()
        {
            string query = "SELECT *FROM bookDetails";
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();
                SqlDataAdapter dataAdapterObj = new SqlDataAdapter(query, connectionObj);
                DataTable dataTableObj = new DataTable();
                dataAdapterObj.Fill(dataTableObj);
                dataGridView.DataSource = dataTableObj;

                dataGridView.Columns[0].HeaderText = "Book Title";
                dataGridView.Columns[1].HeaderText = "Book Author";
                dataGridView.Columns[2].HeaderText = "Book Edition";
                dataGridView.Columns[3].HeaderText = "Book Copies";
                dataGridView.Columns[4].HeaderText = "Book Session Number";
                dataGridView.Columns[5].HeaderText = "Book Price";
                dataGridView.Columns[6].HeaderText = "Book Status";
                dataGridView.Columns[7].HeaderText = "Book Issue Date";
                dataGridView.Columns[8].HeaderText = "Registeration Number";
                dataGridView.Sort(dataGridView.Columns[4], ListSortDirection.Ascending);

                connectionObj.Close();
            }
        }
    }
}
