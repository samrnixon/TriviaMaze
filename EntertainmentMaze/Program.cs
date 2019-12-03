using EntertainmentMaze.maze;
using System;

namespace EntertainmentMaze
{
    public class Program
    {
        public static void Main()
        {
            MazeBuilder mazeBuilder = new MazeBuilder();
            Maze simpleMaze = mazeBuilder.setRows(5).setColumns(5).Build();
        }
    }
}
