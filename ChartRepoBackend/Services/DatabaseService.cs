using ChartRepoBackend.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace ChartRepoBackend.Services
{
    public class DatabaseService
    {
        public DataSet GetDataSet(DatabaseConnection dbConnection, string tableName)
        {
            var dataSet = new DataSet();
            using (var connection = new SqlConnection($"Server={dbConnection.Server};Database={dbConnection.Database};User Id={dbConnection.Username};Password={dbConnection.Password};TrustServerCertificate=True;"))
            {
                var command = new SqlCommand($"SELECT * FROM {tableName}", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    dataSet.Labels = new List<string>();
                    dataSet.Data = new List<decimal>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        dataSet.Labels.Add(reader.GetName(i));
                    }

                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (decimal.TryParse(reader.GetValue(i).ToString(), out decimal value))
                            {
                                dataSet.Data.Add(value);
                            }
                            else
                            {
                                dataSet.Data.Add(0);
                            }
                        }
                    }
                }
            }
            return dataSet;
        }

        public List<string> GetDatabaseObjects(DatabaseConnection dbConnection)
        {
            var objects = new List<string>();
            try
            {
                Console.WriteLine("Connecting to database...");
                using (var connection = new SqlConnection($"Server={dbConnection.Server};Database={dbConnection.Database};User Id={dbConnection.Username};Password={dbConnection.Password};TrustServerCertificate=True;"))
                {
                    var command = new SqlCommand("SELECT name FROM sys.objects WHERE type = 'U'", connection);
                    connection.Open();
                    Console.WriteLine("Connected to database.");
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Executing query...");
                        while (reader.Read())
                        {
                            objects.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            Console.WriteLine("Query executed. Objects found: " + objects.Count);
            return objects;
        }
    }
}
