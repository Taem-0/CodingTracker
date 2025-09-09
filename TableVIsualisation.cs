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
            table.Border = TableBorder.Minimal;
            table.BorderStyle = new Style(foreground: Color.Silver);
            table.Expand();
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

        internal static void showGoals (List<Goal> goals)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var goalsTable = new Table();
            goalsTable.Border = TableBorder.Minimal;
            goalsTable.BorderStyle = new Style(foreground: Color.Yellow);
            goalsTable.Expand();
            goalsTable.AddColumn("Id");
            goalsTable.AddColumn("Goal");
            goalsTable.AddColumn("Deadline");

            foreach (var entry in goals)
            {
                goalsTable.AddRow(entry.Id.ToString(), entry.GoalHours, entry.Deadline);
            }

            goalsTable.ShowRowSeparators();

            AnsiConsole.Write(goalsTable);

        }


    }
}   