using Contact.App.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.App.Services
{
    public class ContactRepository : IContactRepository

    {
        private string connectionString = "Data Source=DESKTOP-8T8G7TJ\\SQLSERVER2022;Initial Catalog=Contact_DB;Integrated Security=true";


        public bool Delete(int contactId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                string query;
                query = "delete from mycontacts where contact_id=@id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", contactId);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool Insert(string name, string family, string mobile, string email, int age, string address)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                string query;
                query = "insert into mycontacts(contactname,contactfamily,mobile,email,age,address) values(@contactname,@contactfamily,@mobile,@email,@age,@address)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@contactname", name);
                command.Parameters.AddWithValue("@contactfamily", family);
                command.Parameters.AddWithValue("@mobile", mobile);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@age", age);
                command.Parameters.AddWithValue("@address", address);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public DataTable Search(string parameter)
        {
            string query = "Select * From MyContacts where contactname like @parameter or contactfamily like @parameter";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            adapter.SelectCommand.Parameters.AddWithValue("@parameter", "%" + parameter + "%");
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }

        public DataTable SelectAll()
        {
            string query;
            query = "select * from MyContacts";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }

        public DataTable SelectById(int contactId)
        {
            string query = "Select * From MyContacts where Contact_ID=" + contactId;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            return data;
        }

        public bool Update(int contactId, string name, string family, string mobile, string email, int age, string address)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                string query = "Update MyContacts Set contactname=@Name,contactfamily=@Family,Mobile=@Mobile,Email=@Email,Age=@Age,Address=@Address Where Contact_ID=@ID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID", contactId);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Family", family);
                command.Parameters.AddWithValue("@Mobile", mobile);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Age", age);
                command.Parameters.AddWithValue("@Address", address);
                connection.Open();
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
