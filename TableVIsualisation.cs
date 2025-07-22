using Spectre.Console;

namespace CodingTracker
{
    internal class TableVIsualisation
    {

        internal static void showTable (List<Coding> data)
        {
            var table = new Table();

            table.AddColumn("Id");
            table.AddColumn("Date");
            table.AddColumn("Start Time");
            table.AddColumn ("End Time");
            table.AddColumn("Duration");

            foreach (var entry in data)
            {
                table.AddRow(entry.Id.ToString(), entry.Date, entry.StartTime, entry.EndTime, entry.Duration);
            }

            AnsiConsole.Write(table);

        }


    }
}