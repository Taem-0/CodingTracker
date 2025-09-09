using System.Diagnostics;
using System.Globalization;

namespace CodingTracker
{
    internal class Helpers
    {


        internal static string GetDateInput()
        {
            Console.WriteLine("Please insert the date: (Format: yyyy-MM-dd). Type 0 to return to main menu.");

            string? dateInput = Console.ReadLine();

            if (dateInput == "0")
            {
                return null;
            }
            else if (dateInput == "now")
            {
                dateInput = DateTime.Now.ToString("yyyy-MM-dd");
            }

            while (!DateTime.TryParseExact(dateInput, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                Console.WriteLine("Invalid input. Please insert the date: (Format: yyyy-MM-dd). Type 0 to return to main menu.");
                dateInput = Console.ReadLine();
            }

            return dateInput;

        }

        internal static string GetTimeInput()
        {
            Console.WriteLine("Please insert the time: (Format: h:mm). Type 0 to return to main menu.");

            string? timeInput = Console.ReadLine();

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


        internal static string CalculateDuration(string start, string end)
        {

            TimeSpan startTime = TimeSpan.ParseExact(start, "h\\:mm", CultureInfo.InvariantCulture);
            TimeSpan endTime = TimeSpan.ParseExact(end, "h\\:mm", CultureInfo.InvariantCulture);

            TimeSpan duration = endTime - startTime;

            return duration.ToString(@"h\:mm");

        }

        internal static void LiveTimeRecord()
        {

            GetUserInput getUserInput = new GetUserInput();

            var date = GetDateInput();

            Console.WriteLine("Press any key to start...");
            Console.ReadLine();

            string startTime = DateTime.Now.ToString("h\\:mm", CultureInfo.InvariantCulture);
            Console.WriteLine($"Started at {startTime}");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.Write("Press any key to stop...");
            Console.ReadLine();

            stopwatch.Stop();

            string endTime = DateTime.Now.ToString("h\\:mm", CultureInfo.InvariantCulture);
            TimeSpan duration = stopwatch.Elapsed;

            string formattedDuration = $"{(int)duration.TotalHours}:{duration.Minutes:D2}";

            Console.WriteLine($"Ended at {endTime}");
            Console.WriteLine($"Duration: {formattedDuration}");

            Coding coding = new();

            coding.Date = date;
            coding.StartTime = startTime;
            coding.EndTime = endTime;
            coding.Duration = formattedDuration;

            Codingcontroller.Post(coding);

        }

        internal static (string period, string date) FilterDate()
        {

            Console.WriteLine("Filter by period:");
            Console.WriteLine("1. Day");
            Console.WriteLine("2. Week");
            Console.WriteLine("3. Year");
            Console.WriteLine("0. Back");
            
            string? periodInput = Console.ReadLine();

            string? dateInput = "";

            switch(periodInput)
            {
                case "1":
                    Console.WriteLine("Please insert the date: (Format: yyyy-MM-dd).");
                    dateInput = Console.ReadLine();
                    while (!DateTime.TryParseExact(dateInput, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None, out _))
                    {
                        Console.WriteLine("Invalid input. Please insert the date: (Format: yyyy-MM-dd).");
                        dateInput = Console.ReadLine();
                    }

                    
                    break;
                case "2":
                    Console.WriteLine("Please insert the date: (Format: yyyy-MM-dd).");
                    dateInput = Console.ReadLine();
                    while (!DateTime.TryParseExact(dateInput, "yyyy-MM-dd", new CultureInfo("en-US"), DateTimeStyles.None, out _))
                    {
                        Console.WriteLine("Invalid input. Please insert the date: (Format: yyyy-MM-dd).");
                        dateInput = Console.ReadLine();
                    }
                    break; 
                
                case "3":
                    Console.WriteLine("Please insert the year: (yyyy).");
                    dateInput = Console.ReadLine();
                    while (!DateTime.TryParseExact(dateInput, "yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
                    {
                        Console.WriteLine("Invalid input. Please insert the date: (Format: yyyy).");
                        dateInput = Console.ReadLine();
                    }

                    break;
            }

            (string? Period, string? Input) userInput = (periodInput, dateInput);

            return userInput;

        }

        internal static string FilterOrder()
        {
            Console.WriteLine("Filter by order:");
            Console.WriteLine("1. Start time ascending");
            Console.WriteLine("2. Start time descending");
            Console.WriteLine("3. Duration ascending");
            Console.WriteLine("4. Duration descending");
            Console.WriteLine("0. Back");

            string? orderInput = Console.ReadLine();

            return orderInput;
        }

    }
}