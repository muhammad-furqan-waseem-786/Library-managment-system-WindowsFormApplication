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
    
    public partial class createAccountForm : Form
    {
        private SqlCommand commandObj;
        private string insertQuery = "\0", selectQuery = "\0";
        private SqlDataAdapter dataAdapterObj;
        private DataTable dataTableObj = new DataTable();
        private string userName = "\0", password = "\0", confirmPassword = "\0";
        private LoginForm loginObj = new LoginForm();
        private Validation validationObj = new Validation();

        public createAccountForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            loginObj.Show();
        }

        private void createAccount(string fuserName , string fPassword)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            {
                connectionObj.Open();
                insertQuery = "INSERT INTO LoginDataBase VALUES(@userName , @password)";

                commandObj = new SqlCommand(insertQuery, connectionObj);

                commandObj.Parameters.AddWithValue("@userName", fuserName);
                commandObj.Parameters.AddWithValue("@password" , fPassword);
                commandObj.ExecuteNonQuery();
                connectionObj.Close();
            }
        }
        private bool accountExistance(string fUserName , string fPassword)
        {
            using (SqlConnection connectionObj = new SqlConnection(validationObj.connectionString()))
            { //SQL connection is open
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
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                userName = validationObj.encrypt(userNameTextBox.Text);
                password = validationObj.encrypt(passwordTextBox.Text);
                confirmPassword = validationObj.encrypt(confirmPasswordTextBox.Text);

                if(validationObj.nonEmptyOrNullString(userNameTextBox.Text) || validationObj.nonEmptyOrNullString(passwordTextBox.Text) || validationObj.nonEmptyOrNullString(confirmPasswordTextBox.Text))
                {
                    MessageBox.Show("None of the field can be empty ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (password == confirmPassword)
                {
                    if(accountExistance(userName , password))
                    {
                        MessageBox.Show("Account and password already exist", "Data exist", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        createAccount(userName,password);
                        MessageBox.Show("Account created", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Password and confirm password should be same", "Invalid password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception obj)
            {
                MessageBox.Show(obj.Message);
            }
        }
    }
}
