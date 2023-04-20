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
using System.Configuration;

namespace PROJECT
{
    public partial class addStudentForm : Form
    {
        //Private variables and objects are declared

        private Validation validationObj = new Validation();
        private string insertQuery = "\0" , selectQuery = "\0";
        private string registerationNumber = "\0", name = "\0", department = "\0";
        private int session = 0;
        private SqlCommand commandObj;

        public addStudentForm()
        {
            InitializeComponent();
        }
        
        private void displayGridView()
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();

                selectQuery = "SELECT RegisterationNumber , Name , Session , Department FROM Student";

                using(SqlDataAdapter dataAdapterObj = new SqlDataAdapter(selectQuery , connectionObj))
                {
                    using(DataTable tableObj = new DataTable())
                    {
                        dataAdapterObj.Fill(tableObj);
                        dataGridView.DataSource = tableObj;
                    }
                }
                connectionObj.Close();
            }
        }
        private void insertStudent(string fRegisterationNumber , string fName , int fSession , string fDepartment , int fIssuedBook)
        {
            /* C# using statement makes sure that the allocated objects 
            * within the using statement is disposed once the using block 
            * code is executed.*/

            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                //SQL connection is open
                connectionObj.Open();

                //In this database INSERT query is written
                insertQuery = "INSERT INTO Student VALUES(@RegisterationNumber,@Name,@Session,@Department,@IssuedBook)";

                commandObj = new SqlCommand(insertQuery, connectionObj);

                //Insertion into the tables are done here
                commandObj.Parameters.AddWithValue("@Name", fName);
                commandObj.Parameters.AddWithValue("@RegisterationNumber", fRegisterationNumber);
                commandObj.Parameters.AddWithValue("@Session", fSession);
                commandObj.Parameters.AddWithValue("@Department", fDepartment);
                commandObj.Parameters.AddWithValue("@IssuedBook", fIssuedBook);
                commandObj.ExecuteNonQuery();

                MessageBox.Show("Data Saved", "Add Student", MessageBoxButtons.OK, MessageBoxIcon.Information);

                connectionObj.Close();
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // IF any of the text box is empty than this condition will be executed
                if (validationObj.nonEmptyOrNullString(nameTextBox.Text) || validationObj.nonEmptyOrNullString(idTextBox.Text) || validationObj.nonEmptyOrNullString(sessionTextBox.Text) || validationObj.nonEmptyOrNullString(departmentTextBox.Text))
                {
                    MessageBox.Show("None of the field can be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if(sessionTextBox.Text != "2019" && sessionTextBox.Text != "2020")
                {
                    MessageBox.Show("Select the correct session", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                   
                }
                else if(departmentTextBox.Text != "CS" && departmentTextBox.Text != "CE" && departmentTextBox.Text != "EE")
                {
                    MessageBox.Show("Select the correct department", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    name = nameTextBox.Text;
                    registerationNumber = int.Parse(sessionTextBox.Text) + "-UET-ShCET-LHR-" + departmentTextBox.Text + "-" + int.Parse(idTextBox.Text);
                    session = int.Parse(sessionTextBox.Text);
                    department = departmentTextBox.Text;

                    insertStudent(registerationNumber, name, session, department, 0);
                    displayGridView();
                }
            }
            catch(Exception obj)
            {
                MessageBox.Show(obj.Message);
            }
        }

        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {
            //If name text box is not string then it clears the text box and display the
            //invalid message on the screen
            
            if (!validationObj.stringOnly(nameTextBox.Text))
            {
                MessageBox.Show("Name contains characters only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nameTextBox.Clear();
            }
        }

        private void sessionTextBox_TextChanged(object sender, EventArgs e)
        {
            //If session text box is not integer then it clears the text box and display the
            //invalid message on the screen
            
            if (!validationObj.integerOnly(sessionTextBox.Text))
            {
                MessageBox.Show("Session contains digits only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //This button clears the all text box on the screen
            nameTextBox.Clear();
            idTextBox.Clear();
        }

        private void addStudentForm_Load(object sender, EventArgs e)
        {
            displayGridView();
        }

        private void idTextBox_TextChanged(object sender, EventArgs e)
        {
            //If id text box is not integer then it clears the text box and display the
            //invalid message on the screen
            
            if (idTextBox.Text == "" || idTextBox.Text == " ")
            {
                return;
            }
            else if (!validationObj.checkLength(idTextBox.Text, 3))
            {
                MessageBox.Show("Student id consists of two digits only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                idTextBox.Clear();
            }
            else if (!validationObj.integerOnly(idTextBox.Text))
            {
                MessageBox.Show("Id contains digits only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                idTextBox.Clear();
            }
            else if (int.Parse(idTextBox.Text) <= 0)
            {
                MessageBox.Show("Student id should be greater than zero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                idTextBox.Clear();
            }
        }
    }
}
