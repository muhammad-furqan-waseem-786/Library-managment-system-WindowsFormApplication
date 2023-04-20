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
    public partial class issueBookForm : Form
    {
        private Validation validationObj = new Validation();
        private SqlCommand commandObj;
        private int totalIssuedBook = 0;
        private string selectQuery = "\0", updateQuery = "\0", date = "\0" , status = "\0";        
        private SqlDataReader dataReaderObj;
        private SqlDataAdapter dataAdapterObj;
        private DataTable tableObj = new DataTable();

        public issueBookForm()
        {
            InitializeComponent();
        }
        private bool bookExistanceInDataBase(int bookSessionNmber)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                selectQuery = "SELECT *FROM bookDetails WHERE sessionNumber = '"+ bookSessionNmber +"'";
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
        private bool studentExistanceInDataBase(string fRegisterationNumber)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();
                selectQuery = "SELECT *FROM Student WHERE RegisterationNumber = '" + fRegisterationNumber + "'";
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
        private bool bookAvailabilityStatus(int bookSessionNmber)
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

                if (status == "Issued" || status == "Missing")
                {
                    return false;
                }
                else
                {
                    return true;
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
        private void generateQrCode(string bookSessionNmber)
        {
            Zen.Barcode.CodeQrBarcodeDraw qrCodeObj = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            qrCodePictureBox.Image = qrCodeObj.Draw(bookSessionNumberTextBox.Text,1);
        }
        private void incrementBook(int issuedBookQuantity , string fRegisterationNumber)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                issuedBookQuantity = issuedBookQuantity + 1;                
                updateQuery = "UPDATE Student SET IssuedBook = '" + issuedBookQuantity + "' WHERE RegisterationNumber = '" + fRegisterationNumber + "'";
                commandObj = new SqlCommand(updateQuery, connectionObj);
                commandObj.ExecuteNonQuery();

                connectionObj.Close();
            }
        }
        private void updateBookDataBase(string fRegisterationNumber , string pDate , string status, int bookSessionNumber)
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
        
        private void issueBookButton(object sender, EventArgs e)
        {
            string registerationNumber = int.Parse(sessionTextBox.Text) + "-UET-ShCET-LHR-" + departmentTextBox.Text + "-" + int.Parse(idTextBox.Text);
            int bookSessionNumber = int.Parse(bookSessionNumberTextBox.Text);
            date = dateTimePicker.Value.ToString();

            try
            {
                if (validationObj.nonEmptyOrNullString(idTextBox.Text) || validationObj.nonEmptyOrNullString(sessionTextBox.Text) || validationObj.nonEmptyOrNullString(bookSessionNumberTextBox.Text) || validationObj.nonEmptyOrNullString(departmentTextBox.Text))
                {
                    MessageBox.Show("None of the field can be empty" , "Error" , MessageBoxButtons.OK , MessageBoxIcon.Error);
                }                
                else
                {
                    if(bookExistanceInDataBase(bookSessionNumber))
                    {
                        if(bookAvailabilityStatus(bookSessionNumber))
                        {
                            if(studentExistanceInDataBase(registerationNumber))
                            {
                                totalIssuedBook = totalBookIssuedByStudent(registerationNumber);

                                if(totalIssuedBook < 2)
                                {
                                    incrementBook(totalIssuedBook , registerationNumber);
                                    updateBookDataBase(registerationNumber , date , "Issued" , bookSessionNumber);
                                    generateQrCode(bookSessionNumberTextBox.Text);
                                    MessageBox.Show("Book Issued", "Book Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show(registerationNumber + " already issued two books" , "Error" , MessageBoxButtons.OK , MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show(registerationNumber + " student doesn't exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show(bookSessionNumber + " is already issued ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show(bookSessionNumber + " book doesn't exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception obj)
            {
                MessageBox.Show(obj.Message);
            }
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            if (idTextBox.Text == "" || idTextBox.Text == " ")
            {
                return;
            }
            else if (!validationObj.integerOnly(idTextBox.Text))
            {
                MessageBox.Show("Id contains digits only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                idTextBox.Clear();
            }
            else if(int.Parse(idTextBox.Text) <= 0)
            {
                MessageBox.Show("Id should be greater than zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                idTextBox.Clear();
            }
        }

        private void bookSessionNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            if (bookSessionNumberTextBox.Text == "")
            {
                return;
            }
            else if (!validationObj.integerOnly(bookSessionNumberTextBox.Text))
            {
                MessageBox.Show("Book Session contains digits only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bookSessionNumberTextBox.Clear();
            }
            else if (!validationObj.checkLength(bookSessionNumberTextBox.Text, 5))
            {
                MessageBox.Show("Book Session contains 4 digits only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bookSessionNumberTextBox.Clear();
            }
            else if(int.Parse(bookSessionNumberTextBox.Text) <= 0)
            {
                MessageBox.Show("Book Session should be greater than zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bookSessionNumberTextBox.Clear();
            }
        }
    }
}
