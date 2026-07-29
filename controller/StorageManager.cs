using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.SqlClient;

public class StorageManager
{
    private SqlConnection conn;
    public StorageManager(string connectionString)
    {
        try
        {
            conn = new SqlConnection(connectionString);
            conn.Open();
            Console.WriteLine("Connection Successful");
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Invalid connection string or connection already open.");
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"SQL Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected Error: {ex.Message}");
        }
    } 
     public string Login(string username, string password)
     {
        string role = "";
        try
        {
            using (SqlCommand cmd = new SqlCommand("SELECT Role FROM Users WHERE Username = @username AND Password = @password", conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    role = result.ToString();
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Login error: " + ex.Message);
        }
        return role;
    }

     public void closeconnections()
     {
        if (conn != null && conn.State == System.Data.ConnectionState.Open)
        {
            conn.Close();
            Console.WriteLine("Database connection closed.");
        }
     }
}