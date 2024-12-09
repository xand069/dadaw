using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Records
{
    internal class ConnectionClass
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        private DataTable dataTable;
        public SqlDataReader sqlReader;
        public BindingSource bindingsource;

        public ConnectionClass()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Xander\source\repos\Update\Update\bin\Debug\dbStudentss.mdf;Integrated Security=True;Connect Timeout=30";
            sqlConnect = new SqlConnection(connectionString);
            dataTable = new DataTable();
            bindingsource = new BindingSource();
        }
        public SqlDataReader IDList()
        {
            sqlConnect.Open();
            sqlCommand = new SqlCommand("Select StudentId from ClubMembers", sqlConnect);
            sqlReader = sqlCommand.ExecuteReader();
            return sqlReader;
        }
        public SqlDataReader ShowData(string query)
        {
            sqlConnect.Open();
            sqlCommand = new SqlCommand(query, sqlConnect);
            sqlReader = sqlCommand.ExecuteReader();
            return sqlReader;
        }
        public bool RegisterUpdate(long studentId, string firstName, string middleName,
    string lastName, int age, string gender, string program)
        {
            try
            {
                string query = "UPDATE ClubMembers SET FirstName = @FirstName, MiddleName = @MiddleName, " +
                               "LastName = @LastName, Age = @Age, Gender = @Gender, Program = @Program WHERE StudentId = @StudentId";
                sqlCommand = new SqlCommand(query, sqlConnect);

                sqlCommand.Parameters.AddWithValue("@StudentId", studentId);
                sqlCommand.Parameters.AddWithValue("@FirstName", firstName);
                sqlCommand.Parameters.AddWithValue("@MiddleName", middleName);
                sqlCommand.Parameters.AddWithValue("@LastName", lastName);
                sqlCommand.Parameters.AddWithValue("@Age", age);
                sqlCommand.Parameters.AddWithValue("@Gender", gender);
                sqlCommand.Parameters.AddWithValue("@Program", program);

                sqlConnect.Open();
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating student: {ex.Message}");
                return false;
            }
            finally
            {
                sqlConnect.Close();
            }
        }
    }
}
