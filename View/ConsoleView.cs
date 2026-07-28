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

        public int MemberMenu()
        {
            Console.WriteLine("\nMember Menu:");
            Console.WriteLine("1. Search Books");
            Console.WriteLine("2. Borrow Book");
            Console.WriteLine("3. View My Loans");
            Console.WriteLine("4. Logout");
            Console.Write("Please enter your choice: ");
            return int.Parse(Console.ReadLine());
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
            return int.Parse(Console.ReadLine());
        }           
        public void DisplayMessage (string message)
        {
            Console.WriteLine(message);
        }
        public string GetInput()
        {
            return Console.ReadLine();
        }
        public int GetIntInput()
        {
           return int.Parse(Console.ReadLine());
        }
    }
}
