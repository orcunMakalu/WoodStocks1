using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoodStocks1
{
    // This class for conecting to database table.
    public class StockFileRepository
    {
        DbProviderFactory factory;
        string provider;
        string connectionString;

        // This code for constractor.
        public StockFileRepository()
        {
            provider = ConfigurationManager.AppSettings["provider"];
            connectionString = ConfigurationManager.AppSettings["connectionString"];
            factory = DbProviderFactories.GetFactory(provider);
        }

        // Method for data from StockFile table.
        public List<StockFile> GetAll()
        {
            var stockFile = new List<StockFile>();
            using(var connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                var command = factory.CreateCommand();
                command.Connection = connection;
                command.CommandText = "Select ItemCode, ItemDescription, CurrentCount, OnOrder From StockFile;";
                using(DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stockFile.Add(new StockFile
                        {
                            ItemCode = (string)reader["ItemCode"],
                            ItemDescription = (string)reader["ItemDescription"],
                            CurrentCount = (int)reader["CurrentCount"],
                            OnOrder = (string)reader["OnOrder"]
                        });
                    }
                }
            }
            return stockFile;
        }

        internal void WriteXml(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
