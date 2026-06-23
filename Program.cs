using LibraryManagement.controller;

namespace LibraryManagement
{
    internal class Program
    {
        private static StorageManager storageManager;
        static void Main(string[] args)
        {

            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=BikeStores;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;";

            storageManager = new StorageManager(connectionString);
        }



    }


}
