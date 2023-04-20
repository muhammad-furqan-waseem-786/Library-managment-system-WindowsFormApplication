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
    public partial class returnBookForm : Form
    {
        private Validation validationObj = new Validation();
        private SqlCommand commandObj;
        private SqlDataReader dataReaderObj;
        private SqlDataAdapter dataAdapterObj;
        private DataTable tableObj = new DataTable();
        private string updateQuery = "\0", selectQuery = "\0", insertQuery = "\0";
        private string status = "Available", registerationNumber = "\0", readDateFromDataBase = "\0";
        private string readFineFromDataBase = "\0" , storeDate = "0";
        private DateTime date = new DateTime();
        private int storeFine = 0 , calaulateNumberOfDays = 0 , calculatedFine = 0 , totalIssuedBook = 0;

        private void returnBookForm_Load(object sender, EventArgs e)
        {
            gridViewDisplay();
        }

        public returnBookForm()
        {
            InitializeComponent();
        }

        private bool bookIssuedStatus(int bookSessionNmber)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                selectQuery = "SELECT Status FROM bookDetails WHERE SessionNumber = '" + bookSessionNmber + "'";
                commandObj = new SqlCommand(selectQuery, connectionObj);
                dataReaderObj = commandObj.ExecuteReader();

                while (dataReaderObj.Read())
                {
                    status = dataReaderObj.GetValue(0).ToString();
                }
                dataReaderObj.Close();
                connectionObj.Close();

                if (status == "Issued")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private bool registerationNumberExistance(string regisisterationNumber)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                selectQuery = "SELECT *FROM FineTable WHERE RegisterationNumber = '" + registerationNumber + "'";
                dataAdapterObj = new SqlDataAdapter(selectQuery, connectionObj);
                dataAdapterObj.Fill(tableObj);
                connectionObj.Close();

                if (tableObj.Rows.Count == 1)
                {
                    tableObj.Rows.Clear();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private string issueBookDate(int bookSessionNumber)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                selectQuery = "SELECT IssueDate FROM bookDetails WHERE SessionNumber = '" + bookSessionNumber + "'";
                commandObj = new SqlCommand(selectQuery, connectionObj);
                dataReaderObj = commandObj.ExecuteReader();

                while (dataReaderObj.Read())
                {
                    readDateFromDataBase = dataReaderObj.GetValue(0).ToString();
                }

                dataReaderObj.Close();
                connectionObj.Close();
            }

            return readDateFromDataBase;
        }
        private string studentRegisterationNumber(int sessionNumber)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();
                selectQuery = "SELECT RegisterationNumber FROM bookDetails WHERE SessionNumber = '" + sessionNumber + "'";
                commandObj = new SqlCommand(selectQuery, connectionObj);
                dataReaderObj = commandObj.ExecuteReader();

                while (dataReaderObj.Read())
                {
                    registerationNumber = dataReaderObj.GetValue(0).ToString();
                }
                dataReaderObj.Close();
                connectionObj.Close();
            }
            return registerationNumber;
        }

        private void bookSessionNumber_TextChanged(object sender, EventArgs e)
        {
            if (bookSessionNumber.Text == "" || bookSessionNumber.Text == " ")
            {
                return;
            }
            else if (!validationObj.integerOnly(bookSessionNumber.Text))
            {
                MessageBox.Show("Book session number contains digits only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bookSessionNumber.Clear();
            }
            else if (!validationObj.checkLength(bookSessionNumber.Text, 5))
            {
                MessageBox.Show("Book session number contains 4 digits only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bookSessionNumber.Clear();
            }
            else if(int.Parse(bookSessionNumber.Text) <= 0)
            {
                MessageBox.Show("Book session number should be greater than 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bookSessionNumber.Clear();
            }
        }

        private void gridViewDisplay()
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                selectQuery = "SELECT Title,SessionNumber,Status,IssueDate,RegisterationNumber FROM bookDetails";
                SqlDataAdapter dataAdapterObj = new SqlDataAdapter(selectQuery , connectionObj);
                DataTable dataTableObj = new DataTable();
                dataAdapterObj.Fill(dataTableObj);
                dataGridView.DataSource = dataTableObj;
                dataGridView.Columns[0].HeaderText = "Book Title";
                dataGridView.Columns[1].HeaderText = "Book Session Number";
                dataGridView.Columns[2].HeaderText= "Book Status";
                dataGridView.Columns[3].HeaderText = "Book Issue Date";
                dataGridView.Columns[4].HeaderText = "Registeration Number";
                dataGridView.Sort(dataGridView.Columns[1], ListSortDirection.Ascending);
            }
        }
        private void updateBookDataBase(string fRegisterationNumber, string pDate, string status, int bookSessionNumber)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                updateQuery = "UPDATE bookDetails SET IssueDate = '" + pDate + "' , Status = '" + status + "' , RegisterationNumber = '" + fRegisterationNumber + "' WHERE SessionNumber = '" + bookSessionNumber + "'";
                commandObj = new SqlCommand(updateQuery, connectionObj);
                commandObj.ExecuteNonQuery();

                connectionObj.Close();
            }
        }
        private void fineDataBase(int fine , string registerationNumber , int flag)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                if(flag == 1)
                {
                    updateQuery = "UPDATE FineTable SET Fine = '" + fine + "' WHERE RegisterationNumber = '" + registerationNumber + "'";
                    commandObj = new SqlCommand(updateQuery, connectionObj);                    
                }
                else
                {
                    insertQuery = "INSERT INTO FineTable VALUES(@RegisterationNumber , @Fine)";
                    commandObj = new SqlCommand(insertQuery, connectionObj);
                    commandObj.Parameters.AddWithValue("@RegisterationNumber" , registerationNumber);
                    commandObj.Parameters.AddWithValue("@Fine" , fine);
                }

                commandObj.ExecuteNonQuery();
                connectionObj.Close();
            }
        }        
        private void decrementBook(int issuedBookQuantity, string fRegisterationNumber)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                issuedBookQuantity = issuedBookQuantity - 1;
                updateQuery = "UPDATE Student SET IssuedBook = '" + issuedBookQuantity + "' WHERE RegisterationNumber = '" + fRegisterationNumber + "'";
                commandObj = new SqlCommand(updateQuery, connectionObj);
                commandObj.ExecuteNonQuery();

                connectionObj.Close();
            }
        }
        private int existingFine(string registerationNumber)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                selectQuery = "SELECT Fine FROM FineTable WHERE RegisterationNumber = '" + registerationNumber + "'";
                commandObj = new SqlCommand(selectQuery, connectionObj);
                dataReaderObj = commandObj.ExecuteReader();

                while (dataReaderObj.Read())
                {
                    readFineFromDataBase = dataReaderObj.GetValue(0).ToString();
                }

                dataReaderObj.Close();
                connectionObj.Close();

                storeFine = int.Parse(readFineFromDataBase);

                if (storeFine > 0)
                {
                    return storeFine;
                }
                else
                {
                    return 0;
                }
            }
        }
        private int totalBookIssuedByStudent(string fRegisterationNumber)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                selectQuery = "SELECT issuedBook FROM Student WHERE RegisterationNumber = '" + fRegisterationNumber + "'";
                commandObj = new SqlCommand(selectQuery, connectionObj);
                dataReaderObj = commandObj.ExecuteReader();

                while (dataReaderObj.Read())
                {
                    totalIssuedBook = int.Parse(dataReaderObj.GetValue(0).ToString());
                }
                dataReaderObj.Close();

                connectionObj.Close();
            }
            return totalIssuedBook;
        }
        private bool sessionNumberExistance(int fSessionNumber)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                selectQuery = "SELECT *From bookDetails WHERE sessionNumber = '" + fSessionNumber + "' ";
                dataAdapterObj = new SqlDataAdapter(selectQuery, connectionObj);
                dataAdapterObj.Fill(tableObj);
                connectionObj.Close();

                if (tableObj.Rows.Count > 0)
                {
                    tableObj.Rows.Clear();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int sessionNumber = int.Parse(bookSessionNumber.Text);

                if (sessionNumberExistance(sessionNumber))
                {
                    if (bookIssuedStatus(sessionNumber))
                    {
                        registerationNumber = studentRegisterationNumber(sessionNumber);
                        storeDate = issueBookDate(sessionNumber);
                        date = DateTime.Parse(storeDate);
                        calaulateNumberOfDays = (int)(dateTimePicker.Value - date).TotalDays;

                        if (calaulateNumberOfDays < 0)
                        {
                            MessageBox.Show("Select the correct date ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            if (calaulateNumberOfDays > 14)
                            {
                                calaulateNumberOfDays = calaulateNumberOfDays - 14;
                                calculatedFine = calaulateNumberOfDays * 10;

                                if (registerationNumberExistance(registerationNumber))
                                {
                                    storeFine = existingFine(registerationNumber);
                                    calculatedFine = calculatedFine + storeFine;
                                    fineDataBase(calculatedFine, registerationNumber, 1);
                                }
                                else
                                {
                                    fineDataBase(calculatedFine, registerationNumber, 0);
                                }

                                MessageBox.Show(registerationNumber + " has to pay " + calculatedFine + " rupees fine", "Pay Fine", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            totalIssuedBook = totalBookIssuedByStudent(registerationNumber);
                            decrementBook(totalIssuedBook, registerationNumber);
                            updateBookDataBase("None", "0/0/0", "Available", sessionNumber);
                            MessageBox.Show("Book returned", "Return Book", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            gridViewDisplay();
                        }
                    }
                    else
                    {
                        MessageBox.Show("This book never issued" , "Error" , MessageBoxButtons.OK , MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("This book doesn't exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception obj)
            {
                MessageBox.Show(obj.Message);
            }
        }
    }
}
