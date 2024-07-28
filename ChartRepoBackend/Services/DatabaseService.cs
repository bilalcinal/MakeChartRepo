using ChartRepoBackend.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace ChartRepoBackend.Services
{
    public class DatabaseService
    {
       public DataSet GetDataSet(DatabaseConnection dbConnection, string tableName, string columns)
{
    var dataSet = new DataSet();
    using (var connection = new SqlConnection($"Server={dbConnection.Server};Database={dbConnection.Database};User Id={dbConnection.Username};Password={dbConnection.Password};TrustServerCertificate=True;"))
    {
        var selectedColumns = columns.Split(',');
        dataSet.Labels = new List<string>();
        dataSet.Data = new List<int>();

        foreach (var column in selectedColumns)
        {
            string query = $"SELECT COUNT({column}) as Count FROM {tableName}";

            var command = new SqlCommand(query, connection);
            connection.Open();
            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    dataSet.Labels.Add(column);
                    dataSet.Data.Add(reader.GetInt32(0));
                }
            }
            connection.Close();
        }
    }
    return dataSet;
}

        public List<ColumnInfo> GetColumnsWithTypes(DatabaseConnection dbConnection, string tableName)
        {
            var columns = new List<ColumnInfo>();
            try
            {
                using (var connection = new SqlConnection($"Server={dbConnection.Server};Database={dbConnection.Database};User Id={dbConnection.Username};Password={dbConnection.Password};TrustServerCertificate=True;"))
                {
                    var command = new SqlCommand($"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'", connection);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            columns.Add(new ColumnInfo
                            {
                                Name = reader.GetString(0),
                                Type = reader.GetString(1)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return columns;
        }

        public List<string> GetDatabaseObjects(DatabaseConnection dbConnection)
        {
            var objects = new List<string>();
            try
            {
                using (var connection = new SqlConnection($"Server={dbConnection.Server};Database={dbConnection.Database};User Id={dbConnection.Username};Password={dbConnection.Password};TrustServerCertificate=True;"))
                {
                    var command = new SqlCommand("SELECT name FROM sys.objects WHERE type = 'U'", connection);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
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
            return objects;
        }

        public List<string> GetColumns(DatabaseConnection dbConnection, string tableName)
        {
            var columns = new List<string>();
            try
            {
                using (var connection = new SqlConnection($"Server={dbConnection.Server};Database={dbConnection.Database};User Id={dbConnection.Username};Password={dbConnection.Password};TrustServerCertificate=True;"))
                {
                    var command = new SqlCommand($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'", connection);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            columns.Add(reader.GetString(0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return columns;
        }
    }
}
