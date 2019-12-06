using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EntertainmentMaze.maze
{
    [Serializable]
    public class Player
    {
        private string FirstName { get; set; }
        private string LastName { get; set; }

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


        public override string ToString()
        {
            return $"{FirstName}, {LastName}";
        }
    }
}
