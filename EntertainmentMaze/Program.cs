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
using System.Xml;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace EntertainmentMaze
{
    public class Program
    {
        internal static Player newPlayer;
        public static Maze playerMaze;
        internal static IFormatter formatter = new BinaryFormatter();
        private static string PlayResumeMenuOption = "Play";
        private static int SaveCounter = 0;

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
                        playerMaze = LoadGame();
                        Console.WriteLine("\nGame Loaded! Select Resume to continue your saved game.\n");
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
                    default:
                        PlayResumeMenuOption = "Resume";
                        return;
                }
            }
        }

        private static string LoadOptions()
        {
            Console.WriteLine("Which Save would you like to Load?");
            Console.WriteLine($"Type the number from 0-{SaveCounter-1} to choose your desired save.");

            for (int x = 0; x < SaveCounter; x++)
            {
                Console.WriteLine($"{x}. GameSave{x}");
            }

            int entry;
            
            while (!int.TryParse(Console.ReadLine(), out entry) || entry > SaveCounter || entry < 0)
            {
                Console.WriteLine("Please enter a valid save number: ");
            }

            string saveFile = $"GameSave{entry}.xml";

            return saveFile;
        }

        public static Maze LoadGame()
        {
            if (SaveCounter == 0)
            {
                Console.WriteLine("You do not have any saves yet!");
                return playerMaze;
            }
            else
            {
                string saveFileName = LoadOptions();

                FileStream fs = new FileStream(saveFileName, FileMode.Open);
                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
                DataContractSerializer serialized = new DataContractSerializer(typeof(Maze));
                playerMaze = (Maze)serialized.ReadObject(reader, true);
                reader.Close();
                fs.Close();

                return playerMaze;
            }
        }

        public static void SaveGame(Maze maze)
        {
            var serializer = new JsonSerializer();
            string SaveFile = $"GameSave{SaveCounter}.xml";
            SaveCounter++;
            FileStream writer = new FileStream(SaveFile, FileMode.Create);
            DataContractSerializer serialized = new DataContractSerializer(typeof(Maze));
            serialized.WriteObject(writer, playerMaze);
            writer.Close();
        }

    }
}
