using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentMaze.maze
{
    public class Maze
    {
        private Room[,] _Rooms;
        public Player Player { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public static MazeBuilder CreateBuilder() => new MazeBuilder();

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
        }

        private void BuildRooms()
        {
            _Rooms = new Room[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < Columns; j++)
                {
                    _Rooms[i,j] = new Room();
                }
            }
        }

        
    }
}
