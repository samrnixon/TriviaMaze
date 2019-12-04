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
        public static MazeBuilder createBuilder() => new MazeBuilder();

        public Maze(int rows, int columns, Player player)
        {
            Rows = rows;
            Columns = columns;
            Player = player;
        }

        public Maze() { }

        public void CompleteBuild()
        {
            _Rooms = new Room[Rows, Columns];
        }

        
    }
}
