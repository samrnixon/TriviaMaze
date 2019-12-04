using EntertainmentMaze.maze;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentMazeTests
{
    [TestClass]
    internal class PlayerTests
    {
        [TestMethod]
        public void GetName_InputNameIsValid_Success()
        {
            //Arrange
            string expectedName = "Devin";
            string actualName = Player.NameCheck();
            //Act

            //Assert
            Assert.AreEqual<string>(expectedName, actualName);

        }

        [TestMethod]
        public void GetName_InputNameIsNotVaild_Success()
        {
            //Arrange

            //Act
            
            //Assert
        }
    }
}
