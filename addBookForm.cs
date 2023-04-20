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
    public partial class addBookForm : Form
    {
        private Validation validationObj = new Validation();
        private SqlCommand commandObj;
        private SqlDataAdapter dataAdapter;
        private DataTable tableObj = new DataTable();
        private string insertQuery = "\0", selectQuery = "\0";
        private string title = "\0", author = "\0", edition = "\0";
        private float price = 0;
        private int copies = 0;
        private int session = 0;

        public addBookForm()
        {
            InitializeComponent();
        }
        private void insertData(string fTitle, string fAuthor, string fEdition, int fCopies, int fSessionNumber, float fPrice, string fStatus, string fIssueDate, string fRegisterationNumber)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                for (int i = 1; i <= int.Parse(copiesTextBox.Text); i++)
                {

                    insertQuery = "INSERT INTO bookDetails VALUES(@Title,@Author,@Edition,@Copies,@SessionNumber,@Price,@Status,@IssueDate,@RegisterationNumber)";
                    commandObj = new SqlCommand(insertQuery, connectionObj);

                    commandObj.Parameters.AddWithValue("@Title", fTitle);
                    commandObj.Parameters.AddWithValue("@Author", fAuthor);
                    commandObj.Parameters.AddWithValue("@Edition", fEdition);
                    commandObj.Parameters.AddWithValue("@Copies", fCopies);
                    commandObj.Parameters.AddWithValue("@SessionNumber", fSessionNumber);
                    commandObj.Parameters.AddWithValue("@Price", fPrice);
                    commandObj.Parameters.AddWithValue("@Status", fStatus);
                    commandObj.Parameters.AddWithValue("@IssueDate", fIssueDate);
                    commandObj.Parameters.AddWithValue("@RegisterationNumber", fRegisterationNumber);
                    fSessionNumber++;

                    commandObj.ExecuteNonQuery();
                }

                connectionObj.Close();
            }
        }
        private bool sessionNumberExistance(int fSessionNumber , int copies)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                for (int i = 1; i <= copies; i++)
                {
                    selectQuery = "SELECT *From bookDetails WHERE sessionNumber = '" + fSessionNumber + "' ";
                    dataAdapter = new SqlDataAdapter(selectQuery, connectionObj);
                    dataAdapter.Fill(tableObj);
                    fSessionNumber++;
                }
                
                connectionObj.Close();

                if (tableObj.Rows.Count == 0)
                {
                    return true;
                }
                else
                {
                    tableObj.Rows.Clear();
                    return false;
                }
            }
        }
        private void loadGridView()
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                SqlDataAdapter dataAdapterObj = new SqlDataAdapter("SELECT Title,Author,Edition,Copies,SessionNumber,Price FROM bookDetails", connectionObj);
                DataTable dataTableObj = new DataTable();
                dataAdapterObj.Fill(dataTableObj);
                dataGridView.DataSource = dataTableObj;

                dataGridView.Columns[0].HeaderText = "Book Title";
                dataGridView.Columns[1].HeaderText = "Book Author";
                dataGridView.Columns[2].HeaderText = "Book Edition";
                dataGridView.Columns[3].HeaderText = "Number of copies";
                dataGridView.Columns[4].HeaderText = "Book Session Number";
                dataGridView.Columns[5].HeaderText = "Price";
                dataGridView.Sort(dataGridView.Columns[4], ListSortDirection.Ascending);
                connectionObj.Close();
            }
        }

        private void addBookButton_Click(object sender, EventArgs e)
        {
            if (validationObj.nonEmptyOrNullString(title) || validationObj.nonEmptyOrNullString(author) || validationObj.nonEmptyOrNullString(edition) || validationObj.nonEmptyOrNullString(sessionNumberTextBox.Text) || validationObj.nonEmptyOrNullString(priceTextBox.Text) || validationObj.nonEmptyOrNullString(copiesTextBox.Text))
            {
                MessageBox.Show("None of the field can be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                title = titleTextBox.Text;
                author = authorTextBox.Text;
                edition = editionTextBox.Text;
                copies = int.Parse(copiesTextBox.Text);
                session = int.Parse(sessionNumberTextBox.Text);
                price = float.Parse(priceTextBox.Text);

                if (sessionNumberExistance(session, copies))
                {
                    insertData(title, author, edition, copies, session, price, "Available", "0/0/0", "None");
                    MessageBox.Show("Data saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadGridView();
                }
                else
                {
                    MessageBox.Show("Session Number already exist " , "Error" , MessageBoxButtons.OK , MessageBoxIcon.Error);
                    sessionNumberTextBox.Clear();
                }
            }
        }

        private void titleTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!validationObj.stringOnly(titleTextBox.Text))
            {
                MessageBox.Show("Title contains characters only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                titleTextBox.Clear();
            }
        }

        private void authorTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!validationObj.stringOnly(authorTextBox.Text))
            {
                MessageBox.Show("Author contains characters only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                authorTextBox.Clear();
            }
        }

        private void editionTextBox_TextChanged(object sender, EventArgs e)
        {
            if (editionTextBox.Text == "" || editionTextBox.Text == " ")
            {
                return;
            }
            else if (!validationObj.integerOnly(editionTextBox.Text))
            {
                MessageBox.Show("Edition contains digits only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                editionTextBox.Clear();
            }
            else if(int.Parse(editionTextBox.Text) < 0)
            {
                MessageBox.Show("Edition should be greater than zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                editionTextBox.Clear();
            }
        }

        private void copiesTextBox_TextChanged(object sender, EventArgs e)
        {
            if (copiesTextBox.Text == "" || copiesTextBox.Text == " ")
            {
                return;
            }
            else if (!validationObj.integerOnly(copiesTextBox.Text))
            {
                MessageBox.Show("Copies contains digits only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                copiesTextBox.Clear();
            }
            else if (int.Parse(copiesTextBox.Text) <= 0)
            {
                MessageBox.Show("Number of copies should be greater than 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                copiesTextBox.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            titleTextBox.Clear();
            authorTextBox.Clear();
            sessionNumberTextBox.Clear();
            editionTextBox.Clear();
            copiesTextBox.Clear();
            priceTextBox.Clear();
        }

        private void sessionNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            if (sessionNumberTextBox.Text == "" || sessionNumberTextBox.Text == " ")
            {
                return;
            }
            else if (!validationObj.integerOnly(sessionNumberTextBox.Text))
            {
                MessageBox.Show("Session Number contains digits only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sessionNumberTextBox.Clear();
            }
            else if (!validationObj.checkLength(sessionNumberTextBox.Text, 5))
            {
                MessageBox.Show("Session Number contains 4 digits", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sessionNumberTextBox.Clear();
            }
            else if (int.Parse(sessionNumberTextBox.Text) <= 0)
            {
                MessageBox.Show("Number of copies should be greater than 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                sessionNumberTextBox.Clear();
            }
        }

        private void priceTextBox_TextChanged(object sender, EventArgs e)
        {
            if (priceTextBox.Text == "" || priceTextBox.Text == " ")
            {
                return;
            }
            else if (!validationObj.floatOnly(priceTextBox.Text))
            {
                MessageBox.Show("Price contains digits only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                priceTextBox.Clear();
            }
            else if (int.Parse(priceTextBox.Text) <= 0)
            {
                MessageBox.Show("Number of copies should be greater than 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                priceTextBox.Clear();
            }
        }

        private void addBookForm_Load(object sender, EventArgs e)
        {
            loadGridView();
        }
    }
}

