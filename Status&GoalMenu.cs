

namespace CodingTracker
{
    internal class Status_GoalMenu
    {

        GoalController goalController = new();

        internal void StatusMenu()
        {

            Codingcontroller.GetTotalAverage();

            bool closeInterface = false;

            while (closeInterface == false)
            {
                Console.WriteLine("\n");
                Console.WriteLine("\t-Type 0 to back");
                Console.WriteLine("\t-Type 1 to view goals");
                Console.WriteLine("\t-Type 2 to set goal");

                string? userCommand = Console.ReadLine();

                switch (userCommand)
                {
                    case "0":
                        closeInterface = true;
                        break;

                    case "1":
                        GoalController.GetGoal();
                        break;

                    case "2":
                        
                        ProcessAdd();
                        break;
                }
            }
        }

        private void ProcessAdd()
        {
            var targetHours = Helpers.GetTimeInput();

            var deadLine = Helpers.GetDateInput();

            Goal goal = new Goal();

            goal.GoalHours = targetHours;
            goal.Deadline = deadLine;   

            GoalController.PostGoal(goal);

        }

    }
}
