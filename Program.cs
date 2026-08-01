using LibraryManagement.controller;
using LibraryManagement.SqlClient;
using LibraryManagement.View;
using System.Diagnostics;

namespace LibraryManagement
{
    internal class Program
    {
        private static StorageManager storageManager = null!;
        private static ConsoleView myView = null!;
        private static string loggedInUsername = string.Empty;

        static void Main(string[] args)
        {

            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagement;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;";

            storageManager = new StorageManager(connectionString);
            myView = new ConsoleView();
            bool exit = false;
            while (!exit)
            {
                myView.DisplayMainMenu();
                string choice = myView.GetInput();

                switch (choice)
                {
                    case "1":
                        LoginMenu();
                        break;

                    case "3":
                        exit = true;
                        break;

                    default:
                        myView.DisplayMessage("Invalid choice. Please try again.");
                        break;

                }

            }

        }

        private static void LoginMenu()
        {
            bool back = false;
            while (!back)
            {
                int option = myView.LoginMenu();
                switch (option)
                {
                    case 1:
                        Login("Member");
                        break;
                    case 2:
                        Login("Staff");
                        break;
                    case 3:
                        Login("Admin");
                        break;
                    case 4:
                        back = true;
                        break;
                    default:
                        myView.DisplayMessage("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private static void Login(string role)
        {
            string username;

            myView.DisplayMessage("Enter username: ");
            username = myView.GetInput();

            // Username Validation
            while (string.IsNullOrWhiteSpace(username) || username.Length < 4 || username.Length > 20 || username.Contains(" "))
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    myView.DisplayMessage("Username cannot be empty.");
                }
                else if (username.Length < 4 || username.Length > 20)
                {
                    myView.DisplayMessage("Username must be between 4 and 20 characters.");
                }
                else if (username.Contains(" "))
                {
                    myView.DisplayMessage("Username cannot contain spaces.");
                }
                myView.DisplayMessage("Enter username: ");
                username = myView.GetInput();
            }
            myView.DisplayMessage("Enter password: ");
            string password = myView.GetInput();

            while (string.IsNullOrWhiteSpace(password) ||
           password.Length < 6)
            {
                if (string.IsNullOrWhiteSpace(password))
                {
                    myView.DisplayMessage("Password cannot be empty.");
                }
                else
                {
                    myView.DisplayMessage("Password must be at least 6 characters.");
                }
                myView.DisplayMessage("Enter password: ");
                password = myView.GetInput();
            }

            string userRole = storageManager.Login(username, password);

            if (userRole == role)
            {
                loggedInUsername = username;

                myView.DisplayMessage("Login successful!");

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();

                switch (role)
                {
                    case "Member":
                        MemberMenu();
                        break;

                    case "Staff":
                        StaffMenu();
                        break;

                    case "Admin":
                        AdminMenu();
                        break;
                }
            }
            else
            {
                myView.DisplayMessage("Invalid username, password or selected role.");

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        private static void MemberMenu()
        {
            bool logout = false;

            Console.WriteLine("========= MEMBER MENU =========");

            while (!logout)
            {
                int option = myView.MemberMenu();

                switch (option)
                {
                    case 1:
                        SearchBooks();
                        break;

                    case 2:
                        BorrowBook();
                        break;

                    case 3:
                        ViewMyLoans();
                        break;

                    case 4:
                        myView.DisplayMessage("Logging out " + loggedInUsername + "...");
                        loggedInUsername = "";
                        logout = true;

                        myView.DisplayMessage("You have successfully logged out.");

                        Console.WriteLine("Press any key to return to the main menu...");
                        Console.ReadKey();
                        break;

                    default:
                        myView.DisplayMessage("Invalid choice. Please try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void SearchBooks()
        {
            Console.Clear();

            myView.DisplayMessage("===== Search Books =====");
            myView.DisplayMessage("Enter book title: ");

            string title = myView.GetInput();

            while (string.IsNullOrWhiteSpace(title))
            {
                myView.DisplayMessage("Title cannot be empty.");
                myView.DisplayMessage("Enter a book title: ");
                title = myView.GetInput();
            }

            bool bookFound = storageManager.SearchBook(title);

            if (!bookFound)
            {
                myView.DisplayMessage(
                    "No books were found.");
            }

            Console.WriteLine();
            myView.DisplayMessage("Press any key to return...");

            Console.ReadKey();
        }

        private static void BorrowBook()
        {
            Console.Clear();

            myView.DisplayMessage("===== BORROW BOOK =====");
            myView.DisplayMessage("Enter the Book ID:");

            int bookId = myView.GetIntInput();

            while (bookId <= 0)
            {
                myView.DisplayMessage("Book ID must be greater than 0.");
                myView.DisplayMessage("Enter the Book ID:");

                bookId = myView.GetIntInput();
            }
            bool borrowed = storageManager.BorrowBook(loggedInUsername, bookId);

            if (borrowed)
            {
                myView.DisplayMessage("Book borrowed successfully.");
            }
            else
            {
                myView.DisplayMessage("The book could not be borrowed.");

                myView.DisplayMessage("The Book ID may be incorrect or the book may not be available.");
            }
            Console.WriteLine();
            myView.DisplayMessage("Press any key to return...");

            Console.ReadKey();
        }
        private static void ViewMyLoans()
        {
            Console.Clear();
            myView.DisplayMessage("===== MY LOANS =====");
            myView.DisplayMessage("Current and returned books:");

            bool hasLoans = storageManager.ViewMyLoans(loggedInUsername);

            if (!hasLoans)
            {
                myView.DisplayMessage("You have no current loans.");
            }
            Console.WriteLine();
            myView.DisplayMessage("Press any key to return...");
            Console.ReadKey();
        }

        private static void StaffMenu()
        {
            bool logout = false;

            Console.WriteLine("========= STAFF MENU =========");

            while (!logout)
            {
                int option = myView.StaffMenu();
                switch (option)
                {
                    case 1:
                        myView.DisplayMessage("Add books");
                        break;
                    case 2:
                        myView.DisplayMessage("View books");
                        break;
                    case 3:
                        myView.DisplayMessage("Update books");
                        break;
                    case 4:
                        myView.DisplayMessage("Delete books");
                        break;
                    case 5:
                        myView.DisplayMessage("Register member");
                        break;
                    case 6:
                        myView.DisplayMessage("View member");
                        break;
                    case 7:
                        myView.DisplayMessage("Delete member");
                        break;
                    case 8:
                        myView.DisplayMessage("Process returns");
                        break;
                    case 9:
                        logout = true;
                        myView.DisplayMessage("Logging out...");
                        break;

                }
            }
        }

        private static void AddBook()
        {
            Console.Clear();
            myView.DisplayMessage("===== ADD BOOK =====");
            myView.DisplayMessage("Enter book title:");

            string title = myView.GetInput();

            while (string.IsNullOrWhiteSpace(title))
            {
                myView.DisplayMessage("Book title cannot be empty.");
                myView.DisplayMessage("Enter book title:");

                title = myView.GetInput();
            }

            bool added = storageManager.AddBook(title);
            if (added)
            {
                myView.DisplayMessage(
                    "Book added successfully.");
            }
            else
            {
                myView.DisplayMessage(
                    "Book could not be added.");
            }
            Console.WriteLine();
            myView.DisplayMessage(
                "Press any key to return...");

            Console.ReadKey();
        }

        private static void ViewBooks()
        {
            Console.Clear();

            myView.DisplayMessage("===== VIEW BOOKS =====");

            bool booksFound = storageManager.ViewBooks();

            if (!booksFound)
            {
                myView.DisplayMessage("No books were found.");
            }

            Console.WriteLine();

            myView.DisplayMessage("Press any key to return...");

            Console.ReadKey();
        }

        private static void UpdateBook()
        {
            Console.Clear();

            myView.DisplayMessage("===== UPDATE BOOK =====");

            myView.DisplayMessage("Enter Book ID:");

            int bookId = myView.GetIntInput();

            while (bookId <= 0)
            {
                myView.DisplayMessage("Book ID must be greater than 0.");

                myView.DisplayMessage("Enter Book ID:");

                bookId = myView.GetIntInput();
            }

            myView.DisplayMessage("Enter the new book title:");

            string newTitle = myView.GetInput();

            while (string.IsNullOrWhiteSpace(newTitle))
            {
                myView.DisplayMessage("Book title cannot be empty.");

                myView.DisplayMessage("Enter the new book title:");

                newTitle = myView.GetInput();
            }

            bool updated = storageManager.UpdateBook(bookId, newTitle);

            if (updated)
            {
                myView.DisplayMessage("Book updated successfully.");
            }
            else
            {
                myView.DisplayMessage("Book was not found.");
            }

            Console.WriteLine();
            myView.DisplayMessage("Press any key to return...");

            Console.ReadKey();
        }

        private static void DeleteBook()
        {
            Console.Clear();

            myView.DisplayMessage("===== DELETE BOOK =====");
            myView.DisplayMessage("Enter Book ID:");

            int bookId = myView.GetIntInput();

            while (bookId <= 0)
            {
                myView.DisplayMessage("Book ID must be greater than 0.");
                myView.DisplayMessage("Enter Book ID:");
                bookId = myView.GetIntInput();
            }
            bool deleted = storageManager.DeleteBook(bookId);
            if (deleted)
            {
                myView.DisplayMessage("Book deleted successfully.");
            }
            else
            {
                myView.DisplayMessage("Book was not found.");
            }
            Console.WriteLine();

            myView.DisplayMessage("Press any key to return...");

            Console.ReadKey();
        }

        private static void RegisterMember()
        {
            Console.Clear();
            myView.DisplayMessage("===== REGISTER MEMBER =====");
            myView.DisplayMessage("Enter member username:");

            string username = myView.GetInput();

            while (string.IsNullOrWhiteSpace(username) || username.Length < 4 || username.Length > 20 || username.Contains(" "))
            {
                if (string.IsNullOrWhiteSpace(username))
                {
                    myView.DisplayMessage("Username cannot be empty.");
                }
                else if (username.Length < 4 || username.Length > 20)
                {
                    myView.DisplayMessage("Username must be between 4 and 20 characters.");
                }
                else if (username.Contains(" "))
                {
                    myView.DisplayMessage("Username cannot contain spaces.");
                }
                myView.DisplayMessage("Enter member username:");
                username = myView.GetInput();
            }

            myView.DisplayMessage("Enter member password:");
            string password = myView.GetInput();

            while (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                myView.DisplayMessage("Password must be at least 6 characters.");

                myView.DisplayMessage("Enter member password:");

                password = myView.GetInput();
            }

            bool registered = storageManager.RegisterMember(username, password);
            if (registered)
            {
                myView.DisplayMessage("Member registered successfully.");
            }
            else
            {
                myView.DisplayMessage("Member could not be registered.");
            }
            Console.WriteLine();
            myView.DisplayMessage("Press any key to return...");
            Console.ReadKey();
        }

        private static void ViewMembers()
        {
            Console.Clear();

            myView.DisplayMessage("===== VIEW MEMBERS =====");

            bool membersFound = storageManager.ViewMember();

            if (!membersFound)
            {
                myView.DisplayMessage("No members were found.");
            }

            Console.WriteLine();

            myView.DisplayMessage("Press any key to return...");

            Console.ReadKey();
        }

        private static void DeleteMember()
        {
            Console.Clear();

            myView.DisplayMessage("===== DELETE MEMBER =====");

            myView.DisplayMessage("Enter the member username:");

            string username = myView.GetInput();

            while (string.IsNullOrWhiteSpace(username))
            {
                myView.DisplayMessage("Username cannot be empty.");

                myView.DisplayMessage("Enter the member username:");

                username = myView.GetInput();
            }

            bool deleted = storageManager.DeleteMember(username);

            if (deleted)
            {
                myView.DisplayMessage("Member deleted successfully.");
            }
            else
            {
                myView.DisplayMessage("Member was not found.");
            }

            Console.WriteLine();

            myView.DisplayMessage("Press any key to return...");

            Console.ReadKey();
        }

        private static void ProcessReturns()
        {
            Console.Clear();
            myView.DisplayMessage("===== PROCESS RETURNS =====");
            myView.DisplayMessage("Enter Loan ID:");
            int loanId = myView.GetIntInput();
            while (loanId <= 0)
            {
                myView.DisplayMessage("Loan ID must be greater than 0.");
                myView.DisplayMessage("Enter Loan ID:");
                loanId = myView.GetIntInput();
            }
            bool returned = storageManager.ProcessReturn(loanId);
            if (returned)
            {
                myView.DisplayMessage("Book return processed successfully.");
            }
            else
            {
                myView.DisplayMessage("Return could not be processed.");
                myView.DisplayMessage("The Loan ID may be incorrect or the book may already be returned.");
            }
            Console.WriteLine();
            myView.DisplayMessage("Press any key to return...");

            Console.ReadKey();
        }

        private static void AdminMenu()
        {
            bool logout = false;

            Console.Clear();
            Console.WriteLine("========= ADMIN MENU =========");
            while (!logout)
            {
                int option = myView.AdminMenu();
                switch (option)
                {
                    case 1:
                        myView.DisplayMessage("Add staff");
                        break;
                    case 2:
                        myView.DisplayMessage("View staff");
                        break;
                    case 3:
                        myView.DisplayMessage("Update staff");
                        break;
                    case 4:
                        myView.DisplayMessage("Delete staff");
                        break;
                    case 5:
                        myView.DisplayMessage("Add Member");
                        break;
                    case 6:
                        myView.DisplayMessage("View Member");
                        break;
                    case 7:
                        myView.DisplayMessage("Update Member");
                        break;
                    case 8:
                        myView.DisplayMessage("Delete Member");
                        break;
                    case 9:
                        myView.DisplayMessage("Logging out " + loggedInUsername + "...");

                        loggedInUsername = "";

                        logout = true;

                        myView.DisplayMessage("You have successfully logged out.");

                        Console.WriteLine("Press any key to return to the main menu...");

                        Console.ReadKey();
                        break;

                    default:
                        myView.DisplayMessage("Invalid choice. Please try again.");

                        Console.ReadKey();
                        break;


                }
            }
        }

        private static void AddStaff()
        {
            Console.Clear();

            myView.DisplayMessage("===== ADD STAFF =====");

            myView.DisplayMessage("Enter staff username:");
            string username = myView.GetInput();

            while (string.IsNullOrWhiteSpace(username))
            {
                myView.DisplayMessage("Username cannot be empty.");

                myView.DisplayMessage("Enter staff username:");

                username = myView.GetInput();
            }

            myView.DisplayMessage("Enter staff password:");
            string password = myView.GetInput();

            while (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                myView.DisplayMessage("Password must be at least 6 characters.");

                myView.DisplayMessage("Enter staff password:");

                password = myView.GetInput();
            }

            bool added = storageManager.AddStaff(username, password);

            if (added)
            {
                myView.DisplayMessage("Staff member added successfully.");
            }
            else
            {
                myView.DisplayMessage("Staff member could not be added.");
            }

            Console.WriteLine();
            myView.DisplayMessage("Press any key to return...");

            Console.ReadKey();
        }

        private static void ViewStaff()
        {
            Console.Clear();

            myView.DisplayMessage("===== VIEW STAFF =====");

            bool staffFound =
                storageManager.ViewStaff();

            if (!staffFound)
            {
                myView.DisplayMessage("No staff members were found.");
            }

            Console.WriteLine();
            myView.DisplayMessage("Press any key to return...");

            Console.ReadKey();
        }

        private static void UpdateStaff()
        {
            Console.Clear();

            myView.DisplayMessage("===== UPDATE STAFF =====");

            myView.DisplayMessage("Enter staff username:");

            string username = myView.GetInput();

            myView.DisplayMessage("Enter new password:");

            string newPassword = myView.GetInput();

            bool updated = storageManager.UpdateStaff(username, newPassword);

            if (updated)
            {
                myView.DisplayMessage("Staff details updated successfully.");
            }
            else
            {
                myView.DisplayMessage("Staff member was not found.");
            }

            Console.WriteLine();
            myView.DisplayMessage("Press any key to return...");

            Console.ReadKey();
        }

        private static void DeleteStaff()
        {
            Console.Clear();
            myView.DisplayMessage("===== DELETE STAFF =====");
            myView.DisplayMessage("Enter staff username:");
            string username = myView.GetInput();
            bool deleted = storageManager.DeleteStaff(username);
            if (deleted)
            {
                myView.DisplayMessage("Staff member deleted successfully.");
            }
            else
            {
                myView.DisplayMessage("Staff member was not found.");
            }
            Console.WriteLine();
            myView.DisplayMessage("Press any key to return...");
            Console.ReadKey();
        }

        private static void AddMember()
        {
            Console.Clear();

            myView.DisplayMessage("===== ADD MEMBER =====");

            myView.DisplayMessage("Enter member username:");

            string username = myView.GetInput();

            myView.DisplayMessage("Enter member password:");

            string password = myView.GetInput();

            bool added = storageManager.AddMember(username, password);

            if (added)
            {
                myView.DisplayMessage("Member added successfully.");
            }
            else
            {
                myView.DisplayMessage("Member could not be added.");
            }

            Console.WriteLine();
            myView.DisplayMessage("Press any key to return...");

            Console.ReadKey();
        }

        private static void ViewMember()
        {
            Console.Clear();
            myView.DisplayMessage("===== VIEW MEMBER =====");

            bool memberFound = storageManager.ViewMembers();

            if (!memberFound)
            {
                myView.DisplayMessage("No members were found.");
            }
            Console.WriteLine();
            myView.DisplayMessage("Press any key to return...");

            Console.ReadKey();
        }

        private static void UpdateMember()
        {
            Console.Clear();

            myView.DisplayMessage("===== UPDATE MEMBER =====");

            myView.DisplayMessage("Enter member username:");

            string username = myView.GetInput();

            myView.DisplayMessage("Enter new password:");

            string newPassword = myView.GetInput();

            bool updated = storageManager.UpdateMember(username, newPassword);

            if (updated)
            {
                myView.DisplayMessage("Member details updated successfully.");
            }
            else
            {
                myView.DisplayMessage("Member was not found.");
            }

            Console.WriteLine();
            myView.DisplayMessage("Press any key to return...");

            Console.ReadKey();
        }

        private static void DeleteMembers()
        {
            Console.Clear();
            myView.DisplayMessage("===== DELETE MEMBERS =====");
            myView.DisplayMessage("Enter member username:");

            string username = myView.GetInput();
            
            bool deleted = storageManager.DeleteMember(username);

            if (deleted)
            {
                myView.DisplayMessage("Member deleted successfully.");
            }
            else
            {
                myView.DisplayMessage("Member was not found.");
            }
            Console.WriteLine();
            myView.DisplayMessage("Press any key to return...");

            Console.ReadKey();
        }
    }
}
