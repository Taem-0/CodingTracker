namespace CodingTracker
{
    internal class Program
    {

        static string connectionString = @"Data Source=coding-tracker.db";


        static void Main(string[] args)
        {
            

            DataBaseManager dataBaseManager = new();
            GetUserInput getUserInput = new();
            dataBaseManager.CreateTable(connectionString);

        }
    }
}
