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

        public Player(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static string GetName(string nameType)
        {
            switch (nameType)
            {
                case "FirstName":
                    Console.WriteLine("Please enter your first name: ");
                    return NameCheck();
                case "LastName":
                    Console.WriteLine("Please enter your last name: ");
                    return NameCheck();
                default:
                    return "No name";
            }
        }

        public static string NameCheck()
        {
            
            bool value = true;
            var name = "";
            while(value)
            {
                name = Console.ReadLine();
                MatchCollection matchCollection = Regex.Matches(name, "[a-zA-z]{1,50}");
                if (matchCollection.Count > 0)
                {
                    value = false;
                }
            }

            return name;
        }

        public string toString()
        {
            return $"{FirstName}, {LastName}";
        }
    }
}
