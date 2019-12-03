using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentMaze.maze
{
    public class Maze
    {
        private Room[,] _Rooms;
        private Player _Player;
        public int Rows { get; set; }
        public int Columns { get; set; }
        public static MazeBuilder createBuilder() => new MazeBuilder();

        public Maze(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
        }

        public Maze() { }

        public void CompleteBuild()
        {
            _Rooms = new Room[Rows, Columns];
        }

        
    }
}
