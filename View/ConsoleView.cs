using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.View
{
    public class ConsoleView
    {
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

        public int MemberMenu()
        {

            Console.WriteLine("\nMember Menu:");
            Console.WriteLine("1. Search Books");
            Console.WriteLine("2. Borrow Book");
            Console.WriteLine("3. View My Loans");
            Console.WriteLine("4. Logout");
            Console.Write("Please enter your choice: ");
            return ReadIntFromConsole();
        }

        public int StaffMenu()
        {
            Console.WriteLine("\nStaff Menu:");
            Console.WriteLine("1. Add Books");
            Console.WriteLine("2. Register member");
            Console.WriteLine("3. Update books");
            Console.WriteLine("4. Process returns");
            Console.WriteLine("5. Logout");
            Console.Write("Please enter your choice: ");
            return ReadIntFromConsole();
        }           

        public int AdminMenu()
        {
            Console.WriteLine("\nAdmin Menu:");
            Console.WriteLine("1. Add staff");
            Console.WriteLine("2. View staff");
            Console.WriteLine("3. Logout");
            Console.Write("Please enter your choice: ");
            return ReadIntFromConsole();       
        }
        public void DisplayMessage (string message)
        {
            Console.WriteLine(message);
        }
        public string GetInput()
        {
            return Console.ReadLine() ?? string.Empty;
        }

        public int GetIntInput()
        {
            return ReadIntFromConsole();
        }

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
