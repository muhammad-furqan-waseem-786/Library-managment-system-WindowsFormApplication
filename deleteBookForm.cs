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
    public partial class deleteBookForm : Form
    {
        private Validation validationObj = new Validation();
        private SqlDataAdapter dataAdapterObj;
        private DataTable tableObj = new DataTable();
        private string selectQuery = "\0" , deleteQuery = "\0";
        private string bookTitle = "\0";
        private int bookSessionNumber = 0;

        public deleteBookForm()
        {
            InitializeComponent();
        }
        //this loads the data into the gridview when form loads
        private void LoadGridView()
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                SqlDataAdapter dataAdapterObj = new SqlDataAdapter("SELECT Title,Author,Edition,Copies,SessionNumber FROM bookDetails", connectionObj);
                DataTable dataTableObj = new DataTable();
                dataAdapterObj.Fill(dataTableObj);
                dataGridView.DataSource = dataTableObj;

                dataGridView.Columns[0].HeaderText = "Book Title";
                dataGridView.Columns[1].HeaderText = "Book Author";
                dataGridView.Columns[2].HeaderText = "Book Edition";
                dataGridView.Columns[3].HeaderText = "Number of copies";
                dataGridView.Columns[4].HeaderText = "Book Session Number";
                dataGridView.Sort(dataGridView.Columns[4], ListSortDirection.Ascending);
                connectionObj.Close();
            }
        }
        private bool titleExistance(string title)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                selectQuery = "SELECT *FROM bookDetails WHERE Title = '" + title + "'";
                dataAdapterObj = new SqlDataAdapter(selectQuery, connectionObj);
                dataAdapterObj.Fill(tableObj);

                connectionObj.Close();

                if (tableObj.Rows.Count > 0)
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
        private bool sessionNumberExistance(int bookSessionNmber)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                selectQuery = "SELECT *FROM bookDetails WHERE sessionNumber = '" + bookSessionNmber + "'";
                dataAdapterObj = new SqlDataAdapter(selectQuery, connectionObj);
                dataAdapterObj.Fill(tableObj);

                connectionObj.Close();

                if (tableObj.Rows.Count > 0)
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
        private void deleteBookOnTitle(string title)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();
                deleteQuery = "DELETE bookDetails WHERE Title = '" + title + "'";
                SqlCommand commandObj = new SqlCommand(deleteQuery, connectionObj);
                commandObj.ExecuteNonQuery();
                connectionObj.Close();
            }
        }
        private void deleteBookOnSessionNumber(int session)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                deleteQuery = "DELETE bookDetails WHERE SessionNumber = '" + session + "'";
                SqlCommand commandObj = new SqlCommand(deleteQuery, connectionObj);
                commandObj.ExecuteNonQuery();

                connectionObj.Close();
            }
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(selectDeleteElement.Text != "Book Title" && selectDeleteElement.Text != "Session Number")
            {
                MessageBox.Show("Select the correct option" , "Error" , MessageBoxButtons.OK , MessageBoxIcon.Error);
            }
            else if (selectDeleteElement.Text == "Book Title")
            {
                if (!validationObj.stringOnly(inputTextBox.Text))
                {
                    MessageBox.Show("Title contains characters only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    inputTextBox.Clear();
                }
            }
            else if (selectDeleteElement.Text == "Session Number")
            {
                if (inputTextBox.Text == "" || inputTextBox.Text == " ")
                {
                    return;
                }
                else if (!validationObj.integerOnly(inputTextBox.Text))
                {
                    MessageBox.Show("Session Number contains digits only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    inputTextBox.Clear();
                }
                else if (!validationObj.checkLength((inputTextBox.Text), 5))
                {
                    MessageBox.Show("Session contains maximum 4 digits", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    inputTextBox.Clear();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(validationObj.nonEmptyOrNullString(selectDeleteElement.Text) || validationObj.nonEmptyOrNullString(inputTextBox.Text))
                {
                    MessageBox.Show("None of the field can be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (selectDeleteElement.Text == "Book Title")
                    {
                        bookTitle = inputTextBox.Text;

                        if (titleExistance(bookTitle))
                        {
                            deleteBookOnTitle(bookTitle);
                            MessageBox.Show("Data Deleted", "Delete Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadGridView();
                        }
                        else
                        {
                            MessageBox.Show("Book doesn't exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (selectDeleteElement.Text == "Session Number")
                    {
                        bookSessionNumber = int.Parse(inputTextBox.Text);
                        if(sessionNumberExistance(bookSessionNumber))
                        {
                            deleteBookOnSessionNumber(bookSessionNumber);
                            MessageBox.Show("Data Deleted", "Delete Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadGridView();
                        }
                        else
                        {
                            MessageBox.Show("Book with "+ bookSessionNumber + " session number doesn't exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch(Exception obj)
            {
                MessageBox.Show(obj.Message);
            }
            
        }

        private void deleteBook_Load(object sender, EventArgs e)
        {
            LoadGridView();
        }
    }
}
