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

    public bool SearchBook(string title)
    {
        bool bookFound = false;

        try
        {
            string query = "SELECT * FROM Books WHERE Title LIKE @title";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@title", "%" + title + "%");

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bookFound = true;
                        Console.WriteLine("Book ID: " + reader["BookID"]);
                        Console.WriteLine("Title: " + reader["Title"]);
                        Console.WriteLine("--------------------");
                    }
                }


            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error searching for book: " + ex.Message);
        }
        return bookFound;
    }

    public bool BorrowBook(string username, int bookId)
    {
        try
        {
            string checkAvailabilityQuery = "SELECT COUNT(*) FROM Loans WHERE BookID = @bookId AND ReturnDate IS NULL";
            using (SqlCommand checkCmd = new SqlCommand(checkAvailabilityQuery, conn))
            {
                checkCmd.Parameters.AddWithValue("@bookId", bookId);
                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                {
                    Console.WriteLine("Book is currently unavailable.");
                    return false;
                }
            }
            string insertLoanQuery = "INSERT INTO Loans (Username, BookID, LoanDate) VALUES (@username, @bookId, @loanDate)";
            using (SqlCommand insertCmd = new SqlCommand(insertLoanQuery, conn))
            {
                insertCmd.Parameters.AddWithValue("@username", username);
                insertCmd.Parameters.AddWithValue("@bookId", bookId);
                insertCmd.Parameters.AddWithValue("@loanDate", DateTime.Now);
                int rowsAffected = insertCmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Book borrowed successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Failed to borrow the book.");
                    return false;
                }
            }

        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error borrowing book: " + ex.Message);
            return false;
        }
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