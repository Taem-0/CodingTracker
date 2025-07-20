namespace CodingTracker
{
    internal class GetUserInput
    {

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

                switch(userCommand)
                {
                    case "0":

                        closeApp = true;
                        break;
                    case "1":

                        break;

                    case "2":

                        break;

                }
            }
        }

    }
}