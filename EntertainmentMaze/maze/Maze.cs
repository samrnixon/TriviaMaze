using EntertainmentMaze.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentMaze.maze
{
    public class Maze
    {
        private enum Location
        {
            Row = 0,
            Column = 1
        }
        
        private Room[,] _Rooms;
        public Player Player { get; set; }
        private int[] PlayerLocation = new int[2];
        public int Rows { get; set; }
        public int Columns { get; set; }

        public static MazeBuilder CreateBuilder() => new MazeBuilder();
        private Room GetHeroLocation() => (_Rooms[PlayerLocation[(int)Location.Row], PlayerLocation[(int)Location.Column]]);

        
        public Maze(int rows, int columns, Player player)
        {
            Rows = rows;
            Columns = columns;
            Player = player;
        }

        public Maze() { }

        public void CompleteBuild()
        {
            BuildRooms();
            SetHeroLocation(0, 0);
        }

        private void BuildRooms()
        {
            _Rooms = new Room[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    _Rooms[i, j] = new Room(i, j, Rows, Columns);
                }
            }
        }

        private void SetHeroLocation(int rowLocation, int columnLocation)
        {
            PlayerLocation[(int)Location.Row] = rowLocation;
            PlayerLocation[(int)Location.Column] = columnLocation;
            GetHeroLocation().SetPlayerInRoom();
        }

        

        public void MoveHero(String direction)
        {

            var currentRoomHeroLocatedIn = GetHeroLocation();

            GetHeroLocation().RemovePreviousPlayerLocation();

            if (direction == "N")
            {
                if (!(currentRoomHeroLocatedIn.NorthDoor is null))
                {
                    SetHeroLocation(PlayerLocation[(int)Location.Row] - 1, PlayerLocation[(int)Location.Column]);
                }
            }
            else if (direction == "E")
            {
                if (!(currentRoomHeroLocatedIn.EastDoor is null))
                {
                    SetHeroLocation(PlayerLocation[(int)Location.Row], PlayerLocation[(int)Location.Column] + 1);
                }
            }
            else if (direction == "S")
            {
                if (!(currentRoomHeroLocatedIn.SouthDoor is null))
                {
                    SetHeroLocation(PlayerLocation[(int)Location.Row] + 1, PlayerLocation[(int)Location.Column]);
                }
            }
            else if (direction == "W")
            {
                if (!(currentRoomHeroLocatedIn.WestDoor is null))
                {
                    SetHeroLocation(PlayerLocation[(int)Location.Row], PlayerLocation[(int)Location.Column] - 1);
                }
            }

        }

        internal void DisplayHeroLocation()
        {
            Console.WriteLine($"{(PlayerLocation[(int)Location.Row] + 1).ToString()}, {(PlayerLocation[(int)Location.Column] + 1).ToString()}");
        } 

        public string PrintMaze()
        {
            String entireDungeon = "";
            int j;

            for (int i = 0; i < Rows; i++)
            {

                for (j = 0; j < Columns; j++)
                {
                    entireDungeon += _Rooms[i,j].GetTopOfRoom();
                }

                entireDungeon += "\n";

                for (j = 0; j < Columns; j++)
                {
                    entireDungeon += _Rooms[i, j].GetMiddleOfRoom();
                }

                entireDungeon += "\n";

                for (j = 0; j < Columns; j++)
                {
                    entireDungeon += _Rooms[i, j].GetBottomOfRoom();
                }

                entireDungeon += "\n";

            }

            return entireDungeon;
        }
    }
}
