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
    public partial class LoginForm : Form
    {
        //Private variables and objects are declared
        private SqlDataAdapter dataAdapterObj;
        private DataTable dataTableObj = new DataTable();
        private Validation validationObj = new Validation();
        private MenuForm menuObj = new MenuForm();
        string selectQuery = "\0" , userName = "\0" , password = "\0";

        public LoginForm()
        {
            InitializeComponent();
        }

        //This shows and hides the password
        private void showPasswordCheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            if (showPasswordCheckBox.Checked)
            {
                passwordTextBox.UseSystemPasswordChar = false;
            }
            else
            {
                passwordTextBox.UseSystemPasswordChar = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createAccountForm createObj = new createAccountForm();
            this.Hide();
            createObj.Show();
        }

        private bool login(string fUserName , string fPassword)
        {
            /* C# using statement makes sure that the allocated objects 
            * within the using statement is disposed once the using block 
            * code is executed.*/

            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {

                //SQL connection is open
                connectionObj.Open();

                //In this database SELECT query is written
                selectQuery = "SELECT *FROM LoginDataBase WHERE userName = '" + fUserName + "' and password = '" + fPassword + "'";

                /*The DataAdapter serves as a bridge between a DataSet 
                 * and a data source for retrieving and saving data*/
                dataAdapterObj = new SqlDataAdapter(selectQuery, connectionObj);

                /*The DataAdapter provides this bridge by mapping Fill, 
                 * which changes the data in the DataSet to match the data 
                 * in the data source, and Update, which changes the 
                 * data in the data source to match the data in the DataSet*/

                dataAdapterObj.Fill(dataTableObj);

                //SQL connection is closed
                connectionObj.Close();

                //If the required row os found than count value will incremented
                if (dataTableObj.Rows.Count == 1)
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
        //Login button code
        private void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                userName =  validationObj.encrypt(usernameTextBox.Text);
                password = validationObj.encrypt(passwordTextBox.Text);

                //IF any of the text box is empty than this condition will be executed
                if (validationObj.nonEmptyOrNullString(usernameTextBox.Text) || validationObj.nonEmptyOrNullString(passwordTextBox.Text))
                {
                    MessageBox.Show("None of the  field can be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //This is the main functionallity of the login button
                else
                {
                    if(login(userName , password))
                    {
                        MessageBox.Show("Login Successfull", "Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();
                        menuObj.Show();
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            /* If any error exist than this displays the error 
             * message. This also prevents the program from 
             * crashing */
            catch(Exception exceptionObj)
            {
                MessageBox.Show(exceptionObj.Message);
            }
        }
    }
}