
namespace PROJECT
{
    partial class issueBookForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.idTextBox = new System.Windows.Forms.TextBox();
            this.bookSessionNumberTextBox = new System.Windows.Forms.TextBox();
            this.label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.sessionTextBox = new System.Windows.Forms.ComboBox();
            this.departmentTextBox = new System.Windows.Forms.ComboBox();
            this.qrCodePictureBox = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.qrCodePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(483, 408);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 53);
            this.button1.TabIndex = 0;
            this.button1.Text = "Issue Book";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.issueBookButton);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(237, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Student id";
            // 
            // idTextBox
            // 
            this.idTextBox.Location = new System.Drawing.Point(406, 52);
            this.idTextBox.Name = "idTextBox";
            this.idTextBox.Size = new System.Drawing.Size(202, 35);
            this.idTextBox.TabIndex = 2;
            this.idTextBox.TextChanged += new System.EventHandler(this.idTextBox_TextChanged);
            // 
            // bookSessionNumberTextBox
            // 
            this.bookSessionNumberTextBox.Location = new System.Drawing.Point(406, 103);
            this.bookSessionNumberTextBox.Name = "bookSessionNumberTextBox";
            this.bookSessionNumberTextBox.Size = new System.Drawing.Size(202, 35);
            this.bookSessionNumberTextBox.TabIndex = 3;
            this.bookSessionNumberTextBox.TextChanged += new System.EventHandler(this.bookSessionNumberTextBox_TextChanged);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.ForeColor = System.Drawing.Color.White;
            this.label.Location = new System.Drawing.Point(237, 106);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(255, 29);
            this.label.TabIndex = 4;
            this.label.Text = "Book Session Number";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(237, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 29);
            this.label3.TabIndex = 6;
            this.label3.Text = "Session";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(237, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(138, 29);
            this.label4.TabIndex = 7;
            this.label4.Text = "Department";
            // 
            // sessionTextBox
            // 
            this.sessionTextBox.FormattingEnabled = true;
            this.sessionTextBox.Items.AddRange(new object[] {
            "2019",
            "2020"});
            this.sessionTextBox.Location = new System.Drawing.Point(406, 155);
            this.sessionTextBox.Name = "sessionTextBox";
            this.sessionTextBox.Size = new System.Drawing.Size(202, 37);
            this.sessionTextBox.TabIndex = 8;
            this.sessionTextBox.Text = "2019";
            // 
            // departmentTextBox
            // 
            this.departmentTextBox.FormattingEnabled = true;
            this.departmentTextBox.Items.AddRange(new object[] {
            "CS",
            "EE",
            "CE"});
            this.departmentTextBox.Location = new System.Drawing.Point(406, 207);
            this.departmentTextBox.Name = "departmentTextBox";
            this.departmentTextBox.Size = new System.Drawing.Size(202, 37);
            this.departmentTextBox.TabIndex = 9;
            this.departmentTextBox.Text = "CS";
            // 
            // qrCodePictureBox
            // 
            this.qrCodePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.qrCodePictureBox.Location = new System.Drawing.Point(794, 58);
            this.qrCodePictureBox.Name = "qrCodePictureBox";
            this.qrCodePictureBox.Size = new System.Drawing.Size(186, 186);
            this.qrCodePictureBox.TabIndex = 10;
            this.qrCodePictureBox.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(237, 287);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 29);
            this.label5.TabIndex = 12;
            this.label5.Text = "Issue Date";
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.CustomFormat = "";
            this.dateTimePicker.Location = new System.Drawing.Point(406, 287);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(397, 35);
            this.dateTimePicker.TabIndex = 14;
            // 
            // issueBookForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DodgerBlue;
            this.ClientSize = new System.Drawing.Size(1244, 653);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.qrCodePictureBox);
            this.Controls.Add(this.departmentTextBox);
            this.Controls.Add(this.sessionTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label);
            this.Controls.Add(this.bookSessionNumberTextBox);
            this.Controls.Add(this.idTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "issueBookForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Issue Book";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.qrCodePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox idTextBox;
        private System.Windows.Forms.TextBox bookSessionNumberTextBox;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox sessionTextBox;
        private System.Windows.Forms.ComboBox departmentTextBox;
        private System.Windows.Forms.PictureBox qrCodePictureBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
    }
}