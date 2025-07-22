using System.Globalization;

namespace CodingTracker
{
    internal class GetUserInput
    {

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
                Console.WriteLine("\t-Type 3 to update a record");
                Console.WriteLine("\t-Type 4 to delete a record");

                String? userCommand = Console.ReadLine();

                while (string.IsNullOrEmpty(userCommand))
                {
                    Console.WriteLine("Invalid, please enter a number from 0-4");
                    userCommand = Console.ReadLine();
                }

                switch (userCommand)
                {
                    case "0":

                        closeApp = true;
                        break;
                    case "1":
                        Codingcontroller.Get();
                        break;
                    case "2":
                        ProcessAdd();
                        break;
                    case "3":
                        ProcessUpdate();
                        break;
                    case "4":
                        ProcessDelete();
                        break;
                    default:
                        Console.WriteLine("Invalid, please enter a number from 0-4");
                        break;

                }
            }
        }

        private void ProcessAdd()
        {
            var date = GetDateInput();

            var startTime = GetTimeInput();

            var endTime = GetTimeInput();

            

            Coding coding = new();

            coding.Date = date;
            coding.StartTime = startTime;
            coding.EndTime = endTime;
            coding.Duration = CalculateDuration(startTime, endTime);

            Codingcontroller.Post(coding);

        }

        private void ProcessDelete()
        {
            Codingcontroller.Get();

            Console.WriteLine("Please insert the id of the category you want to delete. Type 0 to return to main menu.");

            string commandInput = Console.ReadLine();

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

            string commandInput = Console.ReadLine();

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
                Console.WriteLine("enter 0 to return to main menu");

                string updateCommandInput = Console.ReadLine();

                switch (updateCommandInput)
                {
                    case "d":
                        coding.Date = GetDateInput();
                        break;
                    case "s":
                        string updateStart = GetTimeInput();
                        coding.StartTime = updateStart;
                        coding.Duration = CalculateDuration(coding.StartTime, coding.EndTime);
                        break;
                    case "l":
                        string updateEnd = GetTimeInput();
                        coding.EndTime = updateEnd;
                        coding.Duration = CalculateDuration(coding.StartTime, coding.EndTime);    
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

        internal string GetDateInput()
        {
            Console.WriteLine("Please insert the date: (Format: dd-mm-yy). Type 0 to return to main menu.");

            string dateInput = Console.ReadLine();

            if (dateInput == "0")
            {
                return null;
            } else if (dateInput == "now")
            {
                dateInput = DateTime.Now.ToString("dd-MM-yy");
            }

            while (!DateTime.TryParseExact(dateInput, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                Console.WriteLine("Invalid input. Please insert the date: (Format: dd-mm-yy). Type 0 to return to main menu.");
                dateInput = Console.ReadLine();    
            }

            return dateInput;

        }

        internal string GetTimeInput()
        {
            Console.WriteLine("Please insert the time: (Format: h:mm). Type 0 to return to main menu.");

            string timeInput = Console.ReadLine();

            if (timeInput == "0")
            {
                return null;
            }

            while (!TimeSpan.TryParseExact(timeInput, "h\\:mm", CultureInfo.InvariantCulture, out _))
            {
                Console.WriteLine("Invalid input. Please insert the time: (Format: h:mm). Type 0 to return to main menu.");
                timeInput = Console.ReadLine();
            }

            return timeInput;
        }

        internal string CalculateDuration(string start, string end)
        {

            TimeSpan startTime = TimeSpan.ParseExact(start, "h\\:mm", CultureInfo.InvariantCulture);
            TimeSpan endTime = TimeSpan.ParseExact(end, "h\\:mm", CultureInfo.InvariantCulture);

            TimeSpan duration = endTime - startTime;

            return duration.ToString(@"h\:mm");

        }

    }
}