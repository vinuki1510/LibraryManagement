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
            Console.WriteLine("Welcome to the Library Management System");
            Console.WriteLine("1. Manage Members");
            Console.WriteLine("2. Manage Books");
            Console.WriteLine("3. Manage Loans");
            Console.WriteLine("4. Exit");
            Console.Write("Please enter your choice: ");
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
