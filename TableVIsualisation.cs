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
            table.AddColumn("Duration");

            foreach (var entry in data)
            {
                table.AddRow(entry.Id.ToString(), entry.Date, entry.Duration);
            }

            AnsiConsole.Write(table);

        }


    }
}