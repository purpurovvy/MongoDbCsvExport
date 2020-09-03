using MongoDB.Driver;
using System;
using System.IO;

namespace MongoDbExportCSV
{
	public static class Validator
    {
        public static bool PathValidator(string path)
        {
			try
			{
				System.IO.Directory.CreateDirectory(path);
				
			}
			catch (Exception)
			{
				Console.WriteLine($"Incorrect output path: {path}");
				return true;
			}
			return false;
		}

		public static bool ConnectionStringValidator(string connectionString)
		{
			try
			{
				new MongoClient(connectionString).ListDatabases(); ;

			}
			catch (Exception)
			{
				Console.WriteLine($"Invalid connection string: {connectionString}");
				return true;
			}
			return false;
		}

		public static bool MongoExportPathValidator(string path)
		{
			try
			{
				if (!File.Exists(path) || !path.Contains("mongoexport.exe"))
				{
					Console.WriteLine($"mongoexport.exe has been not found in: {path}");
					return true;
				}

			}
			catch (Exception)
			{
				Console.WriteLine("Something went wrong with path to mongoexport.exe...");
				return true;
			}
			return false;
		}

	}
}
