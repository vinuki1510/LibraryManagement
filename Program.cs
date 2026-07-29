using LibraryManagement.controller;
using LibraryManagement.SqlClient;
using LibraryManagement.View;

namespace LibraryManagement
{
    internal class Program
    {
        private static StorageManager storageManager;
        private static ConsoleView myView;
        static void Main(string[] args)
        {

            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=BikeStores;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;";

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
            myView.DisplayMessage("Enter username: ");
            string username = myView.GetInput();
       
            // Username Validation
            while (string.IsNullOrWhiteSpace(username))
            {
                myView.DisplayMessage("Username cannot be empty.");
                myView.DisplayMessage("Enter username: ");
                username = myView.GetInput();
            }

            while (username.Length < 4 || username.Length > 20)
            {
                myView.DisplayMessage("Username must be between 4 and 20 characters.");
                myView.DisplayMessage("Enter username: ");
                username = myView.GetInput();
            }
            myView.DisplayMessage("Enter password: ");
            string password = myView.GetInput();

            // Password Validation  
            while (string.IsNullOrWhiteSpace(password) || password.Length < 6)
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
                myView.DisplayMessage("Invalid username or password. Please try again.");
            }
        }
        private static void MemberMenu()
        {
            bool logout = false;
            while (!logout)
            {
                int option = myView.MemberMenu();
                switch (option)
                {
                    case 1:
                        myView.DisplayMessage("Search books");
                        break;
                    case 2:
                        myView.DisplayMessage("Borrow book");
                        break;
                    case 3:
                        myView.DisplayMessage("View my loans");
                        break;
                    case 4:
                        logout = true;
                        myView.DisplayMessage("Logging out...");
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

            storageManager.SearchBook(title);

            Console.WriteLine();
            myView.DisplayMessage("Press any key to return...");
            Console.ReadKey();
        }
        private static void StaffMenu()
        {
            bool logout = false;
            while (!logout)
            {
                int option = myView.StaffMenu();
                switch (option)
                {
                    case 1:
                        myView.DisplayMessage("Add books");
                        break;
                    case 2:
                        myView.DisplayMessage("Register member");
                        break;
                    case 3:
                        myView.DisplayMessage("Update books");
                        break;
                    case 4:
                        myView.DisplayMessage("Process returns");
                        break;
                    case 5:
                        logout = true;
                        myView.DisplayMessage("Logging out...");
                        break;

                }
            }
        }
        private static void AdminMenu()
        {
            bool logout = false;
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
                        logout = true;
                        myView.DisplayMessage("Logging out...");
                        break;
                }
            }
        }

    }
     
}
