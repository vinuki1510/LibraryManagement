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
