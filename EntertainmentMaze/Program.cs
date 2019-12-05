using EntertainmentMaze.Database;
using EntertainmentMaze.maze;
using System;

namespace EntertainmentMaze
{
    public class Program
    {
        internal static Player newPlayer;
        public static Maze playerMaze;
        public static void Main()
        {
            DisplayGreeting();

            DatabaseListRetrieval.InitializeList();
            var mazeBuilder = new MazeBuilder();
            newPlayer = new Player(Player.GetName("FirstName"), Player.GetName("LastName"));
            playerMaze = mazeBuilder
                .SetRows(5)
                .SetColumns(5)
                .SetPlayer(newPlayer)
                .Build();

            Menu();
        }

        private static void DisplayGreeting()
        {
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------\n");
            Console.WriteLine("Welcome to the Entertainment Trivia Maze!\n");
            Console.WriteLine("-----------------------------------------");
        }
        private static void Menu()
        {
            while (true)
            {
                int selection;
                do
                {
                    Console.WriteLine("-----------------------------------------");
                    Console.WriteLine("MAIN MENU\n");
                    Console.WriteLine(" 1. Play");
                    Console.WriteLine(" 2. Load Game");
                    Console.WriteLine(" 3. Quit");
                    Console.WriteLine("-----------------------------------------\n");

                    if (!int.TryParse(Console.ReadLine(), out selection))
                    {
                        selection = 0;
                    }
                } while (selection == 0);

                switch (selection)
                {
                    case 1:
                        InGameMenu();
                        break;
                    case 2:
                        //Display Saves? Not sure if thats how saves work.
                        break;
                    default:
                        return;
                }
            }
        }

        private static void InGameMenu()
        {
            while (true)
            {
                int selection;
                do
                {
                    Console.WriteLine(playerMaze.PrintMaze());
                    Console.WriteLine("Where would you like to go?");
                    Console.WriteLine(" 1. Go North");
                    Console.WriteLine(" 2. Go East");
                    Console.WriteLine(" 3. Go South");
                    Console.WriteLine(" 4. Go West");
                    Console.WriteLine(" 5. Save Game");
                    Console.WriteLine(" 6. Display Player Location (x,y)");
                    Console.WriteLine(" 7. Quit to Main Menu");
                    Console.WriteLine("-----------------------------------------\n");

                    if (!int.TryParse(Console.ReadLine(), out selection))
                    {
                        selection = 0;
                    }
                } while (selection == 0);

                switch (selection)
                {
                    case 1:
                        //Ask question, if correct then move North
                        playerMaze.MoveHero("N");
                        break;
                    case 2:
                        //Ask question, if correct then move East
                        playerMaze.MoveHero("E");
                        break;
                    case 3:
                        //Ask question, if correct then move South
                        playerMaze.MoveHero("S");
                        break;
                    case 4:
                        //Ask question, if correct then move West
                        playerMaze.MoveHero("W");
                        break;
                    case 5:
                        //Save game
                        break;
                    case 6:
                        playerMaze.DisplayHeroLocation();
                        break;
                    default:
                        return;
                }
            }
        }
    }
}
