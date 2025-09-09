using System.Globalization;
using Microsoft.Data.Sqlite;

namespace CodingTracker
{
    internal class GetUserInput
    {

        Status_GoalMenu goalMenu = new();   

        Codingcontroller codingController = new();

        internal void MainMenu()
        {
            bool closeApp = false;

            while (closeApp == false)
            {
                Console.WriteLine("MAIN MENU");
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("\t-Type 0 to close the app");
                Console.WriteLine("\t-Type 1 to view records");
                Console.WriteLine("\t-Type 2 to add a record");
                Console.WriteLine("\t-Type 3 to track your coding live");
                Console.WriteLine("\t-Type 4 to update a record");
                Console.WriteLine("\t-Type 5 to delete a record");
                Console.WriteLine("\t-Type 6 to view stats and goals");

                String? userCommand = Console.ReadLine(); 

                switch (userCommand)
                {
                    case "0":

                        closeApp = true;
                        break;
                    case "1":
                        ProcessGet();
                        break;
                    case "2":
                        ProcessAdd();
                        break;
                    case "3":
                        Helpers.LiveTimeRecord();
                        break;
                    case "4":
                        ProcessUpdate();
                        break;
                    case "5":
                        ProcessDelete();
                        break;
                    case "6":
                        goalMenu.StatusMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid, please enter a number from 0-4");
                        break;

                }
            }
        }       

        private void ProcessGet()
        {
            Codingcontroller.Get();

            bool closeInterface = false;

            var (periodQuery, orderQuery, parameter) = ("", "", new List<SqliteParameter>());

            while (closeInterface == false)
            {

                Console.WriteLine("\nSort by:");
                Console.WriteLine("\t-Type 0 to back");
                Console.WriteLine("\t-Type 1 to show table");
                Console.WriteLine("\t-Type 2 to sort by period");
                Console.WriteLine("\t-Type 3 to sort by order");

                string? userCommand = Console.ReadLine();

                switch (userCommand)
                {
                    case "0":

                        closeInterface = true; 
                        break;

                    case "1":

                        Codingcontroller.Get();
                        break;

                    case "2":

                        (periodQuery, parameter) = Codingcontroller.SortByPeriod();
                        Codingcontroller.Get(periodQuery, orderQuery, parameter);
                        break;

                     case "3":

                        (orderQuery) = Codingcontroller.SortByOrder();
                        Codingcontroller.Get(periodQuery, orderQuery, parameter);
                        
                        break;
                }
            }

        }

        private void ProcessAdd()
        {
           
            var date = Helpers.GetDateInput();

            var startTime = Helpers.GetTimeInput();

            var endTime = Helpers.GetTimeInput();



            Coding coding = new();

            coding.Date = date;
            coding.StartTime = startTime;
            coding.EndTime = endTime;
            coding.Duration = Helpers.CalculateDuration(startTime, endTime);

            Codingcontroller.Post(coding);

        }

        private void ProcessDelete()
        {
            Codingcontroller.Get();

            Console.WriteLine("Please insert the id of the category you want to delete. Type 0 to return to main menu.");

            string? commandInput = Console.ReadLine();

            while (!Int32.TryParse(commandInput, out _) || string.IsNullOrEmpty(commandInput) || Int32.Parse(commandInput) < 0) 
            {
                Console.WriteLine("Invalid input. Please insert the id of the category you want to delete. Type 0 to return to main menu.");
                commandInput = Console.ReadLine();
            }

            var id = Int32.Parse(commandInput);

            if (id == 0) return;

            var coding = codingController.GetId(id);

            while (coding.Id == 0)
            {
                Console.WriteLine($"Record with {id} does not exist");
                Console.WriteLine("Please insert the id of the category you want to delete. Type 0 to return to main menu.");
                ProcessDelete();
            }

            Codingcontroller.Delete(id);

        }

        private void ProcessUpdate()
        {
            Codingcontroller.Get();

            Console.WriteLine("Please insert the id of the category you want to update. Type 0 to return to main menu.");

            string? commandInput = Console.ReadLine();

            while (!Int32.TryParse(commandInput, out _) || string.IsNullOrEmpty(commandInput) || Int32.Parse(commandInput) < 0)
            {
                Console.WriteLine("Invalid input. Please insert the id of the category you want to delete. Type 0 to return to main menu.");
                commandInput = Console.ReadLine();
            }

            var id = Int32.Parse(commandInput);

            if (id == 0) return;

            var coding = codingController.GetId(id);

            while (coding.Id == 0)
            {
                Console.WriteLine($"Record with {id} does not exist");
                Console.WriteLine("Please insert the id of the category you want to delete. Type 0 to return to main menu.");
                ProcessUpdate();
            }

            bool recordUpdating = true;

            while (recordUpdating == true)
            {

                Console.WriteLine("enter 'd' to update the date.");
                Console.WriteLine("enter 's' to update the start time");
                Console.WriteLine("enter 'l' to update the end time");
                Console.WriteLine("enter 'b' to finalize the update");
                Console.WriteLine("enter 0 to return to main menu");

                string? updateCommandInput = Console.ReadLine();

                switch (updateCommandInput)
                {
                    case "d":
                        coding.Date = Helpers.GetDateInput();
                        break;
                    case "s":
                        string updateStart = Helpers.GetTimeInput();
                        coding.StartTime = updateStart;
                        coding.Duration = Helpers.CalculateDuration(coding.StartTime, coding.EndTime);
                        break;
                    case "l":
                        string updateEnd = Helpers.GetTimeInput();
                        coding.EndTime = updateEnd;
                        coding.Duration = Helpers.CalculateDuration(coding.StartTime, coding.EndTime);    
                        break;
                    case "b":
                        recordUpdating = false;
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("enter 0 to return to main menu");
                        break;
                }
            }

            Codingcontroller.Update(coding);

        }

        

        

       

        

    }
}