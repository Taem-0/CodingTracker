using Spectre.Console;
using Spectre.Console.Cli;
namespace CodingTracker
{
    internal class TableVIsualisation
    {

        internal static void showTable (List<Coding> data)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;


            var table = new Table();
            table.Border = TableBorder.Rounded;
            table.BorderStyle = new Style(foreground: Color.Yellow);

            table.AddColumn("Id");
            table.AddColumn("Date");
            table.AddColumn("Start Time");
            table.AddColumn ("End Time");
            table.AddColumn("Duration");

            foreach (var entry in data)
            {
                table.AddRow(entry.Id.ToString(), entry.Date, entry.StartTime, entry.EndTime, entry.Duration);
            }

            table.ShowRowSeparators();
            

            AnsiConsole.Write(table);
            

        }


    }
}