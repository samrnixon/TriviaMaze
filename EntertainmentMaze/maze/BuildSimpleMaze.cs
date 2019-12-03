using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentMaze.maze
{
    internal class BuildSimpleMaze
    {
        public Maze BuildMaze() => new MazeBuilder().setRows(5).setColumns(5).Build();
    }
}
