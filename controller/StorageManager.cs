using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.SqlClient;

public class StorageManager
{
    private SqlConnection? conn;

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

    // Helper method to ensure connection is ready before queries run
    private bool EnsureConnectionOpen()
    {
        if (conn == null)
        {
            Console.WriteLine("No database connection.");
            return false;
        }

        if (conn.State != ConnectionState.Open)
        {
            conn.Open();
        }

        return true;
    }

    public string Login(string username, string password)
    {
        string role = string.Empty;

        if (!EnsureConnectionOpen()) return role;

        try
        {
            using (SqlCommand cmd = new SqlCommand("SELECT Role FROM Users WHERE Username = @username AND Password = @password", conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    role = result.ToString() ?? string.Empty;
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

        if (!EnsureConnectionOpen()) return false;

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
        if (!EnsureConnectionOpen()) return false;

        try
        {
            string checkAvailabilityQuery = "SELECT COUNT(*) FROM Loans WHERE BookID = @bookId AND ReturnDate IS NULL";
            using (SqlCommand checkCmd = new SqlCommand(checkAvailabilityQuery, conn))
            {
                checkCmd.Parameters.AddWithValue("@bookId", bookId);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());
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
                return rowsAffected > 0;
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error borrowing book: " + ex.Message);
            return false;
        }
    }

    public bool ViewMyLoans(string username)
    {
        bool loansFound = false;

        if (!EnsureConnectionOpen()) return false;

        try
        {
            string query = "SELECT * FROM Loans WHERE Username = @username";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        loansFound = true;
                        Console.WriteLine("Book ID: " + reader["BookID"]);
                        Console.WriteLine("Loan Date: " + reader["LoanDate"]);

                        if (reader["ReturnDate"] == DBNull.Value)
                        {
                            Console.WriteLine("Status: Book needs to be returned");
                        }
                        else
                        {
                            Console.WriteLine("Return Date: " + reader["ReturnDate"]);
                            Console.WriteLine("Status: Returned");
                        }
                        Console.WriteLine("--------------------");
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error viewing loans: " + ex.Message);
        }
        return loansFound;
    }

    public bool AddBook(string title)
    {
        if (!EnsureConnectionOpen()) return false;

        try
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO Books (Title) VALUES (@title)", conn))
            {
                cmd.Parameters.AddWithValue("@title", title);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error adding book: " + ex.Message);
        }
        return false;
    }

    public bool ViewBooks()
    {
        bool booksFound = false;

        if (!EnsureConnectionOpen()) return false;

        try
        {
            string query = "SELECT * FROM Books";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        booksFound = true;
                        Console.WriteLine("Book ID: " + reader["BookID"]);
                        Console.WriteLine("Title: " + reader["Title"]);
                        Console.WriteLine("--------------------");
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error viewing books: " + ex.Message);
        }
        return booksFound;
    }

    public bool UpdateBook(int bookId, string newTitle)
    {
        if (!EnsureConnectionOpen()) return false;

        try
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE Books SET Title = @title WHERE BookID = @bookId", conn))
            {
                cmd.Parameters.AddWithValue("@title", newTitle);
                cmd.Parameters.AddWithValue("@bookId", bookId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error updating book: " + ex.Message);
        }
        return false;
    }

    public bool DeleteBook(int bookId)
    {
        if (!EnsureConnectionOpen()) return false;

        try
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Books WHERE BookID = @bookId", conn))
            {
                cmd.Parameters.AddWithValue("@bookId", bookId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error deleting book: " + ex.Message);
        }
        return false;
    }

    public bool RegisterMember(string username, string password)
    {
        return AddMember(username, password);
    }

    public bool ViewMembers()
    {
        bool membersFound = false;

        if (!EnsureConnectionOpen()) return false;

        try
        {
            string query = "SELECT Username, Role FROM Users WHERE Role = 'Member'";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        membersFound = true;
                        Console.WriteLine("Username: " + reader["Username"]);
                        Console.WriteLine("Role: " + reader["Role"]);
                        Console.WriteLine("--------------------");
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error viewing members: " + ex.Message);
        }
        return membersFound;
    }

    public bool ViewMember()
    {
        return ViewMembers();
    }

    public bool DeleteMember(string username)
    {
        if (!EnsureConnectionOpen()) return false;

        try
        {
            string query = "DELETE FROM Users WHERE Username = @username AND Role = 'Member'";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error deleting member: " + ex.Message);
        }
        return false;
    }

    public bool ProcessReturn(int loanId)
    {
        if (!EnsureConnectionOpen()) return false;

        try
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE Loans SET ReturnDate = GETDATE() WHERE LoanID = @loanId AND ReturnDate IS NULL", conn))
            {
                cmd.Parameters.AddWithValue("@loanId", loanId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error processing return: " + ex.Message);
        }
        return false;
    }

    public bool AddStaff(string username, string password)
    {
        if (!EnsureConnectionOpen()) return false;

        try
        {
            using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @username", conn))
            {
                checkCommand.Parameters.AddWithValue("@username", username);
                int userCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (userCount > 0)
                {
                    Console.WriteLine("This username already exists.");
                    return false;
                }
            }

            using (SqlCommand cmd = new SqlCommand("INSERT INTO Users (Username, Password, Role) VALUES (@username, @password, 'Staff')", conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error adding staff: " + ex.Message);
        }
        return false;
    }

    public bool ViewStaff()
    {
        bool staffFound = false;

        if (!EnsureConnectionOpen()) return false;

        try
        {
            using (SqlCommand cmd = new SqlCommand("SELECT Username, Role FROM Users WHERE Role = 'Staff'", conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        staffFound = true;
                        Console.WriteLine("Username: " + reader["Username"]);
                        Console.WriteLine("Role: " + reader["Role"]);
                        Console.WriteLine("--------------------");
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error viewing staff: " + ex.Message);
        }
        return staffFound;
    }

    public bool UpdateStaff(string username, string newPassword)
    {
        if (!EnsureConnectionOpen()) return false;

        try
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE Users SET Password = @password WHERE Username = @username AND Role = 'Staff'", conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", newPassword);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error updating staff: " + ex.Message);
        }
        return false;
    }

    public bool DeleteStaff(string username)
    {
        if (!EnsureConnectionOpen()) return false;

        try
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE Username = @username AND Role = 'Staff'", conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error deleting staff: " + ex.Message);
        }
        return false;
    }

    public bool AddMember(string username, string password)
    {
        if (!EnsureConnectionOpen()) return false;

        try
        {
            using (SqlCommand checkCommand = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username = @username", conn))
            {
                checkCommand.Parameters.AddWithValue("@username", username);
                int userCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (userCount > 0)
                {
                    Console.WriteLine("This username already exists.");
                    return false;
                }
            }

            using (SqlCommand cmd = new SqlCommand("INSERT INTO Users (Username, Password, Role) VALUES (@username, @password, 'Member')", conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error adding member: " + ex.Message);
        }
        return false;
    }

    public bool UpdateMember(string username, string newPassword)
    {
        if (!EnsureConnectionOpen()) return false;

        try
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE Users SET Password = @password WHERE Username = @username AND Role = 'Member'", conn))
            {
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", newPassword);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error updating member: " + ex.Message);
        }
        return false;
    }

    public void CloseConnections()
    {
        if (conn != null && conn.State == ConnectionState.Open)
        {
            conn.Close();
            Console.WriteLine("Database connection closed.");
        }
    }
}