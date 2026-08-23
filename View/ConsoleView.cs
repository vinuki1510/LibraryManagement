using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.View
{ 
    public class ConsoleView
    {
        // Displays the main menu and allows the user to choose whether to login or exit.
        public void DisplayMainMenu()
        {
            Console.WriteLine("========================================");
            Console.WriteLine("Welcome to the Library Management System");
            Console.WriteLine("========================================");

            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Exit");
            Console.Write("Please enter your choice: ");
        }

        // Displays the login options for Members, Staff and Admins.
        public int LoginMenu()
        {
            Console.Clear();

            Console.WriteLine("========== LOGIN ==========");
            Console.WriteLine("1. Member Login");
            Console.WriteLine("2. Staff Login");
            Console.WriteLine("3. Admin Login");
            Console.WriteLine("4. Back");

            Console.Write("\nChoose an option: ");

            return ReadIntFromConsole();
        }

        // Displays the options available to a logged-in Member.
        public int MemberMenu()
        {
            Console.WriteLine("1. Search Books");
            Console.WriteLine("2. Borrow Book");
            Console.WriteLine("3. View My Loans");
            Console.WriteLine("4. Logout");

            Console.Write("Please enter your choice: ");
            return ReadIntFromConsole();
        }

        // Displays the options available to a logged-in Staff member.
        public int StaffMenu()
        {
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. View Books");
            Console.WriteLine("3. Update Book");
            Console.WriteLine("4. Delete Book");
            Console.WriteLine("5. Register Member");
            Console.WriteLine("6. View Members");
            Console.WriteLine("7. Delete Member");
            Console.WriteLine("8. Process Returns");
            Console.WriteLine("9. Logout");

            Console.Write("Please enter your choice: ");
            return ReadIntFromConsole();
        }

        // Displays the options available to a logged-in Admin.
        public int AdminMenu()
        {
            Console.WriteLine("1. Add Staff");
            Console.WriteLine("2. View Staff");
            Console.WriteLine("3. Update Staff");
            Console.WriteLine("4. Delete Staff");
            Console.WriteLine("5. Add Member");
            Console.WriteLine("6. View Members");
            Console.WriteLine("7. Update Member");
            Console.WriteLine("8. Delete Member");
            Console.WriteLine("9. Logout");

            Console.Write("Please enter your choice: ");
            return ReadIntFromConsole();
        }

        // Displays a message to the user.
        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        // Gets text input from the user.
        public string GetInput()
        {
            return Console.ReadLine() ?? string.Empty;
        }

        // Gets a valid integer input from the user.
        public int GetIntInput()
        {
            return ReadIntFromConsole();
        }

        // Reads and validates an integer entered by the user.
        private int ReadIntFromConsole()
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (int.TryParse(input, out int value))
                    return value;
                Console.Write("Invalid input. Please enter a valid number: ");
            }
        }
    }
}