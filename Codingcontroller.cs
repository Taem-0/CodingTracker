
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
                        @"INSERT INTO coding (Date, StartTime, EndTime, Duration)
                            VALUES (@date, @start, @end, @duration)";

                    tableCmd.Parameters.AddWithValue("@date", coding.Date);
                    tableCmd.Parameters.AddWithValue("@start", coding.StartTime);
                    tableCmd.Parameters.AddWithValue("@end", coding.EndTime);
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
                                    StartTime = reader.GetString(2),
                                    EndTime = reader.GetString(3),
                                    Duration = reader.GetString(4),
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

        internal Coding GetId(int id)
        {

            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();

                    tableCmd.CommandText =
                        @$"SELECT * FROM coding WHERE Id = '{id}'";

                    using ( var reader = tableCmd.ExecuteReader())
                    {
                        Coding coding = new();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            coding.Id = reader.GetInt32(0);
                            coding.Date = reader.GetString(1);
                            coding.StartTime = reader.GetString(2);
                            coding.EndTime = reader.GetString(3);
                            coding.Duration = reader.GetString(4);
                        }

                        return coding;
                    }
                }
            }
     

        }

        internal static void Delete(int id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd  = connection.CreateCommand())
                {
                    connection.Open();

                    tableCmd.CommandText =
                        $@"DELETE FROM coding WHERE Id = @id";
                    tableCmd.Parameters.AddWithValue("@id", id);

                    tableCmd.ExecuteNonQuery();

                    Console.WriteLine($"Record with {id} was deleted");
                }
            }
        }

        internal static void Update(Coding coding)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();

                    tableCmd.CommandText =
                        @"UPDATE coding 
                        SET Date = @date, StartTime = @start, EndTime = @end, Duration = @duration
                        WHERE Id = @id";

                    tableCmd.Parameters.AddWithValue("@date", coding.Date);
                    tableCmd.Parameters.AddWithValue("@start", coding.StartTime);
                    tableCmd.Parameters.AddWithValue("@end", coding.EndTime);
                    tableCmd.Parameters.AddWithValue("@duration", coding.Duration);
                    tableCmd.Parameters.AddWithValue("@id", coding.Id);

                    tableCmd.ExecuteNonQuery();
                }
            }

        }
    }
}