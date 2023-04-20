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
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void viewBookPicture(object sender, EventArgs e)
        {
            viewBookForm viewBookObject = new viewBookForm();
            viewBookObject.Show();
        }

        private void addStudent(object sender, EventArgs e)
        {
            addStudentForm addStudentObj = new addStudentForm();
            addStudentObj.Show();
        }

        private void addBook(object sender, EventArgs e)
        {
            addBookForm addbookObj = new addBookForm();
            addbookObj.Show();
        }

        private void deleteBook(object sender, EventArgs e)
        {
            deleteBookForm deleteBookObj = new deleteBookForm();
            deleteBookObj.Show();
        }

        private void updateBook(object sender, EventArgs e)
        {
            updateBookForm updateBookObj = new updateBookForm();
            updateBookObj.Show();
        }

        private void searchBook(object sender, EventArgs e)
        {
            searchBookForm searchBookObj = new searchBookForm();
            searchBookObj.Show();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            issueBookForm issueBookObj = new issueBookForm();
            issueBookObj.Show();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            returnBookForm returnBookObj = new returnBookForm();
            returnBookObj.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            PayFineForm fineObj = new PayFineForm();
            fineObj.Show();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginObj = new LoginForm();
            loginObj.Show();
        }
    }
}
