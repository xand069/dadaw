using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Records
{
    public partial class Records : Form
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        private DataTable datatable;
        public SqlDataReader sqlReader;
        public BindingSource bindingsource;
        private ConnectionClass connectionClass;
        public Records()
        {
            InitializeComponent();
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Xander\source\repos\Update\Update\bin\Debug\dbStudentss.mdf;Integrated Security=True;Connect Timeout=30";
            sqlConnect = new SqlConnection(connectionString);
            connectionClass = new ConnectionClass();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cbStID.Text))
                {
                    MessageBox.Show("Please select a Student ID.");
                    return;
                }

                long studentId = long.Parse(cbStID.Text);
                string firstName = txtFirstName.Text;
                string middleName = txtMiddleName.Text;
                string lastName = txtLastName.Text;
                int age = int.Parse(txtAge.Text);
                string gender = cbGender.Text;
                string program = cbProgram.Text;

                if (connectionClass.RegisterUpdate(studentId, firstName, middleName, lastName, age, gender, program))
                {
                    MessageBox.Show("Student information updated successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void Records_Load(object sender, EventArgs e)
        {
            try
            {
                sqlConnect.Open();
                string query = "SELECT StudentId FROM ClubMembers";
                SqlCommand cmd = new SqlCommand(query, sqlConnect);
                sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    cbStID.Items.Add(sqlReader[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
            finally
            {
                sqlReader?.Close();
                sqlConnect.Close();
            }
        }

        private void cbStID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sqlConnect.Open();
                string query = "SELECT FirstName, MiddleName, LastName, Age, Gender, Program FROM ClubMembers WHERE StudentId = @StudentId";
                sqlCommand = new SqlCommand(query, sqlConnect);
                sqlCommand.Parameters.AddWithValue("@StudentId", cbStID.Text);

                sqlReader = sqlCommand.ExecuteReader();
                if (sqlReader.Read())
                {
                    txtLastName.Text = sqlReader["LastName"].ToString();
                    txtFirstName.Text = sqlReader["FirstName"].ToString();
                    txtMiddleName.Text = sqlReader["MiddleName"].ToString();
                    txtAge.Text = sqlReader["Age"].ToString();
                    cbGender.Text = sqlReader["Gender"].ToString();
                    cbProgram.Text = sqlReader["Program"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                sqlReader?.Close();
                sqlConnect.Close();
            }
        }
    }
}
