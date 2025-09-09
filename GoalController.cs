using System.Text;
using Microsoft.Data.Sqlite;

namespace CodingTracker
{
    internal class GoalController
    {

        static string connectionString = @"Data Source=coding-tracker.db";

        static List<Goal> goalData = new List<Goal>();

        internal static void PostGoal(Goal goal)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();

                    tableCmd.CommandText =
                        @"INSERT INTO goals (GoalHours, Deadline)
                            VALUES (@GoalHours, @Deadline)";

                    tableCmd.Parameters.AddWithValue("@GoalHours", goal.GoalHours);
                    tableCmd.Parameters.AddWithValue("@Deadline", goal.Deadline);

                    tableCmd.ExecuteNonQuery();
                }
            }
        }

        internal static void GetGoal()
        {
            List<Goal> tableData = new List<Goal>();

            using (var connection = new SqliteConnection(connectionString))
            {
                using(var tableCmd = connection.CreateCommand())
                {
                    connection.Open();

                    tableCmd.CommandText =
                        @"SELECT * FROM goals";

                    using (var reader = tableCmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                tableData.Add(new Goal
                                {
                                    Id = reader.GetInt32(0),
                                    GoalHours = reader.GetString(1),
                                    Deadline = reader.GetString(2),
                                });
                            }
                            TableVIsualisation.showGoals(tableData);
                        } else
                        {
                            Console.WriteLine("No records found. :<");
                        }
                    }
                }
            }
        }

    }

}
