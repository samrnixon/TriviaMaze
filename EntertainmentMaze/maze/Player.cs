using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EntertainmentMaze.maze
{
    public class Player
    {
        private string FirstName { get; set; }
        private string LastName { get; set; }

        private int[] playerLocation = new int[2] { 0, 0 };

        public Player(string firstName, string lastName)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        }

        public static string GetName(string nameType)
        {
            bool value = true;
            var name = "";
            while (value)
            {
                name = GetValidName(nameType);
                if(!(name is null))
                {
                    value = false;
                }
            }

            return name;
        }

        private static string GetValidName(string nameType)
        {
            switch (nameType)
            {
                case "FirstName":
                    Console.Write("Please enter your first name: ");
                    return NameCheck();
                case "LastName":
                    Console.Write("Please enter your last name: ");
                    return NameCheck();
                default:
                    return "No name";
            }
        }

        public static string NameCheck()
        {
            var name = Console.ReadLine();
            MatchCollection matchCollection = Regex.Matches(name, "[a-zA-Z]{1,50}");
            if(matchCollection.Count > 0)
            {
                return name;
            }

            return null;
        }

        public void PlayerControlNorth()
        {
            //TODO check if movement is possible, basically is the door there locked, and is it a blank wall
            if (playerLocation[0] == 0)
            {
                //Wall!
                Console.WriteLine("WALL!");
                return;
            }

            playerLocation[0] -= 1;
        }
        public void PlayerControlEast()
        {
            //TODO check if movement is possible, basically is the door there locked, and is it a blank wall
            if(playerLocation[1] == 4)
            {
                //Wall!
                return;
            }

            playerLocation[1] += 1;
        }
        public void PlayerControlSouth()
        {
            //TODO check if movement is possible, basically is the door there locked, and is it a blank wall
            if(playerLocation[0] == 4)
            {
                //Wall!
                return;
            }

            playerLocation[0] += 1;
        }
        public void PlayerControlWest()
        {
            //TODO check if movement is possible, basically is the door there locked, and is it a blank wall
            if (playerLocation[1] == 0)
            {
                //Wall!
                return;
            }

            playerLocation[1] -= 1;
        }
        internal void DisplayLocation()
        {
            Console.WriteLine($"{playerLocation[0].ToString()}, {playerLocation[1].ToString()}");
        }


        public override string ToString()
        {
            return $"{FirstName}, {LastName}";
        }
    }
}
