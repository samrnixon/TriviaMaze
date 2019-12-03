using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentMaze.maze
{
    internal class Maze
    {
        private Room[,] _Rooms;
        private Player _Player;
        internal int Rows { get; set; }
        internal int Columns { get; set; }
        public static MazeBuilder createBuilder() => new MazeBuilder();

        public void CompleteBuild()
        {
            _Rooms = new Room[Rows, Columns];
        }

        
    }
}
