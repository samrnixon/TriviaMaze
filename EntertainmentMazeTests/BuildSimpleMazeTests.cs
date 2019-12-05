using EntertainmentMaze.Database;
using EntertainmentMaze.maze;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentMazeTests
{
    [TestClass]
    public class BuildSimpleMazeTests
    {
        [TestMethod]
        public void BuildSimpleMaze_BuildsDesiredMazeSize_Success()
        {
            DatabaseListRetrieval.InitializeList();
            var mazeBuilder = new MazeBuilder();
            var newPlayer = new Player("Sam","Nixon");
            Maze playerMaze = mazeBuilder
                .SetRows(5)
                .SetColumns(5)
                .SetPlayer(newPlayer)
                .Build();

            playerMaze.ToString();
        }
    }
}
