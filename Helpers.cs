using System.Diagnostics;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CodingTracker
{
    internal class Helpers
    {

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

            var date = getUserInput.GetDateInput();

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

    }
}