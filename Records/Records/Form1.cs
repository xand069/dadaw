using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Records
{
    public partial class Form1 : Form
    {
        private ClassRegistrationQuery clubRegistrationQuery;
        private int ID, Age, count;
        private string FirstName, LastName, MiddleName, Gender, Program;
        private long StudentID;
        public Form1()
        {
            InitializeComponent();
            clubRegistrationQuery = new ClassRegistrationQuery();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public int RegistrationID()
        {
            return count++;
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                ID = RegistrationID();
                StudentID = long.Parse(txtStudentNo.Text);
                FirstName = txtFirstName.Text;
                MiddleName = txtMiddleName.Text;
                LastName = txtLastName.Text;
                Age = int.Parse(txtAge.Text);
                Gender = cbGender.Text;
                Program = cbProgram.Text;

                if (clubRegistrationQuery.RegisterStudent(ID, StudentID, FirstName, MiddleName, LastName, Age, Gender, Program))
                {
                    MessageBox.Show("Student registered successfully!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshListofClubMembers();
        }
        public void RefreshListofClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            dataGridView1.DataSource = clubRegistrationQuery.bindingsource;
        }
    }
}
