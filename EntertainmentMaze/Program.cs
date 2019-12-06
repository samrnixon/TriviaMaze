using EntertainmentMaze.Database;
using EntertainmentMaze.maze;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace EntertainmentMaze
{
    public class Program
    {
        internal static Player newPlayer;
        public static Maze playerMaze;
        public static string SaveFile = "GameSaves.json";
        internal static IFormatter formatter = new BinaryFormatter();
        private static string PlayResumeMenuOption = "Play";
        private static Stream _Source;

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
                        //LoadGame();
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
                        //SaveGame(playerMaze);
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

        public static Maze LoadGame()
        {
            Source.Position = 0;
            using var reader = new StreamReader(Source, leaveOpen: true);
            string jsonString = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<Maze>(jsonString);
        }

        public static void SaveGame(Maze maze)
        {
            var serializer = new JsonSerializer();

            using (var sw = new StreamWriter(SaveFile))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, maze);
            }
        }
/*
        public static Maze DataLoader(string filePath)
        {
            var serializer = new JsonSerializer();

            using (var sw = new StreamReader(filePath))
            using (var reader = new JsonTextReader(sw))
            {
                return serializer.Deserialize(reader);
            }
        }*/

        /*        public static void SerializeItem(string fileName, IFormatter formatter)
                {
                    FileStream s = new FileStream(fileName, FileMode.Create);
                    formatter.Serialize(s, playerMaze);
                    s.Close();
                }


                public static void DeserializeItem(string fileName, IFormatter formatter)
                {
                    FileStream s = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    playerMaze = (Maze)formatter.Deserialize(s);
                }*/
    }
}
