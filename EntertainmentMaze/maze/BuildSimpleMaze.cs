using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentMaze.maze
{
    internal class BuildSimpleMaze
    {
        public Maze BuildMaze() => new MazeBuilder().SetRows(5).SetColumns(5).Build();
    }
}
