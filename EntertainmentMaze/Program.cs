using EntertainmentMaze.Database;
using EntertainmentMaze.maze;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

/*
 * There is a cheat enabled.
 * When asked which direction you would like to go.
 * Enter "117" and it will take you one door away from the exit.
 * Must answer next question correctly, if done so you will exit the maze.
 * Purpose: to get close to the exit and simulate answering the "last" needed question correctly.
 */

namespace EntertainmentMaze
{
    public class Program
    {
        internal static Player newPlayer;
        public static Maze playerMaze;
        private static string PlayResumeMenuOption = "Play";

        public static void Main()
        {
            RunSetup();
            Menu();
        }

        private static void RunSetup()
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

            PlayResumeMenuOption = "Play";
        }

        private static void DisplayGreeting()
        {
            Console.WriteLine(" ______ _______ __  __ ");
            Console.WriteLine("|  ____|__   __|  \\/  |");
            Console.WriteLine("| |__     | |  | \\  / |");
            Console.WriteLine("|  __|    | |  | |\\/| |");
            Console.WriteLine("| |____   | |  | |  | |");
            Console.WriteLine("|______|  |_|  |_|  |_|");
            Console.WriteLine();
            Console.WriteLine("-----------------------------------------\n");
            Console.WriteLine("Welcome to the Entertainment Trivia Maze!\n");
            Console.WriteLine("By: Irrelevant Generals\n");
            Console.WriteLine("Sam Nixon, Devin Kramer, Cam Sorensen\n");
            Console.WriteLine("-----------------------------------------\n");
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
                        playerMaze = LoadGame();
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
                    Console.WriteLine(
                    "Where would you like to go?\n" +
                    " 1. Go North\n" +
                    " 2. Go East\n" +
                    " 3. Go South\n" +
                    " 4. Go West\n" +
                    " 5. Save Game\n" +
                    " 6. Display Player Location (x,y)\n" +
                    " 7. Quit to Main Menu\n" +
                    "-----------------------------------------\n");

                    if (!int.TryParse(Console.ReadLine(), out selection))
                    {
                        selection = 0;
                    }
                } while (selection == 0);

                switch (selection)
                {
                    case 1:
                        PlayerControl.MovementAttempt(playerMaze, "N");
                        break;
                    case 2:
                        PlayerControl.MovementAttempt(playerMaze, "E");
                        break;
                    case 3:
                        PlayerControl.MovementAttempt(playerMaze, "S");
                        break;
                    case 4:
                        PlayerControl.MovementAttempt(playerMaze, "W");
                        break;
                    case 5:
                        SaveGame(playerMaze);
                        Console.WriteLine("Game Saved!\n");
                        break;
                    case 6:
                        playerMaze.DisplayHeroLocation();
                        break;
                    case 117:
                        playerMaze.SetCheatLocation();
                        break;
                    default:
                        PlayResumeMenuOption = "Resume";
                        return;
                }

                if((playerMaze.isSolvable() is false))
                {
                    Console.WriteLine($"Sorry, {newPlayer.GetFirstName()} {newPlayer.GetLastName()} you have lost!");
                    Console.WriteLine();
                    EndGame();
                }

/*                if ((playerMaze.IsSolvable() is null))
                {
                    Console.WriteLine($"Sorry, {newPlayer.GetFirstName()} {newPlayer.GetLastName()} you have lost!");
                    Console.WriteLine();
                    break;
                }*/

                if (playerMaze.GetLocation() == playerMaze.GetExitLocationOfMaze())
                {
                    Console.WriteLine("-----------------------------------------\n");
                    Console.WriteLine($"Congratulations, {newPlayer.GetFirstName()} {newPlayer.GetLastName()} you have won!\n");
                    EndGame();
                }
            }
        }

        private static void EndGame()
        {
            Console.WriteLine("Would you like to play again? y/n");
            string dec = Console.ReadLine();
            if (dec == "Y" || dec == "y")
            {
                RunSetup();
            }
            else
            {
                Console.WriteLine("Thank you for playing!\n");
                System.Environment.Exit(1);
            }
        }
      
        private static string LoadOptions()
        {
            string curDir = ".\\Saves\\";
            string[] saveFiles = Directory.GetFiles(curDir);
            if(saveFiles is null || saveFiles.Length ==0)
            {
                Console.WriteLine("You do not have any saves yet!");
                return "";
            }

            Console.WriteLine("Which Save would you like to Load?");
            Console.WriteLine("Type the number to choose your desired save.");
            
            for(int x=0; x< saveFiles.Length; x++)
            {
                Console.WriteLine($"GameSave {x}");
            }

            int entry;

            while (!int.TryParse(Console.ReadLine(), out entry) || entry >= saveFiles.Length || entry < 0)
            {
                Console.WriteLine("Please enter a valid save number: ");
            }

            return $"Saves\\GameSave{entry}.xml";
        }


        public static Maze LoadGame()
        {
            string saveFileName = LoadOptions();
            if(saveFileName == "")
            {
                return playerMaze;
            }

            FileStream fs = new FileStream(saveFileName, FileMode.Open);
            XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            DataContractSerializer serialized = new DataContractSerializer(typeof(Maze));
            playerMaze = (Maze)serialized.ReadObject(reader, true);
            reader.Close();
            fs.Close();

            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("\nGame Loaded! Select Play to continue your saved game.\n");

            return playerMaze;
        }

        public static void SaveGame(Maze maze)
        {
            string curDir = ".\\Saves\\";
            string[] saveFiles = Directory.GetFiles(curDir);
            int SaveCount = saveFiles.Length;

            var serializer = new JsonSerializer();
            string SaveFile = $"Saves\\GameSave{SaveCount}.xml";
            FileStream writer = new FileStream(SaveFile, FileMode.Create);
            DataContractSerializer serialized = new DataContractSerializer(typeof(Maze));
            serialized.WriteObject(writer, playerMaze);
            writer.Close();
        }

    }
}
