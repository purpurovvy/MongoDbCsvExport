using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MongoDbExportCSV
{
    class Program
    {
        private static string _connectionString;
        private static string _path = ".\\CSV_Export\\";
        private static string _mongoexportExePath;
        private static string _selectedDatabase;
        private static string _selectedCollection;

        static void Main(string[] args)
        {
                var pressedValue = String.Empty;
                Console.WriteLine("##### MongoDB Export to CSV ####");
                do
                {
                    _connectionString = "mongodb://localhost:27017";
                    Console.WriteLine("Do you want to use default connection string = mongodb://localhost:27017 (N to decline)? ");
                    pressedValue = Console.ReadLine();
                    if (pressedValue.ToLower() == "n")
                    {
                        pressedValue = null;
                        Thread.Sleep(1000);
                        Console.WriteLine("Please provide your connection string to mongoDB: ");
                        _connectionString = Console.ReadLine();
                    }

                } while (Validator.ConnectionStringValidator(_connectionString));

                do
                {
                    Console.WriteLine("Please provide your mongoexport.exe path by defualt it's: C:\\Program Files\\MongoDB\\Server\\4.2\\bin\\mongoexport.exe");
                    _mongoexportExePath = Console.ReadLine();
                    if (_mongoexportExePath == "")
                    {
                    _mongoexportExePath = "C:\\Program Files\\MongoDB\\Server\\4.2\\bin\\mongoexport.exe";
                    }

                } while (Validator.MongoExportPathValidator(_mongoexportExePath));

                do
                {
                    _path = ".\\CSV_Export\\";
                    Console.WriteLine("Would you like to use default path = .\\CSV_export\\ (N to decline)? ");
                    pressedValue = Console.ReadLine();

                    if (pressedValue.ToLower() == "n")
                    {
                        pressedValue = null;
                        Thread.Sleep(1000);
                        Console.WriteLine("Please provide your export path: ");
                        _path = Console.ReadLine();

                    }

                } while (Validator.PathValidator(_path));


                Thread.Sleep(1000);
                Console.WriteLine("Do you want export all databases and collections to .csv (N to decline)?");
                pressedValue = Console.ReadLine();
                if (pressedValue.ToLower() == "n")
                {
                    pressedValue = null;
                Thread.Sleep(1000);
                Console.WriteLine("Please provide your database name to export: ");
                    _selectedDatabase = Console.ReadLine();
                Thread.Sleep(1000);
                Console.WriteLine($"Export all collections from database: {_selectedDatabase} (N to decline)?");
                    pressedValue = Console.ReadLine();
                    if (pressedValue.ToLower() == "n")
                    {
                        pressedValue = null;
                        Thread.Sleep(1000);
                        Console.WriteLine("Please provide collection name: ");
                        _selectedCollection = Console.ReadLine();
                        Console.WriteLine($"Export for DB: {_selectedDatabase}, Collection: {_selectedCollection} started...");
                        BatchBuilder.PrepareSpecificExport(_connectionString, _selectedDatabase, _selectedCollection, _path, _mongoexportExePath);

                    }
                    else
                    {
                        Console.WriteLine($"Export for DB: {_selectedDatabase} started...");
                        BatchBuilder.PrepareSpecificExport(_connectionString, _selectedDatabase, _selectedCollection, _path, _mongoexportExePath);
                    }
                }
                else
                {
                    Console.WriteLine("All databases will be exported, it's gonna take a while...");
                    
                    BatchBuilder.PrepareFullExport(_connectionString, _path, _mongoexportExePath);

                }
                Console.WriteLine("Job has been finished!");
                Console.ReadKey();
        }
        }
      
}
