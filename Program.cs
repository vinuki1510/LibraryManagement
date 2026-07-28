using LibraryManagement.controller;
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
                        Login();
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

    }


}
