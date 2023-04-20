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
    public partial class PayFineForm : Form
    {
        private Validation validationObj = new Validation();
        private string selectQuery = "\0" , deleteQuery = "\0";
        private SqlDataAdapter dataAdapterObj;
        private DataTable dataTableObj = new DataTable();
        private SqlCommand commandObj;
        private string registerationNumber = "\0";

        public PayFineForm()
        {
            InitializeComponent();
        }

        private void gridViewDisplay()
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                selectQuery = "SELECT RegisterationNumber,Fine FROM FineTable";

                using (SqlDataAdapter fDataAdapterObj = new SqlDataAdapter(selectQuery, connectionObj))
                {
                    using (DataTable fDataTableObj = new DataTable())
                    {
                        fDataAdapterObj.Fill(fDataTableObj);
                        dataGridView.DataSource = fDataTableObj;

                        dataGridView.Columns[0].HeaderText = "Student Reisteration Number";
                        dataGridView.Columns[1].HeaderText = "Total Fine";

                        dataGridView.Sort(dataGridView.Columns[0], ListSortDirection.Ascending);
                    }
                }
                connectionObj.Close();
            }
        }
        private bool registerationNumberExist(string fRegisteratonNumber)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                selectQuery = "SELECT  *FROM FineTable WHERE RegisterationNumber = '" + fRegisteratonNumber + "'";
                dataAdapterObj = new SqlDataAdapter(selectQuery, connectionObj);
                dataAdapterObj.Fill(dataTableObj);

                connectionObj.Close();

                if(dataTableObj.Rows.Count == 1)
                {
                    dataTableObj.Rows.Clear();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private void deleteRecord(string fRegiseationNumber)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                deleteQuery = "DELETE FineTable WHERE RegisterationNumber = '"+ fRegiseationNumber +"'";
                commandObj = new SqlCommand(deleteQuery, connectionObj);
                commandObj.ExecuteNonQuery();

                connectionObj.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                registerationNumber = sessionTextBox.Text + "-UET-ShCET-LHR-" + departmentTextBox.Text + "-" + int.Parse(idTextBox.Text);

                if (validationObj.nonEmptyOrNullString(idTextBox.Text) || validationObj.nonEmptyOrNullString(sessionTextBox.Text) || validationObj.nonEmptyOrNullString(departmentTextBox.Text))
                {
                    MessageBox.Show("None of the field can be empty", "Error" , MessageBoxButtons.OK , MessageBoxIcon.Error);
                }
                else if(sessionTextBox.Text != "2019" && sessionTextBox.Text != "2020")
                {
                    MessageBox.Show("Select the correct session", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(departmentTextBox.Text != "CS" && departmentTextBox.Text!= "EE" && departmentTextBox.Text != "CE")
                {
                    MessageBox.Show("Select the correct department", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (registerationNumberExist(registerationNumber))
                {
                    deleteRecord(registerationNumber);
                    MessageBox.Show(registerationNumber + " has payed fine" , "Fine" , MessageBoxButtons.OK , MessageBoxIcon.Information);
                    gridViewDisplay();
                }
                else
                {
                    MessageBox.Show(registerationNumber + " has no fine to pay" , "Error" , MessageBoxButtons.OK , MessageBoxIcon.Error);
                }
            }
            catch(Exception obj)
            {
                MessageBox.Show(obj.Message);
            }
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (idTextBox.Text == " " || idTextBox.Text == "")
                {
                    return;
                }
                else if (!validationObj.integerOnly(idTextBox.Text))
                {
                    MessageBox.Show("Id contains digits only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    idTextBox.Clear();
                }
            }
            catch (Exception obj)
            {
                MessageBox.Show(obj.Message);
            }
        }

        private void PayFineForm_Load(object sender, EventArgs e)
        {
            gridViewDisplay();
        }
    }
    
}
