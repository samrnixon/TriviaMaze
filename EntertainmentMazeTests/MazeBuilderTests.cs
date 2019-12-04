using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using EntertainmentMaze.maze;

namespace EntertainmentMazeTests
{
    [TestClass]
    public class MazeBuilderTests
    {
        [TestMethod]
        [DataRow(0, 0)]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(1, 1)]
        public void MazeBuilder_CreatesMazeWithCorrectInput_Success(int rows, int columns)
        {
            //Arrange
            MazeBuilder mazeBuilder = new MazeBuilder();
            Maze expectedMaze = new Maze(rows, columns, null);
            //Act
            Maze actualMaze = mazeBuilder.SetRows(rows).SetColumns(columns).Build();

            //Assert
            Assert.AreEqual<int>(expectedMaze.Rows, actualMaze.Rows);
            Assert.AreEqual<int>(expectedMaze.Columns, actualMaze.Columns);
        }
    }
}
