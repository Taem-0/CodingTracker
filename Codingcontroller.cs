
using System.Text;
using Microsoft.Data.Sqlite;

namespace CodingTracker
{
    internal class Codingcontroller
    {

        static string connectionString = @"Data Source=coding-tracker.db";

        internal static void Post(Coding coding)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();

                    tableCmd.CommandText =
                        @"INSERT INTO coding (Date, Duration)
                            VALUES (@date, @duration)";

                    tableCmd.Parameters.AddWithValue("@date", coding.Date);
                    tableCmd.Parameters.AddWithValue("@duration", coding.Duration);

                    tableCmd.ExecuteNonQuery();
                }
            }
        }

        internal static void Get()
        {
            List<Coding> tableData = new List<Coding>();

            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();

                    tableCmd.CommandText =
                        @"SELECT * FROM coding";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableData.Add(new Coding
                                {
                                    Id = reader.GetInt32(0),
                                    Date = reader.GetString(1),
                                    Duration = reader.GetString(2),
                                });
                            }
                            TableVIsualisation.showTable(tableData);

                        } else
                        {
                            Console.WriteLine("No records found.");
                        }
                    }
                    


                }
            }
        }
    }
}