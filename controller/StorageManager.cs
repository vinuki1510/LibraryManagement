using LibraryManagement.model;
using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.controller
{
    public class StorageManager
    {

        private SqlConnection conn;


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

            catch (SqlException e)

            {

                Console.WriteLine($"SQL Error: {e.Message}");


                Console.WriteLine($"SQL Error: {e.Message}");


                if (e.Message.Contains("attach an auto-named database"))

                {

                    Console.WriteLine("Fix: Database is already attached OR file is in use.");


                    Console.WriteLine("Try this:");

                    Console.WriteLine("1. Remove AttachDbFilename from connection string");

                    Console.WriteLine("2. Use Initial Catalog instead");

                    Console.WriteLine("3. Or delete/rename duplicate DB in SQL Server");

                }



            }

            catch (Exception ex)

            {

                Console.WriteLine($"Error connecting to database: {ex.Message}");


            }

        }
    }    
