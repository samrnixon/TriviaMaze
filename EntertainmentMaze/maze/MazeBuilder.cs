using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentMaze.maze
{
    internal class MazeBuilder
    {
        internal readonly Maze Maze = new Maze();
        private bool FinishedBuild = false;

        public Maze Build()
        {
            Maze.CompleteBuild();
            FinishedBuild = true;
            return Maze;
        }

        public MazeBuilder setRows(int rows)
        {
            Check();
            Maze.Rows = rows;
            return this;
        }

        public MazeBuilder setColumns(int columns)
        {
            Check();
            Maze.Columns = columns;
            return this;
        }

        private void Check()
        {
            if(FinishedBuild)
            {
                throw new ArgumentException("Do use other builder to create new instance");
            }
        }
    }
}
