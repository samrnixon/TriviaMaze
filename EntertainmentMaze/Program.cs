using EntertainmentMaze.Database;
using EntertainmentMaze.maze;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EntertainmentMaze
{
    public class Program
    {
        internal static Player newPlayer;
        public static Maze playerMaze;
        public static string SaveFile = "GameSaves.txt";
        internal static IFormatter formatter = new BinaryFormatter();
        private static string PlayResumeMenuOption = "Play";

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
                    Console.WriteLine($" 1. {PlayResumeMenuOption}");
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
                        LoadGame();
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
                        //playerMaze.MoveHero("N");
                        MovementAttempt("N");
                        break;
                    case 2:
                        //Ask question, if correct then move East
                        //playerMaze.MoveHero("E");
                        MovementAttempt("E");
                        break;
                    case 3:
                        //Ask question, if correct then move South
                        //playerMaze.MoveHero("S");
                        MovementAttempt("S");
                        break;
                    case 4:
                        //Ask question, if correct then move West
                        //playerMaze.MoveHero("W");
                        MovementAttempt("W");
                        break;
                    case 5:
                        //Save game
                        SaveGame();
                        break;
                    case 6:
                        playerMaze.DisplayHeroLocation();
                        break;
                    default:
                        PlayResumeMenuOption = "Resume";
                        return;
                }
            }
        }

        private static void SaveGame()
        {
            Program.SerializeItem(SaveFile, formatter);
        }

        private static void LoadGame()
        {
            //Program.DeserializeItem(SaveFile, formatter);
        }

        public static void SerializeItem(string fileName, IFormatter formatter)
        {
            FileStream s = new FileStream(fileName, FileMode.Create);
            formatter.Serialize(s, playerMaze);
            s.Close();
        }


        public static void DeserializeItem(string fileName, IFormatter formatter)
        {
            FileStream s = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            playerMaze = (Maze)formatter.Deserialize(s);
        }

        private static void MovementAttempt(string movementDirection)
        {
            if(movementDirection == "N")
            {
                if (playerMaze.GetLocation().NorthDoor.GetDoorStatus() is true)
                {
                    Console.WriteLine("This Door is locked! You cannot go through here.");
                    return;
                }
                else
                {
                    if(QuestionAttempt(playerMaze.GetLocation().NorthDoor.DisplayQuestion(), playerMaze.GetLocation().NorthDoor.DisplayAnswer()) is true)
                    {
                        playerMaze.MoveHero("N");
                    }
                    else
                    {
                        playerMaze.GetLocation().NorthDoor.LockDoor();
                    }
                }

            }
            if (movementDirection == "E")
            {
                if (playerMaze.GetLocation().EastDoor.GetDoorStatus() is true)
                {
                    Console.WriteLine("This Door is locked! You cannot go through here.");
                    return;
                }
                else
                {
                    if(QuestionAttempt(playerMaze.GetLocation().EastDoor.DisplayQuestion(), playerMaze.GetLocation().EastDoor.DisplayAnswer()) is true)
                    {
                        playerMaze.MoveHero("E");
                    }
                    else
                    {
                        playerMaze.GetLocation().EastDoor.LockDoor();
                    }
                }
            }
            if (movementDirection == "S")
            {
                if (playerMaze.GetLocation().SouthDoor.GetDoorStatus() is true)
                {
                    Console.WriteLine("This Door is locked! You cannot go through here.");
                    return;
                }
                else
                {
                    if(QuestionAttempt(playerMaze.GetLocation().SouthDoor.DisplayQuestion(), playerMaze.GetLocation().SouthDoor.DisplayAnswer()) is true)
                    {
                        playerMaze.MoveHero("S");
                    }
                    else
                    {
                        playerMaze.GetLocation().SouthDoor.LockDoor();
                    }
                }
            }
            if (movementDirection == "W")
            {
                if (playerMaze.GetLocation().WestDoor.GetDoorStatus() is true)
                {
                    Console.WriteLine("This Door is locked! You cannot go through here.");
                    return;
                }
                else
                {
                    if(QuestionAttempt(playerMaze.GetLocation().WestDoor.DisplayQuestion(), playerMaze.GetLocation().WestDoor.DisplayAnswer()) is true)
                    {
                        playerMaze.MoveHero("W");
                    }
                    else
                    {
                        playerMaze.GetLocation().WestDoor.LockDoor();
                    }
                }
            }
        }

        private static bool QuestionAttempt(string question, string answer)
        {
            string attemptedAnswer = "";
            Console.WriteLine("Here is the question to open the door:\n");
            Console.WriteLine(question);

            attemptedAnswer = Console.ReadLine();

            if (answer.ToLowerInvariant().Equals(attemptedAnswer.ToLowerInvariant()))
            {
                Console.WriteLine("Correct Answer! You move to the next room...");
                return true;
            }
            else
            {
                Console.WriteLine("Incorrect! The Door locks...");
                return false;
            }
        }
    }
}
