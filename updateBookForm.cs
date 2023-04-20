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
    public partial class updateBookForm : Form
    {
        private Validation validationObj = new Validation();
        private SqlDataAdapter dataAdapterObj;
        private DataTable tableObj = new DataTable();
        private string selectQuery = "\0" , bookStatus = "\0";
        private int bookSessionNumber = 0;

        public updateBookForm()
        {
            InitializeComponent();
        }
        private bool sessionNumberExistance(int bookSessionNmber)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                selectQuery = "SELECT *FROM bookDetails WHERE sessionNumber = '" + bookSessionNmber + "'";
                dataAdapterObj = new SqlDataAdapter(selectQuery, connectionObj);
                dataAdapterObj.Fill(tableObj);

                connectionObj.Close();

                if (tableObj.Rows.Count == 1)
                {
                    //We clear it because when once the rows are effected than it 
                    //does changes to zero but it increments its value
                    tableObj.Rows.Clear();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private void updateBook(string status , int session)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();
                string query = "UPDATE bookDetails SET Status = '" + status + "' WHERE SessionNumber = '" + session + "'";
                SqlCommand commandObj = new SqlCommand(query, connectionObj);
                commandObj.ExecuteNonQuery();
                connectionObj.Close();
                MessageBox.Show("Record Updated", "Book Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (validationObj.nonEmptyOrNullString(bookSessionTextBox.Text))
                {
                    MessageBox.Show("None of the field can be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(statusComboBox.Text != "Missing" && statusComboBox.Text != "Issued")
                {
                    MessageBox.Show("Select the correct book status", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    bookSessionNumber = int.Parse(bookSessionTextBox.Text);
                    bookStatus = statusComboBox.Text;

                    if (sessionNumberExistance(bookSessionNumber))
                    {
                        updateBook(bookStatus, bookSessionNumber);
                    }
                    else
                    {
                        MessageBox.Show("Book with " + bookSessionNumber + " doesn't exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    loadGridView();
                }
            }
            catch(Exception obj)
            {
                MessageBox.Show(obj.Message);
            }
        }
        private void updateBookForm_Load(object sender, EventArgs e)
        {
            loadGridView();
        }

        private void loadGridView()
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                SqlDataAdapter dataAdapterObj = new SqlDataAdapter("SELECT Title,Author,Edition,Copies,Status,SessionNumber,Price FROM bookDetails", connectionObj);
                DataTable dataTableObj = new DataTable();
                dataAdapterObj.Fill(dataTableObj);
                dataGridView.DataSource = dataTableObj;

                dataGridView.Columns[0].HeaderText = "Book Title";
                dataGridView.Columns[1].HeaderText = "Book Author";
                dataGridView.Columns[2].HeaderText = "Book Edition";
                dataGridView.Columns[3].HeaderText = "Number of copies";
                dataGridView.Columns[4].HeaderText = "Book Status";
                dataGridView.Columns[5].HeaderText = "Book Session Number";
                dataGridView.Columns[6].HeaderText = "Price";
                dataGridView.Sort(dataGridView.Columns[5], ListSortDirection.Ascending);
                connectionObj.Close();
            }
        }

        private void bookSessionTextBox_TextChanged(object sender, EventArgs e)
        {
            if(bookSessionTextBox.Text == "" || bookSessionTextBox.Text == " ")
            {
                return;
            }
            else if(!validationObj.integerOnly(bookSessionTextBox.Text))
            {
                MessageBox.Show("Book Session Number contains digits only" , "Error", MessageBoxButtons.OK , MessageBoxIcon.Error);
                bookSessionTextBox.Clear();
            }
            else if(!validationObj.checkLength(bookSessionTextBox.Text , 5))
            {
                MessageBox.Show("Book Session Number contains 4 digits", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bookSessionTextBox.Clear();
            }
            else if(int.Parse(bookSessionTextBox.Text) <= 0)
            {
                MessageBox.Show("Book Session Number should be greater than zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bookSessionTextBox.Clear();
            }
        }
    }
}
