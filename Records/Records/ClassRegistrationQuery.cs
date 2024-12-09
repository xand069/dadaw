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
    internal class ClassRegistrationQuery
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataAdapter sqlAdapter;
        private DataTable dataTable;
        public SqlDataReader sqlReader;
        public BindingSource bindingsource;

        public ClassRegistrationQuery()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Xander\source\repos\Update\Update\bin\Debug\dbStudentss.mdf;Integrated Security=True;Connect Timeout=30";
            sqlConnect = new SqlConnection(connectionString);
            dataTable = new DataTable();
            bindingsource = new BindingSource();
        }
        public bool DisplayList()
        {
            string ViewClubMembers = "SELECT FirstName, MiddleName, LastName, Age, Gender, Program FROM ClubMembers WHERE StudentId = @StudentId";
            sqlAdapter = new SqlDataAdapter(ViewClubMembers, sqlConnect);
            dataTable.Clear();
            sqlAdapter.Fill(dataTable);
            bindingsource.DataSource = dataTable;
            return false;
        }
        public bool RegisterStudent(int ID, long StudentID, string FirstName, string MiddleName, string LastName, int Age, string Gender, string Program)
        {
            try
            {
                string query = "INSERT INTO ClubMembers (ID, StudentId, FirstName, MiddleName, LastName, Age, Gender, Program) " +
                               "VALUES (@ID, @StudentID, @FirstName, @MiddleName, @LastName, @Age, @Gender, @Program)";
                sqlCommand = new SqlCommand(query, sqlConnect);

                sqlCommand.Parameters.AddWithValue("@ID", ID);
                sqlCommand.Parameters.AddWithValue("@StudentID", StudentID);
                sqlCommand.Parameters.AddWithValue("@FirstName", FirstName);
                sqlCommand.Parameters.AddWithValue("@MiddleName", MiddleName);
                sqlCommand.Parameters.AddWithValue("@LastName", LastName);
                sqlCommand.Parameters.AddWithValue("@Age", Age);
                sqlCommand.Parameters.AddWithValue("@Gender", Gender);
                sqlCommand.Parameters.AddWithValue("@Program", Program);

                sqlConnect.Open();
                sqlCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }
            finally
            {
                sqlConnect.Close();
            }
        }
    }
}
